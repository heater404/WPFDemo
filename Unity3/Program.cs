using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Resolution;
using Unity3.Business;
using Unity3.Model;

namespace Unity3
{
    class Program
    {
        static void Main(string[] args)
        {

            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IUser), typeof(UserManager1));
            IUser user = container.Resolve<IUser>();
            user = container.Resolve<IUser>(new ResolverOverride[]
            {
                new ParameterOverride("user", new User { UserName = "张含韵", UserId = 999, Password = "ijoiyioyhe" })
            });

            user.PrintUser();


            container.RegisterType<ICompany, CompanyManger1>();
            ICompany company = container.Resolve<ICompany>();
            company = container.Resolve<ICompany>(new ResolverOverride[]
                {
                    new ParameterOverride("company",new Company{ CompanyName="聚芯微电子",CompanyId=999})
                });
                
            company.PrintCompany();


            container.RegisterType<FullNeed>();
            FullNeed full = container.Resolve<FullNeed>();


            Console.ReadKey();
        }
    }
}
