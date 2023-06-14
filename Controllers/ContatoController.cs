using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNet_FrontEnd_ASPNET_MVC.Context;

namespace DotNet_FrontEnd_ASPNET_MVC.Controllers
{
    public class ContatoController: Controller
    {
        private readonly AgendaContext _agendaContext;

        public ContatoController(AgendaContext context){
            _agendaContext = context;
        }
        public IActionResult Index(){
            var contatos = _agendaContext.Contatos.ToList();

            return View(contatos);
        }        
    }
}