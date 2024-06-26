#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	internal class CmsAuthEnvelopedGenerator
	{
		public static readonly string Aes128Ccm = NistObjectIdentifiers.IdAes128Ccm.Id;
		public static readonly string Aes192Ccm = NistObjectIdentifiers.IdAes192Ccm.Id;
		public static readonly string Aes256Ccm = NistObjectIdentifiers.IdAes256Ccm.Id;
		public static readonly string Aes128Gcm = NistObjectIdentifiers.IdAes128Gcm.Id;
		public static readonly string Aes192Gcm = NistObjectIdentifiers.IdAes192Gcm.Id;
		public static readonly string Aes256Gcm = NistObjectIdentifiers.IdAes256Gcm.Id;
	}
}
#pragma warning restore
#endif
