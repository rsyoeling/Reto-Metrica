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
    public partial class GestionCliente : System.Web.UI.Page
    {
        private ClienteControl objSedeControl = new ClienteControl();
        private SedexClienteControl objSedCliControl = new SedexClienteControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarListaClientes();
                CargarSedesCB();
            }
        }

        public void CargarSedesCB()
        {
            List<Sede> lista = objSedCliControl.ListarTodosSedesOrd();
            cbSedeDepa.DataSource = lista;
            cbSedeDepa.DataTextField = "pais";
            cbSedeDepa.DataValueField = "idSede";
            cbSedeDepa.DataBind();
        }
        
        public void CargarListaClientes()
        {
            List<Cliente> lista = objSedeControl.ListarTodos();
            gridData.DataSource = lista;
            gridData.DataBind();
        }

        public void fnModalReg(bool c)
        {
            string cadena = "";

            if (c)
            {
                cadena = "<script type='text/javascript'>$('#modalReg').modal('show');</script>";
            }
            else
            {
                cadena = "<script type='text/javascript'>$('#modalReg').modal('hide');</script>";
            }
           
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(),
                                       "myFuncionAlerta", cadena, false);
        }

        public void fnModalAsoc(bool c)
        {
            string cadena = "";

            if (c)
            {
                cadena = "<script type='text/javascript'>$('#modalAsoc').modal('show');</script>";
            }
            else
            {
                cadena = "<script type='text/javascript'>$('#modalAsoc').modal('hide');</script>";
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(),
                                       "myFuncionAlerta", cadena, false);
        }

        public void fnMensaje(string mensaje)
        {
            string cadena = "<script type='text/javascript'>alert('" + mensaje + "');</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(),
                                       "myFuncionAlerta", cadena, false);
        }

        public void CargarDatosReg(Cliente obj)
        {
            modalTituloReg.Text = obj.ruc.Equals("") ? "Nuevo Cliente" : "Editar Cliente";
            txtRuc.Text = obj.ruc;
            txtRazonSocial.Text = obj.razonSocial;
            txtContacto.Text = obj.contacto;
            txtTelefono.Text = obj.telefono;
            txtId.Text = obj.ruc;

            txtRuc.Enabled = (obj.ruc.Length == 0);
        }

        public void CargarDatosAsoc(string ruc)
        {
            List<SedeCliente> lista = objSedCliControl.ListarTodosSedesCliente(ruc);
            gridAsoc.DataSource = lista;
            gridAsoc.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Cliente obj = new Cliente();
            obj.ruc = "";
            CargarDatosReg(obj);
            fnModalReg(true);
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gridData.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            string msg = "", code = "";
            int index = 3;

            switch (e.CommandName.ToUpper())
            {
                case "ELIMINAR":
                    {
                        code = gridData.SelectedRow.Cells[index].Text;
                        msg = objSedeControl.Eliminar(code);

                        if (msg.Equals("OK"))
                        {
                            CargarListaClientes();
                        }
                        else
                        {
                            fnMensaje(msg);
                        }

                        break;
                    }
                case "EDITAR":
                    {
                        code = gridData.SelectedRow.Cells[index].Text;

                        Cliente obj = objSedeControl.BuscarPorRuc(code);
                        CargarDatosReg(obj);
                        fnModalReg(true);
                        break;
                    }

                case "ASOCIAR":
                    {
                        code = gridData.SelectedRow.Cells[index].Text;

                        Cliente obj = objSedeControl.BuscarPorRuc(code);

                        txtRucAsoc.Text = obj.ruc;
                        txtRazonSocialAsoc.Text = obj.razonSocial;

                        CargarDatosAsoc(obj.ruc);

                        fnModalAsoc(true);
                        break;
                    }

            }
        }

        protected void btnGuardarReg_Click(object sender, EventArgs e)
        {
            Cliente obj = new Cliente();
            obj.ruc = txtRuc.Text.Trim();
            obj.razonSocial = txtRazonSocial.Text.Trim();
            obj.contacto = txtContacto.Text.Trim();
            obj.telefono = txtTelefono.Text.Trim();

            string msg = "";

            if (modalTituloReg.Text.Equals("Nuevo Cliente"))
            {
                msg = objSedeControl.Agregar(obj);
            }
            else
            {
                msg = objSedeControl.Editar(obj);
            }

            if (msg.Equals("OK"))
            {
                fnModalReg(false);
                CargarListaClientes();
            }
            else
            {
                fnMensaje(msg);
            }
        }
        protected void btnGuardarAsoc_Click(object sender, EventArgs e)
        {
           string ruc = txtRucAsoc.Text.Trim();
           int idSede = int.Parse(cbSedeDepa.SelectedValue.ToString().Trim());

            string msg = msg = objSedCliControl.Agregar(ruc, idSede); 

            if (msg.Equals("OK"))
            {
                CargarDatosAsoc(ruc);
            }
            else
            {
                fnMensaje(msg);
            }
        }
        
    }
}