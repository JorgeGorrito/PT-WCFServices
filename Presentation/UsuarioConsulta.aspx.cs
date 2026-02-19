using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.UserServiceReference;

namespace Presentation
{
    public partial class UsuarioConsulta : Page
    {
        private bool OrdenDescendente
        {
            get
            {
                if (ViewState["OrdenDesc"] == null)
                    ViewState["OrdenDesc"] = true;
                return (bool)ViewState["OrdenDesc"];
            }
            set
            {
                ViewState["OrdenDesc"] = value;
            }
        }

        private string FiltroSexo
        {
            get
            {
                if (ViewState["FiltroSexo"] == null)
                    ViewState["FiltroSexo"] = "T";
                return (string)ViewState["FiltroSexo"];
            }
            set
            {
                ViewState["FiltroSexo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        protected void lnkOrdenar_Click(object sender, EventArgs e)
        {
            OrdenDescendente = !OrdenDescendente;
            CargarUsuarios();
        }

        protected void lnkFiltroSexo_Click(object sender, EventArgs e)
        {
            switch (FiltroSexo)
            {
                case "T":
                    FiltroSexo = "M";
                    break;
                case "M":
                    FiltroSexo = "F";
                    break;
                case "F":
                    FiltroSexo = "T";
                    break;
                default:
                    FiltroSexo = "T";
                    break;
            }
            CargarUsuarios();
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            CargarUsuarios();
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        protected void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                OcultarMensajes();

                using (var client = new UserServiceClient())
                {
                    string busqueda = txtBuscar.Text.Trim();

                    if (string.IsNullOrEmpty(busqueda))
                    {
                        var request = new GetUsersPaginatedContract
                        {
                            order_desc = OrdenDescendente,
                            page_size = 1000,
                            current_page = 1
                        };

                        var response = client.GetUsersPaginated(request);

                        if (response.is_success)
                        {
                            var usuarios = response.result.users;

                            if (FiltroSexo != "T")
                            {
                                usuarios = usuarios.Where(u => u.gender.ToString() == FiltroSexo).ToArray();
                            }

                            gvUsuarios.DataSource = usuarios;
                            gvUsuarios.DataBind();
                            ActualizarPaginador();
                            ActualizarIconoOrden();
                            ActualizarIconoFiltroSexo();
                            ActualizarTotalUsuarios(usuarios.Length);

                            if (response.result.total_users == 0)
                            {
                                MostrarMensaje("No hay usuarios registrados en el sistema.");
                            }
                        }
                        else
                        {
                            string errores = string.Join("<br/>", response.errors);
                            MostrarError($"Error al cargar usuarios: {response.message}<br/>{errores}");
                        }
                    }
                    else
                    {
                        var request = new GetUsersByNamePaginatedContract
                        {
                            name = busqueda,
                            order_desc = OrdenDescendente,
                            page_size = 1000,
                            current_page = 1
                        };

                        var response = client.GetUsersByNamePaginated(request);

                        if (response.is_success)
                        {
                            var usuarios = response.result.users;

                            if (FiltroSexo != "T")
                            {
                                usuarios = usuarios.Where(u => u.gender.ToString() == FiltroSexo).ToArray();
                            }

                            gvUsuarios.DataSource = usuarios;
                            gvUsuarios.DataBind();
                            ActualizarPaginador();
                            ActualizarIconoOrden();
                            ActualizarIconoFiltroSexo();
                            ActualizarTotalUsuarios(usuarios.Length);

                            if (response.result.total_users == 0)
                            {
                                MostrarMensaje($"No se encontraron usuarios con el nombre '{busqueda}'.");
                            }
                            else
                            {
                                MostrarMensaje($"Se encontraron {response.result.total_users} usuario(s) con '{busqueda}'.");
                            }
                        }
                        else
                        {
                            string errores = string.Join("<br/>", response.errors);
                            MostrarError($"Error al buscar usuarios: {response.message}<br/>{errores}");
                        }
                    }
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                MostrarError("No se puede conectar con el servicio. Asegúrese de que el servicio WCF esté ejecutándose.");
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                MostrarError($"Error de comunicación con el servicio: {ex.Message}");
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar usuarios: {ex.Message}");
            }
        }

        protected void gvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsuarios.EditIndex = e.NewEditIndex;
            CargarUsuarios();
        }

        protected void gvUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsuarios.EditIndex = -1;
            CargarUsuarios();
        }

        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
            CargarUsuarios();
        }

        protected void btnIrPagina_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvUsuarios.BottomPagerRow != null)
                {
                    TextBox txtPagina = (TextBox)gvUsuarios.BottomPagerRow.FindControl("txtPaginaDirecta");
                    if (txtPagina != null && !string.IsNullOrWhiteSpace(txtPagina.Text))
                    {
                        int paginaDeseada;
                        if (int.TryParse(txtPagina.Text, out paginaDeseada))
                        {
                            int indice = paginaDeseada - 1;

                            if (indice >= 0 && indice < gvUsuarios.PageCount)
                            {
                                gvUsuarios.PageIndex = indice;
                                CargarUsuarios();
                            }
                            else
                            {
                                MostrarError($"La página debe estar entre 1 y {gvUsuarios.PageCount}");
                            }
                        }
                        else
                        {
                            MostrarError("Por favor ingrese un número válido");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cambiar de página: {ex.Message}");
            }
        }

        private void ActualizarPaginador()
        {
            if (gvUsuarios.BottomPagerRow != null)
            {
                Label lblPageInfo = (Label)gvUsuarios.BottomPagerRow.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = string.Format("Página {0} de {1}", 
                        gvUsuarios.PageIndex + 1, 
                        gvUsuarios.PageCount);
                }

                TextBox txtPagina = (TextBox)gvUsuarios.BottomPagerRow.FindControl("txtPaginaDirecta");
                if (txtPagina != null)
                {
                    txtPagina.Text = string.Empty;
                }
            }
        }

        private void ActualizarIconoOrden()
        {
            if (gvUsuarios.HeaderRow != null)
            {
                LinkButton lnkOrdenar = (LinkButton)gvUsuarios.HeaderRow.FindControl("lnkOrdenar");
                if (lnkOrdenar != null)
                {
                    if (OrdenDescendente)
                    {
                        lnkOrdenar.Text = "<i class=\"bi bi-sort-down-alt\"></i>";
                        lnkOrdenar.ToolTip = "Ordenar ascendente (menor a mayor)";
                    }
                    else
                    {
                        lnkOrdenar.Text = "<i class=\"bi bi-sort-up\"></i>";
                        lnkOrdenar.ToolTip = "Ordenar descendente (mayor a menor)";
                    }
                }
            }
        }

        private void ActualizarIconoFiltroSexo()
        {
            if (gvUsuarios.HeaderRow != null)
            {
                LinkButton lnkFiltro = (LinkButton)gvUsuarios.HeaderRow.FindControl("lnkFiltroSexo");
                if (lnkFiltro != null)
                {
                    switch (FiltroSexo)
                    {
                        case "T":
                            lnkFiltro.Text = "<i class=\"bi bi-gender-ambiguous\"></i>";
                            lnkFiltro.ToolTip = "Mostrando todos - Click para filtrar Masculino";
                            break;
                        case "M":
                            lnkFiltro.Text = "<i class=\"bi bi-gender-male text-primary\"></i>";
                            lnkFiltro.ToolTip = "Mostrando Masculino - Click para filtrar Femenino";
                            break;
                        case "F":
                            lnkFiltro.Text = "<i class=\"bi bi-gender-female text-danger\"></i>";
                            lnkFiltro.ToolTip = "Mostrando Femenino - Click para mostrar todos";
                            break;
                    }
                }
            }
        }

        private void ActualizarTotalUsuarios(int total)
        {
            if (gvUsuarios.BottomPagerRow != null)
            {
                Label lblTotal = (Label)gvUsuarios.BottomPagerRow.FindControl("lblTotalUsuarios");
                if (lblTotal != null)
                {
                    string texto = total == 1 ? "usuario registrado" : "usuarios registrados";
                    lblTotal.Text = string.Format("{0} {1}", total, texto);
                }
            }
        }

        protected void gvUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                OcultarMensajes();

                int userId = Convert.ToInt32(gvUsuarios.DataKeys[e.RowIndex].Value);

                GridViewRow row = gvUsuarios.Rows[e.RowIndex];
                TextBox txtNombre = (TextBox)row.FindControl("txtNombre");
                TextBox txtFechaNacimiento = (TextBox)row.FindControl("txtFechaNacimiento");
                DropDownList ddlSexo = (DropDownList)row.FindControl("ddlSexo");

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MostrarError("El nombre no puede estar vacío.");
                    return;
                }

                DateTime fechaNacimiento;
                if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
                {
                    MostrarError("Fecha de nacimiento inválida.");
                    return;
                }

                if (fechaNacimiento > DateTime.Today)
                {
                    MostrarError("La fecha de nacimiento no puede ser futura.");
                    return;
                }

                using (var client = new UserServiceClient())
                {
                    var request = new UpdateUserContract
                    {
                        user_id = userId,
                        name = txtNombre.Text.Trim(),
                        birth_date = fechaNacimiento,
                        gender = ddlSexo.SelectedValue[0]
                    };

                    var response = client.UpdateUser(request);

                    if (response.is_success)
                    {
                        MostrarMensaje(response.message);
                        gvUsuarios.EditIndex = -1;
                        CargarUsuarios();
                    }
                    else
                    {
                        string errores = string.Join("<br/>", response.errors);
                        MostrarError($"{response.message}<br/>{errores}");
                    }
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                MostrarError("No se puede conectar con el servicio. Asegúrese de que el servicio WCF esté ejecutándose.");
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                MostrarError($"Error de comunicación con el servicio: {ex.Message}");
            }
            catch (Exception ex)
            {
                MostrarError($"Error al actualizar el usuario: {ex.Message}");
            }
        }

        protected void gvUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                OcultarMensajes();

                int userId = Convert.ToInt32(gvUsuarios.DataKeys[e.RowIndex].Value);

                using (var client = new UserServiceClient())
                {
                    var request = new DeleteUserContract
                    {
                        user_id = userId
                    };

                    var response = client.DeleteUser(request);

                    if (response.is_success)
                    {
                        MostrarMensaje(response.message);
                        CargarUsuarios();
                    }
                    else
                    {
                        string errores = string.Join("<br/>", response.errors);
                        MostrarError($"{response.message}<br/>{errores}");
                    }
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                MostrarError("No se puede conectar con el servicio. Asegúrese de que el servicio WCF esté ejecutándose.");
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                MostrarError($"Error de comunicación con el servicio: {ex.Message}");
            }
            catch (Exception ex)
            {
                MostrarError($"Error al eliminar el usuario: {ex.Message}");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
            pnlError.Visible = false;
        }

        private void MostrarError(string error)
        {
            lblError.Text = error;
            pnlError.Visible = true;
            pnlMensaje.Visible = false;
        }

        private void OcultarMensajes()
        {
            pnlMensaje.Visible = false;
            pnlError.Visible = false;
        }
    }
}
