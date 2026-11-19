using DSW_3P_PR02.Models;
using DSW_3P_PR02.Repositories;

namespace DSW_3P_PR02.Services;

public class EventosService : IEventosService
{
    private readonly IEventosRepository _repository;

    public EventosService(IEventosRepository repository)
    {
        _repository = repository;
    }

    public List<Eventos> ObtenerTodas()
    {
        return _repository.ObtenerTodos();
    }

    public Eventos ObtenerPorId(int id)
    {
        return BuscarExistente(id);
    }

    public Eventos Agregar(Eventos nueva)
    {
        Validar(nueva);
        nueva.Estado = true;

        return _repository.Agregar(nueva);
    }

    public void Actualizar(int id, Eventos actualizada)
    {
        BuscarExistente(id);
        Validar(actualizada);

        _repository.Actualizar(id, actualizada);
    }

    public void Eliminar(int id)
    {
        BuscarExistente(id);
        _repository.Eliminar(id);
    }

    private Eventos BuscarExistente(int id)
    {
        var evento = _repository.ObtenerPorId(id);

        if (evento == null)
            throw new KeyNotFoundException($"No existe el evento con id {id}");

        return evento;
    }

    private static void Validar(Eventos evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nombre))
            throw new ArgumentException("El nombre del evento es obligatorio");

        if (string.IsNullOrWhiteSpace(evento.Categoria))
            throw new ArgumentException("La categoría es obligatoria");

        if (string.IsNullOrWhiteSpace(evento.Lugar))
            throw new ArgumentException("El lugar es obligatorio");

        if (evento.CupoMaximo <= 0)
            throw new ArgumentException("El cupo máximo debe ser mayor a 0");

        evento.Nombre = evento.Nombre.Trim();
        evento.Categoria = evento.Categoria.Trim();
        evento.Lugar = evento.Lugar.Trim();
    }
}
