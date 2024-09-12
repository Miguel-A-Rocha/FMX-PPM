<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Captura.aspx.cs" Inherits="PPM.Administrador.Captura" %>

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
                $('#lnkCaptura').addClass(" bg-danger text-white");
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
                    <div class="col-2 h-100">
                        <div class="card small h-100">
                            <div class="card-header shadow-sm">
                                <li class="fa fa-cube fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Fecha-Turno</span></li>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div class="mb-3">
                                            <label class="form-label">Fecha</label>
                                            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control datepicker border-2 border-primary"></asp:TextBox>
                                        </div>
                                        <div class="mb-3">
                                            <label class="form-label">Turno</label>
                                            <asp:DropDownList ID="ddlTurno" runat="server" CssClass="form-control no-chosen border-2 border-primary" OnSelectedIndexChanged="ddlTurno_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1ro"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2do"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3ro"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="mb-3">
                                            <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-light border shadow w-100 text-center border-dark" ToolTip="Buscar" OnClick="lnkSearch_Click" >Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-3 h-100">
                        <div class="card small h-100">
                            <div class="card-header shadow-sm">
                                <li class="fa fa-cube fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Prensas</span></li>
                            </div>
                            <div class="card-body">
                                <div class="row pb-1 border-0 border-bottom border-danger">
                                    <div class="col">
                                        <div class="input-group input-group-sm text-nowrap flex-nowrap">
                                            <span class="input-group-text bg-light">
                                                <li class="fa fa-filter"></li>
                                            </span>
                                            <span class="input-group-text">Prensa:</span>
                                            <asp:TextBox ID="txtPrensaSearch" runat="server" CssClass="form-control" AutoPostBack="false" placeholder="buscar prensa" Style="max-width: 200px!important;"></asp:TextBox>
                                            <asp:LinkButton ID="lnkPrensaSearch" runat="server" CssClass="btn btn-sm small btn-light border shadow" OnClick="lnkPrensaSearch_Click" ToolTip="Buscar">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="max-height: 81vh!important; overflow: auto;">
                                    <div class="col">
                                        <asp:Button ID="btnHiddenPrensa" runat="server" CssClass="d-none" />
                                        <asp:GridView ID="gvPrensas" runat="server" DataKeyNames="id" CssClass="table table-sm small table-hover small w-100" GridLines="None" AutoGenerateColumns="false" OnSelectedIndexChanging="gvPrensas_SelectedIndexChanging">
                                            <SelectedRowStyle CssClass="fw-bold border-primary text-decoration-underline border border-2" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Nombre" DataField="nombre" ReadOnly="true" />
                                                <%--<asp:BoundField HeaderText="Activo" DataField="activo" ReadOnly="true" />--%>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <div class="btn-group border">
                                                            <asp:LinkButton runat="server" CommandName="select" CssClass="btn btn-sm small btn-light spinner-border-sm" ToolTip="Ver programa"><li class="fa fa-hand-pointer-o" ></li></asp:LinkButton>
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
                                <li class="fa fa-list-ol fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Programa</span></li>
                            </div>
                            <div class="card-body">
                                <div class="row pb-1 border-0 border-bottom border-danger">
                                    <div class="col-10">
                                        <div class="input-group input-group-sm text-nowrap flex-nowrap">
                                            <span class="input-group-text bg-light">
                                                <li class="fa fa-filter"></li>
                                            </span>
                                            <span class="input-group-text">Programa:</span>
                                            <asp:TextBox ID="txtProgramaSearch" runat="server" CssClass="form-control" AutoPostBack="false" placeholder="buscar número de parte" Style="max-width: 200px!important;"></asp:TextBox>
                                            <asp:LinkButton ID="lnkProgramaSearch" runat="server" CssClass="btn btn-sm small btn-light border shadow" OnClick="lnkProgramaSearch_Click" ToolTip="Buscar">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <asp:LinkButton ID="lnkPrograma_Agregar" runat="server" CssClass="btn btn-sm small btn-light border float-end" OnClick="lnkPrograma_Agregar_Click" ToolTip="Agregar nuevo proyecto"><span>Agregar</span>&nbsp;<li class="fa fa-plus"></li></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row" style="max-height: 81vh!important; overflow: auto;">
                                    <div class="col">
                                        <asp:Button ID="btnHiddenPrograma" runat="server" CssClass="d-none" />
                                        <asp:ModalPopupExtender ID="mdlPrograma" runat="server" TargetControlID="btnHiddenPrograma" PopupControlID="pnlProgramas" CancelControlID="btnPrograma_Cancelar" BackgroundCssClass="modal-backdrop"></asp:ModalPopupExtender>
                                        <asp:GridView ID="gvProgramas" runat="server" DataKeyNames="id" CssClass="table table-sm small table-hover small w-100" GridLines="None" AutoGenerateColumns="false" OnRowDeleting="gvProgramas_RowDeleting" OnRowEditing="gvProgramas_RowEditing">
                                            <Columns>
                                                <asp:BoundField HeaderText="Fecha" DataField="Fecha" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Turno" DataField="Turno" ReadOnly="true" />
                                                <asp:BoundField HeaderText="No.Parte" DataField="NoParte" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Estatus" DataField="Estatus" ReadOnly="true" />
                                                <%--<asp:BoundField HeaderText="Descripción" DataField="descripcion" ReadOnly="true" />
                                                <asp:CheckBoxField HeaderText="Activo" DataField="activo" ReadOnly="true" />--%>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <div class="btn-group border">
                                                            <asp:LinkButton runat="server" CommandName="edit" CssClass="btn btn-sm small btn-light spinner-border-sm"><li class="fa fa-edit" ></li></asp:LinkButton>
                                                            <asp:LinkButton runat="server" CommandName="delete" CssClass="btn btn-sm small btn-light" OnClientClick="return confirm('Se eliminará el registro seleccioando ¿Continuar?');"><li class="fa fa-trash" title="eliminar"></li></asp:LinkButton>
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
                    <div class="col-2 h-100">
                        <div class="card small h-100">
                            <div class="card-header shadow-sm">
                                <li class="fa fa-tasks fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Estatus</span></li>
                            </div>
                            <div class="card-body">
                                <div class="row pb-1 border-0 border-bottom border-danger">
                                    <div class="col-10">
                                        <div class="input-group input-group-sm text-nowrap flex-nowrap bg-white border-white">
                                            <span class="input-group-text bg-white border-white">
                                                
                                            </span>
                                            <span class="input-group-text bg-white border-white">&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        
                                    </div>
                                </div>
                                <div class="row" style="max-height: 81vh!important; overflow: auto;">
                                    <div class="col">
                                        <asp:GridView ID="gvEstatus" runat="server" DataKeyNames="id" CssClass="table table-sm small small w-100" GridLines="None" AutoGenerateColumns="false" OnSelectedIndexChanging="gvEstatus_SelectedIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <div class="input-group input-group-sm">
                                                            <asp:LinkButton runat="server" CommandName="select" CssClass='<%# " w-100 input-group-text fw-bold " + ( Eval("color").ToString() == "bg-success" ? "text-white ": "text-dark ") + Eval("color").ToString() %>' ToolTip="Asginar estado" Text='<%#Eval("nombre") %>'><li class="fa fa-refresh" ></li></asp:LinkButton>
                                                        </div>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Color" DataField="color" ReadOnly="true" />--%>
                                                <%--<asp:CheckBoxField HeaderText="Activo" DataField="activo" ReadOnly="true" />--%>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <div class="btn-group border">
                                                            <asp:LinkButton runat="server" CommandName="select" CssClass="btn btn-sm small btn-light spinner-border-sm" ToolTip="Asginar estado"><li class="fa fa-refresh" ></li></asp:LinkButton>
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
            <asp:Panel ID="pnlProgramas" runat="server" CssClass=" w-50 shadow-lg">
                <div class="card small shadow">
                    <div class="card-header">
                        <span class="fs-6">Datos del Programa</span>
                        <asp:HiddenField ID="hdnPrensa_id" runat="server" />
                        <asp:HiddenField ID="hdnPrograma_id" runat="server" />
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <div class="mb-3">
                                    <label class="form-label">Número de Parte</label>
                                    <asp:TextBox ID="txtPrograma_NoParte" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Estatus</label>
                                    <asp:DropDownList ID="ddlPrograma_Estatus" runat="server" CssClass="form-control no-chosen" AutoPostBack="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="float-end">
                            <asp:Button ID="btnPrograma_Cancelar" runat="server" CssClass="btn btn-sm btn-light border" Text="Cerrar" />
                            <asp:Button ID="btnPrograma_Guardar" runat="server" CssClass="btn btn-sm btn-light border" Text="Guardar" OnClick="btnPrograma_Guardar_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
