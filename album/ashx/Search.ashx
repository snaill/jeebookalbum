<%@ WebHandler Language="C#" Class="Search" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class Search : IHttpHandler
{
    const int Error_Parameter = 1;
    const int Error_PathNotExist = 2;
        
    public void ProcessRequest (HttpContext context) {
        string strPath = context.Request.QueryString["path"];
        string strKey = context.Request.QueryString["key"];
        if (strPath == null || 0 == strPath.Length || strKey == null || 0 == strKey.Length )
        {
            SendFault(context.Response, Error_Parameter, null);
            return;
        }

        strPath = context.Server.MapPath("../root") + strPath;
        if (!System.IO.Directory.Exists(strPath))
        {
            SendFault(context.Response, Error_Parameter, null);
            return;
        }

        // output result
        string strStart = context.Request.QueryString["start"];
        string strLimit = context.Request.QueryString["limit"];
        int nStart = strStart == null ? 0 : Int32.Parse(strStart);
        int nLimit = strLimit == null ? 20 : Int32.Parse(strLimit);
        
        //
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(strPath);
        System.IO.FileInfo[] fis = di.GetFiles(strKey + "*", System.IO.SearchOption.AllDirectories);
        int nEnd = nStart + nLimit > fis.Length ? fis.Length : nStart + nLimit;

        //
        JsonWriter jw = new JsonTextWriter(context.Response.Output);
        jw.WriteStartObject();
        jw.WritePropertyName("success");
        jw.WriteValue(true);
        jw.WritePropertyName("total");
        jw.WriteValue(fis.Length);
        jw.WritePropertyName("data");
        jw.WriteStartArray();
        
        for (int i = nStart; i < nEnd; i++)
        {
            System.IO.FileInfo fi = fis[i];
            jw.WriteStartObject();
            jw.WritePropertyName("name");
            jw.WriteValue(fi.Name);
            jw.WritePropertyName("size");
            jw.WriteValue(fi.Length);
            jw.WritePropertyName("time");
            jw.WriteValue(fi.CreationTime.ToShortDateString());
            jw.WriteEndObject();
        }
        jw.WriteEndArray();
        jw.WriteEndObject();
    }

    public void SendFault(HttpResponse response, int code, string msg)
    {
        JsonWriter jw = new JsonTextWriter(response.Output);
        jw.WriteStartObject();
        jw.WritePropertyName("success");
        jw.WriteValue(false);
        jw.WritePropertyName("fault");
        jw.WriteStartObject();
        jw.WritePropertyName("code");
        jw.WriteValue(code);
        if (msg != null && 0 < msg.Length)
        {
            jw.WritePropertyName("message");
            jw.WriteValue(msg);
        }
        jw.WriteEndObject();
        jw.WriteEndObject();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}