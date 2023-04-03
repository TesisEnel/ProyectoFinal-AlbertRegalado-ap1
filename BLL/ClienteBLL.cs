using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


public class ClienteBLL{

    #nullable disable
    private ApplicationDbContext _contexto;
    public ClienteBLL(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    private bool Existe(int id)
    {
        bool existe = false;

        try
        {
            existe = _contexto.Clientes.Any(c => c.ClienteId == id);
        }
        catch (Exception)
        {
            throw;
        }
        return existe;
    }

    public Cliente ExisteCedula(string Cedula)
    {
        Cliente existe;

        try
        {
            existe = _contexto.Clientes               
            .Where( p => p.Cedula
            .ToLower() == Cedula.ToLower())
            .AsNoTracking()
            .SingleOrDefault();

        }
        catch
        {
            throw;
        }
        return existe;
    }

    public bool Guardar(Cliente clientes)
    {
        if (!Existe(clientes.ClienteId))
            return Insertar(clientes);
        else
            return Modificar(clientes);
    }

    private bool Insertar(Cliente clientes)
    {
        bool Insertado = false;

        try
        {
            _contexto.Clientes.Add(clientes);
            Insertado = _contexto.SaveChanges() > 0;
        }
        catch (Exception)
        {
            throw;
        }
        return Insertado;
    }

    private bool Modificar(Cliente clientes)
    {
        bool Insertado = false;

        try
        {
            _contexto.Entry(clientes).State = EntityState.Modified;
            Insertado =  _contexto.SaveChanges() > 0;
        }
        catch (Exception)
        {
            throw;
        }
        return Insertado;
    }

    public Cliente Buscar(int id)
    {
        Cliente cliente = new Cliente();

        try
        {
            cliente = _contexto.Clientes.Find(id);
        }
        catch (Exception)
        {
            throw;
        }
        return cliente;
    }

    public async Task<bool> Eliminar(int id)
    {
        bool Eliminado = false;

        try
        {
            var cliente = await _contexto.Clientes.FindAsync(id);

            if (cliente != null)
            {
                _contexto.Clientes.Remove(cliente);
                Eliminado = (await _contexto.SaveChangesAsync() > 0);
            }
        }
        catch (Exception)
        {
            throw;
        }
        return Eliminado;
    }

    public List<Cliente> GetList(Expression<Func<Cliente, bool>> criterio)
    {
        List<Cliente> Lista = new List<Cliente>();
        try
        {
            Lista = _contexto.Clientes
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