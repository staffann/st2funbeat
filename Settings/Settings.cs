using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Diagnostics;

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
                    throw new Exception("Settings not populated");
                return instance;
            }
        }


        private Settings()
        {
            Version = 1;
            User = new UserSettings();
            ActivityTypeMappings = new List<ActivityTypeMapping>();
        }

        public UserSettings User { get; set; }

        public List<ActivityTypeMapping> ActivityTypeMappings { get; set; }

        public static int GetFunbeatActivityTypeID(IActivityCategory st)
        {
            foreach (ActivityTypeMapping m in Instance.ActivityTypeMappings)
                if (m.SportTracks == st.ReferenceId)
                    return m.Funbeat;

            return 51; // The id for "Övriga aktiviteter"
        }

        internal static void PopulateInstance(System.Xml.XmlElement pluginNode, XmlNamespaceManager nsmgr, XmlDocument xmlDoc)
        {
            instance = new Settings();

            foreach (XmlNode node in pluginNode.ChildNodes)
                if (node.Name == "User")
                {
                    instance.User.Username = node.Attributes[0].Value;
                    instance.User.Password = node.Attributes[1].Value;

                }
                else if (node.Name == "Mappings")
                {
                    foreach (XmlNode mapping in node.ChildNodes)
                    {
                        ActivityTypeMapping atm = new ActivityTypeMapping();
                        atm.SportTracks = mapping.Attributes["st2"].Value;
                        atm.Funbeat = int.Parse(mapping.Attributes["funbeat"].Value);
                        instance.ActivityTypeMappings.Add(atm);
                    }
                }
        }

        internal static void WriteInstance(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            XmlElement user = xmlDoc.CreateElement("User");
            XmlAttribute username = xmlDoc.CreateAttribute("username");
            username.Value = instance.User.Username;
            user.Attributes.Append(username);
            XmlAttribute password = xmlDoc.CreateAttribute("password");
            password.Value = instance.User.Password;
            user.Attributes.Append(password);

            XmlNode existing = pluginNode.SelectSingleNode(user.Name);
            if (existing == null)
                pluginNode.AppendChild(user);
            else
                pluginNode.ReplaceChild(user, existing);



            XmlElement mappings = xmlDoc.CreateElement("Mappings");
            foreach (ActivityTypeMapping atm in instance.ActivityTypeMappings)
            {
                XmlElement mapping = xmlDoc.CreateElement("Mapping");
                XmlAttribute funbeat = xmlDoc.CreateAttribute("funbeat");
                XmlAttribute st2 = xmlDoc.CreateAttribute("st2");
                funbeat.Value = atm.Funbeat.ToString();
                st2.Value = atm.SportTracks;
                mapping.Attributes.Append(funbeat);
                mapping.Attributes.Append(st2);

                mappings.AppendChild(mapping);
            }

            existing = pluginNode.SelectSingleNode(mappings.Name);
            if (existing == null)
                pluginNode.AppendChild(mappings);
            else
                pluginNode.ReplaceChild(mappings, existing);
        }
    }
}
