namespace WebAPIAutores.Services
{
    public class WriteFile : IHostedService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string fileName = "Archivo1.txt";
        private Timer timer;
        public WriteFile(IWebHostEnvironment env)
        {
            _env = env;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            Write("Proceso iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            Write("Proceso finalizado");
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Write("Proceso en ejecución: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        private void Write(string mensaje)
        {
            var ruta = $@"{_env.ContentRootPath}\wwroot\{fileName}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
