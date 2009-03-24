using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Threading;
using EPaySoap;
namespace EPaySoap
{
    /// <summary>
    /// process remote requests SOAP
    /// ChungNN 08/2008
    /// </summary>
    [WebService(Namespace = "http://www.vnptepay.com.vn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ListenRequest : System.Web.Services.WebService
    {   
               
        #region public string check_network_status()
        [WebMethod(EnableSession = true, BufferResponse = true, CacheDuration = 0)]
        public string check_network_status()
        {            
            return "0";
        }
        #endregion

        #region public OutputLoginInfo login(string Username, string Password)
        [WebMethod(BufferResponse = true, CacheDuration = 0)]
        public OutputLoginInfo login(string Username, string Password)
        {
            TransferProcess transObj = new TransferProcess();
            return transObj.login(Username, Password);             
        }        
        #endregion

        #region public OutputInfo load(string UserName, string Session_ID, string Request_ID, string targetMsIsdn, Int32 amount)
        [WebMethod(EnableSession = true, BufferResponse = true, CacheDuration = 0)]      
        public OutputInfo load(string UserName, string Session_ID, string Request_ID, string MerchantID, string targetMsIsdn, Int32 amount)
        {
            TransferProcess transObj = new TransferProcess();
            return transObj.load(UserName, Session_ID, Request_ID, MerchantID, targetMsIsdn, amount);
        }
        #endregion

        #region public OutputInfo stock_transfer(string UserName, string Session_ID, string Request_ID, string targetMsIsdn, Int32 amount)
        [WebMethod(EnableSession = true, BufferResponse = true, CacheDuration = 0)]
        public OutputInfo stock_transfer(string UserName, string Session_ID, string Request_ID, string MerchantID, string targetMsIsdn, Int32 amount)
        {
            TransferProcess transObj = new TransferProcess();
            return transObj.stock_transfer(UserName, Session_ID, Request_ID, MerchantID, targetMsIsdn, amount);
        }
        #endregion
              
        #region public OutputLogoutInfo logout(string UserName, string Session_ID)
        [WebMethod(BufferResponse = true, CacheDuration = 0)]
        public OutputLogoutInfo logout(string UserName, string Session_ID)
        {
            TransferProcess transObj = new TransferProcess();
            return transObj.logout(UserName, Session_ID);     
        }

        #endregion

        

        //********************* For test ****************************//

        #region private void login1(string UserName, string Password)
        [WebMethod(EnableSession = true, BufferResponse = true, CacheDuration = 0)]
        private void login1(string UserName, string Password)
        {
            //ThreadStart startThread = new ThreadStart(login());
            //Thread newThread = new Thread(startThread);
            //newThread.Start();
        }
        #endregion

        #region private string confirm_backup(string UserName, string Session_ID, string Trans_ID, string Status)
        //confirm for a load process
        //ChungNN 11/09/2008
        [WebMethod(EnableSession = true)]
        private OutputConfirmInfo confirm(string UserName, string Session_ID, string Trans_ID, string Status)
        {
            TransferProcess transObj = new TransferProcess();
            return transObj.confirm(UserName, Session_ID, Trans_ID, Status);  
        }

        #endregion
       
    }
}
