using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using EPAY_POS_GateWay.Common;

namespace EPAY_POS_GateWay.Configuration
{
    class AppConfiguration
    {
        public readonly static string Server_IP_Address = EPAY_POS_GateWay.Configuration.Settings.Default.Server_IP_Address;
        public readonly static int Server_Port = Convert.ToInt16(EPAY_POS_GateWay.Configuration.Settings.Default.Server_Port);
        public readonly static int Timeout = Convert.ToInt32(EPAY_POS_GateWay.Configuration.Settings.Default.AppTimeout);
        public readonly static string TransactionLogType = EPAY_POS_GateWay.Configuration.Settings.Default.TransactionLogType;
        public readonly static string Data_FileName = EPAY_POS_GateWay.Configuration.Settings.Default.Data_FileName;
        
        public readonly static string AppPasswordKey = EPAY_POS_GateWay.Configuration.Settings.Default.AppPasswordKey;
        public readonly static string App_Path = EPAY_POS_GateWay.Configuration.Settings.Default.App_Path;
        public readonly static string TopupInterfaceUserName = EPAY_POS_GateWay.Configuration.Settings.Default.TopupInterfaceUserName;
        public readonly static string TopupInterfacePassword = EPAY_POS_GateWay.Configuration.Settings.Default.TopupInterfacePassword;
      
        public readonly static string POSConfig_ISOFile = EPAY_POS_GateWay.Configuration.Settings.Default.POSConfig_ISOFile;
                                               
        public AppConfiguration()
        {            
            
        }
       

    }

}
