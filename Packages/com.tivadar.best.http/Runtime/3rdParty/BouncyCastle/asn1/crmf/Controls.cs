#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;
using System.Text;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
    public class Controls
        : Asn1Encodable
    {
        private readonly Asn1Sequence content;

        private Controls(Asn1Sequence seq)
        {
            content = seq;
        }

        public static Controls GetInstance(object obj)
        {
            if (obj is Controls)
                return (Controls)obj;

            if (obj is Asn1Sequence)
                return new Controls((Asn1Sequence)obj);

            throw new ArgumentException("Invalid object: " + Org.BouncyCastle.Utilities.Platform.GetTypeName(obj), "obj");
        }

        public Controls(params AttributeTypeAndValue[] atvs)
        {
            content = new DerSequence(atvs);
        }

        public virtual AttributeTypeAndValue[] ToAttributeTypeAndValueArray()
        {
            AttributeTypeAndValue[] result = new AttributeTypeAndValue[content.Count];
            for (int i = 0; i != result.Length; ++i)
            {
                result[i] = AttributeTypeAndValue.GetInstance(content[i]);
            }
            return result;
        }

        /**
         * <pre>
         * Controls  ::= SEQUENCE SIZE(1..MAX) OF AttributeTypeAndValue
         * </pre>
         * @return a basic ASN.1 object representation.
         */
        public override Asn1Object ToAsn1Object()
        {
            return content;
        }
    }
}
#pragma warning restore
#endif
