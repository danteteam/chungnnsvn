using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using MPS.App_Code.Sys;

namespace MPS
{
    public partial class frmMDI : Form
    {
        MPS.App_Code.App_Lib appLib = new MPS.App_Code.App_Lib();
        Security sObj = new Security();
        ModifyRegistry mObj = new ModifyRegistry();
        public string[] arrMenuName;

        public frmMDI()
        {
            InitializeComponent();   
            
            //MessageBox.Show(Program.UserType.ToString());
            this.FormClosing += new FormClosingEventHandler(CloseMe);
            
        }

        private void frmMDI_Load(object sender, EventArgs e)
        {            
            //Neu chua dang ky thi log time
            if (!sObj.IsRegisterUser())
            {
                //MessageBox.Show(t.Second.ToString());
                mObj.Write_RunTime("SOFTWARE\\Microsoft\\Windows\\policies\\system", "r.u.n.t.i.m.e.s");
            }

            //check user function
            ExplorerMenu(mnuMain);
            //MenuItem_LoadByRight();            

            //lay thong tin cua cty
            CommonInfo cObj = new CommonInfo();
            cObj.Common_Info_List();
            lblLicenseName.Text = cObj.CompanyName;
        }

        public void CloseMe(object sender, FormClosingEventArgs e) 
        {
            Application.Exit();
        }

        private void ExplorerMenu(MenuStrip mnuStrip)
        {
            if (Program.TblSession.DefaultView.Count > 0)
            {
                arrMenuName = new string[Program.TblSession.DefaultView.Count];

                DataTable Tbl = new DataTable();
                Tbl = Program.TblSession;
                //string strMnuName = string.Empty;

                for (int i = 0; i < Tbl.DefaultView.Count; i++)
                {
                    arrMenuName[i] = Tbl.DefaultView[i].Row["MenuName"].ToString().ToUpper().Trim();                   
                }
                
                Tbl.Dispose();
                Tbl = null;
            }           

            // Duyệt toàn bộ Menu trong C# sử dụng đệ quy
            if (mnuStrip == null)
                return;

            foreach (ToolStripMenuItem _item in mnuStrip.Items)
            {
                ExplorerItem(_item);
            }

        }

        private void ExplorerItem(ToolStripMenuItem parentItem)
        {
            // Duyệt toàn bộ Menu Item trong C# sử dụng đệ quy
            if (parentItem != null && parentItem.DropDownItems.Count > 0)
            {                
                foreach (ToolStripMenuItem _item in parentItem.DropDownItems)
                {
                    //if (_item.Name.ToString().ToLower() == "mnucontent")
                    //    MessageBox.Show(_item.Name.ToString().ToUpper());

                    if (Array.IndexOf(arrMenuName, _item.Name.ToString().ToUpper()) > -1)
                        _item.Visible = true;
                    else
                        _item.Visible = false;
                   
                    //MessageBox.Show(_item.Name.ToString().ToUpper() + "|" + Array.IndexOf(arrMenuName, _item.Name.ToString().ToUpper()).ToString());
                    ExplorerItem(_item);
                }
            }
        }       
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime t = DateTime.Now;
            StatusLabel1.Text = t.ToLongTimeString();

            //log to Register
            if (t.Second == 10)
            {
                //Neu chua dang ky thi log time
                if (!sObj.IsRegisterUser())
                {
                    //MessageBox.Show(t.Second.ToString());
                    mObj.Write_Time("SOFTWARE\\Microsoft\\Windows\\policies\\system", "t.i.c.k");
                }
            }
        }       

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btnYesNo = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show("Bạn có muốn thoát khỏi hệ thống không?", "Thông báo !", btnYesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }             

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MPS.Help.frmAbout frmObj = new MPS.Help.frmAbout();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuExport_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmExport frmObj = new MPS.Bussiness.frmExport();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;           
            frmObj.ShowDialog();
        }
               

        private void mnuSupply_Click(object sender, EventArgs e)
        {
            MPS.List.Supply frmObj = new MPS.List.Supply();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }    

        private void mnuReportImportDetail_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportImport_Detail frmObj = new MPS.Report.frmReportImport_Detail();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuExportReportDetail_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportExport_Detail frmObj = new MPS.Report.frmReportExport_Detail();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuStockReportDetail_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportStock_Detail frmObj = new MPS.Report.frmReportStock_Detail();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;

            frmObj.ShowDialog();
        }

        private void mnuStockReport_Click_1(object sender, EventArgs e)
        {
            MPS.Report.frmReportStock_Summary frmObj = new MPS.Report.frmReportStock_Summary();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuReportImport_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportImport_Summary frmObj = new MPS.Report.frmReportImport_Summary();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuExportReport_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportExport_Summary frmObj = new MPS.Report.frmReportExport_Summary();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuDebtImportReportSummary_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportDebtImport_Summary frmObj = new MPS.Report.frmReportDebtImport_Summary();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }
       

        private void mnuDebtExportReportSummary_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportDebtExport_Summary frmObj = new MPS.Report.frmReportDebtExport_Summary();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuManufacture_Click(object sender, EventArgs e)
        {
            MPS.List.Manufacture frmObj = new MPS.List.Manufacture();
            ////frmObj.MdiParent = this;           
            frmObj.ShowDialog();
        }

        private void mnuInventoryType_Click(object sender, EventArgs e)
        {
            MPS.List.InventoryType frmObj = new MPS.List.InventoryType();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuManufacture_Click_1(object sender, EventArgs e)
        {
            MPS.List.Manufacture frmObj = new MPS.List.Manufacture();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuColor_Click(object sender, EventArgs e)
        {
            MPS.List.Color frmObj = new MPS.List.Color();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

       

        private void mnuSysSetting_Click(object sender, EventArgs e)
        {
           
            MPS.Sys.frmSystemSetting frmObj = new MPS.Sys.frmSystemSetting();          
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();           

        }

        private void mnuChangePwd_Click(object sender, EventArgs e)
        {
            MPS.Sys.frmChangePwd frmObj = new MPS.Sys.frmChangePwd();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }
             
        private void mnuReImport_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmReImport frmObj = new MPS.Bussiness.frmReImport();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuRePrintInvoice_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmRePrintInvoice frmObj = new MPS.Bussiness.frmRePrintInvoice();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuUpdateDebtImport_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmUpdateDebImport frmObj = new MPS.Bussiness.frmUpdateDebImport();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuUpdateDebtExport_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmUpdateDebExport frmObj = new MPS.Bussiness.frmUpdateDebExport();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmImport frmObj = new MPS.Bussiness.frmImport();
            ////frmObj.MdiParent = this;     
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuDebtImportReportDetail_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportDebtImport_Detail frmObj = new MPS.Report.frmReportDebtImport_Detail();
            ////frmObj.MdiParent = this;     
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuDebtExportReportDetail_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportDebtExport_Detail frmObj = new MPS.Report.frmReportDebtExport_Detail();
            ////frmObj.MdiParent = this;     
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuAgent_Click(object sender, EventArgs e)
        {
            MPS.List.Agent frmObj = new MPS.List.Agent();
            ////frmObj.MdiParent = this;     
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuExportSale_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmExportAgent frmObj = new MPS.Bussiness.frmExportAgent();            
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();            
        }

        private void mnuReImportAgent_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmReImportAgent frmObj = new MPS.Bussiness.frmReImportAgent();            
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();               
        }

        private void mnuExportAgentReportSummary_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportExportAgent_Summary frmObj = new MPS.Report.frmReportExportAgent_Summary();
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog(); 
        }

        private void mnuExportAgentReportDetail_Click(object sender, EventArgs e)
        {
            MPS.Report.frmReportExportAgent_Detail frmObj = new MPS.Report.frmReportExportAgent_Detail();
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog(); 
        }

        private void mnuRePrintAgentInvoice_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmRePrintInvoiceAgent frmObj = new MPS.Bussiness.frmRePrintInvoiceAgent();
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog(); 
        }

        private void mnuUpdateDebtExportAgent_Click(object sender, EventArgs e)
        {
            MPS.Bussiness.frmUpdateDebExport_Agent frmObj = new MPS.Bussiness.frmUpdateDebExport_Agent();
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog(); 
        }     

        private void nnuSystemInfo_Click(object sender, EventArgs e)
        {
            MPS.Sys.frmSystemSetting frmObj = new MPS.Sys.frmSystemSetting();
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuUsers_Click(object sender, EventArgs e)
        {
            MPS.Sys.frmUserList frmObj = new MPS.Sys.frmUserList();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }
               
        private void mnuRoleFunction_Click(object sender, EventArgs e)
        {
            MPS.Sys.frmRoleFunction frmObj = new MPS.Sys.frmRoleFunction();
            //frmObj.MdiParent = this;
            frmObj.Icon = this.Icon;
            frmObj.ShowDialog();
        }

        private void mnuContent_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Application.StartupPath + " | " + appLib.Get_AppPath());
            string strPath = appLib.Get_AppPath() + "\\" + "MPS.chm";
            if (!System.IO.File.Exists(strPath))
            {
                MessageBox.Show("Lỗi không tìm thấy file trợ giúp, hãy xem lại!", "Thông báo!");  
            }
            else
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = strPath;
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                Process.Start(startInfo);
            }
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:"+linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + linkLabel2.Text);
        }
             
    }
}
