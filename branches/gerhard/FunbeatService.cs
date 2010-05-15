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
using Janohl.ST2Funbeat.Funbeat;
using Janohl.ST2Funbeat.Settings;
using System.Collections.ObjectModel;


namespace Janohl.ST2Funbeat
{
    public class FunbeatService
    {
        private readonly static System.Guid funbeatKey = new System.Guid("65a574f3-1c59-462d-8df6-0cea15c44089");
        private static FunbeatService instance;
        private static FunbeatService Instance
        {
            get
            {
                if (instance == null)
                    instance = new FunbeatService(Settings.Settings.Instance.User);
                return instance;
            }
        }

        private User login;

        private FunbeatService(UserSettings user)
        {
            login = new User();
            login.Password = user.Password;
            login.Username = user.Username;
            login.PasswordFormat = PasswordFormats.Clear;
        }

        public static int? SendTraining(DateTime startDate, TimeSpan duration, float? distance, string comment,
            int? hrAvg, int? hrMax, int? intensity, int kcal, string privateComment, int? repetitions, int? sets,
            int trainingType, TrackPoint[] trackPoints)
        {
            Training training = new Training();
            training.Comment = comment;
            training.Distance = distance;
            training.StartDateTime = startDate;
            training.Hours = duration.Hours;
            training.Minutes = duration.Minutes;
            training.Seconds = duration.Seconds;

            training.HRAvg = hrAvg;
            training.HRMax = hrMax;
            training.Intensity = intensity;
            training.KCal = kcal;
            training.PrivateComment = privateComment;
            training.Repetitions = repetitions;
            training.Sets = sets;
            training.TrainingTypeID = trainingType;
            training.TrackPoints = trackPoints;
            TrainingService client = new TrainingService();
            return client.AddTraining(funbeatKey,Instance.login, training);
        }

        private static Dictionary<int, string> GetTrainingTypes()
        {
            TrainingService client = new TrainingService();
            TrainingType[] types = client.GetTrainingTypes(funbeatKey);
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (TrainingType t in types)
                result.Add(t.ID, t.Name);

            return result;
        }

        private static IList<FunbeatActivityType> funbeatActivityTypes;
        public static IList<FunbeatActivityType> FunbeatActivityTypes
        {
            get
            {
                if (funbeatActivityTypes == null)
                {
                    funbeatActivityTypes = new Collection<FunbeatActivityType>();
                    Dictionary<int, string> funbeatValues = FunbeatService.GetTrainingTypes();
                    foreach (int i in funbeatValues.Keys)
                        funbeatActivityTypes.Add(new FunbeatActivityType(i, funbeatValues[i]));
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
    }
}
