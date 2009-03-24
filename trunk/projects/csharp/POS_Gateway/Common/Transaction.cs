using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;

using EPAY_POS_GateWay.Common;
using EPAY_POS_GateWay.Configuration;
using Solab.Iso8583;
using Solab.Iso8583.Parsing;

namespace EPAY_POS_GateWay.Common
{
    class Transaction
    {
        #region public void WriteLog(string strMessage)
        public void WriteLog(string strMessage)
        {
            Console.Out.WriteLine(strMessage);

            string strLogPath = AppConfiguration.App_Path + @"\Log\";
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            string strDay = DateTime.Now.Day.ToString();
            string strFileName = "Log_" + DateTime.Now.ToString("HH") + "h.log"; ;

            System.IO.DirectoryInfo d1 = new System.IO.DirectoryInfo(strLogPath);
            if (!d1.Exists)
                d1.Create();
            strLogPath += @"\" + strYear;

            System.IO.DirectoryInfo d2 = new System.IO.DirectoryInfo(strLogPath);
            if (!d2.Exists)
                d2.Create();
            strLogPath += @"\" + strMonth;

            System.IO.DirectoryInfo d3 = new System.IO.DirectoryInfo(strLogPath);
            if (!d3.Exists)
                d3.Create();
            strLogPath += @"\" + strDay;

            System.IO.DirectoryInfo d4 = new System.IO.DirectoryInfo(strLogPath);
            if (!d4.Exists)
                d4.Create();

            strLogPath = strLogPath + @"\" + strFileName;
            //strLogPath = "E:\\" + strFileName;

            System.IO.FileStream fs = new System.IO.FileStream(strLogPath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            System.IO.StreamWriter m_streamWriter = new System.IO.StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
            m_streamWriter.WriteLine(DateTime.Now.ToString("hh:mm:ss:ms:tt") + " " + strMessage);
            m_streamWriter.Flush();
            m_streamWriter.Close();

            EventLog evenlogObj = new EventLog();
            evenlogObj.Source = "EPAY_POS_GateWay";
            evenlogObj.WriteEntry(strMessage);
        }
        #endregion

        #region public void WriteLog_Load(...
        public void WriteLog_Load(
              string USERNAME, string SESSION_ID, string REQUEST_ID, string MERCHANT_ID
            , string IPADDRESS, string STATUS, string TRANS_ID, string MESSAGE_TYPE_ID
            , string MSISDN, double TRANSACTION_AMOUNT, string CLIENT_ID, string SYSTEM_TRACE_AUDIT_NUMBER
            , string REQUEST_TRANSMISSION_TIME, string RESPONSE_TRANSMISSION_TIME, string RESPONSE_CODE, string NOTE
            , string SERVICE_PROVIDER_CODE
            )
        {
            #region log into text file

            if (AppConfiguration.TransactionLogType == "1" || AppConfiguration.TransactionLogType == "3")
            {
                string strLogPath = AppConfiguration.App_Path + "\\Log";
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

            if (AppConfiguration.TransactionLogType == "2" || AppConfiguration.TransactionLogType == "3")
            {
                App_Lib DbObj = new App_Lib();
                string strSql = "";
                strSql = "insert into Transactions(";
                strSql += "  USERNAME, SESSION_ID, REQUEST_ID, MERCHANT_ID";
                strSql += ", IPADDRESS, STATUS, TRANS_ID, MESSAGE_TYPE_ID";
                strSql += ", MSISDN, TRANSACTION_AMOUNT, CLIENT_ID, SYSTEM_TRACE_AUDIT_NUMBER";
                strSql += ", REQUEST_TRANSMISSION_TIME, RESPONSE_TRANSMISSION_TIME, RESPONSE_CODE, NOTE";
                strSql += ", SERVICE_PROVIDER_CODE";
                strSql += " ) ";
                //, Status, ActionDate)";
                strSql += "values(";
                strSql += "'" + USERNAME + "', '" + SESSION_ID + "', '" + REQUEST_ID + "', '" + MERCHANT_ID + "'";
                strSql += " ,'" + IPADDRESS + "', '" + STATUS + "', '" + TRANS_ID + "', '" + MESSAGE_TYPE_ID + "'";
                strSql += " ,'" + MSISDN + "', '" + TRANSACTION_AMOUNT + "', '" + CLIENT_ID + "', '" + SYSTEM_TRACE_AUDIT_NUMBER + "'";
                strSql += " ,TO_DATE('" + REQUEST_TRANSMISSION_TIME + "','mm/dd/yyyy hh:mi:ss PM'), TO_DATE('" + RESPONSE_TRANSMISSION_TIME + "','mm/dd/yyyy hh:mi:ss PM'), '" + RESPONSE_CODE + "', '" + NOTE + "'";
                strSql += " ,'" + SERVICE_PROVIDER_CODE + "'";
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

        public void PrintMessage(IsoMessage m)
        {
            Transaction transObj = new Transaction();
            if (m == null)
            {
                transObj.WriteLog("Mensaje nulo!");
                return;
            }
            transObj.WriteLog("TYPE: " + m.Type.ToString("x"));
            for (int i = 2; i < 128; i++)
            {
                if (m.HasField(i))
                {
                    transObj.WriteLog("F " + i.ToString() + ": " + m.GetObjectValue(i) + " -> '" + m.GetField(i).ToString() + "'");
                }
            }
        }
    }
}
