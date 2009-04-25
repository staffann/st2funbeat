using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Janohl.ST2Funbeat.Funbeat;

namespace Janohl.Funbeat
{
    public class FunbeatService
    {

        private Login login;

        public FunbeatService(string userID, string password)
        {

            login = new Login();
            login.Password = password;
            login.Username = userID;
            login.PasswordFormat = PasswordFormats.Clear;
        }

        public int? SendTraining(DateTime startDate, TimeSpan duration, float? distance, string comment,
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
            return client.AddTraining(login, training);
        }

        public Dictionary<int, string> GetTrainingTypes()
        {
            TrainingService client = new TrainingService();
            TrainingType[] types = client.GetTrainingTypes();
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (TrainingType t in types)
                result.Add(t.ID, t.Name);

            return result;
        }
    }
}
