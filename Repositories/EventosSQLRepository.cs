using DSW_3P_PR02.Data;
using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Repositories;

public class EventosSqlRepository : IEventosRepository
{
    private readonly AppDbContext _context;

    public EventosSqlRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Eventos> ObtenerTodos()
    {
        return _context.Eventos.ToList();
    }

    public Eventos? ObtenerPorId(int id)
    {
        return _context.Eventos.Find(id);
    }

    public Eventos Agregar(Eventos nuevo)
    {
        nuevo.Id = 0;
        _context.Eventos.Add(nuevo);
        _context.SaveChanges();

        return nuevo;
    }

    public bool Actualizar(int id, Eventos actualizado)
    {
        var actual = _context.Eventos.Find(id);
        if (actual is null)
        {
            return false;
        }

        actual.Nombre = actualizado.Nombre;
        actual.Categoria = actualizado.Categoria;
        actual.Fecha = actualizado.Fecha;
        actual.Lugar = actualizado.Lugar;
        actual.CupoMaximo = actualizado.CupoMaximo;
        actual.Estado = actualizado.Estado;

        _context.SaveChanges();
        return true;
    }

    public bool Eliminar(int id)
    {
        var evento = _context.Eventos.Find(id);
        if (evento is null)
        {
            return false;
        }

        _context.Eventos.Remove(evento);
        _context.SaveChanges();
        return true;
    }
}
