using EmailSending.Web.Api.Configuration;
using EmailSending.Web.Api.DataAccess;
using EmailSending.Web.Api.DataAccess.Entities;
using EmailSending.Web.Api.Services;
using EmailSending.Web.Api.SmtpEmailSending;
using EmailSending.Web.Api.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmailSending.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddApiVersioning(
                options => { options.ReportApiVersions = true; });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.AddTransient<IValidator<Email>, EmailValidator>();
            services.AddTransient<IValidationMessageFormatter, ValidationMessageFormatter>();
            services.AddTransient<IRequestOrchestrator, RequestOrchestrator>();
            services.AddTransient<IEmailsRepository, EmailsRepository>();
            services.Configure<SmtpConfiguration>(Configuration.GetSection(nameof(SmtpConfiguration)));
            services.AddTransient<IMailMessageBuilder, MailMessageBuilder>();
            services.AddTransient<ISmtpSender, SmtpSender>();

            RegisterDbContext(services);
        }

        private void RegisterDbContext(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "";
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                });
        }
    }
}