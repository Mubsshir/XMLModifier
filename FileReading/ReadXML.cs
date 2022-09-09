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
            int childCount = doc.DocumentElement.ChildNodes.Count; //count all the child of root tag
            StringBuilder newXML = new StringBuilder("<" + doc.DocumentElement.Name + ">");// this will make <root> tag

            //Now we will select every child Node of Root
            for (int i = 0; i < childCount; i++)
            {
                XmlNode node = doc.DocumentElement.ChildNodes[i];
                //select node and make a tag  with it
                newXML.Append("<" + node.Name + ">"); // this will create opening tag, like this <ChildOfRoot>
                //now we need to select every attribute of this node
                foreach (XmlAttribute attr in node.Attributes)
                {
                    newXML.Append("<" + attr.Name + ">");// build opening tag with attribute name <AttributeName>
                    newXML.Append(attr.Value);// insert Attribute value in between opening and closing tag
                    newXML.Append("</" + attr.Name + ">");// build closing tag with attribute name </AttributeName>
                }
                newXML.Append("</" + node.Name + ">");// this will create closing tag like this </ChildOfRoot>
            }
            newXML.Append("</" + doc.DocumentElement.Name + ">"); //this will make cloasing </root> tag
            return newXML.ToString();
        }
    }
}
