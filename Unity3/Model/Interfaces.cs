using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity3.Model
{
    public interface ICompany
    {
        Company OneCompany { get; set; }
        void PrintCompany();
    }

    public interface IUser
    {
        User OneUser { get; set; }
        void PrintUser();
    }
}
