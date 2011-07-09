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
using System.Runtime.Serialization;
using System.IO;
using Janohl.ST2Funbeat.Settings;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Security.Cryptography;
using Janohl.ST2Funbeat.se.funbeat.api;
using FunbeatDll;

namespace Janohl.ST2Funbeat
{
    public class FunbeatService
    {
        private readonly static string appID = "7b93365a-960f-44cf-958a-af030f4c3c68";
        private readonly static string clientID = "ST2Funbeat";
        private static FunbeatService instance;
        private static FunbeatService Instance
        {
            get
            {
                if (instance == null)
                    instance = new FunbeatService();
                return instance;
            }
        }

        private FunbeatService()
        {
        }

        public static int? SendTraining(DateTime startDate, bool hasStartTime, TimeSpan duration, float?TE, int? cadenceAvg, float? distance, string comment,
            int? hrAvg, int? hrMax, int? intensity, int? kcal, string privateComment, int? repetitions, int? sets,
            int trainingType, TrainingInterval[] laps, TrackPoint[] trackPoints, string[] equipment)
        {
            if (Settings.Settings.Instance.User.Username.Length == 0 || Settings.Settings.Instance.User.Password.Length == 0)
            {
                MessageBox.Show("Funbeat user name and password must be entered in the ST2funbeat settings", "Login failure", MessageBoxButtons.OK);
                return null;
            }
            else if (Settings.Settings.Instance.User.LoginId.Length == 0 || Settings.Settings.Instance.User.LoginSecret.Length == 0)
            {
                MessageBox.Show("Funbeat user name and password could not be confirmed. Make sure they are correct and that you are connected to the internet", "Login failure", MessageBoxButtons.OK);
                return null;
            }
            else
            {
                Training training = new Training();
                training.Description = comment;
                training.Distance = distance;
                training.StartDateTime = startDate;
                training.HasTimeOfDay = hasStartTime;
                training.Duration = new Duration();
                training.Duration.Hours = duration.Hours;
                training.Duration.Minutes = duration.Minutes;
                training.Duration.Seconds = duration.Seconds;

                training.TE = TE;
                training.CadAvg = cadenceAvg;
                training.HRAvg = hrAvg;
                training.HRMax = hrMax;
                training.Intensity = intensity;
                training.KCal = kcal;
                training.PrivateComment = privateComment;
                training.Repetitions = repetitions;
                training.Sets = sets;
                training.TrainingTypeID = trainingType;
                training.IntervalsAndLaps = laps;
                training.NewRouteName = startDate.ToString();
                training.NewRoutePrivacy = Privacy.NotSet;
                training.Equipment = equipment;

                // Workaround for the funbeat server not working when two or more subsequent GPS points have identical position
                // Ought to be fixed on the server side
                if (trackPoints != null)
                {
                    List<TrackPoint> trackPointsList = new List<TrackPoint>(trackPoints);
                    if (trackPointsList.Count > 0)
                    {
                        for (int i = trackPointsList.Count - 1; i > 0; i--)
                        {
                            TrackPoint tp = trackPointsList[i];
                            if (tp.Latitude == trackPointsList[i - 1].Latitude && tp.Longitude == trackPointsList[i - 1].Longitude)
                            {
                                tp.Latitude = null;
                                tp.Longitude = null;
                            }
                            if (!tp.Altitude.HasValue && !tp.Cad.HasValue && !tp.Distance.HasValue && !tp.HR.HasValue &&
                                !tp.Latitude.HasValue && !tp.Longitude.HasValue && !tp.Pace.HasValue && !tp.Power.HasValue &&
                                !tp.Speed.HasValue)
                                trackPointsList.Remove(tp);
                        }

                    }
                    training.TrackPoints = trackPointsList.ToArray();
                }
                else
                {
                    training.TrackPoints = trackPoints; // i.e. null
                }

#if DEBUG                
                FileInfo t = new FileInfo("Training.txt");
                StreamWriter Tex = t.CreateText();
                Tex.WriteLine("Description: " + comment.ToString());
                Tex.WriteLine("Distance: " + distance.ToString());
                Tex.WriteLine("StartDateTime: " + startDate.ToString());
                Tex.WriteLine("HasTimeOfDay: " + hasStartTime.ToString());
                Tex.WriteLine("Duration: " + duration.ToString());
                Tex.WriteLine("TE: " + TE.ToString());
                Tex.WriteLine("CadAvg: " + cadenceAvg.ToString());
                Tex.WriteLine("HRAvg: " + hrAvg.ToString());
                Tex.WriteLine("HRMax: " + hrMax.ToString());
                Tex.WriteLine("Intensity: " + intensity.ToString());
                Tex.WriteLine("KCal: " + kcal.ToString());
                Tex.WriteLine("PrivateComment: " + privateComment.ToString());
                Tex.WriteLine("Repetitions: " + repetitions.ToString());
                Tex.WriteLine("Sets: " + sets.ToString());
                Tex.WriteLine("TrainingTypeID: " + trainingType.ToString());
                Tex.WriteLine("NewRouteName: " + startDate.ToString());
                Tex.WriteLine("NewRoutePrivacy: " + training.NewRoutePrivacy.ToString());
                Tex.WriteLine();
                if (trackPoints != null)
                {
                    for(int i=0; i<trackPoints.Length; i++)
                    {
                        TrackPoint tp = trackPoints[i];
                        Tex.WriteLine("Trackpoint");
                        Tex.WriteLine("isStartPoint: " + tp.isStartPoint.ToString());
                        Tex.WriteLine("TimeStamp: " + tp.TimeStamp.ToString());
                        Tex.WriteLine("HR: " + tp.HR.ToString());
                        Tex.WriteLine("Latitude: " + tp.Latitude.ToString());
                        Tex.WriteLine("Longitude: " + tp.Longitude.ToString());
                        Tex.WriteLine("Pace: " + tp.Pace.ToString());
                        Tex.WriteLine("Power: " + tp.Power.ToString());
                        Tex.WriteLine("Speed: " + tp.Speed.ToString());
                        Tex.WriteLine("Altitude: " + tp.Altitude.ToString());
                        Tex.WriteLine("Cadence: " + tp.Cad.ToString());
                        Tex.WriteLine("Distance: " + tp.Distance.ToString());
                        Tex.WriteLine("TrackPoint no;" + i.ToString());

                        if (i>0)
                        {
                            if (tp.Latitude == trackPoints[i - 1].Latitude && tp.Longitude == trackPoints[i - 1].Longitude)
                            {
                                Tex.WriteLine("Marker: Same latitude and longitude");
                            }
                            else
                            {
                                if (tp.Latitude == trackPoints[i - 1].Latitude)
                                    Tex.WriteLine("Marker: Same latitude");
                                if (tp.Longitude == trackPoints[i - 1].Longitude)
                                    Tex.WriteLine("Marker: Same longitude");
                            }
                            if (tp.TimeStamp.CompareTo(trackPoints[i - 1].TimeStamp) == 0)
                                Tex.WriteLine("Marker: Same time");
                        }
                        
                        Tex.WriteLine();
                    }
                }

                if(equipment != null)
                {
                    for (int i = 0; i < equipment.Length; i++)
                    {
                        Tex.WriteLine("Equipment: " + equipment[i]);
                    }
                }
                Tex.Close();
#endif

                try
                {
                    MobileService client = new MobileService();
                    ClientServerRelationResult result = client.SaveTraining(appID, 
                                                                            Settings.Settings.Instance.User.LoginId, 
                                                                            Settings.Settings.Instance.User.LoginSecret, 
                                                                            training, 
                                                                            clientID);
                    if (result != null)
                        return result.ServerID;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Concat("Problem sending the activity to funbeat\n", ex.Message), "Funbeat communication error");
                    return null;
                }
            }
        }

        public static bool CreateLogin(string userName, string password, out string loginId, out string loginSecret)
        {
            MobileService client = new MobileService();
            try
            {
                FunbeatDll.FunbeatEncryption.CreateLogin(Plugin.GetApplication(), appID, userName, password, out loginId, out loginSecret);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Problem communicating login information with funbeat\n", ex.Message), "Funbeat communication error");
                loginId = "";
                loginSecret = "";
                return false;
            }
        }

        private static Dictionary<int, string> GetTrainingTypes()
        {
            MobileService client = new MobileService();
            TrainingType[] types = client.GetTrainingTypes(appID, "sv-SE");
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (TrainingType t in types)
                result.Add(t.TrainingTypeID, t.TrainingTypeName);

            return result;
        }

        private static IList<FunbeatActivityType> funbeatActivityTypes;
        public static IList<FunbeatActivityType> FunbeatActivityTypes
        {
            get
            {
                if (funbeatActivityTypes == null)
                {
                    try
                    {
                        funbeatActivityTypes = new Collection<FunbeatActivityType>();
                        Dictionary<int, string> funbeatValues = FunbeatService.GetTrainingTypes();
                        foreach (int i in funbeatValues.Keys)
                            funbeatActivityTypes.Add(new FunbeatActivityType(i, funbeatValues[i]));
                    }
                    catch (Exception ex)
                    {
                        funbeatActivityTypes = null;
                        MessageBox.Show(string.Concat("The settings page needs to communicate with funbeat. Make sure that you are connected to the internet.\n", ex.Message),
                                        "Funbeat communication error");
                        throw;
                    }
                }
                return funbeatActivityTypes;
            }
        }

        public class FunbeatActivityType
        {
            public int Id;
            public string Name;

            public FunbeatActivityType(int id, string name){
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private static EquipmentShortInfo[] GetEquipment()
        {
            if (Settings.Settings.Instance.User.Username.Length == 0 || Settings.Settings.Instance.User.Password.Length == 0)
            {
                MessageBox.Show("Funbeat user name and password must be entered in the ST2funbeat settings", "Login failure", MessageBoxButtons.OK);
                return new EquipmentShortInfo[0];
            }
            else if (Settings.Settings.Instance.User.LoginId.Length == 0 || Settings.Settings.Instance.User.LoginSecret.Length == 0)
            {
                MessageBox.Show("Funbeat user name and password could not be confirmed. Make sure they are correct and that you are connected to the internet", "Login failure", MessageBoxButtons.OK);
                return new EquipmentShortInfo[0];
            }
            else
            {
                try
                {
                    MobileService client = new MobileService();
                    EquipmentShortInfo[] equipment = client.GetMyEquipment(appID,
                                                                       Settings.Settings.Instance.User.LoginId,
                                                                       Settings.Settings.Instance.User.LoginSecret);
                    return equipment;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Concat("Problem communicating equipment information with funbeat\n", ex.Message), "Funbeat communication error");
                    return new EquipmentShortInfo[0];
                }

            }
        }

        private static IList<string> funbeatEquipment;
        public static IList<string> FunbeatEquipment
        {
            get
            {
                if (funbeatEquipment == null)
                {
                    try
                    {
                        funbeatEquipment = new Collection<string>();
                        funbeatEquipment.Add(""); // Must have the possibility not to map to a funbeat equipment
                        EquipmentShortInfo[] eqInfoArray = FunbeatService.GetEquipment();
                        foreach (EquipmentShortInfo ei in eqInfoArray)
                            funbeatEquipment.Add(ei.EquipmentTitle);
                    }
                    catch (Exception ex)
                    {
                        funbeatEquipment = new Collection<string>();
                        MessageBox.Show(string.Concat("The settings page needs to communicate with funbeat. Make sure that you are connected to the internet.\n", ex.Message),
                                        "Funbeat communication error");
                        throw;
                    }
                }
                return funbeatEquipment;
            }
        }

    }
}
