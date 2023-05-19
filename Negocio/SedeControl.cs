using Modelo;
using ModeloDAO.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SedeControl
    {
        private SedeDAO sedeDao;

        public SedeControl()
        {
            sedeDao = new SedeDAO();
        }

        public List<Sede> ListarTodos()
        {
            return sedeDao.ListarTodosDA();
        }

        public Sede BuscarPorId(int id)
        {
            return sedeDao.BuscarPorIdDA(id);
        }

        public string Agregar(Sede obj)
        {
            return sedeDao.MantenimientoDA(obj , 1);
        }

        public string Editar(Sede obj)
        {
            return sedeDao.MantenimientoDA(obj, 2);
        }
        public string Eliminar(int id)
        {
            Sede obj = new Sede();
            obj.idSede = id;
            return sedeDao.MantenimientoDA(obj, 3);
        }
    }
}
