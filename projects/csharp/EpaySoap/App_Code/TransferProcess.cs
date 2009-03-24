using System;
using System.Data;
using System.Configuration;
using System.Web.Services.Description;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using EPaySoap.Utility;
using mobiEz;

///<Transation management>
///ChungNN 08/2008
///

namespace EPaySoap
{
    public class TransferProcess
    {
        string strTransactionLogType = AppConfiguration.AppTransactionLogType;
        string strPasswordKey = AppConfiguration.AppPasswordKey;       

        #region public OutputLoginInfo login(string username, string password)
        //ChungNN 05/01/2009     
        public OutputLoginInfo login(string username, string password)
        {
            OutputLoginInfo lgnObj = new OutputLoginInfo();
            TransferProcess transObj = new TransferProcess();
            Security SecObj = new Security();

            //check username and password is null?
            if (username != null && password != null)
            {
                //check valid username and password
                if (SecObj.IsValidUser(username, password))
                {
                    //check if sessionid is existed
                    if (ServiceSessionManager.GetSessionInstance().IsExistedSession(username))
                    {
                        lgnObj.session_id = ServiceSessionManager.GetSessionInstance().GetSession(username);
                        lgnObj.status = "0";
                        lgnObj.message = "Login Epay successfull.";
                    }
                    else
                    {
                        lgnObj.session_id = Guid.NewGuid().ToString();
                        lgnObj.status = "0";
                        lgnObj.message = "Login Epay successfull.";
                        ServiceSessionManager.GetSessionInstance().AddSession(lgnObj.session_id, username);
                    }
                }
                else
                {
                    lgnObj.status = "1";
                    lgnObj.message = "Login Epay fail, invalid username or password";
                    lgnObj.session_id = "0";
                }
            }
            else
            {
                lgnObj.status = "1";
                lgnObj.message = "Login Epay fail, invalid username or password";
                lgnObj.session_id = "0";
            }
            transObj.WriteLog(lgnObj.message + ", username=" + username + ", IP=" + HttpContext.Current.Request.UserHostAddress);
            return lgnObj;
        }
        #endregion

        #region public OutputLogoutInfo logout(string username, string password)
        //Check exist user in Users table
        //ChungNN 20/11/2008       
        public OutputLogoutInfo logout(string username, string sessionid)
        {
            OutputLogoutInfo lgoObj = new OutputLogoutInfo();
            //check username and password is null?
            if (username != null || sessionid != null)
            {
                //check if sessionid is existed
                if (ServiceSessionManager.GetSessionInstance().IsExistedSession(username, sessionid))
                {
                    ServiceSessionManager.GetSessionInstance().DelSession(username);
                    lgoObj.status = "0";
                    lgoObj.message = "Logout successfull.";
                }
                else
                {
                    lgoObj.status = "1";
                    lgoObj.message = "Logout fail, session is not existed.";
                }
            }
            else
            {
                lgoObj.status = "1";
                lgoObj.message = "Logout fail, username or password is invalid.";
            }

            return lgoObj;
        }
        #endregion

        #region public OutputInfo load(string UserName, string Session_ID, string Request_ID, string MerchantID, string targetMsIsdn, Int32 amount)
        //direct topup progress
        //ChungNN 26/11/2008       
        public OutputInfo load(string UserName, string Session_ID, string Request_ID, string MerchantID, string targetMsIsdn, Int32 amount)
        {
            //targetMsIsdn: so thue bao can nap tien (vd: 0912368466)
            //amount:       so tien can nap (vd: 10000)

            Security secObj = new Security();
            OutputInfo retObj = new OutputInfo();
            EPaySoap.TransferProcess proObj = new EPaySoap.TransferProcess();

            string strUserName = UserName.Trim();
            string strSession_ID = Session_ID.Trim();
            string strRequest_ID = Request_ID.Trim();
            string strMerchantID = MerchantID.Trim();
            string strtargetMsIsdn = targetMsIsdn.Trim();
            string strProviderCode = string.Empty;
            string strRequest_Time, strResponse_Time;

            retObj = secObj.CheckValid(strUserName, strSession_ID, strRequest_ID, strMerchantID, strtargetMsIsdn, amount, ref strProviderCode);

            //if login failure (status in(1,2,3,4,5))
            if (retObj.status != "0")
                return retObj;

            //If login successfull
            else
            {
                //log pending status before load
                strRequest_Time = DateTime.Now.ToString();
                proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, "2", "", "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strRequest_Time, "00", "log before load", strProviderCode);

                switch (strProviderCode)
                {                    
                    #region Vinaphone
                    case "01":
                        try
                        {
                            //process transfer for VinaPhone e-load system
                            strRequest_Time = DateTime.Now.ToString();
                            retObj = proObj.VinaLoad(strMerchantID, strtargetMsIsdn, amount);
                            //Thread.Sleep(AppConfiguration.VinaPhoneTimeout);

                            //log load result  
                            strResponse_Time = DateTime.Now.ToString();                      
                            proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, retObj.status, retObj.trans_id, "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strResponse_Time, "00", "message=" + retObj.message, strProviderCode);
                        }
                        catch (Exception ex)
                        {
                            strResponse_Time = DateTime.Now.ToString(); 
                            retObj.status = "6";
                            retObj.message = "Fail, occur an exception.";
                            proObj.WriteLog("load on VinaPhone ( targetMsIsdn=" + targetMsIsdn + ", amount = " + amount.ToString() + " ) failure." + Environment.NewLine + "Exception=" + ex.ToString());
                            proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, "1", retObj.trans_id, "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strResponse_Time, "01", "Fail, occur an exception.", strProviderCode);
                        }
                        break;
                    #endregion
                    
                    #region Viettel
                    case "02":
                        try
                        {
                            //add "84" prefix if Viettel
                            if (strtargetMsIsdn.Substring(0, 1) != "84")
                                strtargetMsIsdn = "84" + strtargetMsIsdn.Substring(1, strtargetMsIsdn.Length - 2);

                            //process transfer for Viettel topup system 
                            OutputVTload OutputVTObj = new OutputVTload();                           
                            retObj = proObj.ViettelLoad(strMerchantID, strtargetMsIsdn, amount, OutputVTObj);
                            //Thread.Sleep(AppConfiguration.VinaPhoneTimeout);

                            //log load result                              
                            proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, retObj.status, retObj.trans_id, "0x200", strtargetMsIsdn, amount, AppConfiguration.ViettelClientID, "", OutputVTObj.request_transaction_time, OutputVTObj.response_transaction_time, OutputVTObj.response_code, "message=" + retObj.message, strProviderCode);
                        }
                        catch (Exception ex)
                        {
                            strResponse_Time = DateTime.Now.ToString(); 
                            retObj.status = "6";
                            retObj.message = "Fail, occur an exception.";
                            proObj.WriteLog("load on Viettel ( targetMsIsdn=" + targetMsIsdn + ", amount = " + amount.ToString() + " ) failure." + Environment.NewLine + "Exception=" + ex.ToString());
                            proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, "1", retObj.trans_id, "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strResponse_Time, "01", "Fail, occur an exception.", strProviderCode);
                        }
                        break;
                    #endregion

                    #region Mobifone
                    case "03":
                        try
                        {                           
                            //process transfer for Viettel MobiEz system
                            strRequest_Time = DateTime.Now.ToString();
                            retObj = proObj.MobiLoad(strMerchantID, targetMsIsdn, (decimal) amount);

                            //log load result  
                            strResponse_Time = DateTime.Now.ToString();
                            proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, retObj.status, retObj.trans_id, "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strResponse_Time, "00", "message=" + retObj.message, strProviderCode);
          
                        }
                        catch (Exception ex)
                        {
                            strResponse_Time = DateTime.Now.ToString(); 
                            retObj.status = "6";
                            retObj.message = "Fail, occur an exception.";
                            proObj.WriteLog("load on Mobifone ( targetMsIsdn=" + targetMsIsdn + ", amount = " + amount.ToString() + " ) failure." + Environment.NewLine + "Exception=" + ex.ToString());
                            proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, "1", retObj.trans_id, "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strResponse_Time, "01", "Fail, occur an exception.", strProviderCode);
                        }
                        break;
                    #endregion
                    
                    default:
                        retObj.status = "1";
                        retObj.message = "invalid provider.";
                        break;
                }
            }

            return retObj;
        }
        #endregion

        #region public OutputInfo stock_transfer(string UserName, string Session_ID, string Request_ID, string MerchantID, string targetMsIsdn, Int32 amount)
        //stock transfer for Vinaphone mobile users
        //ChungNN 3/2009
        public OutputInfo stock_transfer(string UserName, string Session_ID, string Request_ID, string MerchantID, string targetMsIsdn, Int32 amount)
        {
            //targetMsIsdn: so thue bao dai ly can nap tien (vd: 0912368466)
            //amount:       so tien can nap (vd: 10000)

            Security secObj = new Security();
            OutputInfo retObj = new OutputInfo();
            EPaySoap.TransferProcess proObj = new EPaySoap.TransferProcess();

            string strUserName = UserName.Trim();
            string strSession_ID = Session_ID.Trim();
            string strRequest_ID = Request_ID.Trim();
            string strMerchantID = MerchantID.Trim();
            string strtargetMsIsdn = targetMsIsdn.Trim();
            string strProviderCode = string.Empty;
            string strRequest_Time, strResponse_Time;

            retObj = secObj.CheckValid(strUserName, strSession_ID, strRequest_ID, strMerchantID, strtargetMsIsdn, amount, ref strProviderCode);

            //if login failure (status in(1,2,3,4,5))
            if (retObj.status != "0")
                return retObj;
            else if (strProviderCode != "01")
            {
                retObj.message = "Invalid service provider, only support for Vinaphone.";
                return retObj;
            }
            //If login successfull
            else
            {
                //log pending status before load
                strRequest_Time = DateTime.Now.ToString();
                proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, "2", "", "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strRequest_Time, "00", "log before load", strProviderCode);

                try
                {
                    //process stock transfer for VinaPhone e-load system
                    strRequest_Time = DateTime.Now.ToString();
                    retObj = proObj.VinaStockTransfer(strMerchantID, strtargetMsIsdn, amount);
                    //Thread.Sleep(AppConfiguration.VinaPhoneTimeout);

                    //log stock transfer result  
                    strResponse_Time = DateTime.Now.ToString();
                    proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, retObj.status, retObj.trans_id, "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strResponse_Time, "00", "message=" + retObj.message, strProviderCode);
                }
                catch (Exception ex)
                {
                    strResponse_Time = DateTime.Now.ToString();
                    retObj.status = "6";
                    retObj.message = "Fail, occur an exception.";
                    proObj.WriteLog("stock transfer on VinaPhone ( targetMsIsdn=" + targetMsIsdn + ", amount = " + amount.ToString() + " ) failure." + Environment.NewLine + "Exception=" + ex.ToString());
                    proObj.WriteLog_Load(strUserName, strSession_ID, strRequest_ID, strMerchantID, HttpContext.Current.Request.UserHostAddress, "1", retObj.trans_id, "0x200", strtargetMsIsdn, amount, "", "", strRequest_Time, strResponse_Time, "01", "Fail, occur an exception.", strProviderCode);
                }
            }

            return retObj;
        }
        #endregion




        #region private EPaySoap.OutputInfo VinaLoad(string MerchantID, string targetMsIsdn, Int32 amount)
        //process load for VinaPhone e-load system
        //ChungNN 08/2008
        private EPaySoap.OutputInfo VinaLoad(string MerchantID, string targetMsIsdn, Int32 amount)
        {            
            EPaySoap.OutputInfo retObj = new EPaySoap.OutputInfo();
            EPaySoap.Security secObj = new Security();

            Random random = new Random();
            int nCounter = random.Next(9000000);
                        
            //load to Vinaphone mobie user
            try
            {
                Vinaphone vinaObj = new Vinaphone();
                EnquiryOutput loadObj = new EnquiryOutput();
                loadObj = vinaObj.load(targetMsIsdn, amount, nCounter);
                
                //If load seccessfull
                if (loadObj.status == "0")
                {
                    retObj.status = loadObj.status;
                    retObj.message = loadObj.message;
                    WriteLog("load on VinaPhone successfull. ( MerchantID=" + MerchantID + ",  targetMsIsdn=" + targetMsIsdn + ", amount=" + amount.ToString() + " )");
                }
                else
                {
                    //VinaPhone fix status = "1"
                    retObj.status = "1";
                    retObj.message = loadObj.message;
                    WriteLog("load on VinaPhone failure. ( MerchantID=" + MerchantID + ", targetMsIsdn=" + targetMsIsdn + ", amount=" + amount.ToString() + " ) ");
                }
            }
            catch (Exception ex)
            {
                WriteLog("load on VinaPhone failure, Exception=" + ex.ToString());
                throw (ex);
            }

            return retObj;
        }

        #endregion

        #region private EPaySoap.OutputInfo VinaStockTransfer(string MerchantID, string targetMsIsdn, Int32 amount)
        //process stock transfer from agent to agent
        //ChungNN 03/2009
        private EPaySoap.OutputInfo VinaStockTransfer(string MerchantID, string targetMsIsdn, Int32 amount)
        {
            EPaySoap.OutputInfo retObj = new EPaySoap.OutputInfo();
            EPaySoap.Security secObj = new Security();

            Random random = new Random();
            int nCounter = random.Next(9000000);

            //stock transfer from agent to agent
            try
            {
                Vinaphone vinaObj = new Vinaphone();
                EnquiryOutput stocktransferObj = new EnquiryOutput();
                stocktransferObj = vinaObj.stock_transfer(targetMsIsdn, amount, nCounter);

                //If transfer seccessfull
                if (stocktransferObj.status == "0")
                {
                    retObj.status = stocktransferObj.status;
                    retObj.message = stocktransferObj.message;
                    WriteLog("stock transfer on VinaPhone successfull. ( MerchantID=" + MerchantID + ",  targetMsIsdn=" + targetMsIsdn + ", amount=" + amount.ToString() + " )");
                }
                else
                {
                    //VinaPhone fix status = "1"
                    retObj.status = "1";
                    retObj.message = stocktransferObj.message;
                    WriteLog("stock transfer on VinaPhone failure. ( MerchantID=" + MerchantID + ", targetMsIsdn=" + targetMsIsdn + ", amount=" + amount.ToString() + " ) ");
                }
            }
            catch (Exception ex)
            {
                WriteLog("stock transfer on VinaPhone failure, Exception=" + ex.ToString());
                throw (ex);
            }

            return retObj;
        }

        #endregion

        #region private EPaySoap.OutputInfo ViettelLoad(string MerchantID, string targetMsIsdn, Int32 amount)
        //process stock transfer for VinaPhone e-load system
        //ChungNN 08/2008
        private EPaySoap.OutputInfo ViettelLoad(string MerchantID, string targetMsIsdn, Int32 amount, OutputVTload OutputVTObj)
        {
            EPaySoap.OutputInfo retObj = new EPaySoap.OutputInfo();
            EPaySoap.Security secObj = new Security();

            Random random = new Random();
            //int nCounter = random.Next(9000000);
            string strCounter = DateTime.Now.ToString("yyyyMMddHHMMss");

            //Login to Viettel eload system
            try
            {
                Viettel viettelObj = new Viettel();
                OutputVTload loadObj = new OutputVTload();
                loadObj = viettelObj.load(targetMsIsdn, amount, strCounter);
                OutputVTObj = loadObj;

                //If load seccessfull
                if (loadObj.status == "0")
                {
                    retObj.status = loadObj.status;
                    retObj.message = loadObj.message;
                    WriteLog("load on Viettel successfull. ( MerchantID=" + MerchantID + ",  targetMsIsdn=" + targetMsIsdn + ", amount=" + amount.ToString() + " )");
                }
                else
                {
                    //status = "1"
                    retObj.status = "1";
                    retObj.message = loadObj.message;
                    WriteLog("load on Viettel failure. ( MerchantID=" + MerchantID + ", targetMsIsdn=" + targetMsIsdn + ", amount=" + amount.ToString() + " ) ");
                }
            }
            catch (Exception ex)
            {
                WriteLog("load on Viettel failure, Exception=" + ex.ToString());
                throw (ex);
            }

            return retObj;
        }

        #endregion

        #region private EPaySoap.OutputInfo MobiLoad(string MerchantID, string targetMsIsdn, Int32 amount)
        //process stock transfer for MobiFone MobiEz system
        //ChungNN 08/2008
        private EPaySoap.OutputInfo MobiLoad(string MerchantID, string targetMsIsdn, decimal amount)
        {
            EPaySoap.OutputInfo retObj = new EPaySoap.OutputInfo();
            Mobifone mobiObj = new Mobifone();
            mobiEz.buyResponse loadObj = new buyResponse();
          
            try
            {
                loadObj = mobiObj.load(targetMsIsdn, amount);
                if (loadObj.buyReturn.result == 0)
                {
                    retObj.status = "0";
                    retObj.message = "ok";
                    WriteLog("load on Mobifone successfull. ( MerchantID=" + MerchantID + ",  targetMsIsdn=" + targetMsIsdn + ", amount=" + amount.ToString() + " )");
                }
                else
                {
                    retObj.status = "1";
                    retObj.message = "not ok";
                    WriteLog("load on Mobifone failure. ( MerchantID=" + MerchantID + ", targetMsIsdn=" + targetMsIsdn + ", amount=" + amount.ToString() + " ), buyReturn.result=" + loadObj.buyReturn.result.ToString() + ", buyReturn.result_namespace=" + loadObj.buyReturn.result_namespace);
                }
            }
            catch (Exception ex)
            {
                retObj.status = "1";
                retObj.message = "not ok";
                WriteLog("load on Mobifone failure, Exception =" + ex.ToString());
                throw (ex);
            }                      
           
            return retObj;
        }

        #endregion

        #region public void WriteLog(string strMessage)

        public void WriteLog(string strMessage)
        {
            //Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:tt") + " " + strMessage);
            //*
            string strLogPath = HttpContext.Current.Server.MapPath("") + "\\Log";
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            string strDay = DateTime.Now.Day.ToString();
            string strFileName = "Log_Action_" + DateTime.Now.ToString("hh") + "h.log"; ;

            System.IO.DirectoryInfo d1 = new System.IO.DirectoryInfo(strLogPath);
            if (!d1.Exists)
                d1.Create();
            strLogPath += "\\" + strYear;

            System.IO.DirectoryInfo d2 = new System.IO.DirectoryInfo(strLogPath);
            if (!d2.Exists)
                d2.Create();
            strLogPath += "\\" + strMonth;
            
            System.IO.DirectoryInfo d3 = new System.IO.DirectoryInfo(strLogPath);
            if (!d3.Exists)
                d3.Create();
            strLogPath += "\\" + strDay;

            System.IO.DirectoryInfo d4 = new System.IO.DirectoryInfo(strLogPath);
            if (!d4.Exists)
                d4.Create();

            strLogPath = strLogPath + "\\" + strFileName;
            //strLogPath = "E:\\" + strFileName;

            System.IO.FileStream fs = new System.IO.FileStream(strLogPath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            System.IO.StreamWriter m_streamWriter = new System.IO.StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
            m_streamWriter.WriteLine(DateTime.Now.ToString("hh:mm:ss:ms:tt") + " " + strMessage);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            //*/
        }

        #endregion

        #region public void WriteLog_Load1(string UserName, string Trans_ID, string MerchantID, string targetMsIsdn, double Amount, string IPAddress, string Status, bool UpdateType)

        public void WriteLog_Load1(string UserName, string Session_ID, string Request_ID, string Trans_ID, string MerchantID, string targetMsIsdn, double Amount, string IPAddress, string Status, bool UpdateType, string Note)
        {
            #region log into text file

            if (strTransactionLogType == "1" || strTransactionLogType == "3")
            {                
                string strLogPath = HttpContext.Current.Server.MapPath("") + "\\Log";
                string strYear = DateTime.Now.Year.ToString();
                string strMonth = DateTime.Now.Month.ToString();
                string strDay = DateTime.Now.Day.ToString();
                string strFileName = "Log_Load_" + DateTime.Now.ToString("hh") + "h.log";
                string strLogDescription = string.Empty;

                System.IO.DirectoryInfo d1 = new System.IO.DirectoryInfo(strLogPath);
                if (!d1.Exists)
                    d1.Create();
                strLogPath += "\\" + strYear;

                System.IO.DirectoryInfo d2 = new System.IO.DirectoryInfo(strLogPath);
                if (!d2.Exists)
                    d2.Create();
                strLogPath += "\\" + strMonth;

                System.IO.DirectoryInfo d3 = new System.IO.DirectoryInfo(strLogPath);
                if (!d3.Exists)
                    d3.Create();
                strLogPath += "\\" + strDay;

                System.IO.DirectoryInfo d4 = new System.IO.DirectoryInfo(strLogPath);
                if (!d4.Exists)
                    d4.Create();

                strLogPath = strLogPath + "\\" + strFileName;
                strLogDescription = UserName + "\t";
                strLogDescription += Session_ID + "\t";
                strLogDescription += Request_ID + "\t";
                strLogDescription += Trans_ID + "\t";
                strLogDescription += MerchantID + "\t";
                strLogDescription += targetMsIsdn + "\t";
                strLogDescription += Amount.ToString() + "\t";
                strLogDescription += IPAddress + "\t";
                strLogDescription += Status.ToString() + "\t";
                strLogDescription += DateTime.Now.ToString("hh:mm:ss:ms:tt") + "\t";
                strLogDescription += Note + "\t";

                System.IO.FileStream fs = new System.IO.FileStream(strLogPath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                System.IO.StreamWriter m_streamWriter = new System.IO.StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                m_streamWriter.WriteLine(strLogDescription);
                m_streamWriter.Flush();
                m_streamWriter.Close();
            }

            #endregion

            #region log into DB

            if (strTransactionLogType == "2" || strTransactionLogType == "3")
            {
                App_Lib DbObj = new App_Lib();
                string strSql = "";
                //If update
                if (UpdateType)
                {
                    strSql = "update Transactions set ConfirmStatus = '" + Status + "', ConfirmDate=sysdate where Trans_ID='" + Trans_ID + "'";
                }
                //Insert
                else
                {
                    strSql = "insert into Transactions(UserName, Session_ID, Request_ID, Trans_ID, Merchant_ID, targetMsIsdn, Amount, IPAddress, Status, ActionDate)";
                    strSql += "values('" + UserName + "', '" + Session_ID + "', '" + Request_ID + "', '" + Trans_ID + "', '" + MerchantID + "', '" + targetMsIsdn + "', '" + Amount.ToString() + "', '" + IPAddress + "', " + Status.ToString() + ", sysdate)";                    
                }
                    try
                    {
                        DbObj.ExecuteQuery(strSql, null, CommandType.Text);
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        //HttpContext.Current.Response.Write(strSql);
                        //HttpContext.Current.Response.End();
                        WriteLog("Insert DB failure, ex=" + ex.ToString());
                    }
            }

            #endregion

        }
        
        #endregion

        #region public void WriteLog_Load(...
        public void WriteLog_Load(   
              string USERNAME, string SESSION_ID, string REQUEST_ID, string MERCHANT_ID
            , string IPADDRESS, string STATUS, string TRANS_ID, string MESSAGE_TYPE_ID
            , string MSISDN, double TRANSACTION_AMOUNT, string CLIENT_ID, string SYSTEM_TRACE_AUDIT_NUMBER
            , string REQUEST_TRANSMISSION_TIME , string RESPONSE_TRANSMISSION_TIME, string RESPONSE_CODE, string NOTE
            , string SERVICE_PROVIDER_CODE
            )
        {
            #region log into text file

            if (strTransactionLogType == "1" || strTransactionLogType == "3")
            {
                string strLogPath = HttpContext.Current.Server.MapPath("") + "\\Log";
                string strYear = DateTime.Now.Year.ToString();
                string strMonth = DateTime.Now.Month.ToString();
                string strDay = DateTime.Now.Day.ToString();
                string strFileName = "Log_Load_" + DateTime.Now.ToString("hh") + "h.log";
                string strLogDescription = string.Empty;

                System.IO.DirectoryInfo d1 = new System.IO.DirectoryInfo(strLogPath);
                if (!d1.Exists)
                    d1.Create();
                strLogPath += "\\" + strYear;

                System.IO.DirectoryInfo d2 = new System.IO.DirectoryInfo(strLogPath);
                if (!d2.Exists)
                    d2.Create();
                strLogPath += "\\" + strMonth;

                System.IO.DirectoryInfo d3 = new System.IO.DirectoryInfo(strLogPath);
                if (!d3.Exists)
                    d3.Create();
                strLogPath += "\\" + strDay;

                System.IO.DirectoryInfo d4 = new System.IO.DirectoryInfo(strLogPath);
                if (!d4.Exists)
                    d4.Create();

                strLogPath = strLogPath + "\\" + strFileName;
                strLogDescription = USERNAME + "\t" + SESSION_ID + "\t" + REQUEST_ID + "\t" + MERCHANT_ID + "\t";
                strLogDescription += IPADDRESS + "\t" + STATUS + "\t" + TRANS_ID + "\t" + MESSAGE_TYPE_ID + "\t";
                strLogDescription += SERVICE_PROVIDER_CODE + "\t" + MSISDN + "\t" + TRANSACTION_AMOUNT + "\t" + CLIENT_ID + "\t";
                strLogDescription += SYSTEM_TRACE_AUDIT_NUMBER + "\t" + REQUEST_TRANSMISSION_TIME + "\t" + RESPONSE_TRANSMISSION_TIME + "\t" + RESPONSE_CODE + "\t";                
                strLogDescription += NOTE + "\t";
                                
                System.IO.FileStream fs = new System.IO.FileStream(strLogPath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                System.IO.StreamWriter m_streamWriter = new System.IO.StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                m_streamWriter.WriteLine(strLogDescription);
                m_streamWriter.Flush();
                m_streamWriter.Close();
            }

            #endregion

            #region log into DB

            if (strTransactionLogType == "2" || strTransactionLogType == "3")
            {
                App_Lib DbObj = new App_Lib();
                string strSql = "";
                strSql = "insert into Transactions(";
                strSql += "  USERNAME, SESSION_ID, REQUEST_ID, MERCHANT_ID";
                strSql += ", IPADDRESS, STATUS, TRANS_ID, MESSAGE_TYPE_ID";
                strSql += ", MSISDN, TRANSACTION_AMOUNT, CLIENT_ID, SYSTEM_TRACE_AUDIT_NUMBER";
                strSql += ", REQUEST_TRANSMISSION_TIME, RESPONSE_TRANSMISSION_TIME, RESPONSE_CODE, NOTE";
                strSql += ", SERVICE_PROVIDER_CODE, CDR_STATUS";
                strSql += " ) ";
                //, Status, ActionDate)";
                strSql += "values(";
                strSql += "'" + USERNAME + "', '" + SESSION_ID + "', '" + REQUEST_ID + "', '" + MERCHANT_ID + "'";
                strSql += " ,'" + IPADDRESS + "', '" + STATUS + "', '" + TRANS_ID + "', '" + MESSAGE_TYPE_ID + "'";
                strSql += " ,'" + MSISDN + "', '" + TRANSACTION_AMOUNT + "', '" + CLIENT_ID + "', '" + SYSTEM_TRACE_AUDIT_NUMBER + "'";
                strSql += " ,TO_DATE('" + REQUEST_TRANSMISSION_TIME + "','mm/dd/yyyy hh:mi:ss PM'), TO_DATE('" + RESPONSE_TRANSMISSION_TIME + "','mm/dd/yyyy hh:mi:ss PM'), '" + RESPONSE_CODE + "', '" + NOTE + "'";
                strSql += " ,'" + SERVICE_PROVIDER_CODE + "', '0'";
                strSql += ")";
                               
                try
                {
                    DbObj.ExecuteQuery(strSql, null, CommandType.Text);
                }
                catch (Exception ex)
                {
                    //throw ex;
                    //HttpContext.Current.Response.Write(strSql);
                    //HttpContext.Current.Response.End();
                    WriteLog("Insert DB failure, ex=" + ex.ToString());
                }
            }

            #endregion

        }

        #endregion

        #region public void WriteLog_Load(string[] inputArray)

        public void WriteLog_Load(string[] inputArray, LogType lgType)
        {       
           string strSql = "";

           switch (lgType)
           {
               case LogType.Viettel:
               {
                   strSql = "insert into Transaction_Log(";
                   strSql += "  Message_Type_ID";
                   strSql += ", MSISDN";
                   strSql += ", Transaction_Amount";
                   strSql += ", Client_ID";
                   strSql += ", System_Trace_Audit_Number";
                   strSql += ", Request_Transmission_Time";
                   strSql += ", Response_Transmission_Time";
                   strSql += ", Response_Code";
                   strSql += " )";
                   strSql += " values ( ";
                   strSql += "'" + inputArray[0] + "'";
                   strSql += ", '" + inputArray[1] + "'";
                   strSql += ", '" + inputArray[2] + "'";
                   strSql += ", '" + inputArray[3] + "'";
                   strSql += ", '" + inputArray[4] + "'";
                   strSql += ", '" + inputArray[5] + "'";
                   strSql += ", '" + inputArray[6] + "'";
                   strSql += ", '" + inputArray[7] + "'";
                   strSql += ", '" + inputArray[8] + "'";
                   strSql += " )";
                    break;
                }    
           }
        }

        #endregion

        #region public OutputConfirmInfo confirm(string username, string password)
        //confirm for a load process
        //ChungNN 11/09/2008     
        public OutputConfirmInfo confirm(string UserName, string Session_ID, string Trans_ID, string Status)
        {
            OutputConfirmInfo retObj = new OutputConfirmInfo();
            Security secObj = new Security();
            TransferProcess traObj = new TransferProcess();
            string strIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (!secObj.Check_Valid_IPAddress(strIPAddress))
            {
                retObj.status = "1";
                retObj.message = "invalid IP Address.";
            }
            else if (!secObj.Check_Valid_Session(UserName, Session_ID))
            {
                retObj.status = "1";
                retObj.message = "invalid username or session_id.";
            }
            else
            {
                try
                {
                    //traObj.WriteLog_Load(UserName, Session_ID, "", "", "", "", 0, strIPAddress, Status, true, "confirm transaction.");
                    retObj.status = "0";
                    retObj.message = "update successfull.";
                }
                catch
                {
                    retObj.status = "1";
                    retObj.message = "update failure.";
                }
            }

            return retObj;
        }
        #endregion
    }
}

