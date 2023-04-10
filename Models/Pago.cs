using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pago{

    [Key]  
    public int PagoId { get; set; }
    [Required(ErrorMessage = "Seleccione un metodo de pago")]
    public string? Metodo { get; set; } 
}