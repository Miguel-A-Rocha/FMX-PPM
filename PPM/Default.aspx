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
            <div class="w-100 p-0 m-0 pt-1 border-bottom border-secondary mb-1">
                <div class="row w-100 m-0 mb-2">
                    <div class="col-12">
                        <div class="input-group input-group-sm flex-nowrap w-75">
                            <span class="input-group-text">
                                <li class="fa fa-calendar"></li>
                                &nbsp;Fecha:</span>
                            <asp:TextBox ID="txtFecha" runat="server" ClientIDMode="Static" CssClass="form-control datepicker" autocomplete="off" placeholder="yyyy/mm/dd" Style="min-width: 120px!important;max-width: 120px!important;"></asp:TextBox>
                            <span class="input-group-text">
                                <li class="fa fa-list"></li>
                                &nbsp;Turno: </span>
                            <asp:DropDownList ID="ddlTurno" runat="server" ClientIDMode="Static" CssClass="form-control no-chosen" AutoPostBack="false" Style="max-width: 250px!important;">
                                <asp:ListItem Value="1" Text="1ro" ></asp:ListItem>
                                <asp:ListItem Value="2" Text="2do" ></asp:ListItem>
                                <asp:ListItem Value="3" Text="3ro" ></asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-sm small btn-light border shadow" Text="Buscar" OnClick="btnBuscar_Click">Buscar&nbsp;<li class="fa fa-search"></li></asp:LinkButton>
                            <asp:Timer runat="server" Interval="10000" Enabled="true" OnTick="Unnamed_Tick"></asp:Timer>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row row-cols-1 row-cols-md-6 g-4 ms-1 me-1">
                <asp:Repeater ID="rptPrensas" runat="server" OnItemDataBound="rptPrensas_ItemDataBound">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card">
                                <div class="card-header">
                                    <asp:Label runat="server" CssClass="h5 fw-bold" Text='<%#Eval("Nombre") %>'></asp:Label>
                                </div>
                                <div class="card-body p-1">
                                    <asp:GridView ID="gvPrograma" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-sm small table-hover small w-100">
                                        <Columns>
                                            <asp:BoundField HeaderText="No.Parte" DataField="NoParte" ReadOnly="true" ItemStyle-CssClass="fw-bold" />
                                            <%--<asp:BoundField HeaderText="Estatus" DataField="Estatus.nombre" ReadOnly="true" />--%>
                                            <asp:TemplateField HeaderText="Estatus">
                                                <ItemTemplate>
                                                    <%--<asp:Label runat="server" Text='<%#Eval("Estatus.nombre") %>' ></asp:Label>--%>
                                                    <div class="input-group input-group-sm">
                                                        <asp:Label runat="server" Text='<%#Eval("Estatus.nombre") %>' CssClass='<%# " w-100 input-group-text fw-bold " + ( Eval("Estatus.color").ToString() == "bg-success" ? "text-white ": "text-dark ") + Eval("Estatus.color").ToString() %>' ></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
        <Triggers>
            
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
