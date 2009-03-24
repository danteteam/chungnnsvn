using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

///<Soap Output information class>
///ChungNN 08/2008
///

namespace EPaySoap
{ 
    public class OutputInfo
    {
        private string _status = "";
        /* 
            "0": login successfull.
            "1": that bai do nha cung cap tra ve
            "2": timeout, ko ket noi dc toi nha cung cap
            "3": invalid targetMsIsdn
            "4": Trans_ID exist (check in day only      
            "5": invalid amount 
         */
        private string _message = "";       //thong bao tra ve
        private string _trans_id;

        public string status
        {
            set { _status = value; }
            get { return _status; }
        }

        public string message
        {
            set { _message = value; }
            get { return _message; }
        }

        public string trans_id
        {
            set { _trans_id = value; }
            get { return _trans_id; }
        }
    }

    public class OutputConfirmInfo
    {
        private string _status = "";
        /* 
            "0": update sucessfull 
            "1": update fail            
         */
        private string _message = "";       //return message
        
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }

        public string message
        {
            set { _message = value; }
            get { return _message; }
        }
    } 

    public class OutputLoginInfo
    {
        enum STATUS { 
            SUCCESS = 0
            , INVALID_USERNAME_PASSWORD = 1 
        };

        private string _status = "";
            /* 
                "0": login successfull.                                    
                "1": login fail, username or password invalid                
            */        
        private string _message = "";       //thong bao tra ve
        private string _session_id = "";
        
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }

        public string session_id
        {
            set { _session_id = value; }
            get { return _session_id; }
        }

        public string message
        {
            set { _message = value; }
            get { return _message; }
        }
               
    }

    public class OutputLogoutInfo
    {   
        private string _status = "";
        private string _message = "";       //thong bao tra ve

        public string status
        {
            set { _status = value; }
            get { return _status; }
        }

        public string message
        {
            set { _message = value; }
            get { return _message; }
        } 

        
    }

    public class OutputVTload
    {
        private string _status = "";
        /* 
            "0": login successfull.
            "1": that bai do nha cung cap tra ve
            "2": timeout, ko ket noi dc toi nha cung cap
            "3": invalid targetMsIsdn
            "4": Trans_ID exist (check in day only      
            "5": invalid amount
         */
        private string _message = "";       //thong bao tra ve
        private string _trans_id;
        private string _request_transaction_time;
        private string _response_transaction_time;
        private string _response_code;
        private string _system_trace_audit_number;

        public string status
        {
            set { _status = value; }
            get { return _status; }
        }

        public string message
        {
            set { _message = value; }
            get { return _message; }
        }

        public string trans_id
        {
            set { _trans_id = value; }
            get { return _trans_id; }
        }
        
        public string request_transaction_time
        {
            set { _request_transaction_time = value; }
            get { return _request_transaction_time; }
        }

        public string response_transaction_time
        {
            set { _response_transaction_time = value; }
            get { return _response_transaction_time; }
        }

        public string response_code
        {
            set { _response_code = value; }
            get { return _response_code; }

        }

        public string system_trace_audit_number
        {
            set { _system_trace_audit_number = value; }
            get { return _system_trace_audit_number; }
        }
        
    }
}
