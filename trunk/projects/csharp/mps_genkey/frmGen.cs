using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
// it's required for reading/writing into the registry:
using Microsoft.Win32;
// and for the MessageBox function:
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;


namespace GenKey
{
    public partial class frmGen : Form
    {
        //private string _Volume_VolumeSerial;
        private string _Customer_Key;
        //private string _Register_Key;

        [DllImport("kernel32.dll")]
        private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, UInt32 VolumeNameSize, ref UInt32 VolumeSerialNumber, ref UInt32 MaximumComponentLength, ref UInt32 FileSystemFlags, StringBuilder FileSystemNameBuffer, UInt32 FileSystemNameSize);

        public frmGen()
        {
            InitializeComponent();
            txtCustomer_Key.Focus();
            //_Volume_VolumeSerial = GetVolumeSerial("C");
            //_Customer_Key = En_Decrypt("ChungNN", GetVolumeSerial("C"));
            //_Register_Key = En_Decrypt("NNC", _Customer_Key);
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            txtCustomer_Key.Focus();

            if (txtCustomer_Key.Text.Trim().Length > 0)
            {
                _Customer_Key = txtCustomer_Key.Text.Trim();
                txtRegister_Key.Text = En_Decrypt("NNC", _Customer_Key);
            }
            else
            {
                txtCustomer_Key.Focus();
            }
        }

        #region Methods

        #region string En_Decrypt(string clearText, string Password)
        //Input:    clearText - Text using 2 mixs 
        //          Password - Text needs 2 En_Decrypt
        //ChungNN - 12/2005
        public string En_Decrypt(string clearText, string Password)
        {
            if (Password != null)
            {
                byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
                byte[] passBytes = System.Text.Encoding.Unicode.GetBytes(Password);

                int nclearCount = 0;

                for (int i = 0; i < passBytes.Length; i++)
                {
                    passBytes[i] ^= clearBytes[nclearCount];

                    nclearCount++;
                    if (nclearCount >= clearBytes.Length)
                        nclearCount = 0;

                }
                return Convert.ToBase64String(passBytes);
            }

            else return ("");           
            
        }

        #endregion

        private void txtRegister_Key_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToByte(e.KeyChar) == 13)
                this.btnGen_Click(null, null);
        }
               
        #endregion

        private void txtCustomer_Key_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtRegister_Key.Focus();
        }
    }  

}

