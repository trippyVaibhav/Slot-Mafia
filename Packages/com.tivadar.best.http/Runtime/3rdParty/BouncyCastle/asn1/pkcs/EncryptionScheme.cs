#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
    public class EncryptionScheme
        : AlgorithmIdentifier
    {
        public EncryptionScheme(
            DerObjectIdentifier	objectID)
            : base(objectID)
        {
        }

        public EncryptionScheme(
            DerObjectIdentifier	objectID,
            Asn1Encodable		parameters)
			: base(objectID, parameters)
		{
		}

		internal EncryptionScheme(
			Asn1Sequence seq)
			: this((DerObjectIdentifier)seq[0], seq[1])
        {
        }

		public new static EncryptionScheme GetInstance(object obj)
		{
			if (obj is EncryptionScheme)
			{
				return (EncryptionScheme)obj;
			}

			if (obj is Asn1Sequence)
			{
				return new EncryptionScheme((Asn1Sequence)obj);
			}

			throw new ArgumentException("Unknown object in factory: " + Org.BouncyCastle.Utilities.Platform.GetTypeName(obj), "obj");
		}

		public Asn1Object Asn1Object
		{
			get { return Parameters.ToAsn1Object(); }
		}

		public override Asn1Object ToAsn1Object()
        {
            return new DerSequence(Algorithm, Parameters);
        }
    }
}
#pragma warning restore
#endif
