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
using System.Xml;
using System.ComponentModel;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using Janohl.ST2Funbeat.Settings;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;


namespace Janohl.ST2Funbeat
{
    class Plugin : IPlugin
    {
        private PropertyChangedEventHandler appEventHandler = null;
        string logBookLocation = null;
        public static FitnessDataHandler dataHandler = null;

        internal static Dictionary<string, string> SportTrackActivityTypes
        {
            get
            {
                Dictionary<string, string> sportTrackActivityTypes = new Dictionary<string, string>();

                IEnumerable<IActivityCategory> categories = application.Logbook.ActivityCategories;
                sportTrackActivityTypes = new Dictionary<string, string>();
                foreach (IActivityCategory cat in categories)
                    FlattenSportTrackActivityTypes(sportTrackActivityTypes, cat);

                return sportTrackActivityTypes;
            }
        }

        internal static Dictionary<string, string> SportTrackEquipment
        {
            get
            {
                Dictionary<string, string> sportTrackEquipment = new Dictionary<string, string>();

                IEnumerable<IEquipmentItem> equipment = application.Logbook.Equipment;
                foreach (IEquipmentItem eq in equipment)
                    sportTrackEquipment.Add(eq.ReferenceId, eq.Name);

                return sportTrackEquipment;
            }
        }

        private static void FlattenSportTrackActivityTypes(Dictionary<string, string> list, IActivityCategory category)
        {
            foreach (IActivityCategory sub in category.SubCategories)
            {
                list.Add(sub.ReferenceId, sub.Name);
                FlattenSportTrackActivityTypes(list, sub);
            }
        }

        internal static Guid PluginId
        {
            get
            {
                return new Guid(Properties.Resources.Plugin_GUID);
            }
        }

        #region IPlugin Members

        private static IApplication application;
        public IApplication Application
        {
            set 
            { 
                application = value;
                if (appEventHandler == null)
                {
                    appEventHandler = new PropertyChangedEventHandler(OnApplicationChanged);
                    application.PropertyChanged += appEventHandler;
                }

            }
        }

        public Guid Id
        {
            get { return PluginId; }
        }

        public string Name
        {
            get { return Properties.Resources.Plugin_Name; }
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            Settings.Settings.PopulateInstance(pluginNode, nsmgr, xmlDoc);
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(3); }
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            Settings.Settings.WriteInstance(xmlDoc, pluginNode);
        }

        #endregion

        public static IApplication GetApplication()
        {
            return application;
        }

        private void OnApplicationChanged(object sender, PropertyChangedEventArgs e)
        {
            // Check for changed logbook. If changed, call datahandler to check for custom data fields
            if (application.Logbook != null)
            {
                if (application.Logbook.FileLocation != logBookLocation)
                {
                    logBookLocation = application.Logbook.FileLocation;
                    if (dataHandler == null)
                        dataHandler = new FitnessDataHandler(application.Logbook, PluginId);
                    else
                    {
#if !ST_2_1
                        dataHandler.CheckCustomDataFields(application.Logbook, PluginId);
#endif
                    }
                }
            }
        }

    }
}
