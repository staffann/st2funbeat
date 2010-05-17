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

namespace Janohl.ST2Funbeat
{
    public class TransferActivity : IAction
    {
        private IList<IActivity> activities;

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
                        hrAvg = (int)activityInfo.AverageHeartRate;
                        if (hrAvg == 0)
                            hrAvg = null;
                        hrMax = (int)activityInfo.MaximumHeartRate;
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

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
