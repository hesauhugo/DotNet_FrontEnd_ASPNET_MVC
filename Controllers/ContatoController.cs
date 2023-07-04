using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNet_FrontEnd_ASPNET_MVC.Context;
using DotNet_FrontEnd_ASPNET_MVC.Models;

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
        
        public IActionResult Criar(){
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Contato contato){

            if (ModelState.IsValid)
            {
                _agendaContext.Contatos.Add(contato);
                _agendaContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(contato);
        }

    }
}