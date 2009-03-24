using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EPaySoap;

/// <summary>
/// Summary description for ServiceSessionManager
/// </summary>
namespace EPaySoap
{
    public class ServiceSessionManager: EPaySoap.SessionManager
    {
   
        private static ServiceSessionManager _instance;

        public ServiceSessionManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static ServiceSessionManager GetSessionInstance()
        {
            if (_instance == null)
                _instance = new ServiceSessionManager();
            return _instance;
        }

        /// <summary>
        /// delete merchant's session
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>        
        public bool DelSession(string username)
        {
            try
            {
                foreach (SessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username)
                    {
                        _sessions.Remove(obj.Session);
                        break;
                    }
                }
                return true;
            }
            catch
            {                
                return false;
            }
        }

        /// <summary>
        /// get token of which channel is signed in
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetSession(string username)
        {
            try
            {
                foreach (SessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username)
                    {
                        return obj.Session;
                    }
                }
                return "";
            }
            catch
            {                
                return "";
            }
        }
        
        /// <summary>
        /// return bool value whether username 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsExistedSession(string username)
        {
            try
            {
                foreach (SessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username)
                    {                        
                        return true;                       
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsExistedSession(string username, string sessionid)
        {
            try
            {
                foreach (SessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username && obj.Session == sessionid)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsExistedUserNameSession(string username, string sessionid)
        {
            try
            {
                foreach (SessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username && obj.Session == sessionid)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class ServiceRequestManager : EPaySoap.RequestManager
    {
        private static ServiceRequestManager _instance;

        public static ServiceRequestManager GetSessionInstance()
        {
            if (_instance == null)
                _instance = new ServiceRequestManager();
            return _instance;
        }

        public ServiceRequestManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }       

        public bool IsExistedRequest(string RequestID, string UserName)
        {
            try
            {
                foreach (RequestObject obj in _Request.Values)
                {
                    if (obj.RequestID == RequestID && obj.UserName == UserName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DelRequest(string RequestID, string UserName)
        {
            try
            {
                foreach (RequestObject obj in _Request.Values)
                {
                    if (obj.RequestID == RequestID && obj.UserName == UserName)
                    {
                        _Request.Remove(obj.RequestID);
                        break;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    public class SrvProviderSessionManager : EPaySoap.ServiceProviderSessionManager
    {

        private static SrvProviderSessionManager _instance;

        public SrvProviderSessionManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static SrvProviderSessionManager GetSessionInstance()
        {
            if (_instance == null)
                _instance = new SrvProviderSessionManager();
            return _instance;
        }
                
        public bool DelSession(string username)
        {
            try
            {
                foreach (ServiceProviderSessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username)
                    {
                        _sessions.Remove(obj.Session);
                        break;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
      
        public string GetSession(string username)
        {
            try
            {
                foreach (ServiceProviderSessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username)
                    {
                        return obj.Session;
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
      
        public bool IsExistedSession(string username)
        {
            try
            {
                foreach (ServiceProviderSessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsExistedSession(string username, string sessionid)
        {
            try
            {
                foreach (ServiceProviderSessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username && obj.Session == sessionid)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool IsExistedUserNameSession(string username, string sessionid)
        {
            try
            {
                foreach (ServiceProviderSessionObject obj in _sessions.Values)
                {
                    if (obj.Username == username && obj.Session == sessionid)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
   
}