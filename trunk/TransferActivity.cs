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
using Janohl.ST2Funbeat.Funbeat;
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
            foreach (IActivity activity in activities)
            {

                bool okToExport = activity.Metadata.Source.IndexOf("Funbeated") < 0;
                if (!okToExport)
                    okToExport = MessageBox.Show("Already exported once, export again?", "Re-Export", MessageBoxButtons.YesNo) == DialogResult.Yes;

                if (okToExport)
                {
                    try
                    {
                        int funbeatActivityTypeID = Settings.Settings.GetFunbeatActivityTypeID(activity.Category);
                        TrackPoint[] trackPoints = GetTrackPoints(activity);
                        int? hrAvg;
                        int? hrMax;

                        // Get pulse data. Works with and without a HR data track.
                        ActivityInfoCache actInfoCache = new ActivityInfoCache();
                        ActivityInfo activityInfo = actInfoCache.GetInfo(activity);
                        hrAvg = (int?)activityInfo.AverageHeartRate;
                        if (hrAvg == 0)
                            hrAvg = null;
                        hrMax = (int?)activityInfo.MaximumHeartRate;
                        if (hrMax == 0)
                            hrMax = null;

                        DateTime activityDate = ConvertToLocalTime(activity.StartTime);

                        int? id = FunbeatService.SendTraining(
                            activityDate,
                            activity.HasStartTime,
                            activity.TotalTimeEntered,
                            activity.TotalDistanceMetersEntered / 1000,
                            activity.Notes,
                            hrAvg,
                            hrMax,
                            null,
                            (int)activity.TotalCalories,
                            "SportsTracks reference: " + activity.ReferenceId,
                            null,
                            null,
                            funbeatActivityTypeID,
                            trackPoints);

                        if (id.HasValue)
                            activity.Metadata.Source += "Funbeated";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private TrackPoint[] GetTrackPoints(IActivity activity)
        {
            if (activity.GPSRoute != null)
            {
                // GPS track exists. Base the trackpoints on that and use HR data if available
                return GetTrackPointsFromGPSTrack(activity);
            }
            else
            {
                // Get any track data that exists
                return GetTrackPointsFromDataTrack(activity);
            }
#if OLDERVERSION
            else if (activity.HeartRatePerMinuteTrack != null)
            {
                // Base the trackpoints on HR data and enter null for the GPS data.
                return GetTrackPointsFromHRTrack(activity);
            }
            else
            {
                // No data track exists.
                return null;
            }
#endif
        }

        // Use the GPS track as a base when creating the TrackPoints.
        // Use heartbeat data if available
        private TrackPoint[] GetTrackPointsFromGPSTrack(IActivity activity)
        {
            if (activity.GPSRoute == null)
                return null;
            List<TrackPoint> tps = new List<TrackPoint>();
            double accDistance = 0;
            ITimeValueEntry<IGPSPoint> prevPoint = null;
            foreach (ITimeValueEntry<IGPSPoint> p in activity.GPSRoute)
            {

                TrackPoint tp = new TrackPoint();
                tp.DateTime = ConvertToLocalTime(activity.StartTime.AddSeconds(p.ElapsedSeconds));

                DateTime actualTime = activity.StartTime.AddSeconds(p.ElapsedSeconds);

                if (activity.HeartRatePerMinuteTrack != null)
                {
                    if (actualTime < activity.HeartRatePerMinuteTrack.StartTime)
                        actualTime = activity.HeartRatePerMinuteTrack.StartTime;

                    ITimeValueEntry<float> interpolatedHR = activity.HeartRatePerMinuteTrack.GetInterpolatedValue(actualTime);
                    if (interpolatedHR != null)
                    {
                        float heartRate = interpolatedHR.Value;
                        tp.HR = Convert.ToInt32(heartRate);
                    }
                    else
                    {
                        tp.HR = null;
                    }
                }
                else
                {
                    tp.HR = null;
                }

                tp.Latitude = Convert.ToDouble(p.Value.LatitudeDegrees);
                tp.Longitude = Convert.ToDouble(p.Value.LongitudeDegrees);
                tp.Altitude = Convert.ToDouble(p.Value.ElevationMeters);
                if (double.IsNaN((double)tp.Altitude))
                {
                    tp.Altitude = null;
                }

                if (prevPoint != null)
                    accDistance += p.Value.DistanceMetersToPoint(prevPoint.Value);
                tp.Distance = accDistance / 1000;


                tps.Add(tp);
                prevPoint = p;
            }
            return tps.ToArray();
        }

        // Use any data track as a base when creating the TrackPoints.
        // Set GPS properties to null
        private TrackPoint[] GetTrackPointsFromDataTrack(IActivity activity)
        {
            ITimeDataSeries<float> ReferenceTrack;
            if (activity.DistanceMetersTrack != null)
                ReferenceTrack = activity.DistanceMetersTrack;
            else if (activity.HeartRatePerMinuteTrack != null)
                ReferenceTrack = activity.HeartRatePerMinuteTrack;
            else if (activity.ElevationMetersTrack != null)
                ReferenceTrack = activity.ElevationMetersTrack;
            else
                return null;

            List<TrackPoint> tps = new List<TrackPoint>();
            foreach (ITimeValueEntry<float> p in ReferenceTrack)
            {

                TrackPoint tp = new TrackPoint();
                tp.DateTime = ConvertToLocalTime(activity.StartTime.AddSeconds(p.ElapsedSeconds));

                DateTime actualTime = activity.StartTime.AddSeconds(p.ElapsedSeconds);

                // Get distance
                if (activity.DistanceMetersTrack != null)
                {
                    if (actualTime < activity.DistanceMetersTrack.StartTime)
                        actualTime = activity.DistanceMetersTrack.StartTime;

                    ITimeValueEntry<float> interpolatedDistance = activity.DistanceMetersTrack.GetInterpolatedValue(actualTime);
                    if (interpolatedDistance != null)
                    {
                        tp.Distance = interpolatedDistance.Value/1000;
                        if (double.IsNaN((double)tp.Distance))
                        {
                            tp.Distance = null;
                        }
                    }
                    else
                    {
                        tp.Distance = null;
                    }
                }
                else
                {
                    tp.Distance = null;
                }

                // Get elevation track
                if (activity.ElevationMetersTrack != null)
                {
                    if (actualTime < activity.ElevationMetersTrack.StartTime)
                        actualTime = activity.ElevationMetersTrack.StartTime;

                    ITimeValueEntry<float> interpolatedElevation = activity.ElevationMetersTrack.GetInterpolatedValue(actualTime);
                    if (interpolatedElevation != null)
                    {
                        tp.Altitude = interpolatedElevation.Value;
                        if (double.IsNaN((double)tp.Altitude))
                        {
                            tp.Altitude = null;
                        }

                    }
                    else
                    {
                        tp.Altitude = null;
                    }
                }
                else
                {
                    tp.Altitude = null;
                }

                // Get heart rate track
                if (activity.HeartRatePerMinuteTrack != null)
                {
                    if (actualTime < activity.HeartRatePerMinuteTrack.StartTime)
                        actualTime = activity.HeartRatePerMinuteTrack.StartTime;

                    ITimeValueEntry<float> interpolatedHR = activity.HeartRatePerMinuteTrack.GetInterpolatedValue(actualTime);
                    if (interpolatedHR != null)
                    {
                        float heartRate = interpolatedHR.Value;
                        tp.HR = Convert.ToInt32(heartRate);
                        if (double.IsNaN((double)tp.HR))
                        {
                            tp.HR = null;
                        }
                    }
                    else
                    {
                        tp.HR = null;
                    }
                }
                else
                {
                    tp.HR = null;
                }

                tp.Latitude = null;
                tp.Longitude = null;

                tps.Add(tp);
            }
            return tps.ToArray();
        }

        // Use the HR track as a base when creating the TrackPoints.
        // Set GPS properties to null
        private TrackPoint[] GetTrackPointsFromHRTrack(IActivity activity)
        {
            List<TrackPoint> tps = new List<TrackPoint>();
            foreach (ITimeValueEntry<float> p in activity.HeartRatePerMinuteTrack)
            {

                TrackPoint tp = new TrackPoint();
                tp.DateTime = ConvertToLocalTime(activity.StartTime.AddSeconds(p.ElapsedSeconds));
                tp.HR = (int?)p.Value;
                tp.Latitude = null;
                tp.Longitude = null; ;
                tp.Altitude = null;
                tp.Distance = null;

                tps.Add(tp);
            }
            return tps.ToArray();
        }

        private DateTime ConvertToLocalTime(DateTime utc)
        {
            TimeSpan diff = new TimeSpan(DateTime.Now.Ticks - DateTime.UtcNow.Ticks);
            return utc.AddHours(diff.Hours);
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
        IList<ItemType> GetAllContainedItems<ItemType>(ISelectionProvider selectionProvider)
        {
            List<ItemType> items = new List<ItemType>();
            foreach (ItemType item in CollectionUtils.GetItemsOfType<ItemType>(selectionProvider.SelectedItems))
            {
                if (!items.Contains(item)) items.Add(item);
            }
            AddGroupItems<ItemType>(CollectionUtils.GetItemsOfType<IGroupedItem<ItemType>>(
                                    selectionProvider.SelectedItems), items);
            return items;
        }

        void AddGroupItems<ItemType>(IList<IGroupedItem<ItemType>> groups, IList<ItemType> allItems)
        {
            foreach (IGroupedItem<ItemType> group in groups)
            {
                foreach (ItemType item in group.Items)
                {
                    if (!allItems.Contains(item)) allItems.Add(item);
                }
                AddGroupItems(group.SubGroups, allItems);
            }
        }
#endif
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
                        return GetAllContainedItems<IActivity>(dailyView.SelectionProvider);
                    }
                    else if (reportView != null)
                    {
                        return GetAllContainedItems<IActivity>(reportView.SelectionProvider);
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
