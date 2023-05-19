using Modelo;
using ModeloDAO.Config;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDAO.DAO
{
    public class SedeDAO
    {
        public List<Sede> ListarTodosDA()
        {
            List<Sede> lista = new List<Sede>();
            try
            {
                using (OracleConnection cn = new ConexionDA().GetSqlConnection())
                {
                    cn.Open();
                    OracleCommand cmd = new OracleCommand("PK_SEDE.SP_ListarSedes", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("TABLA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Sede e = new Sede();
                        e.idSede = Convert.ToInt32(dr["id_sede"]);
                        e.pais = Convert.ToString(dr["pais"]);
                        e.departamento = Convert.ToString(dr["departamento"]);
                        e.direccion = Convert.ToString(dr["direccion"]);
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

        public Sede BuscarPorIdDA(int id)
        {
            Sede e = null;

            using (OracleConnection cn = new ConexionDA().GetSqlConnection())
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand("PK_SEDE.SP_BuscarPorIdSede", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("TABLA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("nId", id);

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    e = new Sede();
                    e.idSede = Convert.ToInt32(dr["id_sede"]);
                    e.pais = Convert.ToString(dr["pais"]);
                    e.departamento = Convert.ToString(dr["departamento"]);
                    e.direccion = Convert.ToString(dr["direccion"]);
                    e.telefono = Convert.ToString(dr["telefono"]);
                    e.contacto = Convert.ToString(dr["contacto"]);
                }

                cn.Close();
            }
            return e;
        }

        public string MantenimientoDA(Sede obj , int accion)
        {
            string msg = "";
            int res = 0;
            try
            {
                using (OracleConnection cn = new ConexionDA().GetSqlConnection())
                {
                    cn.Open();
                    OracleCommand cmd = new OracleCommand("PK_SEDE.SP_Mant_Sede", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("nid_sede", obj.idSede);
                    cmd.Parameters.Add("cPais", obj.pais);
                    cmd.Parameters.Add("cDepartamento", obj.departamento);
                    cmd.Parameters.Add("cDireccion", obj.direccion);
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
                    else if (res == -1)
                    {
                        msg = "Lo sentimos!! La sede ya cuenta con el país "+obj.pais+" y departamento "+obj.departamento+" existente.";
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
