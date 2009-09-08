<%@ WebHandler Language="C#" Class="GetFolders" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class GetFolders : IHttpHandler
{
    const int Error_Parameter = 1;
    const int Error_PathNotExist = 2;
        
    public void ProcessRequest (HttpContext context) {
        string strPath = context.Request.QueryString["path"];
        if (strPath == null || 0 == strPath.Length)
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
        
        //
        string[] strDirs = System.IO.Directory.GetDirectories(strPath);

        // output result
        JsonWriter jw = new JsonTextWriter(context.Response.Output);
        jw.WriteStartObject();
        jw.WritePropertyName("success");
        jw.WriteValue(true);
        jw.WritePropertyName("total");
        jw.WriteValue(strDirs.Length);
        jw.WritePropertyName("data");
        jw.WriteStartArray();
        
        for (int i = 0; i < strDirs.Length; i++)
        {
            jw.WriteValue(strDirs[i].Substring( strPath.Length ) );
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