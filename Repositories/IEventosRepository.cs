using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Repositories;

public interface IEventosRepository
{
    List<Eventos> ObtenerTodos();
    Eventos? ObtenerPorId(int id);
    Eventos Agregar(Eventos nuevo);
    bool Actualizar(int id, Eventos actualizado);
    bool Eliminar(int id);
}