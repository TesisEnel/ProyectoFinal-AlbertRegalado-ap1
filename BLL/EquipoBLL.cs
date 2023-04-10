using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class EquipoBLL{

    #nullable disable
    private ApplicationDbContext _contexto;
    public EquipoBLL(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    private bool Existe(int id)
    {
        bool existe = false;

        try
        {
            existe = _contexto.Equipos.Any(c => c.EquipoId == id);
        }
        catch (Exception)
        {
            throw;
        }
        return existe;
    }

    public Equipo ExisteNombre(string Nombre)
    {
        Equipo existe;
        
        try
        {
            existe = _contexto.Equipos                
            .Where( p => p.Nombre
            .ToLower() == Nombre.ToLower())
            .AsNoTracking()
            .SingleOrDefault();

        }
        catch
        {
            throw;
        }
        return existe;
    }

    public bool Guardar(Equipo equipo)
    {
            
        if (!Existe(equipo.EquipoId))
            return  Insertar(equipo);
        else
            return  Modificar(equipo);
    }

    private bool Insertar(Equipo equipo)
    {
        bool Insertado = false;

        try
        {
            if (_contexto.Equipos.Add(equipo) != null)
            {
                Insertado =  _contexto.SaveChanges() > 0;
            }
        }
        catch (Exception)
        {
            throw;
        }
        return Insertado;
    }

    private bool Modificar(Equipo equipo)
    {
        bool Insertado = false;

        try
        {
            _contexto.Entry(equipo).State = EntityState.Modified;
            Insertado = _contexto.SaveChanges() > 0;
            _contexto.Entry(equipo).State = EntityState.Detached;

        }
        catch (Exception)
        {
            throw;
        }
        return Insertado;
    }

    public Equipo Buscar(int id)
    {
        Equipo equipo = new Equipo();

        try
        {
            equipo = _contexto.Equipos
            .Where(p => p.EquipoId == id)
            .AsNoTracking()
            .SingleOrDefault();
            
        }
        catch (Exception)
        {
            throw;
        }
        return equipo;
    }

    public async Task<bool> Eliminar(int id)
    {
        bool Eliminado = false;

        try
        {
            var equipo = await _contexto.Equipos.FindAsync(id);

            if (equipo!= null)
            {
                _contexto.Equipos.Remove(equipo);
                Eliminado = (await _contexto.SaveChangesAsync() > 0);
            }
        }
        catch (Exception)
        {
            throw;
        }
        return Eliminado;
    }

    public List<Equipo> GetList(Expression<Func<Equipo, bool>> criterio)
    {
        List<Equipo> Lista = new List<Equipo>();
        try
        {
            Lista = _contexto.Equipos
            .Where(criterio)
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
