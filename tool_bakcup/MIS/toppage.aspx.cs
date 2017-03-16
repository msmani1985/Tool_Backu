using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.InteropServices;

public partial class toppage : System.Web.UI.Page
{
    public const UInt32 URLZONE_LOCAL_MACHINE = 0;
    public const UInt32 URLZONE_INTRANET = URLZONE_LOCAL_MACHINE + 1;
    public const UInt32 URLZONE_TRUSTED = URLZONE_INTRANET + 1;
    public const UInt32 URLZONE_INTERNET = URLZONE_TRUSTED + 1;
    public const UInt32 URLZONE_UNTRUSTED = URLZONE_INTERNET + 1;
    public const UInt32 URLZONE_ESC_FLAG = 0x100;
    public const UInt32 SZM_CREATE = 0;
    public const UInt32 SZM_DELETE = 0x1;
    public static Guid CLSID_InternetSecurityManager = new Guid("7b8a2d94-0ac9-11d1-896c-00c04fb6bfc4");
    public static Guid IID_IInternetSecurityManager = new Guid("79eac9ee-baf9-11ce-8c82-00aa004ba90b");
    private IInternetSecurityManager _ism;   // IInternetSecurityManager interface of ecurityManager COM object
    private object _securityManager;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        birthdayImg.Visible = false; 
        try
        {
            //For Sanlucas customer
            if (Request.Url.ToString().IndexOf("203.101.73.205:2080") > 0 && Session["customerid"] != null && Session["customerid"].ToString() == "124")
            { div_sanlucas.Visible = true; div_datapage.Visible = false; }
            else
            { div_datapage.Visible = true; div_sanlucas.Visible = false; }

            Type t = Type.GetTypeFromCLSID(CLSID_InternetSecurityManager);
            _securityManager = Activator.CreateInstance(t);
            _ism = (IInternetSecurityManager)_securityManager;
            if (Session["fullname"] != null)
            {
                username.Text = Session["fullname"].ToString();
                if (Session["date_of_birth"] != null && Session["date_of_birth"].ToString()  != "")
                {
                    DateTime oBirthDate = Convert.ToDateTime(Session["date_of_birth"]);

                    if (oBirthDate.Month == DateTime.Now.Month)
                    {
                        if (oBirthDate.Day == DateTime.Now.Day)
                            birthdayImg.Visible = true;
                    }
                    else
                        birthdayImg.Visible = false;
                }
            }
            else
                logoff_Click(sender, e);
            lblConstring.Text = Session.SessionID.ToString() + "::"  + ConfigurationManager.ConnectionStrings["conStrIB"].ToString().Replace("Driver={Easysoft Interbase ODBC};DB=dpserver2:", "").Replace("UID=sysdba;pwd=masterkey;ROLE=sysdba", "").Replace(".GDB", "").Replace("e:\\db\\", "");
        }
        catch (Exception oex) {}
    }


    protected void logoff_Click(object sender, EventArgs e)
    {
        Session["fullname"] = null;
        Session.Clear();
        Session.Abandon();

        string sHTML = "";
        Page page = HttpContext.Current.Handler as Page;

        sHTML += "<script language='javascript'>";
        sHTML += "window.open('Login.aspx','_top')";
        sHTML += "</script>";

        page.RegisterStartupScript("script", sHTML);

    }

    [ComImport, GuidAttribute("79EAC9EE-BAF9-11CE-8C82-00AA004BA90B"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInternetSecurityManager
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetSecuritySite([In] IntPtr pSite);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetSecuritySite([Out] IntPtr pSite);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int MapUrlToZone([In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl, out UInt32 pdwZone, UInt32 dwFlags);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetSecurityId([MarshalAs(UnmanagedType.LPWStr)] string pwszUrl, [MarshalAs(UnmanagedType.LPArray)] byte[] pbSecurityId, ref UInt32 pcbSecurityId, uint dwReserved);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ProcessUrlAction([In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl, UInt32 dwAction, out byte pPolicy, UInt32 cbPolicy, byte pContext, UInt32 cbContext, UInt32 dwFlags, UInt32 dwReserved);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int QueryCustomPolicy([In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl, ref Guid guidKey, ref byte ppPolicy, ref UInt32 pcbPolicy, ref byte pContext, UInt32 cbContext, UInt32 dwReserved);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int SetZoneMapping(UInt32 dwZone, [In, MarshalAs(UnmanagedType.LPWStr)] string lpszPattern, UInt32 dwFlags);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int GetZoneMappings(UInt32 dwZone, out UCOMIEnumString ppenumString, UInt32 dwFlags);
    }

    protected void trusted_Click(object sender, EventArgs e)
    {
        try
        {
            int result = _ism.SetZoneMapping(2, Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf(":") + 6), 0);
        }
        catch (Exception oex) { }

     }
}
