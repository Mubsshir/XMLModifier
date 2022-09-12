using System.Text;
using System.Xml;

namespace FileReading
{
    public class ReadXML
    {
        public static string Fn_ReadXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            int childCount = doc.DocumentElement.ChildNodes.Count; 
            StringBuilder newXML = new StringBuilder("<" + doc.DocumentElement.Name + ">");

            for (int i = 0; i < childCount; i++)
            {
                XmlNode node = doc.DocumentElement.ChildNodes[i];
                newXML.Append("<" + node.Name + ">");
                foreach (XmlAttribute attr in  node.Attributes)
                {
                    newXML.Append("<" + attr.Name + ">");
                    newXML.Append(attr.Value);
                    newXML.Append("</" + attr.Name + ">");
                }
                newXML.Append("</" + node.Name + ">");
            }
            newXML.Append("</" + doc.DocumentElement.Name + ">"); 
            return newXML.ToString();
        }
    }
}
