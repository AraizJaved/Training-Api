using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class tbl_SupportedBy
    {
        public int Id { get; set; }
        public string SupportedBy { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    }
}
