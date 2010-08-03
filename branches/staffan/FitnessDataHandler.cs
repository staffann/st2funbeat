﻿using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
using Janohl.ST2Funbeat.Funbeat;

namespace Janohl.ST2Funbeat
{
    // This class handles all access to fitness data.
    // Only one instance should exist in the complete plugin
    class FitnessDataHandler
    {
        private Guid RPEGuid = new Guid("B1383C07-CB6E-407E-A78E-7E8DD2FEB5E1");
        private Guid FunbeatExportedGuid = new Guid("16D4BBF1-DDBF-4189-9558-2458D4FD03AB");
        private Guid TEGuid = new Guid("AB32B8CE-30E2-4795-A21B-51C7C481F8F5");
        private Guid RepetitionsGuid = new Guid("684AC20E-F90A-4410-9413-93DF8340872E");
        private Guid SetsGuid = new Guid("86D40A63-E56E-421B-9D13-EE6A372DED4B");

        ICustomDataFieldDefinition RPEField = null;
        ICustomDataFieldDefinition FunbeatExportedField = null;
        ICustomDataFieldDefinition TEField = null;
        ICustomDataFieldDefinition RepetitionsField = null;
        ICustomDataFieldDefinition SetsField = null;

        public FitnessDataHandler(ILogbook Logbook, Guid PluginGuid)
        {
            CheckCustomDataFields(Logbook, PluginGuid);
        }

        public void CheckCustomDataFields(ILogbook Logbook, Guid PluginGuid)
        {
            // Check for custom data fields in the logbook. If they aren't there, create them!
            foreach (ICustomDataFieldDefinition custDataDef in Logbook.CustomDataFieldDefinitions )
            {
                if(custDataDef.Id == RPEGuid)
                {
                    RPEField = custDataDef;
                }
                else if(custDataDef.Id == FunbeatExportedGuid)
                {
                    FunbeatExportedField = custDataDef;
                }
                else if (custDataDef.Id == TEGuid)
                {
                    TEField = custDataDef;
                }
                else if (custDataDef.Id == RepetitionsGuid)
                {
                    RepetitionsField = custDataDef;
                }
                else if (custDataDef.Id == SetsGuid)
                {
                    SetsField = custDataDef;
                }
            }
            if(RPEField == null)
            {
                RPEField = Logbook.CustomDataFieldDefinitions.Add(RPEGuid, 
                                                       CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity)),
                                                       CustomDataFieldDefinitions.StandardDataType(CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId),
                                                       "RPE (Borg Scale)");
                RPEField.CreatedBy = PluginGuid;
            }
            if (FunbeatExportedField == null)
            {
                FunbeatExportedField = Logbook.CustomDataFieldDefinitions.Add(FunbeatExportedGuid,
                                                       CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity)),
                                                       CustomDataFieldDefinitions.StandardDataType(CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId),
                                                       "Exported to Funbeat");
                FunbeatExportedField.CreatedBy = PluginGuid;
            }
            if (TEField == null)
            {
                TEField = Logbook.CustomDataFieldDefinitions.Add(TEGuid,
                                                       CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity)),
                                                       CustomDataFieldDefinitions.StandardDataType(CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId),
                                                       "Training Effect (TE)");
                TEField.CreatedBy = PluginGuid;
            }
            if (RepetitionsField == null)
            {
                RepetitionsField = Logbook.CustomDataFieldDefinitions.Add(RepetitionsGuid,
                                                       CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity)),
                                                       CustomDataFieldDefinitions.StandardDataType(CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId),
                                                       "Repetitions");
                RepetitionsField.CreatedBy = PluginGuid;
            }
            if (SetsField == null)
            {
                SetsField = Logbook.CustomDataFieldDefinitions.Add(SetsGuid,
                                                       CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity)),
                                                       CustomDataFieldDefinitions.StandardDataType(CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId),
                                                       "Sets");
                SetsField.CreatedBy = PluginGuid;
            }
        }

        public void GetCustomFieldsData(IActivity activity, 
                                        out int? RPE, 
                                        out double? TE, 
                                        out int? Repetitions, 
                                        out int? Sets)
        {
            if (activity != null)
            {
                RPE = (int?)(activity.GetCustomDataValue(RPEField) as double?);
                TE = activity.GetCustomDataValue(TEField) as double?;
                Repetitions = (int?)(activity.GetCustomDataValue(RepetitionsField) as double?);
                Sets = (int?)(activity.GetCustomDataValue(SetsField) as double?);
            }
            else
            {
                RPE = null;
                TE = null;
                Repetitions = null;
                Sets = null;
            }
        }

        public void SetCustomFieldsData(IActivity activity,
                                int? RPE,
                                double? TE,
                                int? Repetitions,
                                int? Sets)
        {
            if (activity != null)
            {
                int? CurrentRPE, CurrentRepetitions, CurrentSets;
                double? CurrentTE;
                GetCustomFieldsData(activity,
                                    out CurrentRPE,
                                    out CurrentTE,
                                    out CurrentRepetitions,
                                    out CurrentSets);
                if (CurrentRPE != RPE)
                    activity.SetCustomDataValue(RPEField, (double?)RPE);
                if (CurrentTE != TE)
                    activity.SetCustomDataValue(TEField, TE);
                if (CurrentRepetitions != Repetitions)
                    activity.SetCustomDataValue(RepetitionsField, (double?)Repetitions);
                if (CurrentSets != Sets)
                    activity.SetCustomDataValue(SetsField, (double?)Sets);
            }
        }

        public void GetExportData(IActivity activity,
                                  out DateTime startDate,
                                  out bool hasStartTime,
                                  out TimeSpan duration,
                                  out float? TE,
                                  out int? cadenceAvg,
                                  out float? distance,
                                  out string comment,
                                  out int? hrAvg,
                                  out int? hrMax,
                                  out int? intensity,
                                  out int? kcal,
                                  out string privateComment,
                                  out int? repetitions,
                                  out int? sets,
                                  out TrackPoint[] trackPoints)
        {
            if (activity != null)
            {

                int? RPECustFieldData, RepetitionsCustFieldData, SetsCustFieldData;
                double? TECustFieldData;
                GetCustomFieldsData(activity,
                                    out RPECustFieldData,
                                    out TECustFieldData,
                                    out RepetitionsCustFieldData,
                                    out SetsCustFieldData);

                // Get pulse data. Works with and without a HR data track.
                ActivityInfoCache actInfoCache = new ActivityInfoCache();
                ActivityInfo activityInfo = actInfoCache.GetInfo(activity);
                if (activityInfo.AverageHeartRate == 0)
                    hrAvg = null;
                else
                    hrAvg = (int?)Math.Round(activityInfo.AverageHeartRate);
                if (activityInfo.MaximumHeartRate == 0)
                    hrMax = null;
                else
                    hrMax = (int?)Math.Round(activityInfo.MaximumHeartRate);
                if (activityInfo.AverageCadence == 0)
                    cadenceAvg = null;
                else
                    cadenceAvg = (int?)Math.Round(activityInfo.AverageCadence);

                startDate = ConvertToLocalTime(activity.StartTime);
                hasStartTime = activity.HasStartTime;
                duration = activity.TotalTimeEntered;
                TE = (float?)TECustFieldData;
                distance = activity.TotalDistanceMetersEntered / 1000;
                comment = activity.Notes;
                intensity = RPECustFieldData;
                if (activity.TotalCalories == 0)
                    kcal = null;
                else
                    kcal = (int?)Math.Round(activity.TotalCalories);
                privateComment = "SportsTracks reference: " + activity.ReferenceId;
                repetitions = RepetitionsCustFieldData;
                sets = SetsCustFieldData;
                trackPoints = GetTrackPoints(activity);
            }
            else
            {                
                startDate = new DateTime(1900, 1, 1, 0, 0, 0);
                hasStartTime = false;
                duration = new TimeSpan(0, 0, 0);
                TE = null;
                cadenceAvg = null;
                distance = null;
                comment = "";
                hrAvg = null;
                hrMax = null;
                intensity = null;
                kcal = null;
                privateComment = "";
                repetitions = null;
                sets = null;
                trackPoints = null;

            }
        }

        public void GetAvailableTrackData(IActivity activity,
                                          out bool boGPSCoordAvailable,
                                          out bool boDistanceAvailable,
                                          out bool boAltitudeAvailable,
                                          out bool boHeartRateAvailable)
        {
            if (activity != null)
            {
                if (activity.GPSRoute != null)
                {
                    boGPSCoordAvailable = true;
                    boDistanceAvailable = true;
                    boAltitudeAvailable = true;
                }
                else
                {
                    boGPSCoordAvailable = false;
                    boDistanceAvailable = (activity.DistanceMetersTrack != null);
                    boAltitudeAvailable = (activity.ElevationMetersTrack != null);
                }

                boHeartRateAvailable = (activity.HeartRatePerMinuteTrack != null);
            }
            else
            {
                boGPSCoordAvailable = false;
                boDistanceAvailable = false;
                boAltitudeAvailable = false;
                boHeartRateAvailable = false;
            }
        }

        public bool boGetExported(IActivity activity)
        {
            if (activity != null)
                return (activity.Metadata.Source.IndexOf("Funbeated") >= 0);
            else
                return false;
        }

        public void SetExported(IActivity activity, bool boExported)
        {
            if (activity != null)
            {
                if (boExported)
                    activity.Metadata.Source += "Funbeated";
                else
                {
                    //TODO: Add code to remove the Funbeated string   
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
                        tp.Distance = interpolatedDistance.Value / 1000;
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

        private DateTime ConvertToLocalTime(DateTime utc)
        {
            TimeSpan diff = new TimeSpan(DateTime.Now.Ticks - DateTime.UtcNow.Ticks);
            return utc.AddHours(diff.Hours);
        }

    }
}