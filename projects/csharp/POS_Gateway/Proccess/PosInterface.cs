using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Solab.Iso8583;
using Solab.Iso8583.Parsing;
using EPAY_POS_GateWay.Common;
using EPAY_POS_GateWay.Configuration;
using EPAY_POS_GateWay.TopupInterface;
using EPAY_POS_GateWay.Proccess;

///------------------------------
/// Summary
///Proccess requests from POS
/// ChungNN - 03/2009
///------------------------------
namespace EPAY_POS_GateWay.Proccess
{
    class PosInterface
    {
        TopupInterface topupObj;
        private static MessageFactory mfact;
        Transaction transObj = new Transaction();

        #region public IsoMessage PosSignOnOff(IsoMessage request)
        /// <summary>
        /// SignOn/Off as POS
        /// ChungNN 03/2009
        /// </summary>
        /// <param name="request">IsoMessage</param>
        /// <returns>IsoMessage</returns>
        public IsoMessage PosSignOnOff(IsoMessage request)
        {
            topupObj = new TopupInterface();
            mfact = new MessageFactory();
            mfact = ConfigParser.CreateFromFile(AppConfiguration.App_Path + AppConfiguration.POSConfig_ISOFile);
            IsoMessage response = mfact.CreateResponse(request);

            string strRequestCode = request.GetField(70).Value.ToString();
            int nPos_ID = int.Parse(request.GetField(52).Value.ToString());
            string strPosAdminPassword = request.GetField(48).Value.ToString();
            string strRequest = request.GetField(11).Value.ToString();

            try
            {
                //Signon
                if (strRequestCode == "001")
                {     
                    if (topupObj.PosLogon(nPos_ID, ref strRequest, strPosAdminPassword))
                    {
                        mfact.Setfield(11, strRequest, ref response); 
                        mfact.Setfield(39, "00", ref response);
                        transObj.WriteLog("->POS signon(" + request.GetField(52).Value.ToString() + "|" + request.GetField(48).Value.ToString() + ") successfull");
                    }
                    else
                    {
                        mfact.Setfield(11, request.GetField(11), ref response);
                        mfact.Setfield(39, "01", ref response);
                        transObj.WriteLog("->POS signon(" + request.GetField(52).Value.ToString() + "|" + request.GetField(48).Value.ToString() + ") fail");
                    }
                }
                //Signoff
                else if (strRequestCode == "002")
                {
                    if (topupObj.PosLogout(nPos_ID, strRequest))
                    {
                        mfact.Setfield(11, request.GetField(11), ref response);
                        mfact.Setfield(39, "00", ref response);
                        transObj.WriteLog("->POS Signof successfull");
                    }
                    else
                    {
                        mfact.Setfield(11, request.GetField(11), ref response);
                        mfact.Setfield(39, "01", ref response);
                        transObj.WriteLog("->POS Signof fail");
                    }
                }
           
                //Key Exchange
                else if (strRequestCode == "161")
                {
                    transObj.WriteLog("->POS Key Exchange");
                }
            }
            catch(Exception ex)
            {
                transObj.WriteLog("->Exception=" + ex.Message);
                mfact.Setfield(39, "01", ref response);
            }  
                      
            mfact.Setfield(7, DateTime.Now.ToString("ddMMhhmmss"), ref response);            

            return response;
          
        }
        #endregion

        #region public IsoMessage Download(IsoMessage request)
        /// <summary>
        /// POS download softpin
        /// ChungNN 03/2009
        /// </summary>
        /// <param name="request">IsoMessage</param>
        /// <returns>IsoMessage</returns>
        public IsoMessage Download(IsoMessage request)
        {
            topupObj = new TopupInterface();
            
            //create response message
            mfact = new MessageFactory();
            mfact = ConfigParser.CreateFromFile(AppConfiguration.App_Path + AppConfiguration.POSConfig_ISOFile);
            IsoMessage response = mfact.CreateResponse(request);
            bool blnDownloadTemplate = !request.HasField(48);
            string strRequest = request.GetField(11).Value.ToString();
            int nMerchant_ID = int.Parse(request.GetField(2).Value.ToString());
            int nPos_ID = int.Parse(request.GetField(52).Value.ToString());
            
            //if exist session
            if (!Common.ServiceSessionManager.GetSessionInstance().IsExistedSession(nPos_ID.ToString(), strRequest))
            {
                mfact.Setfield(39, "01", ref response);
                transObj.WriteLog("->download fail, session not exist");
            }
            else
            {
                //Download template
                if (blnDownloadTemplate)
                {
                    try
                    {
                        //get request values
                        topupObj = new TopupInterface();
                        BatchBuyObject buyObj = new BatchBuyObject();
                     
                        //String[] arrRequestValues = request.GetField(48).Value.ToString().Split(AppConfiguration.POS_Seperator_Char);
                        //string strCategoryName = arrRequestValues[0];
                        //string strServiceProviderName = arrRequestValues[1];
                        //int nProductValue = int.Parse(arrRequestValues[2]);
                        //int nStockQuantity = int.Parse(arrRequestValues[3]);
                        //int nDownloadQuantity = int.Parse(arrRequestValues[4]);

                        object[] SoftpinStock = new object[1];

                        StockObject stockObj = new StockObject();
                        stockObj.ProductValue = 10000;
                        stockObj.CategoryName = "Thẻ ĐTDĐ";
                        stockObj.ServiceProviderName = "Vinaphone";
                        stockObj.StockQuantity = 0;
                        SoftpinStock[0] = stockObj;

                        buyObj = topupObj.PosDownloadSoftpinTemplate(nPos_ID, nMerchant_ID, strRequest, SoftpinStock);

                        if (buyObj.ErrorCode == 0)
                        {
                            mfact.Setfield(39, "00", ref response);
                            transObj.WriteLog("->download template successfull");
                        }
                        else
                        {
                            mfact.Setfield(39, "01", ref response);
                            transObj.WriteLog("->download template fail");
                        }
                    }
                    catch (Exception ex)
                    {
                        transObj.WriteLog("->download template execption =" + ex.ToString());
                        throw (ex);
                    }

                }
                //Download single
                else
                {
                    try
                    {
                        //get request values
                        topupObj = new TopupInterface();
                        BatchBuyObject buyObj = new BatchBuyObject();

                        String[] arrRequestValues = request.GetField(48).Value.ToString().Split(AppConfiguration.POS_Seperator_Char);
                        string strCategoryName = arrRequestValues[0];
                        string strServiceProviderName = arrRequestValues[1];
                        int nProductValue = int.Parse(arrRequestValues[2]);
                        int nStockQuantity = int.Parse(arrRequestValues[3]);
                        int nDownloadQuantity = int.Parse(arrRequestValues[4]);

                        buyObj = topupObj.PosDownloadSingleSoftpin(nPos_ID, nMerchant_ID, strRequest, strCategoryName, strServiceProviderName, nProductValue, nStockQuantity, nDownloadQuantity);

                        if (buyObj.ErrorCode == 0)
                        {
                            mfact.Setfield(39, "00", ref response);
                            transObj.WriteLog("->download single successfull");
                        }
                        else
                        {
                            mfact.Setfield(39, "01", ref response);
                            transObj.WriteLog("->download single fail");
                        }

                        //create response message
                        mfact = new MessageFactory();
                        mfact = ConfigParser.CreateFromFile(AppConfiguration.App_Path + AppConfiguration.POSConfig_ISOFile);
                        response = mfact.CreateResponse(request);
                    }
                    catch (Exception ex)
                    {
                        transObj.WriteLog("->download single execption =" + ex.ToString());
                        throw (ex);
                    }
                }
            }            
            
            return response;
        }
        #endregion

    }
}
