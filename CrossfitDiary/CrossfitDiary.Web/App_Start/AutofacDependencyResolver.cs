using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CrossfitDiary.DAL.EF.DataContexts;
using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;
using CrossfitDiary.Service;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.Configuration;
using CrossfitDiary.Web.Mappings;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace CrossfitDiary.Web
{
    public class AutofacDependencyResolver
    {
        public static void Run(IAppBuilder app)
        {
            SetAutofacContainer(app);

            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CrossfitDiaryDbContext>().AsSelf();//.SingleInstance();
//            builder.RegisterType<UserStore<ApplicationUser>>().AsImplementedInterfaces();//.InstancePerRequest();
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<CrossfitDiaryDbContext>())).AsImplementedInterfaces();//.InstancePerRequest();
//            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();


            //Repositories
            builder.RegisterAssemblyTypes(typeof (ExerciseRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            //Services
            builder.RegisterAssemblyTypes(typeof (ExerciseService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerRequest();



            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver

        }
    }
}