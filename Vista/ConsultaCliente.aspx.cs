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
    public partial class ConsultaCliente : System.Web.UI.Page
    {
        private SedexClienteControl objSedCliControl = new SedexClienteControl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltroCB();
            }
        }

        public void CargarFiltroCB() { 
            cbFiltro.Items.Add( new ListItem("Ruc", "1"));
            cbFiltro.Items.Add(new ListItem("Razon Social", "2"));
            cbFiltro.Items.Add(new ListItem("Pais", "3"));
        }

        public void fnMensaje(string mensaje)
        {
            string cadena = "<script type='text/javascript'>alert('" + mensaje + "');</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(),
                                       "myFuncionAlerta", cadena, false);
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            int accion = int.Parse(cbFiltro.SelectedValue.ToString());
            string valor = txtFiltro.Text.Trim();

            List<SedeClienteCons> lista = objSedCliControl.ListarConsultaFiltro(accion, valor);

            if (lista.Count() == 0)
            {
                fnMensaje("No se encontraron coincidencias para el criterio de busqueda.");
            }

            gridData.DataSource = lista;
            gridData.DataBind();
        }
    }
}