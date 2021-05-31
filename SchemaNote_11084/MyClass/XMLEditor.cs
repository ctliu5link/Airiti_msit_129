using Microsoft.AspNetCore.Http;
using SchemaNote_11084.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace SchemaNote_11084.MyClass
{
    public class XMLEditor
    {
        public string xmlPath = Directory.GetCurrentDirectory() + @"\config.xml";
        
        /// <summary>
        /// 如果 xml 檔不存在，建立
        /// </summary>
        public void CreateXML()
        {
            if (!System.IO.File.Exists(xmlPath))
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("Login");
                doc.AppendChild(root);
                XmlElement id = doc.CreateElement("id");
                root.AppendChild(id);
                id.SetAttribute("account", "Leo");
                id.SetAttribute("passWord", "0000");
                root.AppendChild(id);
                id = doc.CreateElement("id");
                id.SetAttribute("account", "Achie");
                id.SetAttribute("passWord", "1111");
                root.AppendChild(id);
                doc.Save("config.xml");
            }
        }

        /// <summary>
        /// 產生帳號清單
        /// </summary>
        public List<TEmployee> CreateEmpList()
        {
            List<TEmployee> empList = new List<TEmployee>();
            XmlDocument x = new XmlDocument();
            x.Load(xmlPath);
            XmlNodeList nodeList = x.SelectNodes("root/Login/id");

            empList = new List<TEmployee>();
            foreach (XmlNode item in nodeList)
            {
                TEmployee emp = new TEmployee();
                emp = new TEmployee();
                emp.FAccount = item.Attributes["account"].Value;
                emp.FPassword = item.Attributes["password"].Value;
                emp.FPhoto = item.Attributes["photo"].Value;
                empList.Add(emp);
            }

            return empList;
        }

        /// <summary>
        /// 打 Log 到 xml
        /// </summary>
        public void Log(string time, string editor, TResult r ,TResult o)
        {
            if (r.欄位說明 == o.欄位說明 && r.備註 == o.備註)
                return;
            string str1 = (r.欄位說明 == o.欄位說明) ? "" : $"\n欄位說明：{ o.欄位說明} => { r.欄位說明}";
            string str2 = (r.備註 == o.備註) ? "" : $"\n備註：{ o.備註} => { r.備註}";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode logList = xmlDoc.SelectSingleNode("root/Log");
            XmlElement info = xmlDoc.CreateElement("info");
            info.SetAttribute("time", time);
            info.SetAttribute("editor", editor);
            info.SetAttribute("message", $"更新資料表：{r.Table_Name}\n欄位：{r.COLUMN_NAME}{str1}{str2}");
            logList.AppendChild(info);            

            xmlDoc.Save(xmlPath);
        }

        /// <summary>
        /// 產生 log 清單
        /// </summary>
        public List<CLog> CreateLogList()
        {
            List<CLog> logList = new List<CLog>();
            XmlDocument x = new XmlDocument();
            x.Load(xmlPath);
            XmlNodeList nodeList = x.SelectNodes("root/Log/info");

            logList = new List<CLog>();
            foreach (XmlNode item in nodeList)
            {
                CLog l = new CLog();                
                l.time = item.Attributes["time"].Value;
                l.editor = item.Attributes["editor"].Value;
                l.message = item.Attributes["message"].Value;
                logList.Add(l);
            }

            return logList;
        }
    }
}
