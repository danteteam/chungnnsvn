using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Summary description for SessionObject
/// </summary>
namespace EPaySoap
{
    public class SessionManager
    {
        private static SessionManager _instance;

        /// <summary>
        /// SonNX changed private type to protected type
        /// </summary>
        protected Hashtable _sessions;       

        /// <summary>
        /// private constructor to public constructor
        /// </summary>
        public SessionManager()
        {
            _sessions = new Hashtable();            
        }

        public static SessionManager GetInstance()
        {
            if (_instance == null)
                _instance = new SessionManager();
            return _instance;
        }

        public bool IsContainSesssion(string session)
        {
            if (_sessions == null)
                return false;
            else
                return _sessions.ContainsKey(session);
        }      

        public void AddSession(string session, string username)
        {
            if (!IsContainSesssion(session))
            {
                _sessions.Add(session, new SessionObject(session, username, DateTime.Now.AddDays(1)));
            }

            bool isChange = true;

            while (isChange)
            {
                isChange = false;
                // remove expired session
                foreach (SessionObject obj in _sessions.Values)
                {
                    if (DateTime.Now.CompareTo(obj.ExpiredDate) > 0)
                    {
                        _sessions.Remove(obj.Session);
                        isChange = true;
                        break;
                    }
                }
            }
        }
       
        /// <summary>       
        public class SessionObject
        {
            public string Session;
            public string Username;
            public DateTime ExpiredDate;

            public SessionObject(string session, string username, DateTime expiredDate)
            {
                this.Session = session;
                this.Username = username;
                this.ExpiredDate = expiredDate;
            }
        }      
    }

    public class RequestManager
    {
        private static RequestManager _instance;
          
        protected Hashtable _Request;

        public RequestManager()
        {
            _Request = new Hashtable();           
        }

        public static RequestManager GetInstance()
        {
            if (_instance == null)
                _instance = new RequestManager();
            return _instance;
        }

        public bool IsContainRequest(string RequestID, string UserName)
        {
            if (_Request == null)
                return false;
            else
                return  _Request.ContainsKey(RequestID);
        }

        public void AddRequest(string RequestID, string UserName)
        {
            if (!IsContainRequest(RequestID, UserName))
            {
                _Request.Add(RequestID, new RequestObject(RequestID, UserName, DateTime.Now.AddDays(1)));
            }

            bool isChange = true;

            while (isChange)
            {
                isChange = false;
                // remove expired session
                foreach (RequestObject obj in _Request.Values)
                {
                    if (DateTime.Now.CompareTo(obj.expiredDate) > 0)
                    {
                        _Request.Remove(obj.RequestID);
                        isChange = true;
                        break;
                    }
                }
            }
        }
                   
        public class RequestObject
        {
            public string RequestID;
            public string UserName;
            public DateTime expiredDate;

            public RequestObject(string RequestID, string UserName, DateTime expiredDate)
            {
                this.RequestID = RequestID;
                this.UserName = UserName;
                this.expiredDate = expiredDate;
            }
        }
       
    }

    public class ServiceProviderSessionManager
    {
        private static ServiceProviderSessionManager _instance;

        /// <summary>
        /// SonNX changed private type to protected type
        /// </summary>
        protected Hashtable _sessions;

        /// <summary>
        /// private constructor to public constructor
        /// </summary>
        public ServiceProviderSessionManager()
        {
            _sessions = new Hashtable();
        }

        public static ServiceProviderSessionManager GetInstance()
        {
            if (_instance == null)
                _instance = new ServiceProviderSessionManager();
            return _instance;
        }

        public bool IsContainSesssion(string session)
        {
            if (_sessions == null)
                return false;
            else
                return _sessions.ContainsKey(session);
        }

        public void AddSession(string session, string username)
        {
            if (IsContainSesssion(session))            
                _sessions.Remove(session);                   
               
            _sessions.Add(session, new ServiceProviderSessionObject(session, username));            
            
        } 

        /// <summary>       
        public class ServiceProviderSessionObject
        {
            public string Session;
            public string Username;            

            public ServiceProviderSessionObject(string session, string username)
            {
                this.Session = session;
                this.Username = username;                
            }
        }
    }

}

