<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PPM.Default" %>

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
                $('#liDefault').addClass(" border-danger border-2 fw-bold");
                $('.datepicker').datepicker({ dateFormat: 'yy/mm/dd' }).css('z-index', '100000');
                var tableContextMenu = new ContextMenu("context-menu-items", menuItemClickListener, {
                    openCallBack: function (contextMenu, menu) {
                        $('#hdnProgramaId').val($(menu).attr("data-row-id"))
                        console.log($('#hdnProgramaId').val());
                    }
                });
                function menuItemClickListener(menu_item, parent) {
                    alert("Menu Item Clicked: " + menu_item.text() + "\nRecord ID: " + parent.attr("data-row-id"));
                }
            }


        });
    </script>
    <asp:UpdateProgress ID="upProgress" runat="server">
        <ProgressTemplate>
            <div style="cursor: wait; top: 0px; left: 0px; width: 100vw; height: 100vh; position: absolute; z-index: 1000000000">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Timer ID="Unnamed" runat="server" Interval="10000" Enabled="true" OnTick="Unnamed_Tick"></asp:Timer>
            <div class="w-100 p-0 m-0 pt-1 border-bottom border-secondary mb-1">
                <div class="row w-100 m-0 mb-2">
                    <div class="col-12">
                        <div class="input-group flex-nowrap w-75">
                            <span class="input-group-text border-primary">
                                <li class="fa fa-calendar"></li>
                                &nbsp;Fecha:</span>
                            <asp:TextBox ID="txtFecha" runat="server" ClientIDMode="Static" CssClass="form-control datepicker border-primary border-start-0 border-end-0" autocomplete="off" placeholder="yyyy/mm/dd" Style="min-width: 120px!important; max-width: 120px!important;"></asp:TextBox>
                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-light border-primary shadow" Text="Buscar" OnClick="btnBuscar_Click">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row w-100" style="height:85vh!important;">
                <div class="col-10">
                    <div class="row row-cols-6 g-2 ms-1 me-1">
                        <asp:Repeater ID="rptPrensas" runat="server" OnItemDataBound="rptPrensas_ItemDataBound">
                            <ItemTemplate>
                                <div class="col">
                                    <div class="card border-dark shadow">
                                        <div class="card-header">
                                            <asp:Label runat="server" CssClass="h5 fw-bold" Text='<%#Eval("Nombre") %>'></asp:Label>
                                        </div>
                                        <div class="card-body p-1">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvPrograma" runat="server" GridLines="None" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" CssClass="table table-borderless table-sm small table-hover small w-100"
                                                    EmptyDataRowStyle-CssClass="text-center" EmptyDataText="Sin programa capturado">
                                                    <HeaderStyle CssClass="border-bottom" />
                                                    <RowStyle CssClass="border border-start-0 border-top-0 border-end-0" />
                                                    <Columns>
                                                        <%----%>
                                                        <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="pb-0 pe-0"><%--ItemStyle-CssClass="p-0 m-0"--%>
                                                            <ItemTemplate>
                                                                <div class="input-group input-group-sm m-0 p-0">
                                                                    <asp:Label runat="server" CssClass='<%# " w-100 input-group-text fw-bold m-0 p-1 border-end-0 rounded-end-0 " + ( Eval("Estatus.color").ToString() == "bg-success" || Eval("Estatus.color").ToString() == "bg-primary" ? "text-white ": "text-dark ") + Eval("Estatus.color").ToString() %>'>&nbsp;</asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No. Parte" HeaderStyle-CssClass="text-wrap" ItemStyle-CssClass="pb-0 ps-0"><%--ItemStyle-CssClass="p-0 m-0"--%>
                                                            <ItemTemplate>
                                                                <div class="input-group input-group-sm m-0 p-0">
                                                                    <asp:Label runat="server" CssClass='<%# " w-100 input-group-text fw-bold m-0 p-1 border-end-0 rounded-end-0 bg-white border-0 " %>'> <%#Eval("NoParte") %> </asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField HeaderText="No.Parte" DataField="NoParte" ReadOnly="true" ItemStyle-CssClass="fw-bold" />--%>
                                                        <asp:BoundField HeaderText="Cant. Prog." DataField="cantidad_programada" ReadOnly="true" />
                                                        <asp:BoundField HeaderText="Cant. Corrida" DataField="cantidad_corrida" ReadOnly="true" />
                                                        <asp:TemplateField HeaderText="" ShowHeader="false" ItemStyle-CssClass="ps-0 pe-0">
                                                            <ItemTemplate>
                                                                <%--<table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td title='<%#Eval("EstatusPrensa.descripcion").ToString() %>' class='<%# (Eval("EstatusPrensa.nombre").ToString() == "OK" ? "" : "input-group-text bg-danger text-white small p-1") %>'><%# (Eval("EstatusPrensa.nombre").ToString() == "OK" ? "" : Eval("EstatusPrensa.nombre").ToString()) %></td>
                                                                    </tr>
                                                                </table>--%>
                                                                <span title='<%#Eval("EstatusPrensa.descripcion").ToString() %>' class='<%# (Eval("EstatusPrensa.nombre").ToString() == "OK" ? "" : "badge rounded-fill fs-6 fw-normal bg-danger text-white small p-1") %>'><%# (Eval("EstatusPrensa.nombre").ToString() == "OK" ? "" : Eval("EstatusPrensa.nombre").ToString()) %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ShowHeader="false">
                                                            <ItemTemplate>
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="right" class="context-menu" data-container-id="context-menu-items" data-row-id='<%#Eval("id").ToString() %>'></td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="col-2 border-start">
                    <div class="row mb-4">
                        <div class="col">
                            <div class="card small border-dark shadow shadow-lg">
                                <div class="card-header bg-dark">
                                    <li class="fa fa-tasks fa-2x text-white">&nbsp;<span class="h6 fw-bold align-middle text-white" style="font-family: Arial!important;">Estatus de Aceros</span></li>
                                </div>
                                <div class="card-body p-2">
                                    <asp:GridView ID="gvAceros" runat="server" DataKeyNames="id" CssClass="table w-100" GridLines="None" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No hay registros para el dia seleccionado">
                                        <EmptyDataRowStyle CssClass="text-center fw-bold" />
                                        <SelectedRowStyle CssClass="fw-bold border-primary text-decoration-underline border border-2" />
                                        <HeaderStyle CssClass=" border-secondary-subtle" />
                                        <RowStyle CssClass=" border-secondary-subtle" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Fecha" DataField="Fecha" ReadOnly="true" DataFormatString="{0: yyyy/MM/dd}" Visible="false" />
                                            <asp:BoundField HeaderText="Responsable" DataField="Responsable" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Rollo" DataField="Rollo" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Hora" DataField="Hora" ReadOnly="true" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-4">
                        <div class="col">
                            <div class="card small border-dark shadow shadow-lg">
                                <div class="card-header bg-dark shadow-sm">
                                    <li class="fa fa-tasks fa-2x text-white">&nbsp;<span class="h6 fw-bold align-middle text-white" style="font-family: Arial!important;">Estatus de Numero de Parte</span></li>
                                </div>
                                <div class="card-body">
                                    <asp:GridView ID="gvEstatus" runat="server" ShowHeader="false" DataKeyNames="id" CssClass="table table-sm small small w-100 table-borderless" GridLines="None" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <div class="input-group input-group-sm">
                                                        <asp:LinkButton runat="server" CommandName="select" Enabled="false" Font-Underline="false" CssClass='<%# " w-100 input-group-text fw-bold border-secondary-subtle " + ( (Eval("color").ToString() == "bg-success" || Eval("color").ToString() == "bg-primary") ? "text-white ": "text-dark ") + Eval("color").ToString() %>' ToolTip="" Text='<%#Eval("nombre") %>'><li class="fa fa-refresh" ></li></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <div class="card small border-dark shadow shadow-lg">
                                <div class="card-header bg-dark shadow-sm">
                                    <li class="fa fa-tasks fa-2x text-white">&nbsp;<span class="h6 fw-bold align-middle text-white" style="font-family: Arial!important;">Estatus de Corrida</span></li>
                                </div>
                                <div class="card-body">
                                    <asp:GridView ID="gvEstatusPrensa" runat="server" ShowHeader="false" DataKeyNames="id" CssClass="table table-sm small small w-100 table-borderless" GridLines="None" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <div class="input-group input-group-sm">
                                                        <asp:LinkButton runat="server" CommandName="select" Enabled="false" Font-Underline="false" CssClass=" w-100 input-group-text fw-bold border-secondary-subtle " ToolTip='<%#Eval("descripcion") %>' Text='<%# Eval("descripcion") %>'><li class="fa fa-refresh" ></li></asp:LinkButton>
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

            <div class="context-menu-container" id="context-menu-items2">
                <ul>
                    <li>Item 1</li>
                    <li>Item 2</li>
                    <li>Item 3</li>
                </ul>
            </div>
            <div class="context-menu-container p-0" id="context-menu-items">
                <asp:HiddenField ID="hdnProgramaId" runat="server" ClientIDMode="Static" />
                <asp:ListBox ID="lstEstatusPrensa" runat="server" CssClass="form-control no-chosen" OnSelectedIndexChanged="lstEstatusPrensa_SelectedIndexChanged" SelectionMode="Single" AutoPostBack="true"></asp:ListBox>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Unnamed" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
