using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using Janohl.ST2Funbeat.Settings;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using Janohl.Funbeat;

namespace Janohl.ST2Funbeat
{
    class Plugin : IPlugin
    {

        internal static List<STActivityType> SportTrackActivityTypes
        {
            get
            {
                List<STActivityType> sportTrackActivityTypes = new List<STActivityType>();

                IList<IActivityCategory> categories = application.Logbook.ActivityCategories;
                sportTrackActivityTypes = new List<STActivityType>();
                foreach (IActivityCategory cat in categories)
                    FlattenSportTrackActivityTypes(sportTrackActivityTypes, cat);

                return sportTrackActivityTypes;
            }
        }


        private static void FlattenSportTrackActivityTypes(List<STActivityType> list, IActivityCategory category)
        {
            foreach (IActivityCategory sub in category.SubCategories)
            {
                list.Add(new STActivityType(sub.ReferenceId, sub.Name));
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
            set { application = value; }
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
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(3); }
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
        }

        #endregion

        public static IApplication GetApplication()
        {
            return application;
        }
    }
}
