﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterBZ.Models
{
    public class EditQuestionBlockViewModel
    {
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public int? TestId { get; set; }
    }
}