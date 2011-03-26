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
    public partial class EquipmentMappingControl : UserControl
    {
        private EquipmentTypeMapping em;
        private void Init()
        {
            InitializeComponent();
            cbFunbeatEquip.DataSource = FunbeatService.FunbeatEquipment;
            cbFunbeatEquip.SelectedIndexChanged += new EventHandler(OnSelectedIndexChanged);
        }

        public EquipmentMappingControl()
        {
            Init();
        }
        public EquipmentMappingControl(EquipmentTypeMapping em) 
        {
            this.em = em;
            Init();
            
            lblSTActivity.Tag = em.SportTracks;
            lblSTActivity.Text = Plugin.SportTrackEquipment[em.SportTracks];

            string funbeatName = string.Empty;
            foreach (string equipment in FunbeatService.FunbeatEquipment)
                if (equipment == em.Funbeat)
                {
                    funbeatName = equipment;
                    break;
                }

            int selectedIndex = cbFunbeatEquip.FindString(funbeatName);
            cbFunbeatEquip.SelectedIndex = selectedIndex;
        }

        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFunbeatEquip.SelectedItem != null)
            {
                this.em.Funbeat = ((string)cbFunbeatEquip.SelectedItem);
            }
        }
    }
}
