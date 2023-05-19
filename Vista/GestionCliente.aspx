<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionCliente.aspx.cs" Inherits="Vista.GestionCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div >
        <h3>Gestión de Clientes</h3><hr />
            
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
                    <asp:ButtonField ButtonType="Button" Text="Asociar"   CommandName="Asociar" ControlStyle-CssClass="btn btn-primary btn-sm"  />  
      
            </Columns>

        </asp:GridView>


        <div  class="modal fade" id="modalReg" tabindex="-1" role="dialog" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
               <asp:Label ID="modalTituloReg" runat="server" Text="Titulo"></asp:Label>
              </div>
              <div class="modal-body">

                    <asp:Label ID="Label1" runat="server" Text="Ruc:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox><br />

                    <asp:Label ID="Label2" runat="server" Text="Razon Social:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox><br />

                   <asp:Label ID="Label4" runat="server" Text="Teléfono:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox><br />

                    <asp:Label ID="Label5" runat="server" Text="Contacto:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control"></asp:TextBox><br />

                    <asp:TextBox ID="txtId" runat="server" CssClass="form-control" visible="false"></asp:TextBox>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary col-lg-2 sm-2" OnClick="btnGuardarReg_Click" /><br />
              </div>
            </div>
          </div>
        </div>


      <div class="modal fade" id="modalAsoc" tabindex="-1" role="dialog" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
          <div class="modal-dialog  modal-lg"  role="document">
            <div class="modal-content">
              <div class="modal-header">
               <asp:Label ID="Label3" runat="server" Text="Sede/Cliente"></asp:Label>
              </div>
              <div class="modal-body">
                   <div class="form-group row">
                    <label class="col-sm-3 col-form-label" style="font-weight:bold;">RUC:</label>
                    <div class="col-sm-9">
                      <asp:Label ID="txtRucAsoc" runat="server"></asp:Label>
                    </div>
                  </div>

                  <div class="form-group row">
                    <label  class="col-sm-3 col-form-label" style="font-weight:bold;">Razon Social:</label>
                    <div class="col-sm-9">
                        <asp:Label ID="txtRazonSocialAsoc" runat="server" ></asp:Label>
                    </div>
                  </div>

                     <div class="form-group row">
                    <label  class="col-sm-3 col-form-label" style="font-weight:bold;">Pais/Departamento:</label>
                    <div class="col-sm-5">
                           <asp:DropDownList ID="cbSedeDepa" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                     <div class="col-sm-4">
                           <asp:Button ID="Button1" runat="server" Text="Asociar" CssClass="btn btn-primary btn-sm" OnClick="btnGuardarAsoc_Click" />
                    </div>
                  </div>

                   <asp:GridView ID="gridAsoc" runat="server" CssClass="table table-striped table-hover" />
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
             
              </div>
            </div>
          </div>
        </div>
   </div>

</asp:Content>
