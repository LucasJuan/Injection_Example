using Injection_Example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Injection_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        //https://carloscds.net/2020/02/injecao-de-dependencia-em-c-parte-2/


        private readonly IServico _servico;
        private readonly ExecutaServico _executaServico;
        public HomeController(IServico servico, ExecutaServico executaServico)
        {
            _servico = servico;
            _executaServico = executaServico;
        }

        [HttpGet]
        public string Get()
        {
            var textoServico = _servico.RetornaValor();
            var textoExecutaServico = _executaServico.RetornaServicoExecutado();
            return $"Servico: {textoServico} - ExecutaServico: {textoExecutaServico}";
        }
    }
}
