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
