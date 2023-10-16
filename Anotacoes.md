# O padrão mvc
* O padrão de arquitetura mcv (model-view-controller) separa um aplicativo em três grupos de componentes principais: modelos, exibições e componentes. Esse padrão ajuda a obter a separação de interesses 
* <a href = "https://learn.microsoft.com/pt-br/aspnet/core/mvc/overview?view=aspnetcore-6.0">Padrao MVC</a>
* ASP.NET, é o modulo web do .net.
* Existem duas principais versões do ASP.NET: Web Forms e ASP.NET MVC (Model-View-Controller). O ASP.NET Web Forms é baseado em controles e eventos, proporcionando um modelo de desenvolvimento rápido e fácil para a criação de aplicativos web. Já o ASP.NET MVC adota o padrão de arquitetura MVC, separando a lógica de negócios (Model), a apresentação (View) e o controle de fluxo (Controller) em componentes distintos.

# Criando o projeto

* Para criar um novo projeto asp.net core mvc

```console
    dotnet new mvc
```
* Executar o projeto

```console
    dotnet watch run
```

* Avaliando a pasta `Views` subpasta `Home`
* Nela existem dois arquivos com a extensão `cshtml`
* são as duas páginas que aparecem após a execução do programa

## Arquivo Index.cshtml

```html

    @{
        ViewData["Title"] = "Home Page";
    }

    <div class="text-center">
        <h1 class="display-4">Welcome</h1>
        <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
    </div>

```
## Arquivo Privacy.cshtml
```html
    @{
        ViewData["Title"] = "Privacy Policy";
    }
    <h1>@ViewData["Title"]</h1>

    <p>Use this page to detail your site's privacy policy.</p>
```

# Entendendo as rotas
* As controller herdam de `controller`, como por exemplo a `homecontroller`
* o retorno é uma view

```csharp
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using DotNet_FrontEnd_ASPNET_MVC.Models;

    namespace DotNet_FrontEnd_ASPNET_MVC.Controllers;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
```
* como o retorno é uma `View` significa que dentro da pasta view existirá uma visualização para cada retorno.
* como por exemplo já mostrado anteriormente na pasta `Views/Home/` 
* o fluxo é o seguinte: Controller chamada `HomeController`, então dentro da pasta views será procurada uma pasta chamada `Home`, seguindo a rota dentro da pasta `Views/Home/` será producado um arquivo cujo nome é semelhante ao método que retorna a view dentro da `HomeController`, por exemplo o arquivo `Privacy.cshtml`, o caminho da controler será `HomeController/Privacy` e o caminho da view será `Views/Home/Privacy.cshtml`

# Configurando entity framework
* adicionar pacots necessários

```console
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Design
```
* Criar a classe contato
```csharp 
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }
```
* Criar a classe context

```csharp
    using Microsoft.EntityFrameworkCore;
    namespace DotNet_FrontEnd_ASPNET_MVC.Context
    {
        public class AgendaContext:DbContext
        {
            public AgendaContext(DbContextOptions<AgendaContext> options):base(options){

            }

            public DbSet<Contato> Contatos{get; set;}
        }
    }
```
* Criar a string de conexão
```json
    {
    "Logging": {
        "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
        }
    },
    "ConnectionStrings": {
        "ConexaoPadrao":"Server=localhost\\SQLEXPRESS; Initial Catalog=AgendaMvc; Integrated Security=True; TrustServerCertificate=True"
    }
    }
```
* Importar pacote entityframeworkcore  e a dbcontext adicionando o serviço na classe program
```csharp

    using Microsoft.EntityFrameworkCore;
    using DotNet_FrontEnd_ASPNET_MVC.Context;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddDbContext<AgendaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

    builder.Services.AddControllersWithViews();

```
# Criando migrations
* executar no terminal

```console
    dotnet ef migrations add AdicionaTabelaContato
```

* aplicar no projeto

```console
    dotnet ef database update
```

# Criando a primeira página
* primeiro dentro da  pasta `Controler` criar uma nova controller contato
```csharp
    using Microsoft.AspNetCore.Mvc;

    namespace DotNet_FrontEnd_ASPNET_MVC.Controllers
    {
        public class ContatoController: Controller
        {
            public IActionResult Index(){
                return View();
            }        
        }
    }
```
* dentro da pasta `Views`  criar uma pasta chamada `Contato` e dentro dela criar um arquivo chamado `index.cshtml` 
* Com isso a página já está criada, porém sem conteúdo dentro

# Construindo a página de listagem
* dentro de index.cshtml criar a página

```html

@model IEnumerable<DotNet_FrontEnd_ASPNET_MVC.Models.Contato> 

@{
    ViewData["Title"] = "Listagem de contatos";
}

<h2>Contatos</h2>

<p>
    <a asp-action="Criar">Novo Contato</a>
</p>

<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.Id)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Nome)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Telefone)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Ativo)
            </th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>
                    @Html.DisplayFor(model=> item.Id)
                </td>
                <td>
                    @Html.DisplayFor(model=> item.Nome)
                </td>
                <td>
                    @Html.DisplayFor(model=> item.Telefone)
                </td>
                <td>
                    @Html.DisplayFor(model=> item.Ativo)
                </td>
                <td>
                    <a asp-action="Editar" asp-route-id = "item.Id">Editar</a>|
                    <a asp-action="Detalhes" asp-route-id = "item.Id">Detalhes</a>|
                    <a asp-action="Deletar" asp-route-id = "item.Id">Deletar</a>
                </td>
            </tr>
        }
    </tbody>
</table>
```
# Configurando método na controler
* ajustanco a controler para puxar a página criada anteriormente e entityframeworkcore
```csharp
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
```
# Criando página novo contato
* primeiro acrescentar na controler `ContatoController`  mais um método para página criar
* depois dentro de `Views/Contato`  adicionar um novo arquivo chamado `Criar.cshtml` 
```csharp

    namespace DotNet_FrontEnd_ASPNET_MVC.Controllers
    {
        public class ContatoController: Controller
        {
            private readonly AgendaContext _agendaContext;

                /*
                    outros metodos
                */
            public IActionResult Criar(){
                return View();
            }
        }
    }

```
```html
    @model DotNet_FrontEnd_ASPNET_MVC.Models.Contato

    @{
        ViewData["Title"] = "Criar novo contato";
    }

    <h1>Criar novo contato</h1>

    <hr/>

    <div class="row">
        <div class="col-md-4">
            <form asp-action="Criar">
                <div class="form-group">
                    <label asp-for="Nome" class="control-label"></label>
                    <input asp-for="Nome" class="form-control"/>
                </div>
                <div class="form-group">
                    <label asp-for="Telefone" class="control-label"></label>
                    <input asp-for="Telefone" class="form-control"/>
                </div>
                <div class="form-group">
                    <label asp-for="Ativo" class="control-label"></label>
                    <input type="checkbox" asp-for="Ativo" class="form-check-input"/>
                </div>         
                <br>   
                <div class="form-group">
                    <input type="submit" value="Criar" class="btn btn-primary"/>
                </div>
            </form>
        </div>
    </div>
    </hr>
    <div>
        <a asp-action="Index">Voltar</a>
    </div>
```
# Implementando metodo criar como post
 * o httpget atributo é opcional por isso ele é omitido no método anteriormente criado `Criar` 
 * ao entrar pela primeira vez na página o método Criar é chamado como get
 * ao clicar no botão criar é chamado o método post
 * dentro do método post é validado se os dados do formulários são validos com a condição `ModelState.IsValid` 
 * Caso seja então salva o contato e redireciona para a página de índice usando o comando `RedirectToAction`

 ```csharp

    namespace DotNet_FrontEnd_ASPNET_MVC.Controllers
    {
        public class ContatoController: Controller
        {
            private readonly AgendaContext _agendaContext;

            /*
                outros metodos
            */
            
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

```

# Criando página editar contato
* primeiro acrescentar na controler `ContatoController`  mais um método para página criar
* depois dentro de `Views/Contato`  adicionar um novo arquivo chamado `Editar.cshtml` 
```csharp
namespace DotNet_FrontEnd_ASPNET_MVC.Controllers
{
    public class ContatoController: Controller
    {
        private readonly AgendaContext _agendaContext;

        public ContatoController(AgendaContext context){
            _agendaContext = context;
        }
        
        // ...codigo

        public IActionResult Editar(int id){
            
            var contato = _agendaContext.Contatos.Find(id);
            if(contato ==null){
                return NotFound();
            }

            return View(contato);
        }
    }
}
```
* Código dentro do `Editar.cshtml`
```html
@model DotNet_FrontEnd_ASPNET_MVC.Models.Contato

@{
    ViewData["Title"] = "Editar contato";
}

<h1>Editar contato</h1>

<hr/>

<div class="row">
    <div class="col-md-4">
        <form asp-action="Editar">
            <div class="form-group">
                <label asp-for="Nome" class="control-label"></label>
                <input asp-for="Nome" class="form-control"/>
            </div>
            <div class="form-group">
                <label asp-for="Telefone" class="control-label"></label>
                <input asp-for="Telefone" class="form-control"/>
            </div>
            <div class="form-group">
                <label asp-for="Ativo" class="control-label"></label>
                <input type="checkbox" asp-for="Ativo" class="form-check-input"/>
            </div>         
            <br>   
            <div class="form-group">
                <input type="submit" value="Editar" class="btn btn-primary"/>
            </div>
        </form>
    </div>
</div>
</hr>
<div>
    <a asp-action="Index">Voltar</a>
</div>
```
