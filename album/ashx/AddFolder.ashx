<%@ WebHandler Language="C#" Class="AddFolder" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class AddFolder : IHttpHandler {
    
    const int Error_Parameter = 1;
    const int Error_PathNotExist = 2;
    const int Error_LoginFailed = 3;
    const int Error_FolderIsExisted = 4;
    
    public void ProcessRequest (HttpContext context) {
        string strName = context.Request.QueryString["name"];
        string strPath = context.Request.QueryString["path"];
        if (strName == null || 0 == strName.Length || strPath == null || 0 == strPath.Length)
        {
            SendFault(context.Response, Error_Parameter, null);
            return;
        } 
               
        //
        // create real folder
        strPath = context.Server.MapPath("../root") + strPath + strName;

        if (System.IO.Directory.Exists(strPath))
        {
            SendFault(context.Response, Error_FolderIsExisted, null);
            return;
        } 
        
        System.IO.Directory.CreateDirectory(strPath);

        JsonWriter jw = new JsonTextWriter(context.Response.Output);
        jw.WriteStartObject();
        jw.WritePropertyName("success");
        jw.WriteValue(true);
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