namespace Janohl.ST2Funbeat
{
    partial class SettingsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.gbAccount = new System.Windows.Forms.GroupBox();
            this.gbMappings = new System.Windows.Forms.GroupBox();
            this.pnlMappings = new System.Windows.Forms.FlowLayoutPanel();
            this.exportNameCheckBox = new System.Windows.Forms.CheckBox();
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.gbAccount.SuspendLayout();
            this.gbMappings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 17);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(148, 17);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(151, 36);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(146, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(9, 36);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(136, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.TextChanged += new System.EventHandler(this.OnUsernameChanged);
            // 
            // gbAccount
            // 
            this.gbAccount.Controls.Add(this.lblPassword);
            this.gbAccount.Controls.Add(this.txtUsername);
            this.gbAccount.Controls.Add(this.txtPassword);
            this.gbAccount.Controls.Add(this.lblUsername);
            this.gbAccount.Location = new System.Drawing.Point(3, 3);
            this.gbAccount.Name = "gbAccount";
            this.gbAccount.Size = new System.Drawing.Size(303, 71);
            this.gbAccount.TabIndex = 4;
            this.gbAccount.TabStop = false;
            this.gbAccount.Text = "Account information";
            // 
            // gbMappings
            // 
            this.gbMappings.AutoSize = true;
            this.gbMappings.Controls.Add(this.pnlMappings);
            this.gbMappings.Location = new System.Drawing.Point(3, 80);
            this.gbMappings.Name = "gbMappings";
            this.gbMappings.Size = new System.Drawing.Size(469, 107);
            this.gbMappings.TabIndex = 5;
            this.gbMappings.TabStop = false;
            this.gbMappings.Text = "Activity Mappings";
            // 
            // pnlMappings
            // 
            this.pnlMappings.AutoScroll = true;
            this.pnlMappings.AutoSize = true;
            this.pnlMappings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMappings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlMappings.Location = new System.Drawing.Point(3, 16);
            this.pnlMappings.Name = "pnlMappings";
            this.pnlMappings.Size = new System.Drawing.Size(463, 88);
            this.pnlMappings.TabIndex = 6;
            // 
            // exportNameCheckBox
            // 
            this.exportNameCheckBox.AutoSize = true;
            this.exportNameCheckBox.Location = new System.Drawing.Point(328, 39);
            this.exportNameCheckBox.Name = "exportNameCheckBox";
            this.exportNameCheckBox.Size = new System.Drawing.Size(218, 17);
            this.exportNameCheckBox.TabIndex = 6;
            this.exportNameCheckBox.Text = "Export activity name to funbeat comment";
            this.exportNameCheckBox.UseVisualStyleBackColor = true;
            this.exportNameCheckBox.CheckedChanged += new System.EventHandler(this.exportNameCheckBox_CheckedChanged);
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.Location = new System.Drawing.Point(312, 3);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(243, 71);
            this.SettingsGroupBox.TabIndex = 7;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Settings";
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.exportNameCheckBox);
            this.Controls.Add(this.gbMappings);
            this.Controls.Add(this.gbAccount);
            this.Controls.Add(this.SettingsGroupBox);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(558, 190);
            this.gbAccount.ResumeLayout(false);
            this.gbAccount.PerformLayout();
            this.gbMappings.ResumeLayout(false);
            this.gbMappings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.GroupBox gbAccount;
        private System.Windows.Forms.GroupBox gbMappings;
        private System.Windows.Forms.FlowLayoutPanel pnlMappings;
        private System.Windows.Forms.CheckBox exportNameCheckBox;
        private System.Windows.Forms.GroupBox SettingsGroupBox;
    }
}
