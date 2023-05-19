using Modelo;
using ModeloDAO.Config;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDAO.DAO
{
    public class ClienteDAO
    {
        public List<Cliente> ListarTodosDA()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                using (OracleConnection cn = new ConexionDA().GetSqlConnection())
                {
                    cn.Open();
                    OracleCommand cmd = new OracleCommand("PK_CLIENTE.SP_ListarClientes", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("TABLA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Cliente e = new Cliente();
                        e.ruc = Convert.ToString(dr["ruc"]);
                        e.razonSocial = Convert.ToString(dr["razon_social"]);
                        e.telefono = Convert.ToString(dr["telefono"]);
                        e.contacto = Convert.ToString(dr["contacto"]);
                        lista.Add(e);
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lista;
        }

        public Cliente BuscarPorRucDA(string ruc)
        {
            Cliente e = null;

            using (OracleConnection cn = new ConexionDA().GetSqlConnection())
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand("PK_CLIENTE.SP_BuscarPorRucCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("TABLA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("cRuc", ruc);

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    e = new Cliente();
                    e.ruc = Convert.ToString(dr["ruc"]);
                    e.razonSocial = Convert.ToString(dr["razon_social"]);
                    e.telefono = Convert.ToString(dr["telefono"]);
                    e.contacto = Convert.ToString(dr["contacto"]);
                }

                cn.Close();
            }
            return e;
        }

        public string MantenimientoDA(Cliente obj, int accion)
        {
            string msg = "";
            int res = 0;
            try
            {
                using (OracleConnection cn = new ConexionDA().GetSqlConnection())
                {
                    cn.Open();
                    OracleCommand cmd = new OracleCommand("PK_CLIENTE.SP_Mant_Cliente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("cRuc", obj.ruc);
                    cmd.Parameters.Add("cRazonSocial", obj.razonSocial);
                    cmd.Parameters.Add("cTelefono", obj.telefono);
                    cmd.Parameters.Add("cContacto", obj.contacto);
                    cmd.Parameters.Add("nAccion", accion);

                    OracleParameter outputParam = cmd.Parameters.Add("nResultado", OracleDbType.Int64);
                    outputParam.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    res = int.Parse(outputParam.Value.ToString());

                    if (res > 0)
                    {
                        msg = "OK";
                    }
                    else
                    {
                        msg = "No se pudo realizar operación";
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }

            return msg;
        }
    }
}
