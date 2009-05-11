using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Runtime.Serialization.Formatters.Binary;

namespace Janohl.ST2Funbeat.Settings
{
    [Serializable]
    public class Settings
    {
        public int Version { get; private set; }

        private static Settings instance;
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                    instance = GetInstance();
                return instance;
            }
        }


        private  Settings()
        {
            Version = 1;
            User = new UserSettings();
            ActivityTypeMappings = new List<ActivityTypeMapping>();
        }

        private static Settings GetInstance()
        {
            Settings settings;
                try
                {
                    byte[] storedSettings = Plugin.GetApplication().Logbook.GetExtensionData(Plugin.PluginId);
                    if (storedSettings != null
                        && storedSettings.Length > 0)
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        MemoryStream ms = new MemoryStream();
                        ms.Write(storedSettings, 0, storedSettings.Length);
                        ms.Seek(0, SeekOrigin.Begin);

                        settings = (Settings)formatter.Deserialize(ms);
                    }
                    else
                    {
                        settings = new Settings();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            return settings;

        }

        public static void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, Instance);
                Plugin.GetApplication().Logbook.SetExtensionData(Plugin.PluginId, ms.ToArray());
                Plugin.GetApplication().Logbook.Modified = true;
            }
        }
        
        public UserSettings User { get; set; }

        public List<ActivityTypeMapping> ActivityTypeMappings { get; set; }

        public static int GetFunbeatActivityTypeID(IActivityCategory st)
        {
            foreach (ActivityTypeMapping m in Instance.ActivityTypeMappings)
                if (m.SportTracks.ReferenceID == st.ReferenceId)
                    return m.Funbeat.ID;

            return 51; // The id for "Övriga aktiviteter"
        }
    }
}
