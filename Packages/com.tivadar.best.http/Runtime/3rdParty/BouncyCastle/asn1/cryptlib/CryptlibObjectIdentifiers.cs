#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cryptlib
{
    internal class CryptlibObjectIdentifiers
    {
        internal static readonly DerObjectIdentifier cryptlib = new DerObjectIdentifier("1.3.6.1.4.1.3029");

        internal static readonly DerObjectIdentifier ecc = cryptlib.Branch("1.5");

        internal static readonly DerObjectIdentifier curvey25519 = ecc.Branch("1");
    }
}
#pragma warning restore
#endif
