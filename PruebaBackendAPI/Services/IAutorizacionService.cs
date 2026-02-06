using PruebaBackendAPI.Models;

namespace PruebaBackendAPI.Services
{
    public interface IAutorizacionService
    {
        Task<string> DevolverToken(AutorizacionRequest autorizacion);
    }
}
