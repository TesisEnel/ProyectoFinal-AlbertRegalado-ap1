using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class VentasBLL{

    #nullable disable
    private ApplicationDbContext _contexto;
    public VentasBLL(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    private bool Existe(int id)
    {
        bool existe = false;

        try
        { 
            existe = _contexto.ventas
            .AsNoTracking()
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
            var lista = _contexto.ventas
            .Where(x => x.VentaId == ventas.VentaId)
            .Include(x => x.ventasDetalle)
            .ThenInclude(x => x.equipo)
            .AsNoTracking()
            .SingleOrDefault();

            if(lista != null)
            {  
                _contexto.Database.ExecuteSqlRaw($"Delete FROM ventasDetalles Where VentaId = {ventas.VentaId}");

                foreach (var item in ventas.ventasDetalle)
                {
                    _contexto.Entry(item).State = EntityState.Added;

                    if (item.equipo != null)
                    {
                        item.equipo.Cantidad -= item.Cantidad;
                    }
                }

                _contexto.Entry(ventas).State = EntityState.Modified;
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

    public Ventas Buscar(int id)
    {
        Ventas venta;

        try
        {
            venta = _contexto.ventas
            .Include( e => e.ventasDetalle)
            .Where(e => e.VentaId == id)
            .Include(e => e.ventasDetalle)
            .AsNoTracking()
            .SingleOrDefault();
        }
        catch (Exception)
        {
            throw;
        }

        return venta;
    }

    public bool Eliminar(int id)
    {
        bool paso = false;

        try
        {
                        
            var venta = _contexto.ventas.Find(id);

            if (venta != null)
            {      

                foreach (var item in venta.ventasDetalle)
                {
                    _contexto.Entry(item).State = EntityState.Modified;
                    item.equipo.Cantidad += item.Cantidad;
                }

                _contexto.ventas.Remove(venta);
                paso = _contexto.SaveChanges() > 0;
                _contexto.Entry(venta).State = EntityState.Detached;
            }
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