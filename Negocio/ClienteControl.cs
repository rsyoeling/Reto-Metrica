using Modelo;
using ModeloDAO.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClienteControl
    {
        private ClienteDAO clienteDao;

        public ClienteControl()
        {
            clienteDao = new ClienteDAO();
        }

        public List<Cliente> ListarTodos()
        {
            return clienteDao.ListarTodosDA();
        }

        public Cliente BuscarPorRuc(string ruc)
        {
            return clienteDao.BuscarPorRucDA(ruc);
        }

        public string Agregar(Cliente obj)
        {
            if (clienteDao.BuscarPorRucDA(obj.ruc) == null)
            {
                return clienteDao.MantenimientoDA(obj, 1);
            }
            else
            {
                return "El ruc " + obj.ruc + " no se encuentra disponible";
            }
            
        }

        public string Editar(Cliente obj)
        {
            return clienteDao.MantenimientoDA(obj, 2);
        }
        public string Eliminar(string ruc)
        {
            Cliente obj = new Cliente();
            obj.ruc = ruc;
            return clienteDao.MantenimientoDA(obj, 3);
        }
    }
}
