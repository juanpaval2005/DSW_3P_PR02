using DSW_3P_PR02.Models;
namespace DSW_3P_PR02.Services;

public interface IEventosService
{
    List<Eventos> ObtenerTodas();
    Eventos ObtenerPorId(int id);
    Eventos Agregar(Eventos nuevo);
    void Actualizar(int id, Eventos actualizado);
    void Eliminar(int id);
}