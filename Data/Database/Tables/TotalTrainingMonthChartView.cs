using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class TotalTrainingMonthChartView
    {
        public string MonthName { get; set; }
        public int? Total_Training { get; set; }
    }
}
