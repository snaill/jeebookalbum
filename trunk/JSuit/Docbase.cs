using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JSuit
{
    public class Docbase
    {
        string m_strBase = "";
        string m_strPrefix = "";
        bool m_bLazy = true;

        public Docbase(string strBase)
        {
            m_strBase = strBase;
        }

        public Docbase(string strBase, string strPrefix)
        {
            m_strBase = strBase;
            m_strPrefix = strPrefix;
        }

        public Docbase(string strBase, string strPrefix, bool lazy )
        {
            m_strBase = strBase;
            m_strPrefix = strPrefix;
            m_bLazy = lazy;
        }

        public string GetDirectories(string strDir)
        {
            string strPath = m_strBase + strDir;
            string strJson = strPath + m_strPrefix + "dirs.json";
            if (!System.IO.File.Exists(strJson))
            {
                string[] strDirs = System.IO.Directory.GetDirectories(strPath);
                System.IO.TextWriter tw = new System.IO.StreamWriter(strJson);
                JsonWriter jw = new JsonTextWriter(tw);
                jw.WriteStartArray();
                for (int i = 0; i < strDirs.Length; i++)
                {
                    jw.WriteStartObject();
                    jw.WritePropertyName("id");
                    jw.WriteValue(strDirs[i].Substring(strPath.Length));
                    jw.WritePropertyName("text");
                    jw.WriteValue(strDirs[i].Substring(strPath.Length));
                    jw.WritePropertyName("leaf");
                    jw.WriteValue(false);
                    jw.WriteEndObject();
                }
                jw.WriteEndArray();

                tw.Close();
            }

            return strJson;
        }
    }
}
