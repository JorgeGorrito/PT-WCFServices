<%@ Page Title="Agregar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="Presentation.Usuario" ResponseEncoding="utf-8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8 offset-md-2">
            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h4 class="mb-0">
                        <i class="bi bi-person-plus-fill"></i> Registro de Usuario
                    </h4>
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

                    <%-- Campo Nombre --%>
                    <div class="mb-3">
                        <label for="<%= txtNombre.ClientID %>" class="form-label">
                            <i class="bi bi-person"></i> Nombre Completo: <span class="text-danger">*</span>
                        </label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" 
                                     placeholder="Ingrese el nombre completo del usuario" 
                                     MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                                    ControlToValidate="txtNombre"
                                                    ErrorMessage="El nombre es requerido" 
                                                    CssClass="text-danger small" 
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <%-- Campo Fecha de Nacimiento --%>
                    <div class="mb-3">
                        <label for="<%= txtFechaNacimiento.ClientID %>" class="form-label">
                            <i class="bi bi-calendar-event"></i> Fecha de Nacimiento: <span class="text-danger">*</span>
                        </label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" 
                                     TextMode="Date"
                                     CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" 
                                                    ControlToValidate="txtFechaNacimiento"
                                                    ErrorMessage="La fecha de nacimiento es requerida" 
                                                    CssClass="text-danger small" 
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvFechaNacimiento" runat="server"
                                            ControlToValidate="txtFechaNacimiento"
                                            OnServerValidate="cvFechaNacimiento_ServerValidate"
                                            ErrorMessage="La fecha de nacimiento no puede ser futura"
                                            CssClass="text-danger small"
                                            Display="Dynamic"></asp:CustomValidator>
                        <small class="text-muted">
                            <i class="bi bi-info-circle"></i> No se pueden seleccionar fechas futuras
                        </small>
                    </div>

                    <%-- Campo Sexo --%>
                    <div class="mb-3">
                        <label for="<%= ddlSexo.ClientID %>" class="form-label">
                            <i class="bi bi-gender-ambiguous"></i> Sexo: <span class="text-danger">*</span>
                        </label>
                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select">
                            <asp:ListItem Text="-- Seleccione --" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSexo" runat="server" 
                                                    ControlToValidate="ddlSexo"
                                                    ErrorMessage="Debe seleccionar un sexo" 
                                                    CssClass="text-danger small" 
                                                    Display="Dynamic"
                                                    InitialValue=""></asp:RequiredFieldValidator>
                    </div>

                    <hr class="my-4" />

                    <%-- Botones --%>
                    <div class="d-grid gap-2">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" 
                                    CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Formulario" 
                                    CssClass="btn btn-secondary" 
                                    OnClick="btnLimpiar_Click" 
                                    CausesValidation="false" />
                        <a href="UsuarioConsulta.aspx" class="btn btn-outline-primary">
                            Ver Lista de Usuarios
                        </a>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <%-- Modal de Éxito --%>
    <div class="modal fade" id="modalExito" tabindex="-1" aria-labelledby="modalExitoLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title" id="modalExitoLabel">
                        <i class="bi bi-check-circle-fill"></i> ¡Registro Exitoso!
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center">
                    <div class="mb-3">
                        <i class="bi bi-person-check-fill text-success" style="font-size: 4rem;"></i>
                    </div>
                    <h4 class="mb-3">Usuario Registrado</h4>
                    <p class="mb-0">
                        <asp:Label ID="lblMensajeModal" runat="server" CssClass="text-muted"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <a href="UsuarioConsulta.aspx" class="btn btn-primary">
                        <i class="bi bi-list-ul"></i> Ver Lista de Usuarios
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
