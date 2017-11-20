using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceTracker
{
    public class Bools
    {
        public bool dateNeeded;
        public bool safety;
        public DataTable printableDT;

        public Bools()
        {
            printableDT = new DataTable();
            dateNeeded = false;
            safety = false;
        }

        public void setData(DataTable dt)
        {
            printableDT = dt;
        }
    }
}