using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using System.Drawing;
using System.Windows.Forms;
using Janohl.ST2Funbeat.Settings;
using Janohl.Funbeat;
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
                if (activity.Metadata.Source.IndexOf("Funbeated") < 0)
                {
                    try
                    {
                        int funbeatActivityTypeID = Settings.Settings.GetFunbeatActivityTypeID(activity.Category);
                        TrackPoint[] trackPoints = GetTrackPoints(activity.GPSRoute);
                        int? hrAvg;
                        int? hrMax;
                        if (activity.HeartRatePerMinuteTrack == null)
                        {
                            hrAvg = null;
                            hrMax = null;
                        }
                        else
                        {
                            hrAvg = (int)activity.HeartRatePerMinuteTrack.Avg;
                            hrMax = (int)activity.HeartRatePerMinuteTrack.Max;
                        }

                        TimeSpan diff = new TimeSpan(DateTime.Now.Ticks - DateTime.UtcNow.Ticks);
                        DateTime activityDate = activity.StartTime.AddHours(diff.Hours);

                        int? id = FunbeatService.SendTraining(
                            activityDate,    
                            activity.TotalTimeEntered,
                            activity.TotalDistanceMetersEntered / 1000,
                            activity.Notes,
                            hrAvg,
                            hrMax,
                            activity.Intensity * 2,
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
                else
                    MessageBox.Show(string.Format(Properties.Resources.AlreadyExported, activity));
            }
        }

        private TrackPoint[] GetTrackPoints(ZoneFiveSoftware.Common.Data.GPS.IGPSRoute route)
        {
            if (route == null)
                return null;
            List<TrackPoint> tps = new List<TrackPoint>();
            foreach (ITimeValueEntry<IGPSPoint> p in route)
            {
                TrackPoint tp = new TrackPoint();
                tp.Altitude = Convert.ToDouble(p.Value.ElevationMeters);
                tp.Latitude = Convert.ToDouble(p.Value.LatitudeDegrees);
                tp.Longitude = Convert.ToDouble(p.Value.LongitudeDegrees);
                
                tps.Add(tp);
            }
            return tps.ToArray();
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
