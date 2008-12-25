namespace GenKey
{
    partial class frmGen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGen));
            this.btnGen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRegister_Key = new System.Windows.Forms.TextBox();
            this.txtCustomer_Key = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(200, 99);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(114, 23);
            this.btnGen.TabIndex = 0;
            this.btnGen.Text = "Gen Key";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã số khách hàng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã số đăng ký";
            // 
            // txtRegister_Key
            // 
            this.txtRegister_Key.Location = new System.Drawing.Point(132, 58);
            this.txtRegister_Key.MaxLength = 500;
            this.txtRegister_Key.Name = "txtRegister_Key";
            this.txtRegister_Key.Size = new System.Drawing.Size(332, 20);
            this.txtRegister_Key.TabIndex = 7;
            this.txtRegister_Key.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegister_Key_KeyPress);
            // 
            // txtCustomer_Key
            // 
            this.txtCustomer_Key.Location = new System.Drawing.Point(132, 28);
            this.txtCustomer_Key.MaxLength = 500;
            this.txtCustomer_Key.Name = "txtCustomer_Key";
            this.txtCustomer_Key.Size = new System.Drawing.Size(332, 20);
            this.txtCustomer_Key.TabIndex = 6;
            this.txtCustomer_Key.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomer_Key_KeyPress);
            // 
            // frmGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 145);
            this.Controls.Add(this.txtRegister_Key);
            this.Controls.Add(this.txtCustomer_Key);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGen";
            this.Text = "MPS Gen Key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRegister_Key;
        private System.Windows.Forms.TextBox txtCustomer_Key;
    }
}

