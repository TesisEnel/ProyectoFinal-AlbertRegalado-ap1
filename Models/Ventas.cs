using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ventas{

    [Key]
    public int VentaId { get; set; }   
    [DataType(DataType.Date)]  
    public DateOnly Fecha { get; set; }
    public decimal Total { get; set; }
    public decimal ITBIS { get; set; }
    public decimal SubTotal { get; set; }
    public double Existencia { get; set; }
    public double UnidadesVendidas { get; set;}
    [Required(ErrorMessage = "Ingrese el monto a pagar.")]
    public decimal PagoObtenido { get; set;}
    public decimal MontoRestante { get; set;}
    public decimal MetodoDePago { get; set;}

    [ForeignKey("PagoId")]
    public virtual Pago? Pago { get; set; } 
    
    [ForeignKey("VentaId")]
    public virtual List<VentasDetalle>? ventasDetalle { get; set; } 

    public Ventas()
    {
        VentaId = 0;
        Fecha = DateOnly.FromDateTime(DateTime.Now);
        ITBIS = 0;
        SubTotal = 0;
        Total = 0;
        Existencia = 0;

        ventasDetalle = new List<VentasDetalle>();
    }

}