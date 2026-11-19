using DSW_3P_PR02.Models;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace DSW_3P_PR02.Repositories;

public class EventosJsonRepository : IEventosRepository
{
    private readonly string _ruta;

    private static readonly object _bloqueo = new();

    private readonly JsonSerializerOptions _opciones = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public EventosJsonRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(environment.ContentRootPath, "Data", "Eventos.json");

        Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);

        if (!File.Exists(_ruta))
        {
            File.WriteAllText(_ruta, "[]");
        }
    }

    private List<Eventos> Leer()
    {
        string json = File.ReadAllText(_ruta);

        if (string.IsNullOrWhiteSpace(json))
            return new List<Eventos>();

        return JsonSerializer.Deserialize<List<Eventos>>(json, _opciones) ?? new List<Eventos>();
    }

    private void Guardar(List<Eventos> datos)
    {
        string json = JsonSerializer.Serialize(datos, _opciones);

        File.WriteAllText(_ruta, json);
    }

    public List<Eventos> ObtenerTodos()
    {
        lock (_bloqueo)
        {
            return Leer();
        }
    }

    public Eventos? ObtenerPorId(int id)
    {
        lock (_bloqueo)
        {
            return Leer().FirstOrDefault(m => m.Id == id);
        }
    }

    public Eventos Agregar(Eventos nueva)
    {
        lock (_bloqueo)
        {
            var datos = Leer();

            nueva.Id = datos.Count == 0 ? 1 : datos.Max(m => m.Id) + 1;
            datos.Add(nueva);
            Guardar(datos);

            return nueva;
        }
    }

    public bool Actualizar(int id, Eventos actualizada)
    {
        lock (_bloqueo)
        {
            var datos = Leer();

            var existente = datos.FirstOrDefault(m => m.Id == id);

            if (existente == null)
                return false;

            existente.Nombre = actualizada.Nombre;
            existente.Categoria = actualizada.Categoria;
            existente.Fecha = actualizada.Fecha;
            existente.Lugar = actualizada.Lugar;
            existente.CupoMaximo = actualizada.CupoMaximo;
            existente.Estado = actualizada.Estado;

            Guardar(datos);
            return true;
        }
    }

    public bool Eliminar(int id)
    {
        lock (_bloqueo)
        {
            var datos = Leer();

            var existente = datos.FirstOrDefault(m => m.Id == id);

            if (existente == null)
                return false;

            datos.Remove(existente);
            Guardar(datos);
            return true;
        }
    }
}
