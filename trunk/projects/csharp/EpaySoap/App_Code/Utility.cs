using System;
using System.Collections.Generic;
using System.Text;
using Solab.Iso8583;
using Solab.Iso8583.Parsing;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Net.Sockets;
using System.Web.Services.Description;
using System.IO;
using System.Xml;
using Encrypt;

using mobiEz;

/// Epay direct top-up utility
/// ChungNN 12/2008

namespace EPaySoap.Utility
{
    #region public class Vinaphone
    //ChungNN 12/2008
    public class Vinaphone
    {
        private static EloadFunctionService eloadObj = new EloadFunctionService();
        private static TransferProcess transObj = new TransferProcess();

        #region private string login()

        private string login()
        {
            BasicInput LoginInputObj = new BasicInput();
            LoginOutput LoginOutputObj = new LoginOutput();
            Security secObj = new Security();

            LoginInputObj.agentMsIsdn = AppConfiguration.VinaPhoneAgentMsIsdl;
            //LoginInputObj.password = secObj.GetSHA1Hash(secObj.Epay_Decrypt(Password, PasswordKey));
            LoginInputObj.password = secObj.GetSHA1Hash(AppConfiguration.VinaPhonePassword); //fEqNCco3Yq9h5ZUglD3CZJT4lBs=
            LoginInputObj.username = AppConfiguration.VinaPhoneUserName;
                       
            try
            {
                LoginOutputObj = eloadObj.login(LoginInputObj);
                if (LoginOutputObj.status == "0")
                    transObj.WriteLog("Login Vinaphone successful, sessionid=" + LoginOutputObj.sessionid);
                else
                    transObj.WriteLog("Login Vinaphone fail, status=" + LoginOutputObj.status + ", message=" + LoginOutputObj.message);
                //Console.WriteLine("login() result:");
                //Console.WriteLine("status=" + LoginOutputObj.status + ", message=" + LoginOutputObj.message);                
            }
            catch (Exception ex)
            {
                transObj.WriteLog("Login Vinaphone fail, exception=" + ex.ToString());
                return "";
            }


            //if successfull
            if (LoginOutputObj.status == "0")
                return LoginOutputObj.sessionid;
            else
                return "";
            
        }

        #endregion

        #region private string logout(string sessionid)

        private string logout(string sessionid)
        {
            BasicOutput outputObj = new BasicOutput();
            try
            {
                outputObj = eloadObj.logout(sessionid);
                Thread.Sleep(AppConfiguration.VinaPhoneTimeout);
            }
            catch
            {
                return "";
            }

            if (outputObj.status == "0")
                return outputObj.transId;
            else
                return "";

        }

        #endregion

        #region private string login()

        private string login()
        {
            BasicInput LoginInputObj = new BasicInput();
            LoginOutput LoginOutputObj = new LoginOutput();
            Security secObj = new Security();

            LoginInputObj.agentMsIsdn = AppConfiguration.VinaPhoneAgentMsIsdl;
            //LoginInputObj.password = secObj.GetSHA1Hash(secObj.Epay_Decrypt(Password, PasswordKey));
            LoginInputObj.password = secObj.GetSHA1Hash(AppConfiguration.VinaPhonePassword); //fEqNCco3Yq9h5ZUglD3CZJT4lBs=
            LoginInputObj.username = AppConfiguration.VinaPhoneUserName;

            try
            {
                LoginOutputObj = eloadObj.login(LoginInputObj);
                if (LoginOutputObj.status == "0")
                    transObj.WriteLog("Login Vinaphone successful, sessionid=" + LoginOutputObj.sessionid);
                else
                    transObj.WriteLog("Login Vinaphone fail, status=" + LoginOutputObj.status + ", message=" + LoginOutputObj.message);
                //Console.WriteLine("login() result:");
                //Console.WriteLine("status=" + LoginOutputObj.status + ", message=" + LoginOutputObj.message);                
            }
            catch (Exception ex)
            {
                transObj.WriteLog("Login Vinaphone fail, exception=" + ex.ToString());
                return "";
            }


            //if successfull
            if (LoginOutputObj.status == "0")
                return LoginOutputObj.sessionid;
            else
                return "";

        }

        #endregion

        #region public EnquiryOutput stock_transfer(string Target, Int32 Amount, int Counter)
        public EnquiryOutput stock_transfer(string Target, Int32 Amount, int Counter)
        {
            EnquiryOutput outputObj = new EnquiryOutput();
            Security secObj = new Security();
            
            outputObj.status = "1";
            outputObj.message = "not ok";

            string sessionid = string.Empty;
            if (SrvProviderSessionManager.GetSessionInstance().IsExistedSession("VinaphoneSession"))
                sessionid = SrvProviderSessionManager.GetSessionInstance().GetSession("VinaphoneSession");
            else
            {
                sessionid = login();
                SrvProviderSessionManager.GetSessionInstance().DelSession("VinaphoneSession");
                SrvProviderSessionManager.GetSessionInstance().AddSession(sessionid, "VinaphoneSession");
            }
            
            try
            {                   
                TransactionInput tranObj = new TransactionInput();
                tranObj.username = AppConfiguration.VinaPhoneUserName;
                //tranObj.password = secObj.TripleDES_Encrypt(secObj.Epay_Decrypt(Password, PasswordKey), sessionid);
                tranObj.password = secObj.TripleDES_Encrypt(AppConfiguration.VinaPhoneAgentMPIN, sessionid); // 
                tranObj.agentMsIsdn = AppConfiguration.VinaPhoneAgentMsIsdl;
                tranObj.targetMsIsdn = Target;
                tranObj.amount = Amount;
                tranObj.counter = Counter;

                outputObj = eloadObj.stock_transfer(tranObj);
                //Thread.Sleep(Timeout);
                return outputObj;
            }
            catch
            {
                logout(sessionid);
                return outputObj;
            }            

            return outputObj;
        }
        #endregion

        #region public EnquiryOutput load(string Target, Int64 Amount, Int32 Counter)

        public EnquiryOutput load(string Target, Int32 Amount, int Counter)
        {
            EnquiryOutput outputObj = new EnquiryOutput();
            Security secObj = new Security();

            outputObj.status = "1";
            outputObj.message = "not ok";
            
            string sessionid = string.Empty;
            if (SrvProviderSessionManager.GetSessionInstance().IsExistedSession("VinaphoneSession"))
                sessionid = SrvProviderSessionManager.GetSessionInstance().GetSession("VinaphoneSession");
            else
            {
                sessionid = login();
                SrvProviderSessionManager.GetSessionInstance().DelSession("VinaphoneSession");
                SrvProviderSessionManager.GetSessionInstance().AddSession(sessionid, "VinaphoneSession");
            }         
                       
            try
            {
                TransactionInput tranObj = new TransactionInput();
                tranObj.username = AppConfiguration.VinaPhoneUserName;
                //tranObj.password = secObj.TripleDES_Encrypt(secObj.Epay_Decrypt(Password, PasswordKey), sessionid);
                tranObj.password = secObj.TripleDES_Encrypt(AppConfiguration.VinaPhoneAgentMPIN, sessionid); // 
                tranObj.agentMsIsdn = AppConfiguration.VinaPhoneAgentMsIsdl;
                tranObj.targetMsIsdn = Target;
                tranObj.amount = Amount;
                tranObj.counter = Counter;

                outputObj = eloadObj.load(tranObj);
                //Thread.Sleep(Timeout);

                return outputObj;                        
                        
            }
            catch
            {
                logout(sessionid);
                return outputObj;
            }           

            return outputObj;
        }

        #endregion

    }

    #endregion

    #region public class Viettel
    //ChungNN 02/2009
    public class Viettel
    {
        Encrypt.DigitalSign SignObj;
                       
        public Viettel()
        {
            SignObj = new DigitalSign(AppConfiguration.AppPath + AppConfiguration.ViettelCerFilePath, AppConfiguration.AppPath + AppConfiguration.AppRSAPrivateKeyFilePath, "thangtc");
        }
                
        #region private static bool SendMessage(string MSISDN, Int32 Amount, string Counter)
       
        //private static bool SendMessage(string MSISDN, Int32 Amount, string Counter)
        public bool SendMessage(string MSISDN, Int32 Amount, string Counter, ref OutputVTload OutputVTObj)
        {
            //Send ISO message to VietTel Topup Gateway
            //ChungNN 02/2009 --------
            //----------------------------------------
            TransferProcess transObj = new TransferProcess();
            bool blnReturnValue = false;            

            try
            {
                Encrypt.DigitalSign SignObj = new DigitalSign(AppConfiguration.AppPath + AppConfiguration.ViettelCerFilePath, AppConfiguration.AppPath + AppConfiguration.AppRSAPrivateKeyFilePath, AppConfiguration.AppRSAPrivateKeyPassword);
                string strDigitalSign = string.Empty;
                

                MessageFactory mfact = ConfigParser.CreateFromFile(AppConfiguration.AppPath + AppConfiguration.ViettelConfig_ISOFile);
                mfact.AssignDate = true;
                //mfact.TraceGenerator = new i
                IsoMessage m = null;
                TcpClient sock;

                byte[] lenbuf = new byte[1024];
                try
                {
                    sock = new TcpClient(AppConfiguration.ViettelServerAddress, AppConfiguration.ViettelServerPort);
                }
                catch
                {
                    return blnReturnValue;
                    //throw (ex);
                }
                if ((sock == null) || !sock.Connected)
                {
                    return blnReturnValue;
                }

                //if (MSISDN.Substring(0, 2) != "84")
                //    MSISDN = "84" + MSISDN;

                //fill ISO8583 message fields
                m = mfact.NewMessage(0x200);
                mfact.Setfield(2, MSISDN, ref m);
                mfact.Setfield(3, 0, ref m);
                mfact.Setfield(4, Amount, ref m);
                mfact.Setfield(7, DateTime.Now.ToString("yyyyMMddHHMMss"), ref m);
                mfact.Setfield(11, Counter, ref m);                
                mfact.Setfield(63, AppConfiguration.ViettelClientID, ref m);
                
                //create Digital Signature
                if(m.HasField(2) && m.GetField(2)!=null)
                    strDigitalSign += m.GetField(2).ToString();
                if (m.HasField(3) && m.GetField(3) != null)
                    strDigitalSign += m.GetField(3).ToString();
                if (m.HasField(4) && m.GetField(4) != null)
                    strDigitalSign += m.GetField(4).ToString();
                if (m.HasField(7) && m.GetField(7) != null)
                    strDigitalSign += m.GetField(7).ToString();
                if (m.HasField(11) && m.GetField(11) != null)
                    strDigitalSign +=  m.GetField(11).ToString();
                if (m.HasField(63) && m.GetField(63) != null)
                    strDigitalSign +=  m.GetField(63).ToString();

                OutputVTObj.request_transaction_time = m.GetField(7).ToString();
                //verify du lieu nhan dc tu server
                //SignObj._VerifyData("123456", strOub);

                //sign du lieu truoc khi gui di
                SignObj._signData(strDigitalSign, ref strDigitalSign);

                mfact.Setfield(64, strDigitalSign, ref m);
                m.Write(sock.GetStream(), 4, false);
                
                //<field num="2" type="LLVAR" length="0" />    	<!--MSISDN-->
                //<field num="3" type="NUMERIC" length="6" />     <!--Processing Code-->
                //<field num="4" type="NUMERIC" length="12" />    <!--Transaction Amount-->
                //<field num="7" type="NUMERIC" length="14"/>     <!--Transmission Date & Time-->
                //<field num="11" type="NUMERIC" length="15" />   <!--System Trace Audit Number-->
                //<field num="39" type="NUMERIC" length="2" />    <!--Response Code-->
                //<field num="63" type="LLVAR" length="0" />      <!--Client ID-->
                //<field num="64" type="LLLVAR" length="0" />     <!--Message Signature-->

                //send ISO8583 message
                //Console.Out.Write(Encoding.ASCII.GetString(m.getByte(4, false)));
                
                Thread.Sleep(AppConfiguration.ViettelTimeout);
                                
                while ((sock != null) && sock.Connected == true)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        if (sock.GetStream().Read(lenbuf, 0, 4) == 4)
                        {
                            int size, k;
                            size = 0;

                            for (k = 0; k < 4; k++)
                            {
                                size = size + (int)(lenbuf[k] - 48) * (int)(Math.Pow(10, 3 - k));
                            }

                            byte[] buf = new byte[size + 1];
                            //We're not expecting ETX in this case
                            sock.GetStream().Read(buf, 0, buf.Length);

                            //Console.Out.Write(Encoding.ASCII.GetString(buf));
                            IsoMessage incoming = mfact.ParseMessage(buf, 0);

                            if (Convert.ToInt16(incoming.GetField(39).Value) == 0)
                            {
                                blnReturnValue = true;
                                //Ghi giao dich phuc vu doi soat
                                OutputVTObj.response_transaction_time = incoming.GetField(7).ToString(); //Response Transmission Time
                                OutputVTObj.response_code = incoming.GetField(39).ToString();   //Response Code
                               
                            }
                            else
                            {
                                //Ghi giao dich phuc vu doi soat
                                OutputVTObj.response_transaction_time = incoming.GetField(7).ToString(); //Response Transmission Time
                                OutputVTObj.response_code = incoming.GetField(39).ToString();   //Response Code
                                blnReturnValue = true;
                            }
                        }
                        else
                        {
                            OutputVTObj.response_transaction_time = ""; //Response Transmission Time
                            OutputVTObj.response_code = "";   //Response Code
                            blnReturnValue = false;
                        }
                    }
                    catch
                    {
                        //Console.Out.Write(ex.ToString());
                        blnReturnValue = false;
                    }
                }

            }
            catch //(Exception ex)
            {
                blnReturnValue = false;
            }
                        
            return blnReturnValue;

        }

        #endregion

        #region public OutputVTload load(string Target, Int64 Amount, Int32 Counter)

        public OutputVTload load(string Target, Int32 Amount, string Counter)
        {           
            //-----------------------------------------
            OutputVTload outputObj = new OutputVTload();
            Security secObj = new Security();
            outputObj.status = "1";
            outputObj.message = "not ok";
            outputObj.trans_id = "";
            outputObj.system_trace_audit_number = Counter;
            
            string sessionid = string.Empty;
            try
            {
                if (SendMessage(Target, Amount, Counter, ref outputObj))
                {
                    outputObj.status = "0";
                    outputObj.message = "ok";
                    outputObj.trans_id = Guid.NewGuid().ToString();
                }
                else
                {
                    outputObj.status = "1";
                    outputObj.message = "not ok";
                    outputObj.trans_id = "";
                }
                return outputObj;
            }
            catch
            {               
                return outputObj;
            }            
        }

        #endregion
    }

    #endregion

    #region public class Mobifone
    //ChungNN 03/2009
    public class Mobifone
    {
        string sessionid = AppConfiguration.MobifoneSession;
        UMarketSCImplService UMSObj = new UMarketSCImplService();
        private static TransferProcess transObj = new TransferProcess();

        #region private string createsession()
        private string createsession()
        {
            string strReturnValue = string.Empty;

            try
            {
                mobiEz.createsession createsessionObj = new createsession();

                //invoke createsession function
                mobiEz.createsessionResponse createsessionResponseObj = UMSObj.createsession(createsessionObj);
                if (createsessionResponseObj.createsessionReturn.result == 0)
                    strReturnValue = createsessionResponseObj.createsessionReturn.sessionid;
                else
                    strReturnValue = "";
            }
            catch(Exception ex)
            {                
                throw (ex);      
            }
            return strReturnValue;
        }
        #endregion

        #region private bool login(string sessionid, string initiator, string pin)

        private bool login(string sessionid, string initiator, string pin)
        {
            Security secObj = new Security();
            mobiEz.loginResponse loginResponseObj = new loginResponse();           
            
            bool blnRetValue = false;            
           
            if (sessionid.Length != 20)
                blnRetValue = false;
            else
            {               
                try
                {
                    mobiEz.login loginObj = new login();
                    mobiEz.loginRequestType loginRequestTypeObj = new loginRequestType();

                    loginRequestTypeObj.sessionid = sessionid;
                    loginRequestTypeObj.initiator = initiator;
                    loginRequestTypeObj.pin = pin;
                    loginObj.loginRequest = loginRequestTypeObj;                   

                    //invoke login function
                    loginResponseObj = UMSObj.login(loginObj);


                    if (loginResponseObj.loginReturn.result == 0)
                    {
                        blnRetValue = true;
                        transObj.WriteLog("Login Mobifone successfull, sessionid=" + sessionid);
                    }
                    else
                    {
                        blnRetValue = false;
                        transObj.WriteLog("Login Mobifone fail, result=" + loginResponseObj.loginReturn.result.ToString() + ", sessionid=" + sessionid);
                    }
                }
                catch(Exception ex)
                {
                    transObj.WriteLog("Login Mobifone fail, Exception=" + ex.ToString());
                    throw (ex);                    
                }               
            }
            
            return blnRetValue;
        }

        #endregion

        #region private bool pin(string sessionid, string initiator, string pin, string newpin)
        private bool pin(string sessionid, string initiator, string pin, string new_pin)
        {
            mobiEz.pin pinObj = new pin();
            try
            {
                mobiEz.pinRequestType pinRequestTypeObj = new pinRequestType();
                pinRequestTypeObj.sessionid = sessionid;
                pinRequestTypeObj.initiator = initiator;
                pinRequestTypeObj.pin = pin;
                pinRequestTypeObj.new_pin = new_pin;

                pinObj.pinRequest = pinRequestTypeObj;
                mobiEz.pinResponse pinResponseObj = UMSObj.pin(pinObj);

                if (pinResponseObj.pinReturn.result == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }            
        }
        #endregion

        #region private decimal balance(string sessionid)
        private decimal balance(string sessionid)
        {
            mobiEz.balanceRequestType balanceRequestTypeObj = new balanceRequestType();
            mobiEz.balanceResponse balanceResponseObj = new balanceResponse();
            mobiEz.balance balanceObj = new balance();

            balanceRequestTypeObj.sessionid = sessionid;            
            balanceRequestTypeObj.type = 1;

            balanceObj.balanceRequest = balanceRequestTypeObj;
            try
            {
                //invoke balance function
                balanceResponseObj = UMSObj.balance(balanceObj);

                if (balanceResponseObj.balanceReturn.result == 0)
                    return balanceResponseObj.balanceReturn.avail_1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region public EnquiryOutput load(string Recipient, Int64 Amount, Int32 Counter)

        public mobiEz.buyResponse load(string Recipient, decimal Amount)
        {
            Security secObj = new Security();
            mobiEz.buyResponse buyResponseObj = new buyResponse();
            string strPassword = SrvProviderSessionManager.GetSessionInstance().GetSession("Mobifone");
            
            //Gen password
            strPassword = AppConfiguration.MobifoneUserName.ToLower() + AppConfiguration.MobifonePassword;
            strPassword = secObj.GetSHA1_HEX(strPassword).ToLower();
            strPassword = sessionid + strPassword;
            strPassword = secObj.GetSHA1_HEX(strPassword).ToUpper();

            //Console.Out.Write("pin=" + pin(sessionid, AppConfiguration.MobifoneUserName, strPassword, "0123456").ToString()); 

            try
            {
                int nCounter = 2;
                while (nCounter > 0)
                {  
                    //login
                    if (login(sessionid, AppConfiguration.MobifoneUserName, strPassword))
                    {
                        try
                        {
                            mobiEz.buy buyObj = new buy();
                            mobiEz.buyRequestType buyRequestTypeObj = new buyRequestType();

                            buyRequestTypeObj.sessionid = sessionid;
                            buyRequestTypeObj.target = "airtime";
                            buyRequestTypeObj.type = 2;
                            buyRequestTypeObj.recipient = Recipient;
                            buyRequestTypeObj.amount = Amount;
                            buyObj.buyRequest = buyRequestTypeObj;

                            //invoke buy function
                            buyResponseObj = UMSObj.buy(buyObj);
                            if (buyResponseObj.buyReturn.result == 0)
                            {
                                nCounter = 0;
                                break;
                            }

                        }
                        catch (Exception ex)
                        {
                            throw (ex);
                        }
                    }
                    else
                    {
                        nCounter--;

                        try
                        {
                            sessionid = createsession();
                            if(SrvProviderSessionManager.GetSessionInstance().IsContainSesssion("MobifoneSession"))
                                SrvProviderSessionManager.GetSessionInstance().DelSession("MobifoneSession");
                            SrvProviderSessionManager.GetSessionInstance().AddSession(sessionid, "MobifoneSession");
                        }
                        catch (Exception ex)
                        {
                            throw (ex);
                        }
                    }

                }
                            
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return buyResponseObj;
        }

        #endregion
        
    }

    #endregion

}
