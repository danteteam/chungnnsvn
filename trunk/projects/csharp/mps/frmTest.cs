using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace MPS
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }
                                    
        private void button1_Click(object sender, EventArgs e)
        {            
            openFileDialog1.InitialDirectory = "E:\\Pictures";
            openFileDialog1.Filter = "Image files (*.gif, *.jpg)|*.gif;*.jpg";
            //openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        void UploadFile2DB(string strFileName)
        {
            System.IO.Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.FileName = strFileName;
            
            try
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    //using (myStream)
                    //{
                        App_Code.App_Lib appLib = new MPS.App_Code.App_Lib();
                        int len = (int) myStream.Length;
                        byte[] buffer = new byte[1024];
                        byte[] pic = new byte[len];
                        myStream.Read(pic, 0, len);
                      
                        //// Insert the image and comment into the database
                        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(appLib.GetConnectionString());
                        try
                        {
                            connection.Open();
                            //string strSql = "insert into CommonInfo(CompanyName, Logo) values(@CompanyName, @Logo)";
                            string strSql = "insert into CommonInfo(CompanyName) values(@CompanyName)";

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSql, connection);
                            cmd.Parameters.AddWithValue("@CompanyName", "test_" + DateTime.Now.ToString() );     
                            //cmd.Parameters.AddWithValue("@Logo", pic);                            
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw (ex);
                        }
                        finally
                        {
                            connection.Close();
                        }
                        
                    //}
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                UploadFile2DB(textBox1.Text);
                Console.WriteLine("upload successfull");
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                button2.Enabled = true;
            }

            
        }
    }
}