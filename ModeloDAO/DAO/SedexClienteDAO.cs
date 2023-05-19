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
    public class SedexClienteDAO
    {
        public List<Sede> ListarTodosSedesOrdDA()
        {
            List<Sede> lista = new List<Sede>();
            try
            {
                using (OracleConnection cn = new ConexionDA().GetSqlConnection())
                {
                    cn.Open();
                    OracleCommand cmd = new OracleCommand("PK_SEDE.SP_ListarSedeOrd", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("TABLA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Sede e = new Sede();
                        e.idSede = Convert.ToInt32(dr["id_sede"]);
                        e.pais = Convert.ToString(dr["pais"]);
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

        public List<SedeCliente> ListarTodosSedeClienteDA(string ruc)
        {
            List<SedeCliente> lista = new List<SedeCliente>();
            try
            {
                using (OracleConnection cn = new ConexionDA().GetSqlConnection())
                {
                    cn.Open();
                    OracleCommand cmd = new OracleCommand("PK_SEDExCliente.SP_ListarSedeCliente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("TABLA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("cRuc", ruc);
                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        SedeCliente e = new SedeCliente();
                        e.pais = Convert.ToString(dr["pais"]);
                        e.departamento = Convert.ToString(dr["departamento"]);
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

        public string MantenimientoDA(int id , string ruc, int idSede, int accion)
        {
            string msg = "";
            int res = 0;
            try
            {
                using (OracleConnection cn = new ConexionDA().GetSqlConnection())
                {
                    cn.Open();
                    OracleCommand cmd = new OracleCommand("PK_SEDExCliente.SP_Mant_SedeCliente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("nId_sedeCli", id);
                    cmd.Parameters.Add("nId_sede", idSede);
                    cmd.Parameters.Add("cRuc", ruc);
                    cmd.Parameters.Add("nAccion", accion);

                    OracleParameter outputParam = cmd.Parameters.Add("nResultado", OracleDbType.Int64);
                    outputParam.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    res = int.Parse(outputParam.Value.ToString());

                    if (res > 0)
                    {
                        msg = "OK";
                    }
                    else if (res == -1) 
                    {
                        msg = "Lo sentimos!! Usted ya cuenta con una sede: país y departamento existente";
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

        public List<SedeClienteCons> ListarConsultaFiltroDA(int accion , string valor)
        {
            List<SedeClienteCons> lista = new List<SedeClienteCons>();
            try
            {
                using (OracleConnection cn = new ConexionDA().GetSqlConnection())
                {
                    cn.Open();
                    OracleCommand cmd = new OracleCommand("PK_SEDExCliente.SP_ConsultaFiltroClienteSede", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("TABLA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("cValor", valor);
                    cmd.Parameters.Add("nAccion", accion);
                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        SedeClienteCons e = new SedeClienteCons();
                        e.departamento = Convert.ToString(dr["departamento"]);
                        e.pais = Convert.ToString(dr["pais"]);
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
    }
}
