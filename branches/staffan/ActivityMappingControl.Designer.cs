namespace Janohl.ST2Funbeat
{
    partial class ActivityMappingControl
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
            this.lblSTActivity = new System.Windows.Forms.Label();
            this.cbFunbeatActivity = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblSTActivity
            // 
            this.lblSTActivity.AutoSize = true;
            this.lblSTActivity.Location = new System.Drawing.Point(3, 6);
            this.lblSTActivity.Name = "lblSTActivity";
            this.lblSTActivity.Size = new System.Drawing.Size(0, 13);
            this.lblSTActivity.TabIndex = 0;
            // 
            // cbFunbeatActivity
            // 
            this.cbFunbeatActivity.FormattingEnabled = true;
            this.cbFunbeatActivity.Location = new System.Drawing.Point(278, 3);
            this.cbFunbeatActivity.Name = "cbFunbeatActivity";
            this.cbFunbeatActivity.Size = new System.Drawing.Size(322, 21);
            this.cbFunbeatActivity.TabIndex = 1;
            // 
            // ActivityMappingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbFunbeatActivity);
            this.Controls.Add(this.lblSTActivity);
            this.Name = "ActivityMappingControl";
            this.Size = new System.Drawing.Size(603, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSTActivity;
        private System.Windows.Forms.ComboBox cbFunbeatActivity;
    }
}
