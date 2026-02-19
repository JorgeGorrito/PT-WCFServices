using System;
using System.Linq;
using System.Web.UI;
using Presentation.UserServiceReference;

namespace Presentation
{
    public partial class Usuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFechaNacimiento.Text = DateTime.Today.AddYears(-18).ToString("yyyy-MM-dd");
                txtFechaNacimiento.Attributes["max"] = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void cvFechaNacimiento_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            DateTime fecha;
            if (DateTime.TryParse(args.Value, out fecha))
            {
                args.IsValid = fecha <= DateTime.Today;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarMensajes();

                DateTime fechaNacimiento;
                if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
                {
                    MostrarError("Por favor seleccione una fecha de nacimiento válida.");
                    return;
                }

                if (fechaNacimiento > DateTime.Today)
                {
                    MostrarError("La fecha de nacimiento no puede ser futura.");
                    return;
                }

                int edad = DateTime.Today.Year - fechaNacimiento.Year;
                if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;

                if (edad < 0 || edad > 150)
                {
                    MostrarError("La fecha de nacimiento no es válida.");
                    return;
                }

                using (var client = new UserServiceClient())
                {
                    var request = new AddUserContract
                    {
                        name = txtNombre.Text.Trim(),
                        birth_date = fechaNacimiento,
                        gender = ddlSexo.SelectedValue[0]
                    };

                    var response = client.AddUser(request);

                    if (response.is_success)
                    {
                        lblMensajeModal.Text = $"El usuario '{txtNombre.Text.Trim()}' ha sido registrado exitosamente con ID: {response.result.user_added_id}";
                        LimpiarFormulario();
                        MostrarModalExito();
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
                MostrarError($"Error al guardar el usuario: {ex.Message}");
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtFechaNacimiento.Text = DateTime.Today.AddYears(-18).ToString("yyyy-MM-dd");
            ddlSexo.SelectedIndex = 0;
            OcultarMensajes();
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

        private void MostrarModalExito()
        {
            string script = @"
                <script type='text/javascript'>
                    document.addEventListener('DOMContentLoaded', function() {
                        var modalExito = new bootstrap.Modal(document.getElementById('modalExito'));
                        modalExito.show();
                    });
                </script>";

            ClientScript.RegisterStartupScript(this.GetType(), "MostrarModalExito", script, false);
        }

        private void OcultarMensajes()
        {
            pnlMensaje.Visible = false;
            pnlError.Visible = false;
        }
    }
}
