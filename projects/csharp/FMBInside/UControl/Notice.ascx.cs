using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FMB_Libs;

/// <summary>
/// Contains some common info
/// ChungNN - 08/2007
/// </summary>
/// 

public partial class UControl_Notice : System.Web.UI.UserControl
{
    FMB_Libs.CommonFuns wLib = new FMB_Libs.CommonFuns();
    DataSet Ds = new DataSet();
    DataTable Tbl = new DataTable();

    #region Property

    string _Height = "100%";
    string _Width = "100%";

    public string Height
    {
        get { return _Height; }
        set { _Height = value; }
    }

    public string Width
    {
        get { return _Width; }
        set { _Width = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {           
        try
        {
            Ds = wLib.ExecuteDataset("ProAwardNotice ", CommandType.Text);
            //grvPromotion.DataSource = Ds;
            //grvPromotion.DataBind();
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        lblContent.Text = string.Empty;
        Tbl = Ds.Tables[0];
        if(Tbl.DefaultView.Count>0)
        {
            lblContent.Text += "<table border='1' width='"+ Width +"' Height='"+ Height +"' cellspacing='0' cellpadding='0' bordercolor='#C0C0C0'><tr><td>";
            lblContent.Text += "<b><u><font class='clsNotice'>DS trúng thưởng: " + Tbl.DefaultView[0].Row["ProName"].ToString() + "</font></u></b>";
            

            //lblContent.Text += "<tr>";
            //lblContent.Text += "<td width='8%' align='center'>Stt</td>";
            //lblContent.Text += "<td width='24%'>Nguoi trung thuong</td>";
            //lblContent.Text += "<td width='18%'>Dia chi</td>";
            //lblContent.Text += "<td width='17%'>IMEI</td>";
            //lblContent.Text += "<td width='15%'>Loai may</td>";
            //lblContent.Text += "<td width='15%'>Giai thuong</td></tr>";

            //for(int i=0; i<Tbl.DefaultView.Count; i++)
            //{
            //    lblContent.Text += "<tr>";
            //    lblContent.Text += "<td width='8%' align='center'>"+ (i+1).ToString()+"</td>";
            //    lblContent.Text += "<td width='24%'>"+ Tbl.DefaultView[i].Row["Name"].ToString()+"</td>";
            //    lblContent.Text += "<td width='18%'>" + Tbl.DefaultView[i].Row["Addr"].ToString() + "</td>";
            //    lblContent.Text += "<td width='17%'>" + Tbl.DefaultView[i].Row["IMEI"].ToString() + "</td>";
            //    lblContent.Text += "<td width='15%'>" + Tbl.DefaultView[i].Row["InvtName"].ToString() + "</td>";
            //    lblContent.Text += "<td width='15%'>" + Tbl.DefaultView[i].Row["Description"].ToString() + "</td>";
            //    lblContent.Text += "/tr>";
            //}

            
            //lblContent.Text += "</table>";            

           

            lblContent.Text += "<marquee scrollamount='5' width='100%' height='"+ Height +"' scrolldelay='250' direction='up'>";
            for (int i = 0; i < Tbl.DefaultView.Count; i++)
            {
                lblContent.Text += "<font class='clsItem'>" + (i + 1).ToString() + ". " + Tbl.DefaultView[i].Row["Name"].ToString() + " (" + Tbl.DefaultView[i].Row["Addr"].ToString() + ")</font>";// +", giải thưởng: " + Tbl.DefaultView[i].Row["Description"].ToString();
                lblContent.Text += "<br>";                
            }
            lblContent.Text += "</marquee>";
            lblContent.Text += "</td></tr></table>";
        }   

    }
}
