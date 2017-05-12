using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModel
{
    public class ViewEditUser
    {
        public AspNetUsers User { get; set; }
        public List<string> UserRoleNames { get; set; }
        public List<AspNetRoles> Roles { get; set; }
        public string RoleId { get; set; }
        public Stuff stuffreg { get; set; }
    }
}