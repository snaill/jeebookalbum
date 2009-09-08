<%@ WebHandler Language="C#" Class="AddFile" %>

using System;
using System.Web;
using Newtonsoft.Json;

public class AddFile : IHttpHandler {

    const int Error_Unknown = 0;
    const int Error_Parameter = 1;
    const int Error_PathNotExist = 2;
    const int Error_LoginFailed = 3;
    const int Error_FolderIsExisted = 4;
    const int Error_FileIsExisted = 5;
  
    public void ProcessRequest (HttpContext context) {
        string strName = context.Request.QueryString["name"];
        string strPath = context.Request.QueryString["path"];
        string strRemark = context.Request["remark"];
       
        if (strName == null || 0 == strName.Length || strPath == null || 0 == strPath.Length)
        {
            SendFault(context.Response, Error_Parameter, null);
            return;
        }

        try
        {
            // create json
            HttpPostedFile postedFile = context.Request.Files[0];
            string fileName = System.IO.Path.GetFileName(postedFile.FileName);
            string fileType = fileName.Substring(fileName.LastIndexOf("."));
            string saveFilename = Guid.NewGuid().ToString() + fileType;
            string savePath = context.Server.MapPath("../stack/") + saveFilename;
            System.IO.FileStream fs = new System.IO.FileStream(context.Server.MapPath("../root") + strPath + strName, System.IO.FileMode.CreateNew);
            JsonWriter jw = new JsonTextWriter(new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8));
            jw.WriteStartObject();
            jw.WritePropertyName("name");
            jw.WriteValue(strName);
            jw.WritePropertyName("size");
            jw.WriteValue(postedFile.ContentLength);
            jw.WritePropertyName("time");
            jw.WriteValue(DateTime.Now.ToString());
            jw.WritePropertyName("id");
            jw.WriteValue(saveFilename); 
            jw.WritePropertyName("remark");
            jw.WriteValue(strRemark);
            jw.WriteEndObject();
            jw.Close();
            
            //Upload
            if (fileName != "")
            {
                //检查是否在服务器上已经存在用户上传的同名文件
                if (System.IO.File.Exists(savePath))
                {
                    SendFault(context.Response, Error_FileIsExisted, null);
                    return;
                }
                postedFile.SaveAs(savePath);
            }
        }
        catch (Exception ex)
        {
            SendFault(context.Response, Error_Unknown, ex.Message);
            return;
        }
 
        
        JsonWriter jw2 = new JsonTextWriter(context.Response.Output);
        jw2.WriteStartObject();
        jw2.WritePropertyName("success");
        jw2.WriteValue(true);
        jw2.WriteEndObject();
    }

    bool Upload(HttpContext context, int index, string strPath)
    {
        try
        {
            HttpPostedFile postedFile = context.Request.Files[index];
            string fileName = System.IO.Path.GetFileName(postedFile.FileName);
            if (fileName != "")
            {
                string fileType = fileName.Substring(fileName.LastIndexOf("."));
                string savePath = strPath + fileType;
                //检查是否在服务器上已经存在用户上传的同名文件
                if (System.IO.File.Exists(savePath))
                {
                    SendFault(context.Response, Error_FileIsExisted, null);
                    return false;
                }
                postedFile.SaveAs(savePath);
            }
            return true;
        }
        catch (Exception ex)
        {
            SendFault(context.Response, Error_Unknown, ex.Message);
            return false;
        }
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