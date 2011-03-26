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
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using System.Drawing;
using System.Windows.Forms;
using Janohl.ST2Funbeat.Settings;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;
using Janohl.ST2Funbeat.se.funbeat.api;
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Util;
#endif

namespace Janohl.ST2Funbeat
{
    public class TransferActivity : IAction
    {
#if !ST_2_1
        public TransferActivity(IDailyActivityView view)
        {
            this.dailyView = view;
        }
        public TransferActivity(IActivityReportsView view)
        {
            this.reportView = view;
        }
#endif
        public TransferActivity(IList<IActivity> activities)
        {
            this.activities = activities;
        }

        #region IAction Members

        public bool Enabled
        {
            get { return activities != null; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public IList<string> MenuPath
        {
            get
            {
                return new List<string>();
            }
        }

        public System.Drawing.Image Image
        {
            get { return Properties.Resources.FunbeatIcon; }
        }

        public void Refresh()
        {
            //NOOP
        }


        public void Run(System.Drawing.Rectangle rectButton)
        {
            FitnessDataHandler dataHandler = Plugin.dataHandler;

            foreach (IActivity activity in activities)
            {

                bool okToExport = !dataHandler.boGetExported(activity);
                if (!okToExport)
                    okToExport = MessageBox.Show("Already exported once, export again?", "Re-Export", MessageBoxButtons.YesNo) == DialogResult.Yes;

                if (okToExport)
                {
                    try
                    {
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
                        TrainingInterval[] laps;
                        TrackPoint[] trackPoints;

                        
                        int funbeatActivityTypeID = Settings.Settings.GetFunbeatActivityTypeID(activity.Category);
                        string[] equipment = Settings.Settings.GetFunbeatEquipment(activity.EquipmentUsed);

                        dataHandler.GetExportData(activity,
                            Settings.Settings.Instance.boExportNameInComment,
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
                            out laps,
                            out trackPoints);

                        int? id = FunbeatService.SendTraining(
                            startDate,
                            hasStartTime,
                            duration,
                            TE,
                            cadenceAvg,
                            distance,
                            comment,
                            hrAvg,
                            hrMax,
                            intensity,
                            kcal,
                            privateComment,
                            repetitions,
                            sets,
                            funbeatActivityTypeID,
                            laps,
                            trackPoints,
                            equipment);

                        if (id.HasValue)
                            dataHandler.SetExported(activity, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        public string Title
        {
            get { return Properties.Resources.TransferActivity_Title; }
        }

        public bool Visible
        {
            get
            {
                if (activities.Count > 0) return true;
                return false;
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

#pragma warning disable 67
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
#if !ST_2_1
        private IDailyActivityView dailyView = null;
        private IActivityReportsView reportView = null;
#endif
        private IList<IActivity> _activities = null;
        private IList<IActivity> activities
        {
            get
            {
#if !ST_2_1
                //activities are set either directly or by selection,
                //not by more than one
                if (_activities == null)
                {
                    if (dailyView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IActivity>(dailyView.SelectionProvider.SelectedItems);
                    }
                    else if (reportView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IActivity>(reportView.SelectionProvider.SelectedItems);
                    }
                    else
                    {
                        return new List<IActivity>();
                    }
                }
#endif
                return _activities;
            }
            set
            {
                _activities = value;
            }
        }
    }
}
