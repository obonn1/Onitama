namespace Onitama;

internal static class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureWebHost(
              webHost => webHost
                  .UseKestrel(kestrelOptions => { kestrelOptions.ListenAnyIP(5005); })
                  .Configure(app => app
                      .Run(
                          context => {
                              Application.Run(new Form1());
                              return Task.CompletedTask;
                          })));
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

}
