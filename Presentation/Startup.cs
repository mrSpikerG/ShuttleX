using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer;
using LogicLayer.Services;
using Microsoft.EntityFrameworkCore;
using ShuttleX.SignalR;

namespace Presentation
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("Local"),
                b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

            services.AddSignalR();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            // Services
            services.AddTransient<UserService>();
            services.AddTransient<ChatService>();
            services.AddTransient<MessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(origin => true)
                  .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });

            //app.MapControllers();
            //app.MapHub<ChatHub>("/chatHub");
        }
    }
}
