using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class SuplidorBLL{

    #nullable disable
    private ApplicationDbContext _contexto;
    public SuplidorBLL(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    private bool Existe(int id)
    {
        bool existe = false;

        try
        {
            existe = _contexto.Suplidores.Any(c => c.SuplidorId == id);
        }
        catch (Exception)
        {
            throw;
        }
        return existe;
    }

    public Suplidor ExisteNombreSuplidor(string Nombre)
    {
        Suplidor existe;

        try
        {
            existe = _contexto.Suplidores              
            .Where( p => p.Nombre
            .ToLower() == Nombre.ToLower())
            .AsNoTracking()
            .SingleOrDefault();

        }
        catch (Exception)
        {
            throw;
        }
        return existe;
    }

    public bool Guardar(Suplidor suplidor)
    {
        if (!Existe(suplidor.SuplidorId))
            return Insertar(suplidor);
        else
            return Modificar(suplidor);
    }

    private bool Insertar(Suplidor suplidor)
    {
        bool Insertado = false;

        try
        {
            _contexto.Suplidores.Add(suplidor);
            Insertado = _contexto.SaveChanges() > 0;
        }
        catch (Exception)
        {
            throw;
        }
        return Insertado;
    }

    private bool Modificar(Suplidor suplidor)
    {
        bool Insertado = false;

        try
        {
            _contexto.Entry(suplidor).State = EntityState.Modified;
            Insertado =  _contexto.SaveChanges() > 0;
            _contexto.Entry(suplidor).State = EntityState.Detached;
        }
        catch (Exception)
        {
            throw;
        }
        return Insertado;
    }

    public Suplidor Buscar(int id)
    {
        return _contexto.Suplidores
        .Where(s => s.SuplidorId == id && s.Estado == true)
        .AsNoTracking()
        .SingleOrDefault();
    }
 
    public bool Eliminar(int id)
    {
        bool eliminado = false;

        try
        {
            var suplidor = Buscar(id);

            if (suplidor != null)
            {
                suplidor.Estado = false;
                eliminado = _contexto.SaveChanges() > 0;
            }
            
        }
        catch (Exception)
        {
            throw;
        }
        return eliminado;
    }

    public List<Suplidor> GetList(Expression<Func<Suplidor, bool>> suplidor)
    {
        List<Suplidor> Lista = new List<Suplidor>();
        try
        {
            Lista = _contexto.Suplidores
            .Where(c => c.Estado == true)
            .Where(suplidor)
            .AsNoTracking()
            .ToList();
        }
        catch (Exception)
        {
            throw;
        }
        return Lista;
    }
}