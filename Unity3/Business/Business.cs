using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity3.Model;

namespace Unity3.Business
{
    public class UserManager1 : IUser
    {
        public User OneUser { get; set; }

        public UserManager1()
        {

        }

        public UserManager1(User user)
        {
            this.OneUser = user;
        }

        public UserManager1(string name,int id,string password)
        {
            this.OneUser = new User
            {
                UserName = name,
                UserId = id,
                Password=password,
            };
        }

        public void PrintUser()
        {
            Console.WriteLine($"UserManager1:{OneUser?.UserName} {OneUser?.UserId} {OneUser?.Password}");
        }
    }

    public class UserManager2 : IUser
    {
        public User OneUser { get; set; }

        public UserManager2()
        {

        }

        public UserManager2(User user)
        {
            this.OneUser = user;
        }

        public UserManager2(string name, int id, string password)
        {
            this.OneUser = new User
            {
                UserName = name,
                UserId = id,
                Password = password,
            };
        }

        public void PrintUser()
        {
            Console.WriteLine($"UserManager2:{OneUser?.UserName} {OneUser?.UserId} {OneUser?.Password}");
        }
    }

    public class CompanyManger1 : ICompany
    {
        public Company OneCompany { get; set ; }

        public CompanyManger1(Company company)
        {
            this.OneCompany = company;
        }

        public CompanyManger1()
        {

        }

        public CompanyManger1(string name,int id)
        {
            this.OneCompany = new Company
            {
                CompanyName=name,
                CompanyId=id,
            };
        }

        public void PrintCompany()
        {
            Console.WriteLine($"CompanyManger1:{OneCompany?.CompanyName} {OneCompany?.CompanyId}");
        }
    }

    public class CompanyManger2 : ICompany
    {
        public Company OneCompany { get; set; }

        public CompanyManger2(Company company)
        {
            this.OneCompany = company;
        }

        public CompanyManger2()
        {

        }

        public CompanyManger2(string name, int id)
        {
            this.OneCompany = new Company
            {
                CompanyName = name,
                CompanyId = id,
            };
        }

        public void PrintCompany()
        {
            Console.WriteLine($"CompanyManger2:{OneCompany?.CompanyName} {OneCompany?.CompanyId}");
        }
    }

    public class FullNeed
    {
        IUser user;
        ICompany company;

        [InjectionConstructor]
        public FullNeed(IUser user,ICompany company)
        {
            this.user = user;
            this.company = company;
            Console.WriteLine("两个参数的");
        }

        public FullNeed()
        {
            Console.WriteLine("一个参数的");
        }
    }
}
