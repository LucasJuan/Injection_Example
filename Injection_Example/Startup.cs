using Injection_Example.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Injection_Example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Transient
            //A cada chamada todos os objetos s�o criados novamente, ou seja, cada vez que chamamos a nossa aplica��o, tudo � instanciado de novo, nenhum estado � mantido.
            //services.AddTransient<IServico, Servico>();
            services.AddTransient<ExecutaServico, ExecutaServico>();
            //Scoped
            //Os objetos s�o compartilhados dentro de uma mesma chamada, ou seja, todas as inst�ncias do objeto ser�o mantidas enquanto durar a chamada. Isto significa que se voc� usa um mesmo objeto em v�rios momentos do c�digo, dentro de um mesmo fluxo de chamada, este objeto poder� manter a mesma inst�ncia.

            //Singleton
            //Os objetos ser�o compartilhados por todas a aplica��o, independente da chamada, ou seja, sempre iremos acessar o mesmo objeto, incusive independente do usu�rio.Ent�o cuidado com isto!

            //Se voc� nao precisa manter o estado do objeto, use TRANSIENT
            //Se precisa compatilhar dados dentro da mesma chamada, use SCOPED
            //E se precisar manter os �mesmos� dados durante toda a aplica��o, use SINGLETON.
           
            //Scoped 
            //lifetime services are created once per request within the scope.It is equivalent to a singleton in the current scope.For example, in MVC it creates one instance for each HTTP request, but it uses the same instance in the other calls within the same web request.
            services.AddScoped<IServico, Servico>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
