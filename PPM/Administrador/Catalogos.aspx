<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogos.aspx.cs" Inherits="PPM.Administrador.Catalogos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            EndRequestHandler();

            function BeginRequestHandler(sender, args) {

            }

            function EndRequestHandler(sender, args) {
                $('#liAdministrador').addClass(" border-danger border-2 fw-bold");
                $('#lnkCatalogos').addClass(" bg-danger text-white");
            }
        });
    </script>
    <asp:UpdateProgress ID="upProgress" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="position-fixed vw-100 vh-100 bg-white align-middle text-center fw-bold align-self-center text-dark" style="opacity: 0.5!important; top: 0px!important; left: 0px!important; z-index: 9999999!important;">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div class="w-100 p-2 pt-1" style="height: 92vh!important; max-height: 92vh!important; overflow: hidden!important;">
                <div class="row h-100">
                    <div class="col-4 h-100">
                        <div class="card small h-100">
                            <div class="card-header shadow-sm">
                                <li class="fa fa-users fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Administradores</span></li>
                            </div>
                            <div class="card-body">
                                <div class="row pb-1 border-0 border-bottom border-danger">
                                    <div class="col-10">
                                        <div class="input-group input-group-sm text-nowrap flex-nowrap">
                                            <span class="input-group-text bg-light">
                                                <li class="fa fa-filter"></li>
                                            </span>
                                            <span class="input-group-text">Administrador:</span>
                                            <asp:TextBox ID="txtAdministradorSearch" runat="server" CssClass="form-control" AutoPostBack="false" placeholder="buscar por nombre o username" Style="max-width: 200px!important;"></asp:TextBox>
                                            <asp:LinkButton ID="lnkAdministradorSearch" runat="server" CssClass="btn btn-sm small btn-light border shadow" OnClick="lnkAdministradorSearch_Click" ToolTip="Buscar">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <asp:LinkButton ID="lnkAdministrador_Agregar" runat="server" CssClass="btn btn-sm small btn-light border float-end" OnClick="lnkAdministrador_Agregar_Click" ToolTip="Agregar nuevo administrador"><span>Agregar</span>&nbsp;<li class="fa fa-plus"></li></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row" style="max-height: 81vh!important; overflow: auto;">
                                    <div class="col">
                                        <asp:Button ID="btnHiddenAdministrador" runat="server" CssClass="d-none" />
                                        <asp:ModalPopupExtender ID="mdlAdministrador" runat="server" TargetControlID="btnHiddenAdministrador" PopupControlID="pnlAdministradores" CancelControlID="btnAdministrador_Cancelar" BackgroundCssClass="modal-backdrop"></asp:ModalPopupExtender>
                                        <asp:GridView ID="gvAdministradores" runat="server" DataKeyNames="username" CssClass="table table-sm small table-hover small w-100" GridLines="None" AutoGenerateColumns="false" OnRowDeleting="gvAdministradores_RowDeleting" OnRowEditing="gvAdministradores_RowEditing"
                                            EmptyDataText="No se encontraron coincidencias">
                                            <Columns>
                                                <asp:BoundField HeaderText="UserName" DataField="username" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Nombre" DataField="nombre" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Activo" DataField="activo" ReadOnly="true" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <div class="btn-group border">
                                                            <asp:LinkButton runat="server" CommandName="edit" CssClass="btn btn-sm small btn-light spinner-border-sm"><li class="fa fa-edit" title="editar"></li></asp:LinkButton><%--OnClientClick="return confirm('Desea activar/desactivar el administrador ¿Continuar?');"--%>
                                                            <asp:LinkButton runat="server" CommandName="delete" CssClass="btn btn-sm small btn-light" OnClientClick="return confirm('Se eliminará el administrador seleccionado ¿Continuar?');"><li class="fa fa-trash" title="eliminar"></li></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-4 h-100">
                        <div class="card small h-100">
                            <div class="card-header shadow-sm">
                                <li class="fa fa-cube fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Prensas</span></li>
                            </div>
                            <div class="card-body">
                                <div class="row pb-1 border-0 border-bottom border-danger">
                                    <div class="col-10">
                                        <div class="input-group input-group-sm text-nowrap flex-nowrap">
                                            <span class="input-group-text bg-light">
                                                <li class="fa fa-filter"></li>
                                            </span>
                                            <span class="input-group-text">Prensa:</span>
                                            <asp:TextBox ID="txtPrensaSearch" runat="server" CssClass="form-control" AutoPostBack="false" placeholder="buscar por nombre o descripción" Style="max-width: 200px!important;"></asp:TextBox>
                                            <asp:LinkButton ID="lnkPrensaSearch" runat="server" CssClass="btn btn-sm small btn-light border shadow" OnClick="lnkPrensaSearch_Click" ToolTip="Buscar">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <asp:LinkButton ID="lnkPrensa_Agregar" runat="server" CssClass="btn btn-sm small btn-light border float-end" OnClick="lnkPrensa_Agregar_Click" ToolTip="Agregar nueva prensa"><span>Agregar</span>&nbsp;<li class="fa fa-plus"></li></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row" style="max-height: 81vh!important; overflow: auto;">
                                    <div class="col">
                                        <asp:Button ID="btnHiddenPrensa" runat="server" CssClass="d-none" />
                                        <asp:ModalPopupExtender ID="mdlPrensa" runat="server" TargetControlID="btnHiddenPrensa" PopupControlID="pnlPrensas" CancelControlID="btnPrensa_Cancelar" BackgroundCssClass="modal-backdrop"></asp:ModalPopupExtender>
                                        <asp:GridView ID="gvPrensas" runat="server" DataKeyNames="id" CssClass="table table-sm small table-hover small w-100" GridLines="None" AutoGenerateColumns="false" OnRowDeleting="gvPrensas_RowDeleting" OnRowEditing="gvPrensas_RowEditing" OnSelectedIndexChanging="gvPrensas_SelectedIndexChanging">
                                            <SelectedRowStyle CssClass="fw-bold border-primary text-decoration-underline border border-2" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Id" DataField="id" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Nombre" DataField="nombre" ReadOnly="true" />
                                                <%--<asp:BoundField HeaderText="Descripción" DataField="descripcion" ReadOnly="true" />--%>
                                                <asp:BoundField HeaderText="Activo" DataField="activo" ReadOnly="true" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <div class="btn-group border">
                                                            <asp:LinkButton runat="server" CommandName="edit" CssClass="btn btn-sm small btn-light spinner-border-sm"><li class="fa fa-edit" ></li></asp:LinkButton>
                                                            <asp:LinkButton runat="server" CommandName="delete" CssClass="btn btn-sm small btn-light" OnClientClick="return confirm('Se eliminará la prensa seleccioanda ¿Continuar?');"><li class="fa fa-trash" title="eliminar"></li></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlAdministradores" runat="server" CssClass=" w-50 shadow-lg">
                <div class="card small shadow">
                    <div class="card-header">
                        <span class="fs-6">Datos del Administrador</span>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <div class="mb-3">
                                    <label class="form-label">Username</label>
                                    <asp:TextBox ID="txtAdministrador_Username" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtAdministrador_Nombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Activo</label>
                                    <asp:CheckBox ID="chkAdministrador_Activo" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="float-end">
                            <asp:Button ID="btnAdministrador_Cancelar" runat="server" CssClass="btn btn-sm btn-light border" Text="Cerrar" />
                            <asp:Button ID="btnAdministrador_Guardar" runat="server" CssClass="btn btn-sm btn-light border" Text="Guardar" OnClick="btnAdministrador_Guardar_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPrensas" runat="server" CssClass=" w-50 shadow-lg">
                <div class="card small shadow">
                    <div class="card-header">
                        <span class="fs-6">Datos de la Prensa</span>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <div class="mb-3">
                                    <label class="form-label">Id</label>
                                    <asp:TextBox ID="txtPrensa_Id" runat="server" CssClass="form-control" type="Number" step="1"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtPrensa_Nombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <%--<div class="mb-3">
                                    <label class="form-label">Descripción</label>
                                    <asp:TextBox ID="txtPrensa_Descripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>--%>
                                <div class="mb-3">
                                    <label class="form-label">Activo</label>
                                    <asp:CheckBox ID="chkPrensa_Activo" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="float-end">
                            <asp:Button ID="btnPrensa_Cancelar" runat="server" CssClass="btn btn-sm btn-light border" Text="Cerrar" />
                            <asp:Button ID="btnPrensa_Guardar" runat="server" CssClass="btn btn-sm btn-light border" Text="Guardar" OnClick="btnPrensa_Guardar_Click" />
                            </di>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
