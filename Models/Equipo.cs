using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Equipo{

    [Key]

    public int EquipoId { get; set; }
    [Required(ErrorMessage = "Ingrese el nombre del equipo.")]
    public string? Nombre { get; set; }
    [DataType(DataType.Date)]
    public DateOnly FechaCreacion { get; set; }
    [Required]
    [Range(1, float.MaxValue, ErrorMessage = "Ingrese la cantidad del equipo.")]
    public double Cantidad { get; set; }

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Ingrese un costo mayor a 0.")]
    public decimal Costo { get; set; }

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Ingrese un precio mayor a 0.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "Campo ITBIS es obligatorio.")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Seleccione el % de ITBIS.")]
    public float ITBIS {get; set;}

    [ForeignKey("EquipoId")]
    public List<VentasDetalle>? VentasDetalle { get; set; }

    public Equipo()
    {
        EquipoId = 0; 
        Nombre = string.Empty;
        FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        Cantidad = 0;
        Costo = 0;
        Precio = 0;
        
        VentasDetalle = new List<VentasDetalle>();
    }    
}