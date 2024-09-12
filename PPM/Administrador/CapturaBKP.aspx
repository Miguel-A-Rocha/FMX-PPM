<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CapturaBKP.aspx.cs" Inherits="PPM.Administrador.CapturaBKP" %>
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
                $('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' }).css('z-index', '100000');
                $('.timepicker').timepicker({
                    timeFormat: 'HH:mm',
                    interval: 30,
                    //minTime: '10',
                    //maxTime: '6:00pm',
                    //defaultTime: '11',
                    //startTime: '10:00',
                    dynamic: true,
                    dropdown: true,
                    scrollbar: true,
                    zindex: 9999999
                });
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
                    <div class="col h-100">
                        <div class="card small h-100">
                            <div class="card-header shadow-sm">
                                <li class="fa fa-list-ol fa-2x">&nbsp;<span class="h6 fw-bold align-middle" style="font-family: Arial!important;">Captura Manual de Programa</span></li>
                            </div>
                            <div class="card-body">
                                <div class="row pb-1 border-0 border-bottom border-danger">
                                    <div class="col-7">
                                        <div class="input-group input-group-sm text-nowrap flex-nowrap">
                                            <span class="input-group-text bg-light">
                                                <li class="fa fa-filter"></li>
                                            </span>
                                            <span class="input-group-text">Prensa:</span>
                                            <asp:DropDownList ID="ddlPrensas" runat="server" ClientIDMode="Static" CssClass="form-control" AutoPostBack="false" Style="max-width: 150px!important;" OnSelectedIndexChanged="ddlPrensa_SelectedIndexChanged"></asp:DropDownList>
                                            <span class="input-group-text">Turno:</span>
                                            <asp:DropDownList ID="ddlTurno" runat="server" ClientIDMode="Static" CssClass="form-control" AutoPostBack="false" Style="max-width: 150px!important;">
                                                <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1ro"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2do"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3ro"></asp:ListItem>
                                            </asp:DropDownList>
                                            <span class="input-group-text">Fecha:</span>
                                            <asp:TextBox ID="txtFecha" runat="server" ClientIDMode="Static" CssClass="form-control datepicker" autocomplete="off" placeholder="yyyy/mm/dd" Style="min-width: 120px!important;max-width: 120px!important;"></asp:TextBox>
                                            <span class="input-group-text">Estatus:</span>
                                            <asp:DropDownList ID="ddlEstatus" runat="server" ClientIDMode="Static" CssClass="form-control" AutoPostBack="false" Style="max-width: 250px!important;"></asp:DropDownList>
                                            <asp:LinkButton ID="lnkProgramaSearch" runat="server" CssClass="btn btn-sm small btn-light border shadow" OnClick="lnkProgramaSearch_Click" ToolTip="Buscar">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="float-end">
                                            <asp:LinkButton ID="lnkPrograma_Agregar" runat="server" CssClass="btn btn-sm small btn-light border float-end" OnClick="lnkPrograma_Agregar_Click" ToolTip="Agregar nuevo registro"><span>Agregar</span>&nbsp;<li class="fa fa-plus"></li></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="max-height: 81vh!important; overflow: auto;">
                                    <div class="col">
                                        <asp:Button ID="btnHiddenPrograma" runat="server" CssClass="d-none" />
                                        <asp:ModalPopupExtender ID="mdlPrograma" runat="server" TargetControlID="btnHiddenPrograma" PopupControlID="pnlPrograma" CancelControlID="btnPrograma_Cancelar" BackgroundCssClass="modal-backdrop"></asp:ModalPopupExtender>
                                        <asp:GridView ID="gvPrograma" runat="server" DataKeyNames="id" CssClass="table table-sm small table-hover small w-100" GridLines="None" AutoGenerateColumns="false" OnRowDeleting="gvPrograma_RowDeleting" OnRowEditing="gvPrograma_RowEditing">
                                            <SelectedRowStyle CssClass="fw-bold border-primary text-decoration-underline border border-2" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Prensa" DataField="Prensa" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Fecha" DataField="Fecha" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Turno" DataField="Turno" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Hora" DataField="Hora" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Número de Parte" DataField="NoParte" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Estatus" DataField="Estatus" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Usuario Captura" DataField="usuario_captura" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Fecha Captura" DataField="fecha_captura" ReadOnly="true" DataFormatString="{0: yyyy/MM/dd HH:mm}" />
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
                </div>
            </div>
            <asp:Panel ID="pnlPrograma" runat="server" CssClass=" w-50 shadow-lg">
                <div class="card small shadow">
                    <div class="card-header">
                        <span class="fs-6">Datos del Programa</span>
                        <asp:HiddenField ID="hdnPrograma_id" runat="server" />
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <div class="mb-3">
                                    <label class="form-label">Prensa</label>
                                    <asp:DropDownList ID="ddlPrograma_Prensa" runat="server" CssClass="form-control" AutoPostBack="false" OnSelectedIndexChanged="ddlPrograma_Prensa_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Fecha</label>
                                    <asp:TextBox ID="txtPrograma_Fecha" runat="server" CssClass="form-control datepicker" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Turno</label>
                                    <asp:DropDownList ID="ddlPrograma_Turno" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1" Text="1ro"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2do"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3ro"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Hora</label>
                                    <asp:TextBox ID="txtPrograma_Hora" runat="server" CssClass="form-control timepicker" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Número de Parte</label>
                                    <asp:TextBox ID="txtPrograma_NoParte" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="mb-3 row">
                                    <div class="col-8">
                                        <label class="form-label">Estatus</label>
                                        <asp:DropDownList ID="ddlPrograma_Estatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <table class="table table-sm">
                                            <tr>
                                                <td>
                                                    Opciones:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bg-success text-white">
                                                    Corriendo
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bg-warning text-dark">
                                                    En proceso
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="bg-light text-dark">
                                                    Programado
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="float-end">
                            <asp:Button ID="btnPrograma_Cancelar" runat="server" CssClass="btn btn-sm btn-light border" Text="Cerrar" />
                            <asp:Button ID="btnPrograma_Guardar" runat="server" CssClass="btn btn-sm btn-light border" Text="Guardar" OnClick="btnPrograma_Guardar_Click" />
                            </di>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPrensas" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPrograma_Prensa" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
