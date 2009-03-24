using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Collections;
using EPaySoap;

///<Security management>
///ChungNN 08/2008
///
namespace EPaySoap
{
    public class Security
    {

        #region private static byte[] Epay_Encrypt_ByByte(byte[] clearData, byte[] Key, byte[] IV)
        //Ma hoa 1 mang theo 1 phuong thuc xac dinh
        private static byte[] Epay_Encrypt_ByByte(byte[] clearData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream to accept the encrypted bytes 
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;

            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            // Write the data and make it do the encryption 

            cs.Write(clearData, 0, clearData.Length);
            cs.Close();

            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }
        #endregion

        #region private static byte[] Epay_Decrypt_ByByte(byte[] cipherData, byte[] Key, byte[] IV)
        //Giai ma 1 mang theo 1 phuong thuc xac dinh
        private static byte[] Epay_Decrypt_ByByte(byte[] cipherData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the

            // decrypted bytes 

            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 

            // We are going to use Rijndael because it is strong and

            // available on all platforms. 

            // You can use other algorithms, to do so substitute the next

            // line with something like 

            //     TripleDES alg = TripleDES.Create(); 

            Rijndael alg = Rijndael.Create();

            // Now set the key and the IV. 

            // We need the IV (Initialization Vector) because the algorithm

            // is operating in its default 

            // mode called CBC (Cipher Block Chaining). The IV is XORed with

            // the first block (8 byte) 

            // of the data after it is decrypted, and then each decrypted

            // block is XORed with the previous 

            // cipher block. This is done to make encryption more secure. 

            // There is also a mode called ECB which does not need an IV,

            // but it is much less secure. 

            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be

            // pumping our data. 

            // CryptoStreamMode.Write means that we are going to be

            // writing data to the stream 

            // and the output will be written in the MemoryStream

            // we have provided. 

            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the decryption 

            cs.Write(cipherData, 0, cipherData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 

            // This will tell it that we have done our decryption

            // and there is no more data coming in, 

            // and it is now a good time to remove the padding

            // and finalize the decryption process. 

            cs.Close();

            // Now get the decrypted data from the MemoryStream. 

            // Some people make a mistake of using GetBuffer() here,

            // which is not the right way. 

            byte[] decryptedData = ms.ToArray();

            return decryptedData;
        }
        #endregion

        #region private static byte[] HexStringToByteArray(string Hex)
        private static byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x0B, 0x0C, 0x0D,
                                 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }
        #endregion

        #region private static string ByteArrayToHexString(byte[] Bytes)
        private static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder();
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }
        #endregion

        #region private byte[] FillBlock(byte[] dataTemp)
        private byte[] FillBlock(byte[] dataTemp)
        {
            int i = dataTemp.Length % 8;
            int length = (i == 0) ? (dataTemp.Length) : (dataTemp.Length - i + 8);
            byte[] data = new byte[length];
            if (i != 0)
            {
                for (int j = 0; j < length; j++)
                {
                    if (j < dataTemp.Length)
                    {
                        data[j] = dataTemp[j];
                    }
                    else
                    {
                        data[j] = (byte)0xff;
                    }
                }
            }
            else
            {
                data = dataTemp;
            }
            return data;
        }
        #endregion

        #region private byte[] DES_Encrypt(byte[] Keys, byte[] clearText)
        private byte[] DES_Encrypt(byte[] Keys, byte[] clearText)
        {
            try
            {              
                byte[] IVs = new byte[8];
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //des.IV = IVs;
                des.KeySize = 64; //24Bytes                
                des.Key = Keys;
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.None;
                clearText = FillBlock(clearText);
                byte[] cipherText = des.CreateEncryptor().TransformFinalBlock(clearText, 0, clearText.Length);
                return cipherText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region private byte[] TripleDES_Encrypt_Byte(byte[] Keys, byte[] clearText)
        private byte[] TripleDES_Encrypt_Byte(byte[] Keys, byte[] clearText)
        {
            try
            {
                byte[] IVs = new byte[8];
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.IV = IVs;
                des.KeySize = 192; //24Bytes                
                des.Key = Keys;
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.None;
                clearText = FillBlock(clearText);
                byte[] cipherText = des.CreateEncryptor().TransformFinalBlock(clearText, 0, clearText.Length);
                return cipherText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region public string TripleDES_Encrypt(string plainText, string Keys)
        public string TripleDES_Encrypt(string plainText, string Keys)
        {
            /*
                Sesionid duoc tra ve duoi dang 48 chu so hexa.
                Anh chuyen thanh 24 byte
                Dung TripleDES ma hoa voi key tren thanh 1 mang byte
                Convert mang byte sang chuoi hexa                
            */

            byte[] IVs = new byte[8];
            byte[] myKeys = new byte[24];
            //byte[] blockKey1 = new byte[8];
            //byte[] blockKey2 = new byte[8];
            //byte[] blockKey3 = new byte[8];

            //byte[] result1;
            //byte[] result2;
            byte[] result3;

            myKeys = HexStringToByteArray(Keys);
            byte[] clearText = ASCIIEncoding.ASCII.GetBytes(plainText);

            //Array.Copy(myKeys, 0, blockKey1, 0, 8);
            //Array.Copy(myKeys, 8, blockKey2, 0, 8);
            //Array.Copy(myKeys, 16, blockKey3, 0, 8);

            try
            {
                //result1 = DES_Encrypt(blockKey1, clearText);
                //result2 = DES_Encrypt(blockKey2, result1);
                //result3 = DES_Encrypt(blockKey3, result2);
                result3 = TripleDES_Encrypt_Byte(myKeys, clearText);

                return ByteArrayToHexString(result3);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion

        #region public string GetSHA1Hash(string plainText)
        public string GetSHA1Hash(string plainText)
        {
            string strResult = "";

           
            UTF8Encoding utf8encoder = new UTF8Encoding();
            byte[] arrbytHashValue = utf8encoder.GetBytes(plainText);
            
            SHA1Managed sha1Obj = new SHA1Managed();
            arrbytHashValue = sha1Obj.ComputeHash(arrbytHashValue);

            try
            {
                strResult = Convert.ToBase64String(arrbytHashValue);
            }
            catch
            {
                strResult = "";
            }

            return (strResult);

        }
        #endregion public string GetSHA1Hash(string plainText)

        #region public string GetSHA1_HEX(string plainText)
        public string GetSHA1_HEX(string plainText)
        {
            string strResult = "";

            UTF8Encoding utf8encoder = new UTF8Encoding();
            byte[] arrbytHashValue = utf8encoder.GetBytes(plainText);

            SHA1Managed sha1Obj = new SHA1Managed();
            arrbytHashValue = sha1Obj.ComputeHash(arrbytHashValue);

            try
            {
                strResult = ByteArrayToHexString(arrbytHashValue);
            }
            catch
            {
                strResult = "";
            }

            return (strResult);

        }
        #endregion public string GetSHA1Hash(string plainText)

        #region public string Epay_Encrypt(string clearText, string PasswordKey)
        //Ma hoa 1 chuoi 
        public string Epay_Encrypt(string clearText, string PasswordKey)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(PasswordKey,
                new byte[] { 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x20, 0x21, 0x22 });

            byte[] encryptedData = Epay_Encrypt_ByByte(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);

        }
        #endregion

        #region public string Epay_Decrypt(string cipherText, string PasswordKey)
        //Giai ma 1 chuoi
        public string Epay_Decrypt(string cipherText, string PasswordKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(PasswordKey,
                 new byte[] { 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x20, 0x21, 0x22 });

            byte[] decryptedData = Epay_Decrypt_ByByte(cipherBytes,
                pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }
        #endregion
        
        #region public bool Check_Valid_User(string UserName, string Password)
        //Check exist user in Users table
        //ChungNN 11/09/2008

        public bool Check_Valid_User(string UserName, string Password)
        {
            DataSet dsDataList = new DataSet();
            DataTable tblResult = new DataTable();
            string strPassword = Epay_Encrypt(Password, AppConfiguration.AppPasswordKey);
            bool blnRetValue = false;

            //dsDataList = Xml_2_DateSet(HttpContext.Current.Server.MapPath("") + "\\" + AppConfiguration.AppData_FileName);
            ListenRequest lstLib = new ListenRequest();
            //dsDataList = lstLib.dsDataList;
            tblResult = dsDataList.Tables["Users"];            

            if (tblResult.DefaultView.Count != 0)
            {
                for (int i = 0; i < tblResult.DefaultView.Count; i++)
                {
                    if (UserName == tblResult.DefaultView[i].Row["UserName"].ToString())
                        if (strPassword == tblResult.DefaultView[i].Row["Password"].ToString())
                        {
                            blnRetValue = true;
                            break;
                        }
                }
            }
            else
                blnRetValue = false;


            tblResult = null;
            dsDataList = null;

            return blnRetValue;
        }

        #endregion

        #region public bool Check_Valid_Session(string UserName, string Session_ID)
        //Check exist user in Users table
        //ChungNN 20/11/2008

        public bool Check_Valid_Session(string UserName, string Session_ID)
        {           
            bool blnRetValue = true;           

            return blnRetValue;
        }

        #endregion
         
        #region public bool Check_Valid_IPAddress(string IPAddress)
        //Check exist trans_id in IPAddress table
        //ChungNN 11/09/2008

        public bool Check_Valid_IPAddress(string IPAddress)
        {
            DataSet dsDataList = new DataSet();
            DataTable tblResult = new DataTable();
            bool blnRetValue = false;

            dsDataList = Xml_2_DateSet(HttpContext.Current.Server.MapPath("") + "\\" + AppConfiguration.AppData_FileName);
            //ListenRequest lstLib = new ListenRequest();
            //dsDataList = lstLib.dsDataList;
            tblResult = dsDataList.Tables["IPAddressList"];

            if(tblResult == null)
                blnRetValue = true;
            else if(tblResult.DefaultView.Count == 0)
                blnRetValue = true;
            else
            {                
                for (int i = 0; i < tblResult.DefaultView.Count; i++)
                {
                    if (IPAddress == tblResult.DefaultView[i].Row["IPAddress"].ToString())
                    {
                        blnRetValue = true;
                        break;
                    }
                }
            }

            tblResult = null;
            dsDataList = null;

            return blnRetValue;
        }

        #endregion

        #region public OutputInfo CheckValid(string UserName, string Session_ID, string Request_ID, string MerchantID, string targetMsIsdn, Int32 amount, ref string ProviderCode)
        //Input:    UserName, Session_ID         
        //ChungNN - 01/2009
        public OutputInfo CheckValid(string UserName, string Session_ID, string Request_ID, string MerchantID, string targetMsIsdn, Int32 amount, ref string ProviderCode)
        {
            OutputInfo outObj = new OutputInfo();
            TransferProcess proObj = new TransferProcess();
            HttpApplication appObj = new HttpApplication();  
            string strIPAddress = HttpContext.Current.Request.UserHostAddress;
            string strSession_ID = Session_ID;
            byte nErrorNum = 0;
            string strLogMessage = string.Empty;
            
            //0: Successful
            //1: Invalid userName or Session_ID
            //2: Invalid IP Address                      
            //3: Invalid targetMsIsdn or provider is not exist
            //4: Request_ID is exist            
            //5: Invalid amount
            

            DataSet dsDataList = new DataSet();
            DataTable tblResult = new DataTable();
      
            #region Check username and Session_ID.
            nErrorNum = 1;
            //check username and password is null?
            if (UserName != null && Session_ID != null)
            {
                //check if sessionid is existed
                if (ServiceSessionManager.GetSessionInstance().IsExistedSession(UserName, Session_ID))
                {
                    nErrorNum = 0;
                }               
            }  
            #endregion Check username and Session_ID
           
            #region Check IP address if login successfull

            if (nErrorNum == 0)
            {
                nErrorNum = (byte) ((Check_Valid_IPAddress(strIPAddress)? 0:2));
            }
            #endregion Check IP address if login successfull
                      
            #region Check valid targetMsIsdn
            if (nErrorNum == 0)
            {
                dsDataList = null;
                dsDataList = Xml_2_DateSet(HttpContext.Current.Server.MapPath("") + "\\" + AppConfiguration.AppData_FileName); 
                tblResult = dsDataList.Tables["ProviderList"];
                HttpContext hc = HttpContext.Current;

                if (targetMsIsdn.Length < 10 || targetMsIsdn.Length > 15)
                    nErrorNum = 3;
                //if no provider to check
                else if (tblResult.DefaultView.Count == 0)
                {
                    nErrorNum = 3;
                }
                else
                {
                    nErrorNum = 3;
                    string strPrefix = string.Empty;
                    for (int i = 0; i < tblResult.DefaultView.Count; i++)
                    {
                        strPrefix = tblResult.DefaultView[i].Row["Prefix"].ToString();
                        if (targetMsIsdn.IndexOf(strPrefix, 0, strPrefix.Length) != -1)
                        {
                            nErrorNum = 0;
                            ProviderCode = tblResult.DefaultView[i].Row["ProviderCode"].ToString().Trim();
                            break;
                        }
                    }
                }

                tblResult = null;
                dsDataList = null;
            }

            #endregion Check valid targetMsIsdn
            
            #region Check valid acmount

            if (nErrorNum == 0)
            {
                if (Array.IndexOf(AppConfiguration.AppAmountArray, amount) == -1)
                    nErrorNum = 5;
            }

            #endregion Check valid acmount

            #region Check valid Request_ID

            if (nErrorNum == 0)
            {
                if (UserName != null && Request_ID != null)
                {
                    //check if Request_ID is existed
                    if (ServiceRequestManager.GetInstance().IsContainRequest(Request_ID, UserName))
                    {
                        nErrorNum = 4;
                    }
                    else
                    {
                        ServiceRequestManager.GetInstance().AddRequest(Request_ID, UserName);
                        nErrorNum = 0;
                    }
                }
            }

            #endregion Check valid Trans_ID

            #region return and log

            if (nErrorNum == 0)
            {                
                outObj.status = "0";
                //outObj.message = "Login successfull.";
                //strLogMessage = "Login EPay sucessfull. (RequestID=" + Request_ID + ", UserName=" + UserName + ", IPAddress=" + strIPAddress + ")";                
            }
            else
            {
                outObj.status = nErrorNum.ToString();
                outObj.trans_id = "0";

                if (nErrorNum == 1)
                {
                    outObj.message = "Invalid user name or session_id.";
                    strLogMessage = "Load fail, invalid user name or session_id. (RequestID=" + Request_ID + ", UserName=" + UserName + ", IPAddress=" + strIPAddress + ")";
                }
                else if (nErrorNum == 2)
                {
                    outObj.message = "Invalid IP address.";
                    strLogMessage = "Load failure, invalid IP address. (RequestID=" + Request_ID + ", UserName=" + UserName + ", IPAddress=" + strIPAddress + ")";                    
                }
                else if (nErrorNum == 3)
                {
                    outObj.message = "Invalid phone number or service provider not exist.";
                    strLogMessage = "Load failure, invalid targetMsIsdn or provider is not exist. (RequestID=" + Request_ID + ", UserName=" + UserName + ", IPAddress=" + strIPAddress + ")";                    
                }
                else if (nErrorNum == 4)
                {
                    outObj.message = "Request_ID is exist.";
                    strLogMessage = "Load failure, request_id is exist. (RequestID=" + Request_ID + ", UserName=" + UserName + ", IPAddress=" + strIPAddress + ")";                    
                }
                else if (nErrorNum == 5)
                {
                    outObj.message = "Invalid amount." + amount.ToString();
                    strLogMessage = "Load failure, ivalid amount. (RequestID=" + Request_ID + ", UserName=" + UserName + ", IPAddress=" + strIPAddress + ")";
                }

                //write log if error
                proObj.WriteLog(strLogMessage);
            }
            

            #endregion return and log

            return outObj;
        }

        #endregion               

        #region private DataSet Xml_2_DateSet()
        public DataSet Xml_2_DateSet(string FileName)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(FileName);
            DataSet ds = new DataSet(); byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(doc.OuterXml);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
            ds.ReadXml(ms, XmlReadMode.InferSchema);
            ms.Close();

            return ds;
        }
        #endregion      
                
        #region private bool IsValidUser(string username, string password)
        public bool IsValidUser(string username, string password)
        {

            DataSet dsDataList = new DataSet();
            DataTable tblResult = new DataTable();

            dsDataList = Xml_2_DateSet(HttpContext.Current.Server.MapPath("") + "\\" + AppConfiguration.AppData_FileName);

            string strPassword = Epay_Encrypt(password, AppConfiguration.AppPasswordKey);
            bool blnRetValue = false;

            //ListenRequest lstLib = new ListenRequest();
            //dsDataList = lstLib.dsDataList;
            tblResult = dsDataList.Tables["Users"];

            if (tblResult.DefaultView.Count != 0)
            {
                for (int i = 0; i < tblResult.DefaultView.Count; i++)
                {
                    if (username == tblResult.DefaultView[i].Row["UserName"].ToString())
                        if (strPassword == tblResult.DefaultView[i].Row["Password"].ToString())
                        {
                            blnRetValue = true;
                            break;
                        }
                }
            }
            else
                blnRetValue = false;

            return blnRetValue;

        }
        #endregion       
               
        public static string AES_Encrypt(string PlainText, string Password, string Salt, int PasswordIterations, string InitialVector, int KeySize)
        {
            //http://www.gutgames.com/post/AES-Encryption-in-C.aspx
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, "AES", PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            SymmetricKey.Padding = PaddingMode.PKCS7;
            ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes);
            MemoryStream MemStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherTextBytes = MemStream.ToArray();
            MemStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(CipherTextBytes);
        }

        public static string AES_Decrypt(string CipherText, string Password, string Salt, int PasswordIterations, string InitialVector, int KeySize)
        {
            //http://www.gutgames.com/post/AES-Encryption-in-C.aspx
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, "AES", PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes);
            MemoryStream MemStream = new MemoryStream(CipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read);
            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount = cryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
            MemStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
        }       

    }

}
