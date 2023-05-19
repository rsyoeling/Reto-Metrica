using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vista
{
	public partial class GestionSede : System.Web.UI.Page
	{
		private SedeControl objSedeControl = new SedeControl();
        

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
				CargarListaSedes();

			}
		}

		public void CargarListaSedes()
        {
			List<Sede> lista = objSedeControl.ListarTodos();
			gridData.DataSource = lista;
			gridData.DataBind();
        

        }

		public void fnModalAbrir()
        {
            string cadena = "<script type='text/javascript'>$('#modalReg').modal('show');</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(),
                                       "myFuncionAlerta", cadena, false);
        }

        public void fnModalCerrar()
        {
            string cadena = "<script type='text/javascript'>$('#modalReg').modal('hide');</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(),
                                       "myFuncionAlerta", cadena, false);
        }


        public void fnMensaje(string mensaje)
        {
              string cadena = "<script type='text/javascript'>alert('" + mensaje + "');</script>";
          //  string cadena = "<script type='text/javascript'>fnToast('success','" + mensaje + "');</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(),
                                       "myFuncionAlerta2", cadena, false);
        }

        public void CargarDatos(Sede obj)
        {
            modalTituloReg.Text = obj.idSede == 0 ? "Nueva Sede" : "Editar Sede";
            txtPais.Text = obj.pais;
            txtDepartamento.Text = obj.departamento;
            txtDireccion.Text = obj.direccion; 
            txtContacto.Text = obj.contacto;
            txtTelefono.Text = obj.telefono;
            txtId.Text = obj.idSede.ToString();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            CargarDatos(new Sede());
            fnModalAbrir();
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gridData.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            string msg = "";
            int index = 2 , code;
           
            switch (e.CommandName.ToUpper())
            {
                case "ELIMINAR":
                    {
                        code = int.Parse(gridData.SelectedRow.Cells[index].Text);
                        msg = objSedeControl.Eliminar(code);

                        if (msg.Equals("OK"))
                        {
                            CargarListaSedes();
                        }
                        else
                        {
                            fnMensaje(msg);
                        }
                
                        break;
                    }
                case "EDITAR":
                    {
                        code = int.Parse(gridData.SelectedRow.Cells[index].Text);
                       
                        Sede obj = objSedeControl.BuscarPorId(code);
                        CargarDatos(obj);
                        fnModalAbrir();
                        break;
                    }

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Sede obj = new Sede();
            obj.pais = txtPais.Text;
            obj.departamento = txtDepartamento.Text;
            obj.direccion = txtDireccion.Text;
            obj.contacto = txtContacto.Text;
            obj.telefono = txtTelefono.Text;
            obj.idSede = int.Parse(txtId.Text);

            string msg = "";

            if (obj.idSede == 0)
            {
                msg = objSedeControl.Agregar(obj);
            }
            else
            {
                msg = objSedeControl.Editar(obj);
            }

            if (msg.Equals("OK"))
            {
                fnModalCerrar();
                CargarListaSedes();
            }
            else
            {
                fnMensaje(msg);
            }
        }
    }
}