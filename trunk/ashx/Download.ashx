<%@ WebHandler Language="C#" Class="Download" %>

using System;
using System.Web;
using System.IO;
using System.Threading;

public class Download : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string strId = context.Request.QueryString["id"];
        string strName = context.Request.QueryString["name"];
       
        //
        strName += strId.Substring(strId.LastIndexOf("."));
        ResponseFile( context.Request, context.Response, strName, context.Server.MapPath("../stack/") + strId, 1024000 );
        
        //
    //    context.Response.ContentType = "text/plain";
    //    context.Response.Write(strRet);
    }
	
	public bool ResponseFile(HttpRequest _Request,HttpResponse _Response,string _fileName,string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;
     
                double pack = 10240; //10K bytes
                //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] {'=', '-'});
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    //Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength-1, fileLength));
                }
                _Response.ContentType = "application/octet-stream";
                _Response.Charset = "UTF-8";
                _Response.AddHeader("Connection", "Keep-Alive");
                
                HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
                if ( bc.Browser == "IE" )
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));
                else
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + _fileName);
                
                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int) Math.Floor((fileLength - startBytes) / pack) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        i=maxCount; 
                    }
                }
            }
            catch
            {
             return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}