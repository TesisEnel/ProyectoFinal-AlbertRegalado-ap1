using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PagoBLL{

    #nullable disable
    private ApplicationDbContext _contexto;
    public PagoBLL(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<Pago> Buscar(int id)
    {
        Pago pago = new Pago();

        try
        {
            pago = await _contexto.Pagos.FindAsync(id);
        }
        catch (Exception)
        {
            throw;
        }
        return pago;
    }

    public List<Pago> GetList(Expression<Func<Pago, bool>> pago)
    {
        List<Pago> list = new List<Pago>();
        try
        {
            list = _contexto.Pagos
            .Where(pago)
            .AsNoTracking()
            .ToList();
        }
        catch (Exception)
        {
            throw;
        }
        return list;
    }

}