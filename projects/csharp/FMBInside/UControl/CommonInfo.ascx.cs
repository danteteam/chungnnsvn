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

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

/// <summary>
/// Contains some common info
/// ChungNN - 02/2007
/// </summary>
/// 
public partial class UControl_CommonInfo : System.Web.UI.UserControl
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        CommonInfo wLib = new CommonInfo();

        string[] arrColor;
        arrColor = new string[12];
        
        wLib.VisitedFromDate = "01/01/2007";
        wLib.VisitedYear = 2007;
        wLib.Common_Info_Get();
        Int64 nVisitedYearNumbers = wLib.VisitedYearNumbers;

        arrColor[0] = Color.Red.Name;
        arrColor[1] = Color.Green.Name;
        arrColor[2] = Color.Gold.Name;
        arrColor[3] = Color.Aqua.Name;
        arrColor[4] = Color.Brown.Name;
        arrColor[5] = Color.Chartreuse.Name;
        arrColor[6] = Color.Cornsilk.Name;
        arrColor[7] = Color.DeepSkyBlue.Name;
        arrColor[8] = Color.Indigo.Name;
        arrColor[9] = Color.Linen.Name;
        arrColor[10] = Color.Salmon.Name;
        arrColor[11] = Color.SeaShell.Name;

        lblOnlineNumbers.Text = Application["OnlineNumbers"].ToString();
        lblVisitedNumbers.Text = Application["VisitedNumbers"].ToString();
        lblFromDate.Text = "Kể từ ngày: " + Application["VisitedNumbers_FromDate"].ToString();
                     
        //Show chart
        lblChart.Text = "";
        lblChart.Text += "<table width='100%' border='1' colspadding='0' cellspadding='0'>";
        lblChart.Text += "<tr align='center'>";        
        for (int i = 1; i <= DateTime.Now.Month; i++)
        {
            lblChart.Text += "<td bgcolor='" + arrColor[i] + "'>";
            lblChart.Text += Math.Round((float)(wLib.VisitedMonth[i - 1] * 100) / nVisitedYearNumbers,1) + "%";
            lblChart.Text += "</td>";
        }
        lblChart.Text += "<tr align='center'>"; 
        for (int i = 1; i <= DateTime.Now.Month; i++)
        {
            lblChart.Text += "<td bgcolor='" + arrColor[i] + "'>";
            lblChart.Text += "T" + i.ToString();
            lblChart.Text += "</td>";
        }
        lblChart.Text += "</tr>";
        lblChart.Text += "</tr>";
        lblChart.Text += "</table>";
    }   

}
