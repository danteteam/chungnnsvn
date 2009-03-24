using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Epay direct top-up config
/// ChungNN 02/2009

namespace EPaySoap
{
    public class AppConfiguration
    {        
        public readonly static Int32 AppTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["AppTimeout"]);
        public readonly static string AppData_FileName = ConfigurationManager.AppSettings["AppData_FileName"];
        public readonly static string AppPath = ConfigurationManager.AppSettings["AppPath"];
        public readonly static string AppTransactionLogType = ConfigurationManager.AppSettings["AppTransactionLogType"];
        public readonly static string AppPasswordKey = ConfigurationManager.AppSettings["AppPasswordKey"];
        public readonly static string AppRSAPrivateKeyFilePath = ConfigurationManager.AppSettings["AppRSAPrivateKeyFilePath"].ToString();
        public readonly static string AppRSAPrivateKeyPassword = ConfigurationManager.AppSettings["AppRSAPrivateKeyPassword"].ToString();

        public readonly static string VinaPhoneUserName = ConfigurationManager.AppSettings["VinaPhoneUserName"];
        public readonly static string VinaPhonePassword = ConfigurationManager.AppSettings["VinaPhonePassword"];
        public readonly static string VinaPhoneAgentMsIsdl = ConfigurationManager.AppSettings["VinaPhoneAgentMsIsdl"];
        public readonly static string VinaPhoneAgentMPIN = ConfigurationManager.AppSettings["VinaPhoneAgentMPIN"];        
        public readonly static int VinaPhoneTimeout = Convert.ToInt16(ConfigurationManager.AppSettings["VinaPhoneTimeout"]);
        public readonly static string VinaphoneSession = SrvProviderSessionManager.GetSessionInstance().GetSession("VinaphoneSession");


        public readonly static int ViettelTimeout = Convert.ToInt16(ConfigurationManager.AppSettings["ViettelTimeout"]);
        public readonly static string ViettelConfig_ISOFile = ConfigurationManager.AppSettings["ViettelConfig_ISOFile"].ToString();
        public readonly static string ViettelServerAddress = ConfigurationManager.AppSettings["ViettelServerAddress"].ToString();
        public readonly static Int32 ViettelServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["ViettelServerPort"]);
        public readonly static string ViettelClientID = ConfigurationManager.AppSettings["ViettelClientID"].ToString();
        public readonly static string ViettelCerFilePath = ConfigurationManager.AppSettings["ViettelCerFilePath"].ToString();

        public readonly static string MobifoneTimeout = ConfigurationManager.AppSettings["MobifoneTimeout"];
        public readonly static string MobifoneUserName = ConfigurationManager.AppSettings["MobifoneUserName"];
        public readonly static string MobifonePassword = ConfigurationManager.AppSettings["MobifonePassword"];
        public readonly static string MobifoneSession = SrvProviderSessionManager.GetSessionInstance().GetSession("MobifoneSession");

        //private Array AppAmountArray = Array.CreateInstance(typeof(Int32), 8);
        public readonly static Int32[] AppAmountArray = new Int32[8] { 10000, 20000, 30000, 50000, 100000, 200000, 300000, 500000 }; 

        public AppConfiguration()
        {
            //
            // TODO: Add constructor logic here
            //    
        }
       
    }

    public enum LogType 
    { 
        Viettel = 1, 
        Mobifone =2, 
        Vinaphone = 3, 
    }

    public enum MobiSoapResultCode
    { 
        Command_unknown = 1,
        Mandatory_Parameter_missing = 2,
        Communications_Error = 3        
    }

    public enum MobiSessionResultCode
    {
        Session_Required = 1,
        Session_Invalid = 2,
        Session_Not_found = 3
    }

    public enum MobiCoreResultCode
    {
        Insufficient_funds = 1001,
        Transaction_Recovered = 1002,
        Wallet_Balance_Exceeded = 1003,
        Wallet_Cap_Exceeded = 1004,
        Request_Expired = 1005,
        System_Error = 1006,
        Database_Error = 1007,
        Bad_Request = 1008       
    }

    public enum MobiUmarketResultCode
    {
        Badly_formatted_request = 1
    } 

}
