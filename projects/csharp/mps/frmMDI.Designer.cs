namespace MPS
{
    partial class frmMDI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDI));
            this.mnuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.nnuSystemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRoleFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangePwd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBusiness = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReImportAgent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportSale = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRePrintAgentInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRePrintInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateDebtMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateDebtImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateDebtExportAgent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateDebtExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportReportMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportImportSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportImportDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportReportMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportAgentReportSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportAgentReportDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportReportSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportReportDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStockReportMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStockReportSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStockReportDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDebtReportMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDebtImportReportSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDebtImportReportDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDebtExportReportSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDebtExportReportDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSupply = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManufacture = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAgent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInventoryType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuColor = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.panInfo = new System.Windows.Forms.Panel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLicenseName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.CNetHelpProvider = new System.Windows.Forms.HelpProvider();
            this.mnuMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuSystem
            // 
            this.mnuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nnuSystemInfo,
            this.mnuUsers,
            this.mnuRoleFunction,
            this.mnuChangePwd,
            this.mnuLogout});
            this.mnuSystem.Name = "mnuSystem";
            resources.ApplyResources(this.mnuSystem, "mnuSystem");
            // 
            // nnuSystemInfo
            // 
            this.nnuSystemInfo.Name = "nnuSystemInfo";
            resources.ApplyResources(this.nnuSystemInfo, "nnuSystemInfo");
            this.nnuSystemInfo.Click += new System.EventHandler(this.nnuSystemInfo_Click);
            // 
            // mnuUsers
            // 
            this.mnuUsers.Name = "mnuUsers";
            resources.ApplyResources(this.mnuUsers, "mnuUsers");
            this.mnuUsers.Click += new System.EventHandler(this.mnuUsers_Click);
            // 
            // mnuRoleFunction
            // 
            this.mnuRoleFunction.Name = "mnuRoleFunction";
            resources.ApplyResources(this.mnuRoleFunction, "mnuRoleFunction");
            this.mnuRoleFunction.Click += new System.EventHandler(this.mnuRoleFunction_Click);
            // 
            // mnuChangePwd
            // 
            this.mnuChangePwd.Name = "mnuChangePwd";
            resources.ApplyResources(this.mnuChangePwd, "mnuChangePwd");
            this.mnuChangePwd.Click += new System.EventHandler(this.mnuChangePwd_Click);
            // 
            // mnuLogout
            // 
            this.mnuLogout.Name = "mnuLogout";
            resources.ApplyResources(this.mnuLogout, "mnuLogout");
            this.mnuLogout.Click += new System.EventHandler(this.mnuLogout_Click);
            // 
            // mnuBusiness
            // 
            this.mnuBusiness.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImportMain,
            this.mnuExportMain,
            this.mnuUpdateDebtMain});
            this.mnuBusiness.Name = "mnuBusiness";
            resources.ApplyResources(this.mnuBusiness, "mnuBusiness");
            // 
            // mnuImportMain
            // 
            this.mnuImportMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImport,
            this.mnuReImportAgent,
            this.mnuReImport});
            this.mnuImportMain.Name = "mnuImportMain";
            resources.ApplyResources(this.mnuImportMain, "mnuImportMain");
            // 
            // mnuImport
            // 
            this.mnuImport.Name = "mnuImport";
            resources.ApplyResources(this.mnuImport, "mnuImport");
            this.mnuImport.Click += new System.EventHandler(this.mnuImport_Click);
            // 
            // mnuReImportAgent
            // 
            this.mnuReImportAgent.Name = "mnuReImportAgent";
            resources.ApplyResources(this.mnuReImportAgent, "mnuReImportAgent");
            this.mnuReImportAgent.Click += new System.EventHandler(this.mnuReImportAgent_Click);
            // 
            // mnuReImport
            // 
            this.mnuReImport.Name = "mnuReImport";
            resources.ApplyResources(this.mnuReImport, "mnuReImport");
            this.mnuReImport.Click += new System.EventHandler(this.mnuReImport_Click);
            // 
            // mnuExportMain
            // 
            this.mnuExportMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportSale,
            this.mnuRePrintAgentInvoice,
            this.mnuExport,
            this.mnuRePrintInvoice});
            this.mnuExportMain.Name = "mnuExportMain";
            resources.ApplyResources(this.mnuExportMain, "mnuExportMain");
            // 
            // mnuExportSale
            // 
            this.mnuExportSale.Name = "mnuExportSale";
            resources.ApplyResources(this.mnuExportSale, "mnuExportSale");
            this.mnuExportSale.Click += new System.EventHandler(this.mnuExportSale_Click);
            // 
            // mnuRePrintAgentInvoice
            // 
            this.mnuRePrintAgentInvoice.Name = "mnuRePrintAgentInvoice";
            resources.ApplyResources(this.mnuRePrintAgentInvoice, "mnuRePrintAgentInvoice");
            this.mnuRePrintAgentInvoice.Click += new System.EventHandler(this.mnuRePrintAgentInvoice_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.Name = "mnuExport";
            resources.ApplyResources(this.mnuExport, "mnuExport");
            this.mnuExport.Click += new System.EventHandler(this.mnuExport_Click);
            // 
            // mnuRePrintInvoice
            // 
            this.mnuRePrintInvoice.Name = "mnuRePrintInvoice";
            resources.ApplyResources(this.mnuRePrintInvoice, "mnuRePrintInvoice");
            this.mnuRePrintInvoice.Click += new System.EventHandler(this.mnuRePrintInvoice_Click);
            // 
            // mnuUpdateDebtMain
            // 
            this.mnuUpdateDebtMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUpdateDebtImport,
            this.mnuUpdateDebtExportAgent,
            this.mnuUpdateDebtExport});
            this.mnuUpdateDebtMain.Name = "mnuUpdateDebtMain";
            resources.ApplyResources(this.mnuUpdateDebtMain, "mnuUpdateDebtMain");
            // 
            // mnuUpdateDebtImport
            // 
            this.mnuUpdateDebtImport.Name = "mnuUpdateDebtImport";
            resources.ApplyResources(this.mnuUpdateDebtImport, "mnuUpdateDebtImport");
            this.mnuUpdateDebtImport.Click += new System.EventHandler(this.mnuUpdateDebtImport_Click);
            // 
            // mnuUpdateDebtExportAgent
            // 
            this.mnuUpdateDebtExportAgent.Name = "mnuUpdateDebtExportAgent";
            resources.ApplyResources(this.mnuUpdateDebtExportAgent, "mnuUpdateDebtExportAgent");
            this.mnuUpdateDebtExportAgent.Click += new System.EventHandler(this.mnuUpdateDebtExportAgent_Click);
            // 
            // mnuUpdateDebtExport
            // 
            this.mnuUpdateDebtExport.Name = "mnuUpdateDebtExport";
            resources.ApplyResources(this.mnuUpdateDebtExport, "mnuUpdateDebtExport");
            this.mnuUpdateDebtExport.Click += new System.EventHandler(this.mnuUpdateDebtExport_Click);
            // 
            // mnuReport
            // 
            this.mnuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImportReportMain,
            this.mnuExportReportMain,
            this.mnuStockReportMain,
            this.mnuDebtReportMain});
            this.mnuReport.Name = "mnuReport";
            resources.ApplyResources(this.mnuReport, "mnuReport");
            // 
            // mnuImportReportMain
            // 
            this.mnuImportReportMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReportImportSummary,
            this.mnuReportImportDetail});
            this.mnuImportReportMain.Name = "mnuImportReportMain";
            resources.ApplyResources(this.mnuImportReportMain, "mnuImportReportMain");
            // 
            // mnuReportImportSummary
            // 
            this.mnuReportImportSummary.Name = "mnuReportImportSummary";
            resources.ApplyResources(this.mnuReportImportSummary, "mnuReportImportSummary");
            this.mnuReportImportSummary.Click += new System.EventHandler(this.mnuReportImport_Click);
            // 
            // mnuReportImportDetail
            // 
            this.mnuReportImportDetail.Name = "mnuReportImportDetail";
            resources.ApplyResources(this.mnuReportImportDetail, "mnuReportImportDetail");
            this.mnuReportImportDetail.Click += new System.EventHandler(this.mnuReportImportDetail_Click);
            // 
            // mnuExportReportMain
            // 
            this.mnuExportReportMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportAgentReportSummary,
            this.mnuExportAgentReportDetail,
            this.mnuExportReportSummary,
            this.mnuExportReportDetail});
            this.mnuExportReportMain.Name = "mnuExportReportMain";
            resources.ApplyResources(this.mnuExportReportMain, "mnuExportReportMain");
            // 
            // mnuExportAgentReportSummary
            // 
            this.mnuExportAgentReportSummary.Name = "mnuExportAgentReportSummary";
            resources.ApplyResources(this.mnuExportAgentReportSummary, "mnuExportAgentReportSummary");
            this.mnuExportAgentReportSummary.Click += new System.EventHandler(this.mnuExportAgentReportSummary_Click);
            // 
            // mnuExportAgentReportDetail
            // 
            this.mnuExportAgentReportDetail.Name = "mnuExportAgentReportDetail";
            resources.ApplyResources(this.mnuExportAgentReportDetail, "mnuExportAgentReportDetail");
            this.mnuExportAgentReportDetail.Click += new System.EventHandler(this.mnuExportAgentReportDetail_Click);
            // 
            // mnuExportReportSummary
            // 
            this.mnuExportReportSummary.Name = "mnuExportReportSummary";
            resources.ApplyResources(this.mnuExportReportSummary, "mnuExportReportSummary");
            this.mnuExportReportSummary.Click += new System.EventHandler(this.mnuExportReport_Click);
            // 
            // mnuExportReportDetail
            // 
            this.mnuExportReportDetail.Name = "mnuExportReportDetail";
            resources.ApplyResources(this.mnuExportReportDetail, "mnuExportReportDetail");
            this.mnuExportReportDetail.Click += new System.EventHandler(this.mnuExportReportDetail_Click);
            // 
            // mnuStockReportMain
            // 
            this.mnuStockReportMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStockReportSummary,
            this.mnuStockReportDetail});
            this.mnuStockReportMain.Name = "mnuStockReportMain";
            resources.ApplyResources(this.mnuStockReportMain, "mnuStockReportMain");
            // 
            // mnuStockReportSummary
            // 
            this.mnuStockReportSummary.Name = "mnuStockReportSummary";
            resources.ApplyResources(this.mnuStockReportSummary, "mnuStockReportSummary");
            this.mnuStockReportSummary.Click += new System.EventHandler(this.mnuStockReport_Click_1);
            // 
            // mnuStockReportDetail
            // 
            this.mnuStockReportDetail.Name = "mnuStockReportDetail";
            resources.ApplyResources(this.mnuStockReportDetail, "mnuStockReportDetail");
            this.mnuStockReportDetail.Click += new System.EventHandler(this.mnuStockReportDetail_Click);
            // 
            // mnuDebtReportMain
            // 
            this.mnuDebtReportMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDebtImportReportSummary,
            this.mnuDebtImportReportDetail,
            this.mnuDebtExportReportSummary,
            this.mnuDebtExportReportDetail});
            this.mnuDebtReportMain.Name = "mnuDebtReportMain";
            resources.ApplyResources(this.mnuDebtReportMain, "mnuDebtReportMain");
            // 
            // mnuDebtImportReportSummary
            // 
            this.mnuDebtImportReportSummary.Name = "mnuDebtImportReportSummary";
            resources.ApplyResources(this.mnuDebtImportReportSummary, "mnuDebtImportReportSummary");
            this.mnuDebtImportReportSummary.Click += new System.EventHandler(this.mnuDebtImportReportSummary_Click);
            // 
            // mnuDebtImportReportDetail
            // 
            this.mnuDebtImportReportDetail.Name = "mnuDebtImportReportDetail";
            resources.ApplyResources(this.mnuDebtImportReportDetail, "mnuDebtImportReportDetail");
            this.mnuDebtImportReportDetail.Click += new System.EventHandler(this.mnuDebtImportReportDetail_Click);
            // 
            // mnuDebtExportReportSummary
            // 
            this.mnuDebtExportReportSummary.Name = "mnuDebtExportReportSummary";
            resources.ApplyResources(this.mnuDebtExportReportSummary, "mnuDebtExportReportSummary");
            this.mnuDebtExportReportSummary.Click += new System.EventHandler(this.mnuDebtExportReportSummary_Click);
            // 
            // mnuDebtExportReportDetail
            // 
            this.mnuDebtExportReportDetail.Name = "mnuDebtExportReportDetail";
            resources.ApplyResources(this.mnuDebtExportReportDetail, "mnuDebtExportReportDetail");
            this.mnuDebtExportReportDetail.Click += new System.EventHandler(this.mnuDebtExportReportDetail_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContent,
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            // 
            // mnuContent
            // 
            this.mnuContent.Name = global::MPS.Properties.Settings.Default.Help1;
            resources.ApplyResources(this.mnuContent, "mnuContent");
            this.mnuContent.Tag = global::MPS.Properties.Settings.Default.Help;
            this.mnuContent.Click += new System.EventHandler(this.mnuContent_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            resources.ApplyResources(this.mnuAbout, "mnuAbout");
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSystem,
            this.mnuList,
            this.mnuBusiness,
            this.mnuReport,
            this.mnuHelp});
            resources.ApplyResources(this.mnuMain, "mnuMain");
            this.mnuMain.Name = "mnuMain";
            // 
            // mnuList
            // 
            this.mnuList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSupply,
            this.mnuManufacture,
            this.mnuAgent,
            this.mnuInventoryType,
            this.mnuColor});
            this.mnuList.Name = "mnuList";
            resources.ApplyResources(this.mnuList, "mnuList");
            // 
            // mnuSupply
            // 
            this.mnuSupply.Name = "mnuSupply";
            resources.ApplyResources(this.mnuSupply, "mnuSupply");
            this.mnuSupply.Click += new System.EventHandler(this.mnuSupply_Click);
            // 
            // mnuManufacture
            // 
            this.mnuManufacture.Name = "mnuManufacture";
            resources.ApplyResources(this.mnuManufacture, "mnuManufacture");
            this.mnuManufacture.Click += new System.EventHandler(this.mnuManufacture_Click_1);
            // 
            // mnuAgent
            // 
            this.mnuAgent.Name = "mnuAgent";
            resources.ApplyResources(this.mnuAgent, "mnuAgent");
            this.mnuAgent.Click += new System.EventHandler(this.mnuAgent_Click);
            // 
            // mnuInventoryType
            // 
            this.mnuInventoryType.Name = "mnuInventoryType";
            resources.ApplyResources(this.mnuInventoryType, "mnuInventoryType");
            this.mnuInventoryType.Click += new System.EventHandler(this.mnuInventoryType_Click);
            // 
            // mnuColor
            // 
            this.mnuColor.Name = "mnuColor";
            resources.ApplyResources(this.mnuColor, "mnuColor");
            this.mnuColor.Click += new System.EventHandler(this.mnuColor_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.CNetHelpProvider.SetHelpKeyword(this.lblTime, resources.GetString("lblTime.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.lblTime, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("lblTime.HelpNavigator"))));
            this.lblTime.Name = "lblTime";
            this.CNetHelpProvider.SetShowHelp(this.lblTime, ((bool)(resources.GetObject("lblTime.ShowHelp"))));
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.StatusLabel1.Name = "StatusLabel1";
            resources.ApplyResources(this.StatusLabel1, "StatusLabel1");
            // 
            // statusStrip
            // 
            this.CNetHelpProvider.SetHelpKeyword(this.statusStrip, resources.GetString("statusStrip.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.statusStrip, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("statusStrip.HelpNavigator"))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.CNetHelpProvider.SetShowHelp(this.statusStrip, ((bool)(resources.GetObject("statusStrip.ShowHelp"))));
            // 
            // panInfo
            // 
            resources.ApplyResources(this.panInfo, "panInfo");
            this.panInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panInfo.Controls.Add(this.linkLabel2);
            this.panInfo.Controls.Add(this.label8);
            this.panInfo.Controls.Add(this.label7);
            this.panInfo.Controls.Add(this.label6);
            this.panInfo.Controls.Add(this.label5);
            this.panInfo.Controls.Add(this.label4);
            this.panInfo.Controls.Add(this.label3);
            this.panInfo.Controls.Add(this.label2);
            this.panInfo.Controls.Add(this.lblLicenseName);
            this.panInfo.Controls.Add(this.pictureBox1);
            this.panInfo.Controls.Add(this.label1);
            this.panInfo.Controls.Add(this.linkLabel1);
            this.CNetHelpProvider.SetHelpKeyword(this.panInfo, resources.GetString("panInfo.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.panInfo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("panInfo.HelpNavigator"))));
            this.panInfo.Name = "panInfo";
            this.CNetHelpProvider.SetShowHelp(this.panInfo, ((bool)(resources.GetObject("panInfo.ShowHelp"))));
            // 
            // linkLabel2
            // 
            resources.ApplyResources(this.linkLabel2, "linkLabel2");
            this.CNetHelpProvider.SetHelpKeyword(this.linkLabel2, resources.GetString("linkLabel2.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.linkLabel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("linkLabel2.HelpNavigator"))));
            this.linkLabel2.LinkVisited = true;
            this.linkLabel2.Name = "linkLabel2";
            this.CNetHelpProvider.SetShowHelp(this.linkLabel2, ((bool)(resources.GetObject("linkLabel2.ShowHelp"))));
            this.linkLabel2.TabStop = true;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.CNetHelpProvider.SetHelpKeyword(this.label8, resources.GetString("label8.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.label8, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label8.HelpNavigator"))));
            this.label8.Name = "label8";
            this.CNetHelpProvider.SetShowHelp(this.label8, ((bool)(resources.GetObject("label8.ShowHelp"))));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.CNetHelpProvider.SetHelpKeyword(this.label7, resources.GetString("label7.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.label7, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label7.HelpNavigator"))));
            this.label7.Name = "label7";
            this.CNetHelpProvider.SetShowHelp(this.label7, ((bool)(resources.GetObject("label7.ShowHelp"))));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.CNetHelpProvider.SetHelpKeyword(this.label6, resources.GetString("label6.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.label6, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label6.HelpNavigator"))));
            this.label6.Name = "label6";
            this.CNetHelpProvider.SetShowHelp(this.label6, ((bool)(resources.GetObject("label6.ShowHelp"))));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.CNetHelpProvider.SetHelpKeyword(this.label5, resources.GetString("label5.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.label5, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label5.HelpNavigator"))));
            this.label5.Name = "label5";
            this.CNetHelpProvider.SetShowHelp(this.label5, ((bool)(resources.GetObject("label5.ShowHelp"))));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.CNetHelpProvider.SetHelpKeyword(this.label4, resources.GetString("label4.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.label4, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label4.HelpNavigator"))));
            this.label4.Name = "label4";
            this.CNetHelpProvider.SetShowHelp(this.label4, ((bool)(resources.GetObject("label4.ShowHelp"))));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.CNetHelpProvider.SetHelpKeyword(this.label3, resources.GetString("label3.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.label3, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label3.HelpNavigator"))));
            this.label3.Name = "label3";
            this.CNetHelpProvider.SetShowHelp(this.label3, ((bool)(resources.GetObject("label3.ShowHelp"))));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.CNetHelpProvider.SetHelpKeyword(this.label2, resources.GetString("label2.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.label2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label2.HelpNavigator"))));
            this.label2.Name = "label2";
            this.CNetHelpProvider.SetShowHelp(this.label2, ((bool)(resources.GetObject("label2.ShowHelp"))));
            // 
            // lblLicenseName
            // 
            resources.ApplyResources(this.lblLicenseName, "lblLicenseName");
            this.CNetHelpProvider.SetHelpKeyword(this.lblLicenseName, resources.GetString("lblLicenseName.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.lblLicenseName, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("lblLicenseName.HelpNavigator"))));
            this.lblLicenseName.Name = "lblLicenseName";
            this.CNetHelpProvider.SetShowHelp(this.lblLicenseName, ((bool)(resources.GetObject("lblLicenseName.ShowHelp"))));
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CNetHelpProvider.SetHelpKeyword(this.pictureBox1, resources.GetString("pictureBox1.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.pictureBox1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("pictureBox1.HelpNavigator"))));
            this.pictureBox1.Image = global::MPS.Properties.Resources.Logo_Large;
            this.pictureBox1.InitialImage = global::MPS.Properties.Resources.Logo_Large;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.CNetHelpProvider.SetShowHelp(this.pictureBox1, ((bool)(resources.GetObject("pictureBox1.ShowHelp"))));
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.CNetHelpProvider.SetHelpKeyword(this.label1, resources.GetString("label1.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.label1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label1.HelpNavigator"))));
            this.label1.Name = "label1";
            this.CNetHelpProvider.SetShowHelp(this.label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.CNetHelpProvider.SetHelpKeyword(this.linkLabel1, resources.GetString("linkLabel1.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this.linkLabel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("linkLabel1.HelpNavigator"))));
            this.linkLabel1.LinkVisited = true;
            this.linkLabel1.Name = "linkLabel1";
            this.CNetHelpProvider.SetShowHelp(this.linkLabel1, ((bool)(resources.GetObject("linkLabel1.ShowHelp"))));
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AppIcon.ICO");
            this.imageList1.Images.SetKeyName(1, "BackGround2.bmp");
            this.imageList1.Images.SetKeyName(2, "Logo.jpg");
            this.imageList1.Images.SetKeyName(3, "Logo_Large.jpg");
            // 
            // CNetHelpProvider
            // 
            resources.ApplyResources(this.CNetHelpProvider, "CNetHelpProvider");
            // 
            // frmMDI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::MPS.Properties.Resources.bg;
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.CNetHelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.CNetHelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.IsMdiContainer = true;
            this.Name = "frmMDI";
            this.CNetHelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMDI_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panInfo.ResumeLayout(false);
            this.panInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolStripMenuItem mnuSystem;
        private System.Windows.Forms.ToolStripMenuItem mnuSysSetting;
        private System.Windows.Forms.ToolStripMenuItem mnuChangePwd;
        private System.Windows.Forms.ToolStripMenuItem mnuLogout;
        private System.Windows.Forms.ToolStripMenuItem mnuBusiness;
        private System.Windows.Forms.ToolStripMenuItem mnuImportMain;
        private System.Windows.Forms.ToolStripMenuItem mnuExportMain;
        private System.Windows.Forms.ToolStripMenuItem mnuReport;
        private System.Windows.Forms.ToolStripMenuItem mnuImportReportMain;
        private System.Windows.Forms.ToolStripMenuItem mnuExportReportMain;
        private System.Windows.Forms.ToolStripMenuItem mnuStockReportMain;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuContent;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuList;
        private System.Windows.Forms.ToolStripMenuItem mnuSupply;
        private System.Windows.Forms.ToolStripMenuItem mnuInventoryType;
        private System.Windows.Forms.ToolStripMenuItem mnuDebtReportMain;
        private System.Windows.Forms.ToolStripMenuItem mnuReportImportDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuReportImportSummary;
        private System.Windows.Forms.ToolStripMenuItem mnuExportReportSummary;
        private System.Windows.Forms.ToolStripMenuItem mnuStockReportSummary;
        private System.Windows.Forms.ToolStripMenuItem mnuStockReportDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuDebtImportReportSummary;
        private System.Windows.Forms.ToolStripMenuItem mnuDebtExportReportSummary;
        private System.Windows.Forms.Panel panInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblLicenseName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem mnuManufacture;
        private System.Windows.Forms.ToolStripMenuItem mnuColor;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem mnuDebtImportReportDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuDebtExportReportDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private System.Windows.Forms.ToolStripMenuItem mnuReImport;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripMenuItem mnuRePrintInvoice;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdateDebtMain;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdateDebtImport;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdateDebtExport;
        private System.Windows.Forms.ToolStripMenuItem mnuExportSale;
        private System.Windows.Forms.ToolStripMenuItem mnuAgent;
        private System.Windows.Forms.ToolStripMenuItem mnuRePrintAgentInvoice;
        private System.Windows.Forms.ToolStripMenuItem mnuReImportAgent;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdateDebtExportAgent;
        private System.Windows.Forms.ToolStripMenuItem mnuExportAgentReportSummary;
        private System.Windows.Forms.ToolStripMenuItem mnuExportReportDetail;
        private System.Windows.Forms.ToolStripMenuItem mnuExportAgentReportDetail;
        private System.Windows.Forms.HelpProvider CNetHelpProvider;
        private System.Windows.Forms.ToolStripMenuItem nnuSystemInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuUsers;
        private System.Windows.Forms.ToolStripMenuItem mnuRoleFunction;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}



