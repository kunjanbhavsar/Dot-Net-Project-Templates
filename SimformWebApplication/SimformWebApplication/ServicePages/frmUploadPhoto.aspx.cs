using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimformWebApplication.ServicePages
{
    public partial class frmUploadPhoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string MethodName = Request.Form["MethodName"];

            if (MethodName == "SaveProfileImage")
            {
                try
                {
                    string FileName = Request.Form["FileName"];
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] originalData = wc.DownloadData(System.Web.Configuration.WebConfigurationManager.AppSettings["BaseURLApp"] + System.Web.Configuration.WebConfigurationManager.AppSettings["ProfilesImages"] + FileName);

                    MemoryStream stream = new MemoryStream();
                    stream = new MemoryStream(originalData);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);// this line giving exception parameter not valid
                    string Path = System.Web.Configuration.WebConfigurationManager.AppSettings["ProfilesImages"].ToString() + FileName;
                    image.Save(Server.MapPath("~/" + Path));
                    Response.Clear();
                    Response.Write("");
                    Response.End();
                }
                catch (System.Threading.ThreadAbortException tex)
                {

                }
                catch (Exception ex)
                {
                    //  new ExceptionLogController().InsertExceptionLog(ex.Message, ex.Source, ex.StackTrace, ex.InnerException.Message);
                    //  throw ex;
                }
            }
        }
    }
}