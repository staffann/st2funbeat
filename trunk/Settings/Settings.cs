using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace Janohl.ST2Funbeat.Settings
{
    [Serializable]
    public class Settings
    {
        private Settings()
        {
            User = new UserSettings();
            ActivityTypeMappings = new List<ActivityTypeMapping>();
        }

        private static readonly string settingsPath = Application.UserAppDataPath + "\\STFunbeat.xml";

        private static Settings settings;

        public static Settings Get()
        {
            if (settings == null)
            {
                if (File.Exists(settingsPath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                    using (FileStream fs = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        try
                        {
                            settings = (Settings)xmlSerializer.Deserialize(fs);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex);
                            throw;
                        }
                    }
                }
                else
                {
                    settings = new Settings();
                }
            }
            return settings;

        }

        public void Save()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
            using (FileStream fs = new FileStream(settingsPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                xmlSerializer.Serialize(fs, this);
            }
        }

        public UserSettings User { get; set; }
        
        [XmlArray(ElementName="Mappings")]
        [XmlArrayItem(ElementName="Mapping")]
        public List<ActivityTypeMapping> ActivityTypeMappings { get; set; }

        public int GetFunbeatActivityTypeID(IActivityCategory st)
        {
            foreach (ActivityTypeMapping m in ActivityTypeMappings)
                if (m.SportTracks.ReferenceID == st.ReferenceId)
                    return m.Funbeat.ID;

            return 51; // The id for "Övriga aktiviteter"
        }
    }
}
