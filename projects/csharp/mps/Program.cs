using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using MPS.App_Code.Sys;
/// <summary>
/// Program class
/// ChungNN - 8/2006
/// </summary>
/// 
namespace MPS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static int UserID;
        public static int UserType;
        public static string UserName;
        public static DataTable TblSession;     

        [STAThread]
        static void Main()
        {
            UserID = 1;
            UserType = 0;
            UserName = string.Empty;
            TblSession = null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Security cerObj = new Security();

            //Application.Run(new frmTest());

            //if (cerObj.IsRegisterUser())
            //{
                Application.Run(new MPS.Sys.frmLogin());
            //}
            //else
            //{
            //    Application.Run(new MPS.Sys.frmRegister());
            //}                       
        }
    }
}