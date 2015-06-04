using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Threading.Tasks;
using System.Web;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;

using System.Web.Configuration;
using System.Web.Hosting;


namespace VisitorManagement.Controllers
{
    public class UploadController : ApiController
    {

    
        [HttpPost, ActionName("UploadMultipartImage")]
        public async Task<HttpResponseMessage> UploadMultipartImage()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["ProfileImages"].ToString());
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                StringBuilder sb = new StringBuilder();

                await Request.Content.ReadAsMultipartAsync(provider);


                    // This illustrates how to get the file names for uploaded files.
                    string strFileName = "";
                    if (new JavaScriptSerializer().Deserialize(provider.FileData[0].Headers.ContentDisposition.FileName, typeof(string)).ToString() != "")
                    {
                        foreach (var file in provider.FileData)
                        {
                            FileInfo fileInfo = new FileInfo(file.LocalFileName);
                            strFileName = fileInfo.Name;

                            Rename(fileInfo, strFileName + ".jpg");
                            sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));
                        }
                        PostCall(strFileName + ".jpg");
                        string Status = "Success";
                        string JsonResponse = "{ \"FileName\" :  \"" + strFileName + ".jpg" + "\", \"Status\" :  \"" + Status + "\" }";
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonResponse.ToString())
                        };
                    }
                    else
                    {
                        string Status = "Photo Not Selected";
                        string SEmptyFile = "";
                        string JsonResponse = "{ \"FileName\" :  \"" + SEmptyFile + "\", \"Status\" :  \"" + Status + "\" }";
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent(JsonResponse.ToString())
                        };
                    }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Rename(FileInfo fileInfo, string newName)
        {
            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);
        }

        private void PostCall(string FileName)
        {

            //Save Updated Profile Image
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["BaseURLWeb"] + System.Configuration.ConfigurationManager.AppSettings["ServicePageUrlWeb"]);
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(strBase64);
            //string strBase64 = System.Convert.ToBase64String(bytes);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes("MethodName=SaveProfileImage&FileName=" + FileName);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // Get the response
            WebResponse webResponse = webRequest.GetResponse();
            string strResponse = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.UTF8).ReadToEnd();
            List<string> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(strResponse);

        }
    }
}
