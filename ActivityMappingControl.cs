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
