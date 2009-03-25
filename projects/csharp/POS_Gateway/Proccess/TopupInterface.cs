using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using EPAY_POS_GateWay.Common;
using EPAY_POS_GateWay.Configuration;
using EPAY_POS_GateWay.TopupInterface;

///------------------------------
/// Summary
///Proccess invoke TopupInterface
/// ChungNN - 03/2009
///------------------------------
namespace EPAY_POS_GateWay.Proccess
{
    class TopupInterface
    {
        Service srvObj = new Service();

        #region private string SignIn()
        /// <summary>
        /// SignIn TopupInteface
        /// ChungNN 03/2009
        /// </summary>
        /// <returns>token</returns>
        private string SignIn()
        {
            string blnRetvalue = "";
            try
            {
                SignInResult sginObj = srvObj.GetToken(AppConfiguration.TopupInterfaceUserName, AppConfiguration.TopupInterfacePassword);             
                if (sginObj.ErrorCode != 0)
                {
                    sginObj = srvObj.SignIn(AppConfiguration.TopupInterfaceUserName, AppConfiguration.TopupInterfacePassword);
                    if (sginObj.ErrorCode == 0)
                        blnRetvalue = sginObj.Token;
                }
                else
                    blnRetvalue = sginObj.Token;                    
            }
            catch(Exception ex)
            {
                throw (ex); 
            }

            if (blnRetvalue.Length > 0)
                ServiceSessionManager.GetSessionInstance().AddSession(blnRetvalue, "TopupInterfaceToken");

            return blnRetvalue;
        }
        #endregion

        #region public bool PosLogon(int Pos_ID, ref string Request, string PosAdminPassword)
        /// <summary>
        /// POS Logon gateway
        /// ChungNN 03/2009
        /// </summary>
        /// <param name="Pos_ID"></param>
        /// <param name="Request"></param>
        /// <param name="PosAdminPassword"></param>
        /// <returns></returns>
        public bool PosLogon(int Pos_ID, ref string Request, string PosAdminPassword)
        {
            bool blnRetvalue = false;
            string strTopupInterfaceToken = string.Empty;

            try
            {
                //get TopupInterface token
                if (ServiceSessionManager.GetSessionInstance().IsExistedSession("TopupInterfaceToken"))
                    strTopupInterfaceToken = ServiceSessionManager.GetSessionInstance().GetSession("TopupInterfaceToken");
                else
                {
                    //new Thread(new ThreadStart(SignIn)).Start();

                    strTopupInterfaceToken = SignIn();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            //Logon as POS 
            int nCounter = 2;
            while (nCounter > 0)
            {
                try
                {
                    ErrorResult ErrObj = srvObj.PosLogon(Pos_ID, PosAdminPassword, strTopupInterfaceToken);
                    if (ErrObj.ErrorCode == 0)
                    {
                        //Save POS_ID session to server
                        if (!ServiceSessionManager.GetSessionInstance().IsExistedSession(Pos_ID.ToString()))
                            ServiceSessionManager.GetSessionInstance().AddSession(Request, Pos_ID.ToString());
                        else
                            Request = ServiceSessionManager.GetSessionInstance().GetSession(Pos_ID.ToString());

                        nCounter = 0;
                        blnRetvalue = true;
                        break;
                    }
                    //token is not existed on TopupInerface ->invok SignIn() again
                    else //if (ErrObj.ErrorCode == -1)
                    {
                        strTopupInterfaceToken = SignIn();
                        nCounter--;
                    }
                }
                catch(Exception ex)
                {
                    throw (ex);
                }
            }
            return blnRetvalue;
        }
        #endregion

        #region public bool PosLogout(int Pos_ID, ref string Request, string PosAdminPassword)
        /// <summary>
        /// POS Logout gateway
        /// ChungNN 03/2009
        /// </summary>
        /// <param name="Pos_ID"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public bool PosLogout(int Pos_ID, string Request)
        {
            bool blnRetValue = false;
            try
            {
                if (!ServiceSessionManager.GetSessionInstance().IsExistedSession(Pos_ID.ToString(), Request))
                {
                    ServiceSessionManager.GetSessionInstance().DelSession(Pos_ID.ToString());
                    blnRetValue = true;
                }
                else
                    blnRetValue = false;
            }
            catch //(Exception ex)
            {
                blnRetValue = false;
            }

            return blnRetValue;
        }
        #endregion

        #region public BatchBuyObject PosDownloadSingleSoftpin(int Pos_ID, int Merchant_ID, string RequestID, string CategoryName, string ServiceProviderName, int ProductValue, int StockQuantity, int DownloadQuantity)
        /// <summary>
        /// POS download some softpins
        /// ChungNN 03/2009
        /// </summary>
        /// <param name="Pos_ID"></param>
        /// <param name="Merchant_ID"></param>
        /// <param name="RequestID"></param>
        /// <param name="CategoryName"></param>
        /// <param name="ServiceProviderName"></param>
        /// <param name="ProductValue"></param>
        /// <param name="StockQuantity"></param>
        /// <param name="DownloadQuantity"></param>
        /// <returns></returns>
        public BatchBuyObject PosDownloadSingleSoftpin(int Pos_ID, int Merchant_ID, string RequestID, string CategoryName, string ServiceProviderName, int ProductValue, int StockQuantity, int DownloadQuantity)
        {
            BatchBuyObject buyObj = new BatchBuyObject();
            string strTopupInterfaceToken = ServiceSessionManager.GetSessionInstance().GetSession("TopupInterfaceToken");

            //string RequestID = Guid.NewGuid().ToString().Substring(0, 19);
            buyObj = srvObj.PosDownloadSingleSoftpin(Pos_ID, Merchant_ID, RequestID, CategoryName, ServiceProviderName, ProductValue, StockQuantity, DownloadQuantity, strTopupInterfaceToken);
            Thread.Sleep(AppConfiguration.TopupInterface_TimeOut);
            //buyObj = srvObj.PosDownloadSingleSoftpin(Pos_ID, Merchant_ID, RequestID, CategoryName, ServiceProviderName, ProductValue, StockQuantity, strTopupInterfaceToken);
            
            return buyObj;
            //transObj.WriteLog("PosDownloadSingleSoftpin, Errorcode = " + buyObj.ErrorCode.ToString());
        }
        #endregion

        #region public BatchBuyObject PosDownloadSoftpinTemplate(int Pos_ID, int Merchant_ID, string RequestID, object[] SoftpinStock)
        /// <summary>
        /// POS download softpin in template
        /// ChungNN 03/2009
        /// </summary>
        /// <param name="Pos_ID"></param>
        /// <param name="Merchant_ID"></param>
        /// <param name="RequestID"></param>
        /// <param name="SoftpinStock"></param>
        /// <returns></returns>
        public BatchBuyObject PosDownloadSoftpinTemplate(int Pos_ID, int Merchant_ID, string RequestID, object[] SoftpinStock)
        {
            BatchBuyObject buyObj = new BatchBuyObject();
            string strTopupInterfaceToken = ServiceSessionManager.GetSessionInstance().GetSession("TopupInterfaceToken");

            buyObj = srvObj.PosDownloadSoftpinTemplate(Pos_ID, Merchant_ID, RequestID, SoftpinStock, strTopupInterfaceToken);
            Thread.Sleep(AppConfiguration.TopupInterface_TimeOut);

            return buyObj;
            //transObj.WriteLog("PosDownloadSingleSoftpin, Errorcode = " + buyObj.ErrorCode.ToString());
        }
        #endregion

        public void testTopupInterface()
        {
            /*
            Service srvObj = new Service();
            Transaction transObj = new Transaction();

            int Pos_ID = 6;
            int Merchant_ID = 28;
            string RequestID;
            string Merchant_UserName = "camnhung";
            BatchBuyObject buyObj = new BatchBuyObject();

                         
            #region test signin
            SignInResult sginObj = new SignInResult();
            sginObj = srvObj.GetToken("postopup", "123456");
            if (sginObj.ErrorCode != 0)
            {
                sginObj = srvObj.SignIn("postopup", "123456");
            }
            //transObj.WriteLog("SignIn, Errorcode = " + sginObj.ErrorCode + ",token=" + sginObj.Token);
            #endregion

            #region test PosLogon
            ErrorResult ErrObj = srvObj.PosLogon(6, "12345678", sginObj.Token);
            //transObj.WriteLog("PosLogon, Errorcode = " + ErrObj.ErrorCode);
            #endregion
            
            
            #region PosDownloadSoftpinTemplate
            buyObj = new BatchBuyObject();            
            RequestID = Guid.NewGuid().ToString().Substring(0, 19);
            buyObj = srvObj.PosDownloadSingleSoftpin(Pos_ID, Merchant_ID, RequestID, "Thẻ ĐTDĐ", "Vinaphone", 10000, 99, 5, sginObj.Token);
            //transObj.WriteLog("PosDownloadSingleSoftpin, Errorcode = " + buyObj.ErrorCode.ToString());
            #endregion

            #region PosDownloadSoftpinTemplate
            object[] SoftpinStock = new object[2];
            RequestID = Guid.NewGuid().ToString().Substring(0, 19);

            StockObject stockObj = new StockObject();
            stockObj.ProductValue = 10000;
            stockObj.CategoryName = "Thẻ ĐTDĐ";
            stockObj.ServiceProviderName = "Vinaphone";
            stockObj.StockQuantity = 99;
            SoftpinStock[0] = stockObj;

            stockObj = new StockObject();
            stockObj.ProductValue = 10000;
            stockObj.CategoryName = "Thẻ ĐTDĐ";
            stockObj.ServiceProviderName = "Viettel";
            stockObj.StockQuantity = 99;
            SoftpinStock[1] = stockObj;

            buyObj = srvObj.PosDownloadSoftpinTemplate(Pos_ID, Merchant_ID, RequestID, SoftpinStock, sginObj.Token);
            
            transObj.WriteLog("PosDownloadSoftpinTemplate, Errorcode = " + buyObj.ErrorCode.ToString());
            #endregion

            #region PosRevertDownload
            //ErrObj = srvObj.PosRevertDownload(RequestID, Pos_ID, sginObj.Token);
            //transObj.WriteLog("PosRevertDownload, Errorcode = " + ErrObj.ErrorCode.ToString());
            #endregion

            #region PosRecountTrans
            SettleResult SettleResulttObj = new SettleResult();
            object[] SoldSoftpin = new object[2];
            SoldObject SoldObj = new SoldObject();

            SoldObj.SoftpinId = 458456;
            SoldObj.CashierId = 0;
            SoldObj.SoldDatetime = DateTime.Now;
            SoldSoftpin[0] = SoldObj;

            SoldObj.SoftpinId = 326660;
            SoldObj.CashierId = 0;
            SoldObj.SoldDatetime = DateTime.Now;
            SoldSoftpin[1] = SoldObj;

            SettleResulttObj = srvObj.PosRecountTrans(Pos_ID, SoldSoftpin, sginObj.Token);
            transObj.WriteLog("PosRecountTrans, Errorcode = " + SettleResulttObj.ErrorCode.ToString());
            #endregion

            #region PosSynchronized
            SynchronizeResult SynchronizeResultObj = new SynchronizeResult();
            object[] UpdateList = new object[1];

            UpdateObject UpdateObjectObj = new UpdateObject();
            UpdateObjectObj.ItemType = 1;
            UpdateObjectObj.LatestDateTime = DateTime.Now.AddMonths(-10);
            UpdateList[0] = UpdateObjectObj;

            SynchronizeResultObj = srvObj.PosSynchronized(Merchant_UserName, "12345678", UpdateList, sginObj.Token);
            transObj.WriteLog("PosSynchronized, Errorcode = " + SynchronizeResultObj.ErrorCode.ToString());
            #endregion

            #region PosSynchronizedLogo
            SynchronizedLogoResult SynchronizedLogoResultObj = new SynchronizedLogoResult();
            SynchronizedLogoResultObj = srvObj.PosSynchronizedLogo(Pos_ID, DateTime.Now.AddMonths(-10), sginObj.Token);
            transObj.WriteLog("PosSynchronizedLogo, Errorcode = " + SynchronizedLogoResultObj.ErrorCode.ToString());
            #endregion

            #region PosSynchronizedMenu
            SynchronizeMenuResult SynchronizeMenuResultObj = new SynchronizeMenuResult();
            SynchronizeMenuResultObj = srvObj.PosSynchronizedMenu(Pos_ID, DateTime.Now.AddMonths(-10), sginObj.Token);
            transObj.WriteLog("PosSynchronizedMenu, Errorcode = " + SynchronizeMenuResultObj.ErrorCode.ToString());
            #endregion

            #region PosSynchronizedSoftpinKey
            SynchronizedKeyResult SynchronizedKeyResultObj = new SynchronizedKeyResult();
            SynchronizedKeyResultObj = srvObj.PosSynchronizedSoftpinKey(Pos_ID, sginObj.Token);
            transObj.WriteLog("PosSynchronizedSoftpinKey, Errorcode = " + SynchronizedKeyResultObj.ErrorCode.ToString());
            #endregion
             
            */
        }
    }
}
