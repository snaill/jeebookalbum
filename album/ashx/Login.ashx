<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class Login : IHttpHandler
{
    const int Error_Parameter = 1;
    const int Error_PathNotExist = 2;
    const int Error_LoginFailed = 3;
  
    public void ProcessRequest (HttpContext context) {
        string strUser = context.Request.QueryString["user"];
        string strPsw = context.Request.QueryString["psw"];
        if (strUser == null || 0 == strUser.Length)
        {
            SendFault(context.Response, Error_Parameter, null);
            return;
        }    
        
        //
        if ( strUser != "guest@jeebook.com" && strUser != "snaill@jeebook.com" )
        {
            SendFault(context.Response, Error_LoginFailed, null);
            return;
        }
                       
        JsonWriter jw = new JsonTextWriter(context.Response.Output);
        jw.WriteStartObject();
            jw.WritePropertyName("success");
            jw.WriteValue(true);
            jw.WritePropertyName("id"); 
            jw.WriteValue(strUser);
            jw.WritePropertyName("data");
            jw.WriteStartObject();
                WriteStateProperty(jw, "addFolder", false);
                WriteStateProperty(jw, "addFile", false);
                WriteStateProperty(jw, "rename", strUser == "snaill@jeebook.com" ? false : true);
                WriteStateProperty(jw, "delete", strUser == "snaill@jeebook.com" ? false : true);
            jw.WriteEndObject();
       jw.WriteEndObject();
    }

    public void WriteStateProperty(JsonWriter jw, string name, bool disabled)
    {
        jw.WritePropertyName(name);
        
        jw.WriteStartObject();
        jw.WritePropertyName("disabled");
        jw.WriteValue(disabled);
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