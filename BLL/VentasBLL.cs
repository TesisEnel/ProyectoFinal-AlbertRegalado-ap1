using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class VentasBLL{

    #nullable disable
    private ApplicationDbContext _contexto;
    public VentasBLL(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public bool Existe(int id)
    {
        bool existe = false;

        try
        { 
            existe = _contexto.ventas
            .Any(p => p.VentaId == id);
        }
        catch (Exception)
        {
            throw;
        }
        
        return existe;
    }

    public bool Guardar(Ventas ventas)
    { 
        if (!Existe(ventas.VentaId))
            return  Insertar(ventas);
        else
            return  Modificar(ventas);
    }

    private bool Insertar(Ventas ventas)
    { 
        bool paso = false;

        try
        {
            if (_contexto.ventas.Add(ventas) != null)
            {
                foreach (var item in ventas.ventasDetalle) 
                {
     
                    item.equipo.Cantidad -= item.Cantidad;  
                    _contexto.Entry(item.equipo).State = EntityState.Modified;

                }

                paso =  _contexto.SaveChanges() > 0;
                _contexto.Entry(ventas).State = EntityState.Detached;
        
            }

        }
        catch (Exception)
        {
            throw;
        }

        return paso;
    }

    private bool Modificar(Ventas ventas)
    {
        bool paso = false;

        try
        {
            var ventaExistente = _contexto.ventas
            .Include(v => v.ventasDetalle)
            .SingleOrDefault(v => v.VentaId == ventas.VentaId);

            if (ventaExistente != null)
            {
                // Actualizar la cantidad de equipos en stock
                foreach (var detalle in ventaExistente.ventasDetalle)
                {
                    var equipo = _contexto.Equipos.Find(detalle.EquipoId);

                    if (equipo != null)
                    {
                        equipo.Cantidad += detalle.Cantidad;
                        _contexto.Entry(equipo).State = EntityState.Modified;
                    }
                }

                // Eliminar los detalles de la venta existente
                _contexto.ventasDetalles.RemoveRange(ventaExistente.ventasDetalle);

                // Agregar los nuevos detalles a la venta existente
                foreach (var detalle in ventas.ventasDetalle)
                {
                    var equipo = _contexto.Equipos.Find(detalle.EquipoId);

                    if (equipo != null)
                    {
                        equipo.Cantidad -= detalle.Cantidad;
                        _contexto.Entry(equipo).State = EntityState.Modified;
                    }

                    ventaExistente.ventasDetalle.Add(detalle);
                }

                // Actualizar la venta existente
                _contexto.Entry(ventaExistente).State = EntityState.Modified;
                paso = _contexto.SaveChanges() > 0;
                _contexto.Entry(ventaExistente).State = EntityState.Detached;
            }
        }
        catch (Exception)
        {
            throw;
        }

        return paso;
    }

    public Ventas Buscar(int id)
    {
         
        Ventas ventas;

        try
        {
            ventas = _contexto.ventas
            .Where(e => e.VentaId == id)
            .Include(e => e.ventasDetalle)
            .AsNoTracking()
            .SingleOrDefault();
        }
        catch (Exception)
        {
            throw;
        }

        return ventas;

    }

    public bool Eliminar(Ventas ventas)
    {
        bool paso = false;

        try
        {               
            foreach(var item in ventas.ventasDetalle)
            {
                var _item = _contexto.Equipos.Find(item.EquipoId);

                if(_item != null)
                {
                    _item.Cantidad += item.Cantidad;
                    _contexto.Entry(_item).State = EntityState.Modified;
                }
            }

            _contexto.Entry(ventas).State = EntityState.Deleted;
            paso = _contexto.SaveChanges() > 0;
            _contexto.Entry(ventas).State = EntityState.Detached;
                
        }
        catch (Exception)
        {
            throw;
        }
            
        return paso;
    }

    public List<Ventas> GetList(Expression<Func<Ventas, bool>> criterio)
    {
        List<Ventas> lista = new List<Ventas>();

        try
        {
            lista =  _contexto.ventas
            .Include(x => x.ventasDetalle)
            .Where(criterio)
            .AsNoTracking()
            .ToList();
        }
        catch (Exception)
        {
            throw;
        }
        
        return lista;
    }

}