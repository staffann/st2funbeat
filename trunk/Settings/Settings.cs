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
                {
                    instance = new Settings();
                }
                return instance;
            }
        }


        private Settings()
        {
            Version = 1;
            boExportNameInComment = false;
            User = new UserSettings();
            ActivityTypeMappings = new List<ActivityTypeMapping>();
            EquipmentTypeMappings = new List<EquipmentTypeMapping>();
        }

        public UserSettings User {get; set;}

        public List<ActivityTypeMapping> ActivityTypeMappings { get; set; }

        public List<EquipmentTypeMapping> EquipmentTypeMappings { get; set; }

        public bool boExportNameInComment { get; set; }

        public static int GetFunbeatActivityTypeID(IActivityCategory st)
        {
            foreach (ActivityTypeMapping m in Instance.ActivityTypeMappings)
                if (m.SportTracks == st.ReferenceId)
                    return m.Funbeat;

            return 51; // The id for "Övriga aktiviteter"
        }

        public static String GetOneFunbeatEquipment(IEquipmentItem eq)
        {
            foreach (EquipmentTypeMapping m in Instance.EquipmentTypeMappings)
                if (m.SportTracks == eq.ReferenceId)
                    return m.Funbeat;

            return null;
        }

        public static String[] GetFunbeatEquipment(IList<IEquipmentItem> eiList)
        {
            List<string> equipment = new List<string>();
            foreach (IEquipmentItem stEq in eiList)
            {
                string fbEq = Settings.GetOneFunbeatEquipment(stEq);
                if (fbEq != null)
                {
                    equipment.Add(fbEq);
                }
            }
            return equipment.ToArray();
        }

        internal static void PopulateInstance(System.Xml.XmlElement pluginNode, XmlNamespaceManager nsmgr, XmlDocument xmlDoc)
        {
            instance = new Settings();

            foreach (XmlNode node in pluginNode.ChildNodes)
            {
                if (node.Name == "User")
                {
                    instance.User.Username = node.Attributes[0].Value;
                    instance.User.Password = node.Attributes[1].Value;

                }
                else if (node.Name == "Login")
                {
                    instance.User.LoginId = node.Attributes[0].Value;
                    instance.User.LoginSecret = node.Attributes[1].Value;
                }
                else if (node.Name == "ExportNameInComment")
                {
                    instance.boExportNameInComment = bool.Parse(node.Attributes[0].Value);
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
                else if (node.Name == "EquipmentMappings")
                {
                    foreach (XmlNode mapping in node.ChildNodes)
                    {
                        EquipmentTypeMapping etm = new EquipmentTypeMapping();
                        etm.SportTracks = mapping.Attributes["st2"].Value;
                        etm.Funbeat = mapping.Attributes["funbeat"].Value;
                        instance.EquipmentTypeMappings.Add(etm);
                    }
                }
            }
        }

        internal static void WriteInstance(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            if (instance == null)
            {
                //This can occur if a logbook could not be loaded, then ST is closed
                instance = new Settings();
            }
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

            XmlElement login = xmlDoc.CreateElement("Login");
            XmlAttribute loginId = xmlDoc.CreateAttribute("loginId");
            loginId.Value = instance.User.LoginId;
            login.Attributes.Append(loginId);
            XmlAttribute loginSecret = xmlDoc.CreateAttribute("loginSecret");
            loginSecret.Value = instance.User.LoginSecret;
            login.Attributes.Append(loginSecret);
            existing = pluginNode.SelectSingleNode(login.Name);
            if (existing == null)
                pluginNode.AppendChild(login);
            else
                pluginNode.ReplaceChild(login, existing);

            XmlElement ExportNameInComment = xmlDoc.CreateElement("ExportNameInComment");
            XmlAttribute export = xmlDoc.CreateAttribute("export");
            export.Value = instance.boExportNameInComment.ToString();
            ExportNameInComment.Attributes.Append(export);
            existing = pluginNode.SelectSingleNode(ExportNameInComment.Name);
            if (existing == null)
                pluginNode.AppendChild(ExportNameInComment);
            else
                pluginNode.ReplaceChild(ExportNameInComment, existing);            

            // Save activity type mappings
            XmlElement mappings = xmlDoc.CreateElement("Mappings"); //Remains called only Mappings for compatibility reasons
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

            // Save equipment mappings
            XmlElement eqMappings = xmlDoc.CreateElement("EquipmentMappings");
            foreach (EquipmentTypeMapping etm in instance.EquipmentTypeMappings)
            {
                XmlElement mapping = xmlDoc.CreateElement("Mapping");
                XmlAttribute funbeat = xmlDoc.CreateAttribute("funbeat");
                XmlAttribute st2 = xmlDoc.CreateAttribute("st2");
                funbeat.Value = etm.Funbeat;
                st2.Value = etm.SportTracks;
                mapping.Attributes.Append(funbeat);
                mapping.Attributes.Append(st2);

                eqMappings.AppendChild(mapping);
            }

            existing = pluginNode.SelectSingleNode(eqMappings.Name);
            if (existing == null)
                pluginNode.AppendChild(eqMappings);
            else
                pluginNode.ReplaceChild(eqMappings, existing);
        }
    }
}
