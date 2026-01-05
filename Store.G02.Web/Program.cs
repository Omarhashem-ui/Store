using Store.G02.Web.Extansions;

namespace Store.G02.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAllServices(builder.Configuration);

            var app = builder.Build();

            await app.ConfiguareMiddleWaresAsync();

            app.Run();

        }
    }
}
