using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using ArmorPotionFramework.Utility;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System.Xml;

namespace ArmorPotionsExtension
{
    [ContentTypeWriter]
    public class XmlSourceWriter : ContentTypeWriter<XmlSource>
    {
        protected override void Write(ContentWriter output, XmlSource value)
        {
            output.Write(value.XmlCode);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(XmlDocument).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(XmlSourceReader).AssemblyQualifiedName;
        }
    }
}
