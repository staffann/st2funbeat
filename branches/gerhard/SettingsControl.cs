/*
Copyright (C) 2009, 2010 Jan Ohlson

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library. If not, see <http://www.gnu.org/licenses/>.
 */

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
