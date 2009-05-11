using System;
using System.Collections.Generic;
using System.Text;

namespace Janohl.ST2Funbeat.Settings
{
    [Serializable]
    public class ActivityTypeMapping
    {
        public STActivityType SportTracks { get; set; }
        public FunbeatActivityType Funbeat { get; set; }
    }

    [Serializable]
    public class STActivityType{
        private STActivityType() { }
        public STActivityType(string referenceID, string text)
        {
            ReferenceID = referenceID;
            Text = text;
        }
        public string ReferenceID { get; set; }
        public string Text { get; set; }
    }

    [Serializable]
    public class FunbeatActivityType{
        private FunbeatActivityType() { }

        public FunbeatActivityType(int id, string text)
        {
            ID = id;
            Text = text;
        }

        public int ID { get; set; }
        public string Text { get; set; }
    }

}
