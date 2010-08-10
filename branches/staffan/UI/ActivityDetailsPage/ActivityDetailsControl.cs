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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using Janohl.ST2Funbeat.Funbeat;

namespace Janohl.ST2Funbeat
{
    public partial class ActivityDetailsControl : UserControl
    {
#if !ST_2_1
        private FitnessDataHandler fitnessDataHandler = null;
        private IActivity activity = null;
        //private Collection<IActivity> InitialisedActivities=null;

        public IActivity Activity
        {
            get
            {
                return this.activity;
            }
            set
            {
                if(activity!=null)
                    activity.PropertyChanged -= ActivityPropChanged;
                activity = value;
                if (activity != null)
                {
                    //if (!InitialisedActivities.Contains(value))
                    //{
                    //    InitialisedActivities.Add(activity);
                        
                    //}
                    activity.PropertyChanged += ActivityPropChanged;
                }
            }
        }

        public ActivityDetailsControl(IActivity activity)
        {
            InitializeComponent();
            //InitialisedActivities = new Collection<IActivity>();
            this.Activity = activity;
            fitnessDataHandler = Plugin.dataHandler;
            RefreshInfo();
            this.PanelChoiceActionBanner.Text = "User Input";
        }

        public void RefreshInfo()
        {
            bool boExported;

            // Check the range of RPE and TE
            CheckAndCorrectRPEAndTE();
            
            // Update status info
            boExported = fitnessDataHandler.boGetExported(activity);

            this.ExportedCheckBox1.CheckState = BoolToCheckState(boExported);
            this.ExportedCheckBox2.CheckState = BoolToCheckState(boExported);

            // Update user input
            int? RPEInput, RepetitionsInput, SetsInput;
            double? TEInput;
            fitnessDataHandler.GetCustomFieldsData(activity, 
                                out RPEInput, 
                                out TEInput, 
                                out RepetitionsInput, 
                                out SetsInput);
            if(RPEInput == null)
                this.RPEComboBox.SelectedIndex = 0;
            else
                this.RPEComboBox.SelectedIndex = (int)RPEInput - 5;
            if (TEInput == null)
                this.TEInputTextBox.Text = "";
            else
                this.TEInputTextBox.Text = Convert.ToString(TEInput);
            if (RepetitionsInput == null)
                this.RepInputTextBox.Text = "";
            else
                this.RepInputTextBox.Text = Convert.ToString(RepetitionsInput);
            if (SetsInput == null)
                this.SetsInputTextBox.Text = "";
            else
                this.SetsInputTextBox.Text = Convert.ToString(SetsInput);

            // Update export preview
            DateTime startDate;
            bool hasStartTime;
            TimeSpan duration;
            float? TE;
            int? cadenceAvg;
            float? distance;
            string comment;
            int? hrAvg;
            int? hrMax;
            int? intensity;
            int? kcal;
            string privateComment;
            int? repetitions;
            int? sets;
            TrackPoint[] trackPoints;
            fitnessDataHandler.GetExportData(activity,
                                  out startDate,
                                  out hasStartTime,
                                  out duration,
                                  out TE,
                                  out cadenceAvg,
                                  out distance,
                                  out comment,
                                  out hrAvg,
                                  out hrMax,
                                  out intensity,
                                  out kcal,
                                  out privateComment,
                                  out repetitions,
                                  out sets,
                                  out trackPoints);
            this.DateTextBox.Text = startDate.Date.ToString();
            this.DateTextBox.Text = this.DateTextBox.Text.Remove(10); //For some reason the Data.ToString method includes the time of midnight in its output. We have to remove that.
            if (hasStartTime)
                this.StartTimeTextBox.Text = startDate.TimeOfDay.ToString();
            else
                this.StartTimeTextBox.Text = "";
            this.DurationTextBox.Text = duration.ToString();
            this.TETextBox.Text = TE.ToString();
            this.CadenceTextBox.Text = cadenceAvg.ToString();
            this.DistanceTextBox.Text = distance.ToString();
            this.CommentTextBox.Text = comment;
            this.HRAvgTextBox.Text = hrAvg.ToString();
            this.HRMaxTextBox.Text = hrMax.ToString();
            this.RPEExportTextBox.Text = intensity.ToString();
            this.CaloriesTextBox.Text = kcal.ToString();
            this.PrivCommentTextBox.Text = privateComment;
            this.RepetitionsTextBox.Text = repetitions.ToString();
            this.SetsTextBox.Text = sets.ToString();

            bool boGPSAvail, boDistanceAvail, boAltitudeAvail, boHRAvail;
            fitnessDataHandler.GetAvailableTrackData(activity, out boGPSAvail, out boDistanceAvail, out boAltitudeAvail, out boHRAvail);
            this.DistanceCheckBox.CheckState = BoolToCheckState(boDistanceAvail);
            this.AltitudeCheckBox.CheckState = BoolToCheckState(boAltitudeAvail);
            this.HRCheckBox.CheckState = BoolToCheckState(boHRAvail);
            this.GPSCheckBox.CheckState = BoolToCheckState(boGPSAvail);

        }

        public void SetExported(bool boExported)
        {
            if (boExported)
            {
                this.ExportedCheckBox1.CheckState = CheckState.Checked;
            }
            else
            {
                this.ExportedCheckBox1.CheckState = CheckState.Unchecked;
            }
        }

        public void ThemeChanged(ZoneFiveSoftware.Common.Visuals.ITheme visualTheme)
        {
            List<ZoneFiveSoftware.Common.Visuals.TextBox> TextBoxes = new List<ZoneFiveSoftware.Common.Visuals.TextBox>();

            this.PanelChoiceActionBanner.ThemeChanged(visualTheme);
            this.BackColor = visualTheme.Control;
            this.InputsPanel.ThemeChanged(visualTheme);
            this.ExportPreviewPanel.ThemeChanged(visualTheme);

            foreach (System.Windows.Forms.Control control in InputsPanel.Controls)
            {
                TextBoxes.AddRange(FindTextBoxes(control));
            }
            foreach (System.Windows.Forms.Control control in ExportPreviewPanel.Controls)
            {
                TextBoxes.AddRange(FindTextBoxes(control));
            }
            
            foreach (ZoneFiveSoftware.Common.Visuals.TextBox TB in TextBoxes)
            {
                TB.ThemeChanged(visualTheme);
            }
            // ThemeChanged combo box looks worse when following the colors of the theme
            // since the frame and arrow remain white
            //this.RPEComboBox.BackColor = visualTheme.Window;
            //this.RPEComboBox.ForeColor = visualTheme.ControlText;
        }

        private List<ZoneFiveSoftware.Common.Visuals.TextBox> FindTextBoxes(Control control)
        {
            List<ZoneFiveSoftware.Common.Visuals.TextBox> TextBoxes = new List<ZoneFiveSoftware.Common.Visuals.TextBox>();

            foreach (System.Windows.Forms.Control ctrl in control.Controls)
            {
                if (ctrl.GetType().Equals(typeof(ZoneFiveSoftware.Common.Visuals.TextBox)))
                {
                    TextBoxes.Add((ZoneFiveSoftware.Common.Visuals.TextBox)ctrl);
                }
                else if (ctrl.GetType().Equals(typeof(GroupBox)))
                {
                    TextBoxes.AddRange(FindTextBoxes(ctrl));
                }
            }
            return (TextBoxes);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
        }

        private CheckState BoolToCheckState(bool Boolean)
        {
            if (Boolean)
                return CheckState.Checked;
            else
                return CheckState.Unchecked;
        }

        // Event handler: User has changed the contents of an input box
        private void NewUserInput(object sender, EventArgs e)
        {
            int? RPE, Rep, Sets;
            double? TE;
            // User has changed a value in the dialog. Check it, update the custom fields and refresh
            if (this.RPEComboBox.SelectedIndex == 0)
                RPE = null;
            else
                RPE = this.RPEComboBox.SelectedIndex + 5;

            if (this.TEInputTextBox.Text == "")
                TE = null;
            else
            {
                try
                {
                    TE = Convert.ToDouble(this.TEInputTextBox.Text);
                    TE = Math.Min(Math.Max((double)TE, 1.0), 5.0); // Limit TE to values between 1 and 5
                }
                catch
                {
                    TE = null; // Invalid value for TE - reset and let the user try again
                    this.TEInputTextBox.Text = "";
                }
            }


            if (this.RepInputTextBox.Text == "")
                Rep = null;
            else
                try
                {
                    Rep = Convert.ToInt16(this.RepInputTextBox.Text);
                }
                catch
                {
                    Rep = null; // Invalid value for Repetitions - reset and let the user try again
                    this.RepInputTextBox.Text = "";
                }

            if (this.SetsInputTextBox.Text == "")
                Sets = null;
            else
                try
                {
                    Sets = Convert.ToInt16(this.SetsInputTextBox.Text);
                }
                catch
                {
                    Sets = null; // Invalid value for sets - reset and let the user try again
                    this.SetsInputTextBox.Text = "";
                }

            fitnessDataHandler.SetCustomFieldsData(activity, RPE, TE, Rep, Sets);

        }

        // Event handler: activity properties (maybe custom fields data!) has changed
        public void ActivityPropChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshInfo();
        }

        private void CheckAndCorrectRPEAndTE()
        {
            bool modifyCustData = false;

            // Check valid range of data
            int? RPE, Repetitions, Sets;
            double? TE;
            fitnessDataHandler.GetCustomFieldsData(activity,
                                out RPE,
                                out TE,
                                out Repetitions,
                                out Sets);

            if (RPE != null)
            {
                if (RPE > 20 || RPE < 6)
                {
                    RPE = null;
                    modifyCustData = true;
                }
            }
            if (TE != null)
            {
                if (TE < 1 || TE > 5)
                {
                    TE = null;
                    modifyCustData = true;
                }
            }

            if (modifyCustData)
            {
                fitnessDataHandler.SetCustomFieldsData(activity, RPE, TE, Repetitions, Sets);
            }
        }

        void PanelChoiceActionBanner_MenuClicked(object sender, System.EventArgs e)
        {
            PanelChoiceActionBanner.ContextMenuStrip.Width = 100;
            PanelChoiceActionBanner.ContextMenuStrip.Show(PanelChoiceActionBanner.Parent.PointToScreen(new System.Drawing.Point(PanelChoiceActionBanner.Right - PanelChoiceActionBanner.ContextMenuStrip.Width - 2,
                PanelChoiceActionBanner.Bottom + 1)));
        }

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainSplitContainer.Panel1Collapsed = false;
            MainSplitContainer.Panel2Collapsed = true;
            this.PanelChoiceActionBanner.Text = "User Input";

        }

        private void exportPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainSplitContainer.Panel1Collapsed = true;
            MainSplitContainer.Panel2Collapsed = false;
            this.PanelChoiceActionBanner.Text = "Export Preview";
        }



#endif
    }
}
