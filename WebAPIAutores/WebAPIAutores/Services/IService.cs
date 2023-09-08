namespace WebAPIAutores.Services
{
    public interface IService
    {
        void RealizarTarea();
    }
    public class ServicioA : IService
    {
        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }
    public class ServicioB : IService
    {
        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }
}
