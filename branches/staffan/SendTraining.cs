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
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace Janohl.ST2Funbeat
{
    public class SendTraining:
#if ST_2_1
    IExtendActivityExportActions
#else
    IExtendDailyActivityViewActions, IExtendActivityReportsViewActions
#endif
        
    {
#if ST_2_1
        #region IExtendActivityExportActions Members
        public IList<IAction> GetActions(IList<IActivity> activities)
        {
            List<IAction> list = new List<IAction>(1);
            list.Add(new TransferActivity(activities));
            return list;
        }

        public IList<IAction> GetActions(IActivity activity)
        {
            List<IActivity> list = new List<IActivity>(1);
            list.Add(activity);
            return GetActions(list);
        }
        #endregion
#else
        #region IExtendDailyActivityViewActions Members
        public IList<IAction> GetActions(IDailyActivityView view,
                                                 ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.ExportMenu)
            {
                return new IAction[] { new TransferActivity(view) };
            }
            else return new IAction[0];
        }
        public IList<IAction> GetActions(IActivityReportsView view,
                                         ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.ExportMenu)
            {
                return new IAction[] { new TransferActivity(view) };
            }
            else return new IAction[0];
        }
        #endregion
#endif
    }
}
