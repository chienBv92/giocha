﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDuhoc.Models.Define
{
    public class DataTableModel
    {
        public string sEcho { get; set; }
        public int iColumns { get; set; }
        public string sColumns { get; set; }
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public int? iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }
        public int? iSortingCols { get; set; }
    }
}