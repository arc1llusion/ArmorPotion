using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using ArmorPotionFramework.Utility;

namespace ArmorPotionsExtension
{
    [ContentImporter(".xml", DisplayName = "Xml Source Importer")]
    public class XmlSourceImporter : ContentImporter<XmlSource>
    {
        public override XmlSource Import(string filename, ContentImporterContext context)
        {
            String sourceCode = System.IO.File.ReadAllText(filename);
            return new XmlSource(sourceCode);
        }
    }
}
