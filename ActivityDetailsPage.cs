
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Util;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace Janohl.ST2Funbeat
{
#if !ST_2_1
    public class ActivityDetailsPage:IExtendActivityDetailPages
    {
        #region IExtendActivityDetailPages Members
        
        public IList<IDetailPage> GetDetailPages(IDailyActivityView view, ExtendViewDetailPages.Location location)
        {
            //IActivity activity = CollectionUtils.GetSingleItemOfType<IActivity>(view.SelectionProvider.SelectedItems);
            return new IDetailPage[] { new ST2FunbeatDetailsPage(view) };
        }

        #endregion
    }

    public class ST2FunbeatDetailsPage : IDetailPage
    {
        private IList<string> menuPath = null;
        private bool menuEnabled = true;
        private bool menuVisible = true;
        private bool pageMaximized = false;
        private ActivityDetailsControl control = null;
        private IDailyActivityView view;
        private IActivity activity = null;

        public ST2FunbeatDetailsPage(IDailyActivityView view)
        {
            view.SelectionProvider.SelectedItemsChanged += new EventHandler(OnViewSelectedItemsChanged);
            this.view = view;
        }

        private void OnViewSelectedItemsChanged(object sender, EventArgs e)
        {
            IActivity activity = CollectionUtils.GetSingleItemOfType<IActivity>(view.SelectionProvider.SelectedItems);
            this.activity = activity;
            if (control != null)
            {
                control.Activity = activity;
                control.RefreshInfo();
            }
        }

        #region IDetailPage Members
        public Guid Id
        {
            get { return new Guid("ED315538-25EE-4F54-A68D-27E79B81DEBB"); }
        }
        
        public bool MenuEnabled
        {
            get { return menuEnabled; }
            set { menuEnabled = value; OnPropertyChanged("MenuEnabled"); }
        }

        public IList<string> MenuPath
        {
            get { return menuPath; }
            set { menuPath = value; OnPropertyChanged("MenuPath"); }
        }

        public bool MenuVisible
        {
            get { return menuVisible; }
            set { menuVisible = value; OnPropertyChanged("MenuVisible"); }
        }

        public bool PageMaximized
        {
            get { return pageMaximized; }
            set { pageMaximized = value; OnPropertyChanged("PageMaximized"); }
        }

        #endregion

        #region IDialogPage Members

        public System.Windows.Forms.Control CreatePageControl()
        {
            if (control == null)
            {
                control = new ActivityDetailsControl(activity);
            }
            return control;
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
            
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            //Should refresh the page here;
        }

        public string Title
        {
            get { return "ST2Funbeat"; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            //RefreshPage();
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
#endif
}
