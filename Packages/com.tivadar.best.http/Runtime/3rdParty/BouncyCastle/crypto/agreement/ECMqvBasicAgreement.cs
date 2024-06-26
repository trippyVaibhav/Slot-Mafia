#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Math;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Math.EC;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
    public class ECMqvBasicAgreement
        : IBasicAgreement
    {
        protected internal MqvPrivateParameters privParams;

        public virtual void Init(
            ICipherParameters parameters)
        {
            if (parameters is ParametersWithRandom)
            {
                parameters = ((ParametersWithRandom)parameters).Parameters;
            }

            this.privParams = (MqvPrivateParameters)parameters;
        }

        public virtual int GetFieldSize()
        {
            return (privParams.StaticPrivateKey.Parameters.Curve.FieldSize + 7) / 8;
        }

        public virtual BigInteger CalculateAgreement(
            ICipherParameters pubKey)
        {
            MqvPublicParameters pubParams = (MqvPublicParameters)pubKey;

            ECPrivateKeyParameters staticPrivateKey = privParams.StaticPrivateKey;
            ECDomainParameters parameters = staticPrivateKey.Parameters;

            if (!parameters.Equals(pubParams.StaticPublicKey.Parameters))
                throw new InvalidOperationException("ECMQV public key components have wrong domain parameters");

            ECPoint agreement = CalculateMqvAgreement(parameters, staticPrivateKey,
                privParams.EphemeralPrivateKey, privParams.EphemeralPublicKey,
                pubParams.StaticPublicKey, pubParams.EphemeralPublicKey).Normalize();

            if (agreement.IsInfinity)
                throw new InvalidOperationException("Infinity is not a valid agreement value for MQV");

            return agreement.AffineXCoord.ToBigInteger();
        }

        // The ECMQV Primitive as described in SEC-1, 3.4
        private static ECPoint CalculateMqvAgreement(
            ECDomainParameters		parameters,
            ECPrivateKeyParameters	d1U,
            ECPrivateKeyParameters	d2U,
            ECPublicKeyParameters	Q2U,
            ECPublicKeyParameters	Q1V,
            ECPublicKeyParameters	Q2V)
        {
            BigInteger n = parameters.N;
            int e = (n.BitLength + 1) / 2;
            BigInteger powE = BigInteger.One.ShiftLeft(e);

            ECCurve curve = parameters.Curve;

            ECPoint q2u = ECAlgorithms.CleanPoint(curve, Q2U.Q);
            ECPoint q1v = ECAlgorithms.CleanPoint(curve, Q1V.Q);
            ECPoint q2v = ECAlgorithms.CleanPoint(curve, Q2V.Q);

            BigInteger x = q2u.AffineXCoord.ToBigInteger();
            BigInteger xBar = x.Mod(powE);
            BigInteger Q2UBar = xBar.SetBit(e);
            BigInteger s = d1U.D.Multiply(Q2UBar).Add(d2U.D).Mod(n);

            BigInteger xPrime = q2v.AffineXCoord.ToBigInteger();
            BigInteger xPrimeBar = xPrime.Mod(powE);
            BigInteger Q2VBar = xPrimeBar.SetBit(e);

            BigInteger hs = parameters.H.Multiply(s).Mod(n);

            return ECAlgorithms.SumOfTwoMultiplies(
                q1v, Q2VBar.Multiply(hs).Mod(n), q2v, hs);
        }
    }
}
#pragma warning restore
#endif
