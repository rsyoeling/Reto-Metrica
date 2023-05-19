<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaCliente.aspx.cs" Inherits="Vista.ConsultaCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-row">
    <div class="form-group col-md-3">
      <label>Consultar Por:</label>
     <asp:DropDownList ID="cbFiltro" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    <div class="form-group col-md-3">
      <label>Dato:</label>
      <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Dato a consultar"></asp:TextBox>
    </div>
      <div class="form-group col-md-3 mt-1">
      <br />
      <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn btn-primary" OnClick="btnConsultar_Click" />
    </div>

  </div>

   <asp:GridView ID="gridData" runat="server" CssClass="table table-striped table-hover"  />
      
</asp:Content>
