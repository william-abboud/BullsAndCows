
namespace BullsAndCows.Web
{
    using Models;
    using ViewModels;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using AutoMapper;
    using Dtos;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Player, PlayerDto>();
                cfg.CreateMap<PlayerDto, Player>();
                cfg.CreateMap<PlayerGuessResultViewModel, PlayerGuessResult>();
                cfg.CreateMap<PlayerGuessResult, PlayerGuessResultViewModel>();
            });
         }
    }
}
