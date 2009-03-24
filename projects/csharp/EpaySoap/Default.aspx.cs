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
using EPaySoap;
using EPaySoap.Utility;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    public DataSet dsDataList;

    protected void Page_Load(object sender, EventArgs e)
    {           
        Label3.Text = "test";
        Response.Write("<br><h3>Hello it's now " + DateTime.Now.ToString() + "</h3>");
        Response.Write("<br><h3>Click <a href='ListenRequest.asmx'>here</a> to goto Service." + "</h3>");
        //Viettel vtObj = new Viettel();
        //vtObj.testPOSGW();
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        //System.Threading.
        try
        {            
            Security secObj = new Security();
            string strPassword = string.Empty;

            //strPassword = secObj.GetSHA1_HEX(txtInput.Text).ToLower();
            //strPassword = "1UT3UKBGYNNRTB5W34P5" + strPassword;
            //strPassword = secObj.GetSHA1_HEX(strPassword).ToUpper();
            //txtOutput.Text = strPassword;

            //EPaySoap.Security secObj = new Security();
            //txtOutput.Text = secObj.Epay_Encrypt(txtInput.Text, ConfigurationManager.AppSettings["PasswordKey"]);
        }
        catch (Exception ex)
        {
            txtOutput.Text = ex.ToString();
        }
    }

}
