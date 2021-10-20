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
            //A cada chamada todos os objetos são criados novamente, ou seja, cada vez que chamamos a nossa aplicação, tudo é instanciado de novo, nenhum estado é mantido.
            //services.AddTransient<IServico, Servico>();
            services.AddTransient<ExecutaServico, ExecutaServico>();
            //Scoped
            //Os objetos são compartilhados dentro de uma mesma chamada, ou seja, todas as instâncias do objeto serão mantidas enquanto durar a chamada. Isto significa que se você usa um mesmo objeto em vários momentos do código, dentro de um mesmo fluxo de chamada, este objeto poderá manter a mesma instância.

            //Singleton
            //Os objetos serão compartilhados por todas a aplicação, independente da chamada, ou seja, sempre iremos acessar o mesmo objeto, incusive independente do usuário.Então cuidado com isto!

            //Se você nao precisa manter o estado do objeto, use TRANSIENT
            //Se precisa compatilhar dados dentro da mesma chamada, use SCOPED
            //E se precisar manter os “mesmos” dados durante toda a aplicação, use SINGLETON.
           
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
