using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity3.Model
{
    public class User
    {
        public string UserName { set; get; }
        public int UserId { set; get; }
        public string Password { get; set; }
    }

    public class Role
    {
        public int Id { set; get; }
    }

    public class Department
    {
        public int Id { set; get; }
        public string Name { get; set; }
    }

    public class Company
    {
        public string CompanyName { set; get; }
        public int CompanyId { set; get; }
    }
}
