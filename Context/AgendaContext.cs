using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNet_FrontEnd_ASPNET_MVC.Models;

namespace DotNet_FrontEnd_ASPNET_MVC.Context
{
    public class AgendaContext:DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options):base(options){

        }

        public DbSet<Contato> Contatos{get; set;}
    }
}