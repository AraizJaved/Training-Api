﻿using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class tbl_TaskLog
    {
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public string Comments { get; set; }
        public string Attachment { get; set; }
        public string TaskStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? RecordStatus { get; set; }
    }
}
