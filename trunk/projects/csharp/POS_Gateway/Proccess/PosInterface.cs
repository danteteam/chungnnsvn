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
            catch
            {
                mfact.Setfield(39, "01", ref response);
            }    

            //Create a response            
            ///*            
            //mfact.Setfield(2, request.GetField(2), ref response);
            mfact.Setfield(7, DateTime.Now.ToString("ddMMhhmmss"), ref response);            
            //mfact.Setfield(18, request.GetField(18), ref response);
            //mfact.Setfield(32, request.GetField(32), ref response);            
            //mfact.Setfield(48, request.GetField(48), ref response);
            //mfact.Setfield(52, request.GetField(52), ref response);
            //mfact.Setfield(70, request.GetField(70), ref response);
            //*/

            return response;
            
            //<parse type="0800"> 
            //<field num="2" type="LLVAR" length="0" />    	    <!--mã đại lý -->   
            //<field num="7" type="NUMERIC" length="10"/>     <!--Transaction Date and Time -->
            //<field num="11" type="NUMERIC" length="6" />   <!--System Trace-->
            //<field num="18" type="NUMERIC" length="4" />    <!--Merchant Type: Giá trị là 6011 đối với POS-->
            //<field num="32" type="LLVAR" length="0" />      <!--Acquiring Institution Identification Code-->
            //<field num="48" type="LLVAR" length="0" />    <!--nội dung hướng dẫn -->
            //<field num="52" type="LLLVAR" length="0" />     <!--Đưa mã máy POS vào-->
            //<field num="70" type="LLLVAR" length="0" />    <!--kiểu network request cần xử lý-	001: Signon-	002: Signoff-	161: Key Exchange--> 
        }
        #endregion

        #region public IsoMessage Download(IsoMessage request)
        public IsoMessage Download(IsoMessage request)
        {
            topupObj = new TopupInterface();
            bool blnDownloadTemplate = !request.HasField(48);

            //Download template
            if (blnDownloadTemplate)
            {

            }
            //Download single
            else
            {

            }

            mfact = new MessageFactory();
            mfact = ConfigParser.CreateFromFile(AppConfiguration.App_Path + AppConfiguration.POSConfig_ISOFile);
            IsoMessage response = mfact.CreateResponse(request);
            return response;
        }
        #endregion


    }
}
