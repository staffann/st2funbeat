using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using Janohl.ST2Funbeat.Settings;

namespace Janohl.ST2Funbeat
{
    public partial class SettingsControl : UserControl
    {

        public SettingsControl()
        {
            InitializeComponent();

            try
            {
                txtUsername.Text = Settings.Settings.Instance.User.Username;
                txtPassword.Text = Settings.Settings.Instance.User.Password;
                List<Control> amcs = new List<Control>();
                ActivityTypeMapping active = null;
                foreach (string ac in Plugin.SportTrackActivityTypes.Keys)
                {
                    active = null;

                    foreach (ActivityTypeMapping atm in Settings.Settings.Instance.ActivityTypeMappings)
                        if (atm.SportTracks == ac)
                        {
                            active = atm;
                            break;
                        }
                    if (active == null)
                    {
                        active = new ActivityTypeMapping();
                        active.SportTracks = ac;
                        active.Funbeat = 51;
                        Settings.Settings.Instance.ActivityTypeMappings.Add(active);
                    }
                    ActivityMappingControl amc = new ActivityMappingControl(active);
                    amcs.Add(amc);
                }
                pnlMappings.Controls.AddRange(amcs.ToArray());
            }
            catch (Exception ex)
            {
                TextBox ErrMessageBox = new TextBox();
                ErrMessageBox.Text = string.Concat("Failed to read training type mappings from funbeat.\r\nException message:\r\n",
                                                    ex.ToString());
                ErrMessageBox.Size = pnlMappings.Size;
                ErrMessageBox.ReadOnly = true;
                ErrMessageBox.Multiline = true;
                pnlMappings.Controls.Add(ErrMessageBox);
            }
        }



        private void OnUsernameChanged(object sender, EventArgs e)
        {
            Settings.Settings.Instance.User.Username = txtUsername.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            Settings.Settings.Instance.User.Password = txtPassword.Text;
        }
    }
}
