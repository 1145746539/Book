using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace 工具类与测试的集合._0516
{
    class XmlHelp
    {

        private static string XMLPath = @"C:\para\SLInformation.xml";

        public static List<string> station = new List<string>();
        public static List<string> location = new List<string>();
        //以后可以写   键是枚举型 enum  
        public static Dictionary<string, string> dynamicDic = new Dictionary<string, string>();
        /// <summary>
        /// 初始化读取XMl
        /// </summary>
        public static void ReadStation()
        {
            if (File.Exists(XMLPath))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(XMLPath);
                XmlElement documentElement = xml.DocumentElement;
                foreach (XmlNode xmlNode in documentElement)
                {
                    if (!location.Contains(xmlNode.InnerText))
                    {
                        location.Add(xmlNode.InnerText);
                    }
                    if (!station.Contains(xmlNode.Name)) //不包含
                    {
                        station.Add(xmlNode.Name);
                    }
                }

            }
            else
            {
                MessageBox.Show("SLInformation参数文件不存在", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }


        public static List<string> GetLocation(string name)
        {
            List<string> valueList = new List<string>();
            XmlDocument xml = new XmlDocument();
            xml.Load(XMLPath);
            XmlElement documentElement = xml.DocumentElement;

            XmlNodeList nodeList = documentElement.SelectNodes(name);
            foreach (XmlNode xmlNode in nodeList)
            {
                valueList.Add(xmlNode.InnerText);
            }

                return valueList;
        }

       

        /// <summary>
        /// 读取XML，只读取节点和文本值(如果节点没有文本值则不存入键值对)
        /// </summary>
        /// <param name="XMLPath"></param>
        public static void Read_XML_Text(string XMLPath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(XMLPath);
            XmlElement documentElement = xml.DocumentElement;
            foreach (XmlNode xmlNode in documentElement)
                GetDic(xmlNode);

        }

        public static void GetDic(XmlNode element)
        {
            foreach (XmlNode xmlNode in element)
            {
                if (xmlNode.HasChildNodes)  //一直判断 直到碰到文本节点
                {
                    GetDic(xmlNode);
                }
                else
                {
                    string value = xmlNode.InnerText;  //值
                    string key = xmlNode.ParentNode.Name;  //键
                    if (dynamicDic.ContainsKey(key))  //存在则修改
                    {
                        dynamicDic[key] = value;
                    }
                    else
                    {
                        dynamicDic.Add(key, value);
                    }

                }
            }
        }

    }
}
