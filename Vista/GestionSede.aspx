<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionSede.aspx.cs" Inherits="Vista.GestionSede" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div >
    <h3>Gestión de Sedes</h3><hr />
            
        <div class="row">
            <div class="col-lg-1 sm-1">
                <asp:Button ID="btnAgregar" runat="server" Text="Nuevo"  CssClass="btn btn-success btn-sm" OnClick="btnAgregar_Click"  />
            </div>
        </div>
        <br />  
    <asp:GridView ID="gridData" runat="server" CssClass="table table-striped table-hover" style="background-color:white ;" 
        
         OnRowCommand="gridData_RowCommand" >
        <Columns>
                <asp:ButtonField ButtonType="Button" Text="Editar"   CommandName="Editar" ControlStyle-CssClass="btn btn-info btn-sm" />  
                <asp:ButtonField ButtonType="Button" Text="Eliminar"   CommandName="Eliminar" ControlStyle-CssClass="btn btn-danger btn-sm"  />  
        </Columns>

    </asp:GridView>


    <div  class="modal fade" id="modalReg" tabindex="-1" role="dialog" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
           <asp:Label ID="modalTituloReg" runat="server" Text="Titulo"></asp:Label>
          </div>
          <div class="modal-body">

                <asp:Label ID="Label1" runat="server" Text="Pais:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPais" runat="server" CssClass="form-control"></asp:TextBox><br />

                <asp:Label ID="Label2" runat="server" Text="Departamento:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control"></asp:TextBox><br />

                <asp:Label ID="Label3" runat="server" Text="Dirección:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox><br />

               <asp:Label ID="Label4" runat="server" Text="Teléfono:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox><br />

                <asp:Label ID="Label5" runat="server" Text="Contacto:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control"></asp:TextBox><br />

                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" visible="false"></asp:TextBox>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary col-lg-2 sm-2" OnClick="btnGuardar_Click" /><br />
          </div>
        </div>
      </div>
    </div>
   </div>

</asp:Content>
