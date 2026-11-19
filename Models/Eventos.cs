namespace DSW_3P_PR02.Models;

public class Eventos
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string Lugar { get; set; } = string.Empty;
    public int CupoMaximo { get; set; }
    public bool Estado { get; set; }
}