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
            cbFunbeatActivity.DisplayMember = "Text";
            cbFunbeatActivity.DataSource = Janohl.Funbeat.FunbeatService.FunbeatActivityTypes;
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
            lblSTActivity.Text = ac.SportTracks.Text;
            lblSTActivity.Tag = ac.SportTracks.ReferenceID;
            int selectedIndex = cbFunbeatActivity.FindString(ac.Funbeat.Text);
            cbFunbeatActivity.SelectedIndex = selectedIndex;
        }

        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ac.Funbeat = (FunbeatActivityType)cbFunbeatActivity.SelectedItem;
            Settings.Settings.Save();
        }
    }
}
