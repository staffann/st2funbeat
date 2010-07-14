using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace Janohl.ST2Funbeat
{
    public class SendTraining: IExtendActivityExportActions
    {
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
    }
}
