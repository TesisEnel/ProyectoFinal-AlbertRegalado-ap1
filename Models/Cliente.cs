using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Cliente{

    [Key]  
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Ingrese un nombre.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Campo email es requerido.")]
    [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*",ErrorMessage = "Formato inválido. name@gmail.com")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Ingrese un numero teléfonico.")]
    [RegularExpression(@"^\d{3}[- ]?\d{3}[- ]?\d{4}$",ErrorMessage = "Formato inválido. 000-000-0000")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "Ingrese numero de celular.")]
    [RegularExpression(@"^\d{3}[- ]?\d{3}[- ]?\d{4}$",ErrorMessage = "Formato inválido. 000-000-0000")]
    public string? Celular { get; set; }

    [Required(ErrorMessage = "Ingrese una dirección.")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "Ingrese un numero de cédula.")]
    [RegularExpression(@"^\d{3}[- ]?\d{7}[- ]?\d{1}$",ErrorMessage = "Formato inválido 000-0000000-0")]
    public string? Cedula { get; set; }

    [DataType(DataType.Date)]
    public DateOnly FechaNacimiento { get; set; }

    public Cliente()
    {
        ClienteId = 0; 
        Nombre = string.Empty;
        Email = string.Empty;
        Telefono = string.Empty;
        Celular = string.Empty;
        Direccion = string.Empty;
        Cedula = string.Empty;
        FechaNacimiento = DateOnly.FromDateTime(DateTime.Now);
    }  
        
}