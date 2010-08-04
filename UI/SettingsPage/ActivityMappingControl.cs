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
using Janohl.ST2Funbeat.Settings;

namespace Janohl.ST2Funbeat
{
    public partial class ActivityMappingControl : UserControl
    {
        private ActivityTypeMapping ac;
        private void Init()
        {
            InitializeComponent();
            cbFunbeatActivity.DataSource = FunbeatService.FunbeatActivityTypes;
            cbFunbeatActivity.SelectedIndexChanged += new EventHandler(OnSelectedIndexChanged);
        }

        public ActivityMappingControl()
        {
            Init();
        }
        public ActivityMappingControl(ActivityTypeMapping ac) 
        {
            this.ac = ac;
            Init();
            
            lblSTActivity.Tag = ac.SportTracks;
            lblSTActivity.Text = Plugin.SportTrackActivityTypes[ac.SportTracks];

            string funbeatName = string.Empty;
            foreach (FunbeatService.FunbeatActivityType type in FunbeatService.FunbeatActivityTypes)
                if (type.Id == ac.Funbeat)
                {
                    funbeatName = type.Name;
                    break;
                }

            int selectedIndex = cbFunbeatActivity.FindString(funbeatName);
            cbFunbeatActivity.SelectedIndex = selectedIndex;
        }

        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFunbeatActivity.SelectedItem != null)
            {
                this.ac.Funbeat = ((FunbeatService.FunbeatActivityType)cbFunbeatActivity.SelectedItem).Id;
            }
        }
    }
}
