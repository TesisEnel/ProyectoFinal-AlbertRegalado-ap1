using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Suplidor{

    [Key]  
    public int SuplidorId { get; set; }

    [Required(ErrorMessage = "Ingrese un nombre.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El email es requerido.")]
    [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*",ErrorMessage = "Formato inválido. name@gmail.com")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Ingrese un numero teléfonico.")]
    [RegularExpression(@"^\d{3}[- ]?\d{3}[- ]?\d{4}$",ErrorMessage = "Formato inválido. 000-000-0000")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "Ingrese una dirección.")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "Ingrese el nombre del equipo.")]
    public string? DescripcionEquipo { get; set; }

    [DataType(DataType.Date)]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Estado { get; set; } = true; 

}