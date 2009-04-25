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

            txtUsername.Text = Plugin.PluginSettings.User.Username;
            txtPassword.Text = Plugin.PluginSettings.User.Password;
            List<Control> amcs = new List<Control>();
            ActivityTypeMapping active = null;
            foreach (STActivityType ac in Plugin.SportTrackActivityTypes)
            {
                active = null;

                foreach (ActivityTypeMapping atm in Plugin.PluginSettings.ActivityTypeMappings)
                    if (atm.SportTracks.ReferenceID == ac.ReferenceID)
                    {
                        active = atm;
                        break;
                    }
                if (active == null)
                {
                    active = new ActivityTypeMapping();
                    active.SportTracks = ac;
                    active.Funbeat = new FunbeatActivityType(51, "Övriga aktiviteter");
                    Plugin.PluginSettings.ActivityTypeMappings.Add(active);
                }
                ActivityMappingControl amc = new ActivityMappingControl(active);
                amcs.Add(amc);
            }
            pnlMappings.Controls.AddRange(amcs.ToArray());
        }



        private void OnUsernameChanged(object sender, EventArgs e)
        {
            Plugin.PluginSettings.User.Username = txtUsername.Text;
            Plugin.PluginSettings.Save();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            Plugin.PluginSettings.User.Password = txtPassword.Text;
            Plugin.PluginSettings.Save();
        }
    }
}
