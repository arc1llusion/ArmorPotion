using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace ArmorPotionFramework.Utility
{
    public class XmlSource
    {
        private String _xmlCode;

        public XmlSource(String xmlCode)
        {
            this._xmlCode = xmlCode;
        }

        public String XmlCode
        {
            get
            {
                return this._xmlCode;
            }
        }
    }

    public class XmlSourceReader : ContentTypeReader<XmlSource>
    {
        protected override XmlSource Read(ContentReader input, XmlSource existingInstance)
        {
            String xmlData = input.ReadString();
            return new XmlSource(xmlData);
        }
    }
}
