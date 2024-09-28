<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aceros.aspx.cs" Inherits="PPM.Administrador.Aceros" %>

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
                $('#lnkAceros').addClass(" bg-danger text-white");
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
                <div class="input-group flex-nowrap">
                    <span class="input-group-text border-primary">
                        <li class="fa fa-calendar"></li>
                        &nbsp;Fecha:</span>
                    <asp:TextBox ID="txtFecha" runat="server" ClientIDMode="Static" CssClass="form-control datepicker border-primary border-start-0 border-end-0" autocomplete="off" placeholder="yyyy/mm/dd" Style="min-width: 120px!important; max-width: 120px!important;"></asp:TextBox>
                    <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-light border border-primary shadow" ToolTip="Buscar" OnClick="lnkSearch_Click">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                </div>
                    </div>
                </div>
            </div>
            <div class="w-100 p-2 pt-1" style="height: 89vh!important; max-height: 89vh!important; overflow: hidden!important;">
                <div class="row h-100">
                    <div class="col h-100">
                        <div class="card small h-100">
                            <div class="card-header shadow-sm">
                                <li class="fa fa-circle-o fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Estatus de Aceros</span></li>
                            </div>
                            <div class="card-body">
                                <div class="row pb-1 border-0 border-bottom border-danger">
                                    <div class="col-10">
                                        <div class="input-group input-group-sm text-nowrap flex-nowrap">
                                            <span class="input-group-text bg-light">
                                                <li class="fa fa-filter"></li>
                                            </span>
                                            <span class="input-group-text">Aceros:</span>
                                            <asp:TextBox ID="txtAcerosSearch" runat="server" CssClass="form-control" AutoPostBack="false" placeholder="buscar acero" Style="max-width: 200px!important;"></asp:TextBox>
                                            <asp:LinkButton ID="lnkAcerosSearch" runat="server" CssClass="btn btn-sm small btn-light border shadow" OnClick="lnkAcerosSearch_Click" ToolTip="Buscar">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <asp:LinkButton ID="lnkAceros_Agregar" runat="server" CssClass="btn btn-sm small btn-light border float-end" OnClick="lnkAceros_Agregar_Click" ToolTip="Agregar nuevo proyecto"><span>Agregar</span>&nbsp;<li class="fa fa-plus"></li></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row" style="max-height: 81vh!important; overflow: auto;">
                                    <div class="col">
                                        <asp:Button ID="btnHiddenAceros" runat="server" CssClass="d-none" />
                                        <asp:ModalPopupExtender ID="mdlAceros" runat="server" TargetControlID="btnHiddenAceros" PopupControlID="pnlAceros" CancelControlID="btnAcero_Cancelar" BackgroundCssClass="modal-backdrop"></asp:ModalPopupExtender>
                                        <asp:GridView ID="gvAceros" runat="server" DataKeyNames="id" CssClass="table table-sm small table-hover small w-100" GridLines="None" AutoGenerateColumns="false" EmptyDataText="Da clic en el boton agregar"
                                            OnRowDeleting="gvAceros_RowDeleting"
                                            OnRowEditing="gvAceros_RowEditing"
                                            OnSelectedIndexChanging="gvAceros_SelectedIndexChanging">
                                            <EmptyDataRowStyle CssClass="text-center fw-bold" />
                                            <SelectedRowStyle CssClass="fw-bold border-primary text-decoration-underline border border-2" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Fecha" DataField="Fecha" ReadOnly="true" DataFormatString="{0: yyyy/MM/dd}" Visible="false" />
                                                <asp:BoundField HeaderText="Responsable" DataField="Responsable" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Rollo" DataField="Rollo" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Hora" DataField="Hora" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Fecha Captura" DataField="fecha_captura" ReadOnly="true" DataFormatString="{0: yyyy/MM/dd HH:mm}" />
                                                <asp:BoundField HeaderText="Usuario Captura" DataField="usuario_captura" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <div class="input-group input-group-sm">
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
                </div>
            </div>
            <asp:Panel ID="pnlAceros" runat="server" CssClass=" w-50 shadow-lg">
                <div class="card small shadow">
                    <div class="card-header">
                        <span class="fs-6">Datos del Acero</span>
                        <asp:HiddenField ID="hdnAcero_id" runat="server" />
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <div class="mb-3">
                                    <label class="form-label">Responsable</label>
                                    <asp:TextBox ID="txtAcero_Responsable" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Acero</label>
                                    <asp:TextBox ID="txtAcero_Rollo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Hora</label>
                                    <asp:TextBox ID="txtAcero_Hora" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="float-end">
                            <asp:Button ID="btnAcero_Cancelar" runat="server" CssClass="btn btn-sm btn-light border" Text="Cerrar" />
                            <asp:Button ID="btnAcero_Guardar" runat="server" CssClass="btn btn-sm btn-light border" Text="Guardar" OnClick="btnAcero_Guardar_Click" />
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
