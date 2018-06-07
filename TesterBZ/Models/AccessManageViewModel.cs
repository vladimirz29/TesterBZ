using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterBZ.Models
{
    public class AccessManageViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public List<UserTestAccessModel> UsersAccesses { get; set; }
    }
}