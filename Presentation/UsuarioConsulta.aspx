<%@ Page Title="Consultar Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuarioConsulta.aspx.cs" Inherits="Presentation.UsuarioConsulta" ResponseEncoding="utf-8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .table-actions .btn {
            margin: 2px;
            padding: 5px 10px;
            font-size: 0.85rem;
        }
        .gridview-container {
            overflow-x: auto;
        }
        /* Estilos de paginación */
        .pagination-container {
            margin-top: 20px;
            padding: 15px;
            background-color: #f8f9fa;
            border-radius: 8px;
        }
        .pagination-container .btn {
            font-size: 0.875rem;
            font-weight: 500;
            transition: all 0.3s;
        }
        .pagination-container .btn:hover:not(:disabled):not(.active) {
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(13, 110, 253, 0.3);
        }
        .pagination-container .btn:disabled {
            opacity: 0.4;
            cursor: not-allowed;
        }
        .pagination-container .btn.active {
            cursor: default;
            box-shadow: 0 2px 4px rgba(13, 110, 253, 0.4);
        }
        .pagination-container .btn-group {
            box-shadow: 0 1px 3px rgba(0,0,0,0.1);
        }
        /* Estilo elegante para contador de usuarios */
        .text-muted.fst-italic.small {
            color: #6c757d !important;
            font-size: 0.875rem;
            letter-spacing: 0.3px;
            opacity: 0.85;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="card">
                <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                    <h4 class="mb-0">
                        <i class="bi bi-list-ul"></i> Lista de Usuarios
                    </h4>
                    <div>
                        <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" 
                                    CssClass="btn btn-light btn-sm" 
                                    OnClick="btnRefrescar_Click" />
                    </div>
                </div>
                <div class="card-body">
                    
                    <%-- Mensajes --%>
                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show">
                        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                        <i class="bi bi-check-circle-fill"></i>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show">
                        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
                        <i class="bi bi-exclamation-triangle-fill"></i>
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </asp:Panel>

                    <%-- Barra de Búsqueda y Botón Agregar --%>
                    <div class="row mb-3">
                        <div class="col-md-8">
                            <div class="input-group">
                                <span class="input-group-text bg-primary text-white">
                                    <i class="bi bi-search"></i>
                                </span>
                                <asp:TextBox ID="txtBuscar" runat="server" 
                                             CssClass="form-control" 
                                             placeholder="Buscar usuario por nombre..."
                                             AutoPostBack="true"
                                             OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnLimpiarBusqueda" runat="server" 
                                            Text="Limpiar" 
                                            CssClass="btn btn-outline-secondary"
                                            OnClick="btnLimpiarBusqueda_Click" />
                            </div>
                            <small class="text-muted">
                                <i class="bi bi-info-circle"></i> Escribe para buscar usuarios por nombre
                            </small>
                        </div>
                        <div class="col-md-4 text-end">
                            <a href="Usuario.aspx" class="btn btn-primary btn-lg">
                                <i class="bi bi-person-plus"></i> Agregar Nuevo Usuario
                            </a>
                        </div>
                    </div>

                    <%-- GridView de Usuarios --%>
                    <div class="gridview-container">
                        <asp:GridView ID="gvUsuarios" runat="server" 
                                      CssClass="table table-striped table-hover table-bordered"
                                      AutoGenerateColumns="False"
                                      DataKeyNames="id"
                                      AllowPaging="True"
                                      PageSize="10"
                                      OnPageIndexChanging="gvUsuarios_PageIndexChanging"
                                      OnRowEditing="gvUsuarios_RowEditing"
                                      OnRowCancelingEdit="gvUsuarios_RowCancelingEdit"
                                      OnRowUpdating="gvUsuarios_RowUpdating"
                                      OnRowDeleting="gvUsuarios_RowDeleting"
                                      EmptyDataText="No hay usuarios registrados en el sistema."
                                      GridLines="None">

                            <Columns>
                                <%-- ID con Ordenamiento --%>
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                    <HeaderTemplate>
                                        ID
                                        <asp:LinkButton ID="lnkOrdenar" runat="server" 
                                                        OnClick="lnkOrdenar_Click"
                                                        CssClass="ms-1"
                                                        ToolTip="Cambiar orden">
                                            <i class="bi bi-arrow-down-up"></i>
                                        </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- Nombre --%>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("name") %>' 
                                                     CssClass="form-control form-control-sm" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNombreEdit" runat="server" 
                                                                    ControlToValidate="txtNombre"
                                                                    ErrorMessage="Requerido" 
                                                                    CssClass="text-danger small" 
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%-- Fecha de Nacimiento --%>
                                <asp:TemplateField HeaderText="Fecha Nacimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaNacimiento" runat="server" 
                                                   Text='<%# Eval("birth_date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFechaNacimiento" runat="server" 
                                                     Text='<%# Bind("birth_date", "{0:yyyy-MM-dd}") %>' 
                                                     TextMode="Date"
                                                     CssClass="form-control form-control-sm"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaEdit" runat="server" 
                                                                    ControlToValidate="txtFechaNacimiento"
                                                                    ErrorMessage="Requerido" 
                                                                    CssClass="text-danger small" 
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemStyle CssClass="text-center" />
                                </asp:TemplateField>

                                <%-- Sexo con Filtro --%>
                                <asp:TemplateField ItemStyle-CssClass="text-center">
                                    <HeaderTemplate>
                                        Sexo
                                        <asp:LinkButton ID="lnkFiltroSexo" runat="server" 
                                                        OnClick="lnkFiltroSexo_Click"
                                                        CssClass="ms-1"
                                                        ToolTip="Filtrar por sexo">
                                            <i class="bi bi-gender-ambiguous"></i>
                                        </asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSexo" runat="server" 
                                                   Text='<%# Eval("gender").ToString() == "M" ? "Masculino" : "Femenino" %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlSexo" runat="server" 
                                                          SelectedValue='<%# Bind("gender") %>'
                                                          CssClass="form-select form-select-sm">
                                            <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%-- Botones de Acción --%>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="table-actions">
                                            <asp:Button ID="btnEditar" runat="server" 
                                                        CommandName="Edit" 
                                                        Text="Modificar" 
                                                        CssClass="btn btn-warning btn-sm" />
                                            <asp:Button ID="btnEliminar" runat="server" 
                                                        CommandName="Delete" 
                                                        Text="Eliminar" 
                                                        CssClass="btn btn-danger btn-sm"
                                                        OnClientClick="return confirm('¿Está seguro de eliminar este usuario?');" />
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="table-actions">
                                            <asp:Button ID="btnGuardar" runat="server" 
                                                        CommandName="Update" 
                                                        Text="Guardar" 
                                                        CssClass="btn btn-success btn-sm" />
                                            <asp:Button ID="btnCancelar" runat="server" 
                                                        CommandName="Cancel" 
                                                        Text="Cancelar" 
                                                        CssClass="btn btn-secondary btn-sm" 
                                                        CausesValidation="false" />
                                        </div>
                                    </EditItemTemplate>
                                    <ItemStyle CssClass="text-center" />
                                </asp:TemplateField>
                            </Columns>

                            <HeaderStyle CssClass="table-dark" />
                            <EmptyDataRowStyle CssClass="text-center text-muted" />

                            <%-- Estilos de Paginación --%>
                            <PagerStyle CssClass="pagination-container" HorizontalAlign="Center" />
                            <PagerTemplate>
                                <%-- Total de Usuarios --%>
                                <div class="text-end mb-2 pe-2">
                                    <asp:Label ID="lblTotalUsuarios" runat="server" 
                                               CssClass="text-muted fst-italic small"></asp:Label>
                                </div>

                                <table border="0" cellpadding="0" cellspacing="0" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lnkFirst" runat="server" 
                                                            CommandName="Page" CommandArgument="First"
                                                            CssClass="btn btn-outline-primary btn-sm me-2"
                                                            Enabled='<%# gvUsuarios.PageIndex > 0 %>'>
                                                &laquo; Primera
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkPrevious" runat="server" 
                                                            CommandName="Page" CommandArgument="Prev"
                                                            CssClass="btn btn-outline-primary btn-sm me-2"
                                                            Enabled='<%# gvUsuarios.PageIndex > 0 %>'>
                                                &lsaquo; Anterior
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPageInfo" runat="server" 
                                                       CssClass="btn btn-primary btn-sm active">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkNext" runat="server" 
                                                            CommandName="Page" CommandArgument="Next"
                                                            CssClass="btn btn-outline-primary btn-sm ms-2"
                                                            Enabled='<%# gvUsuarios.PageIndex < gvUsuarios.PageCount - 1 %>'>
                                                Siguiente &rsaquo;
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkLast" runat="server" 
                                                            CommandName="Page" CommandArgument="Last"
                                                            CssClass="btn btn-outline-primary btn-sm ms-2"
                                                            Enabled='<%# gvUsuarios.PageIndex < gvUsuarios.PageCount - 1 %>'>
                                                Última &raquo;
                                            </asp:LinkButton>
                                        </td>
                                        <td style="padding-left: 15px;">
                                            <span class="btn btn-outline-secondary btn-sm" style="cursor: default;">Ir a:</span>
                                            <asp:TextBox ID="txtPaginaDirecta" runat="server" 
                                                         CssClass="form-control form-control-sm d-inline-block text-center" 
                                                         style="width: 50px; vertical-align: middle;"
                                                         MaxLength="4"
                                                         placeholder="#"></asp:TextBox>
                                            <asp:Button ID="btnIrPagina" runat="server" 
                                                        Text="Ir" 
                                                        CssClass="btn btn-outline-primary btn-sm"
                                                        OnClick="btnIrPagina_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </PagerTemplate>
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
