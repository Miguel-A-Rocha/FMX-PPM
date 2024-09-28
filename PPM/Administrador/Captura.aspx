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
            <div class="w-100 p-0 m-0 pt-1 border-bottom border-secondary mb-1">
                <div class="row w-100 m-0 mb-2">
                    <div class="col-12">
                        <div class="input-group flex-nowrap w-75">
                            <span class="input-group-text border-primary">
                                <li class="fa fa-calendar"></li>
                                &nbsp;Fecha:</span>
                            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control datepicker  border-primary border-start-0 border-end-0" autocomplete="off" placeholder="yyyy/mm/dd" Style="min-width: 120px!important; max-width: 120px!important;"></asp:TextBox>
                            <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-light border shadow text-center border-primary" ToolTip="Buscar" OnClick="lnkSearch_Click">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="w-100 p-2 pt-1" style="height: 89vh!important; max-height: 89vh!important; overflow: hidden!important;">
                <div class="row h-100">
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
                    <div class="col-7 h-100">
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
                                        <asp:GridView ID="gvProgramas" runat="server" DataKeyNames="id" CssClass="table table-sm small table-hover small w-100" GridLines="None" AutoGenerateColumns="false" EmptyDataText="Selecciona un registro en la sección de Prensas"
                                            OnRowDeleting="gvProgramas_RowDeleting"
                                            OnRowEditing="gvProgramas_RowEditing"
                                            OnSelectedIndexChanging="gvProgramas_SelectedIndexChanging"
                                            OnRowCommand="gvProgramas_RowCommand">
                                            <EmptyDataRowStyle CssClass="text-center fw-bold" />
                                            <SelectedRowStyle CssClass="fw-bold border-primary text-decoration-underline border border-2" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Fecha" DataField="Fecha" ReadOnly="true" DataFormatString="{0: yyyy/MM/dd}" />
                                                <asp:BoundField HeaderText="Hora" DataField="Hora" ReadOnly="true" />
                                                <asp:BoundField HeaderText="No.Parte" DataField="NoParte" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Cant. Programada" DataField="cantidad_programada" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField HeaderText="Cant. Corrida" DataField="cantidad_corrida" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField HeaderText="Estatus" DataField="Estatus.nombre" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Estatus Prensa" DataField="EstatusPrensa.nombre" ReadOnly="true" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%--<asp:Label runat="server" Text='<%#Eval("Estatus.nombre") %>' ></asp:Label>--%>
                                                        <div class="input-group input-group-sm">
                                                            <asp:Label runat="server" CssClass='<%# "input-group-text fw-bold " + ( Eval("Estatus.color").ToString() == "bg-success" ? "text-white ": "text-dark ") + Eval("Estatus.color").ToString() %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Secuencia" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Visible="false">
                                                    <ItemTemplate>
                                                        <div class="input-group input-group-sm">
                                                            <%--<asp:Label runat="server" CssClass="input-group-text" Text='<%#Eval("Secuencia") %>' ></asp:Label>--%>
                                                            <asp:LinkButton runat="server" CommandName="subir" CommandArgument='<%# Eval("Secuencia") %>' CssClass="btn btn-sm small btn-light spinner-border-sm"><li class="fa fa-arrow-up" ></li></asp:LinkButton>
                                                            <asp:LinkButton runat="server" CommandName="bajar" CommandArgument='<%# Eval("Secuencia") %>' CssClass="btn btn-sm small btn-light spinner-border-sm"><li class="fa fa-arrow-down" ></li></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <div class="input-group input-group-sm">
                                                            <%--<asp:LinkButton runat="server" CommandName="select" CssClass="btn btn-sm small btn-light spinner-border-sm" ToolTip="Seleccionar"><li class="fa fa-hand-pointer-o" ></li></asp:LinkButton>--%>
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
                        <div class="row">
                            <div class="col">
                                <div class="card small">
                                    <div class="card-header shadow-sm">
                                        <li class="fa fa-tasks fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Estatus</span></li>
                                    </div>
                                    <div class="card-body">
                                        <asp:GridView ID="gvEstatus" runat="server" ShowHeader="false" DataKeyNames="id" CssClass="table table-sm small small w-100" GridLines="None" AutoGenerateColumns="false" OnSelectedIndexChanging="gvEstatus_SelectedIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <div class="input-group input-group-sm">
                                                            <asp:LinkButton runat="server" CommandName="select" Enabled="false" Font-Underline="false" CssClass='<%# " w-100 input-group-text fw-bold " + ( (Eval("color").ToString() == "bg-success" || Eval("color").ToString() == "bg-primary") ? "text-white ": "text-dark ") + Eval("color").ToString() %>' ToolTip="" Text='<%#Eval("nombre") %>'><li class="fa fa-refresh" ></li></asp:LinkButton>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Color" DataField="color" ReadOnly="true" />--%>
                                                <%--<asp:CheckBoxField HeaderText="Activo" DataField="activo" ReadOnly="true" />--%>
                                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <div class="btn-group border">
                                                                    <asp:LinkButton runat="server" CommandName="select" CssClass="btn btn-sm small btn-light spinner-border-sm" ToolTip="Asginar estado"><li class="fa fa-refresh" ></li></asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col">
                                <div class="card small">
                                    <div class="card-header shadow-sm">
                                        <li class="fa fa-tasks fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Estatus de Prensa</span></li>
                                    </div>
                                    <div class="card-body">
                                        <asp:GridView ID="gvEstatusPrensa" runat="server" ShowHeader="false" DataKeyNames="id" CssClass="table table-sm small small w-100" GridLines="None" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <div class="input-group input-group-sm">
                                                            <asp:LinkButton runat="server" CommandName="select" Enabled="false" Font-Underline="false" CssClass=" w-100 input-group-text fw-bold " ToolTip='<%#Eval("descripcion") %>' Text='<%# Eval("descripcion") %>'><li class="fa fa-refresh" ></li></asp:LinkButton>
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
                                    <label class="form-label">Hora</label>
                                    <asp:TextBox ID="txtPrograma_Hora" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Número de Parte</label>
                                    <asp:TextBox ID="txtPrograma_NoParte" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Cantidad Programada</label>
                                    <asp:TextBox ID="txtPrograma_CantidadProgramada" runat="server" CssClass="form-control" type="Number" step="1"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Cantidad Corrida</label>
                                    <asp:TextBox ID="txtPrograma_CantidadCorrida" runat="server" CssClass="form-control" type="Number" step="1"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Estatus</label>
                                    <asp:DropDownList ID="ddlPrograma_Estatus" runat="server" CssClass="form-control no-chosen" AutoPostBack="false"></asp:DropDownList>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Estatus Prensa</label>
                                    <asp:DropDownList ID="ddlPrograma_EstatusPrensa" runat="server" CssClass="form-control no-chosen" AutoPostBack="false"></asp:DropDownList>
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
            <asp:AsyncPostBackTrigger ControlID="lnkSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
