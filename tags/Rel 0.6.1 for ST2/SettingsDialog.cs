using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;

namespace Janohl.ST2Funbeat
{
    public class SettingsDialog : ZoneFiveSoftware.Common.Visuals.Fitness.IExtendSettingsPages
    {
        #region IExtendSettingsPages Members

        public IList<ZoneFiveSoftware.Common.Visuals.ISettingsPage> SettingsPages
        {
            get
            {
                List<ISettingsPage> pageList = new List<ISettingsPage>();
                pageList.Add(new SettingsPage());
                return pageList;
            }
        }

        #endregion
    }

    public class SettingsPage : ISettingsPage
    {
        #region ISettingsPage Members

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

        public IList<ISettingsPage> SubPages
        {
            get { return null; }
        }

        #endregion

        #region IDialogPage Members

        public System.Windows.Forms.Control CreatePageControl()
        {
            return new SettingsControl();
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return "ST2Funbeat"; }
        }

        public void ShowPage(string bookmark)
        {
            //NOOP
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            //NOOP
        }

        public string Title
        {
            get { return "ST2Funbeat settings"; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            //NOOP
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

}
