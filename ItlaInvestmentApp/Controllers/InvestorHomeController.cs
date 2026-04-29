using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;

namespace ItlaInvestmentApp.Controllers
{
    public class InvestorHomeController : Controller
    {
        private readonly IUserSession _usersession;

        public InvestorHomeController(IUserSession userSession)
        {
            _usersession = userSession;
        }
        public ActionResult Index()
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            return View();
        }

      

        
      
        }
    }

