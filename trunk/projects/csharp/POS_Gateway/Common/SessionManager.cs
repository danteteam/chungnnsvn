using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EPAY_POS_GateWay.Common
{
    public class SessionManager
    {
        private static SessionManager _instance;

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
                return _Request.ContainsKey(RequestID);
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

    public class ServiceSessionManager : SessionManager
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

    public class ServiceRequestManager : RequestManager
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

    public class SrvProviderSessionManager : ServiceProviderSessionManager
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
