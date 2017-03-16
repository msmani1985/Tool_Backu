using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls.Adapters;

/// <summary>
/// Summary description for SAMProfileHtmlTextWriter
/// </summary>
namespace QMS_SAProfile_Edit
{
    [AspNetHostingPermission(SecurityAction.Demand,Level=AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand,Level=AspNetHostingPermissionLevel.Minimal)]
    public class SAMProfileHtmlTextWriter:HtmlTextWriter
    {
        public SAMProfileHtmlTextWriter(TextWriter writer):this(writer,DefaultTabString)
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public SAMProfileHtmlTextWriter(TextWriter writer,string tabstring):base(writer,tabstring)
        {

        }

        protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
        {
            if(key==HtmlTextWriterStyle.FontStyle)
            {
                if(string.Compare(value,"Italic")==0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return base.OnStyleAttributeRender(name,value,key);
            }
        }

    }
}