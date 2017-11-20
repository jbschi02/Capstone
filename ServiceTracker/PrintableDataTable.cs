using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceTracker
{
    public class PrintableDataTable
    {
        DataTable printDT;

        PrintableDataTable()
        {
            printDT = new DataTable();
        }
    }
}