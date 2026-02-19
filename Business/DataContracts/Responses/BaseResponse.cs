using System.Collections.Generic;
using System.Runtime.Serialization;
using PruebaTecnica.BDO.Enums;

namespace PruebaTecnica.Business.DataContracts.Responses
{
        

    /// <summary>
    /// Estructura genérica de respuesta (Envelope Pattern) para todos los métodos del servicio.
    /// Centraliza el control de errores, mensajes y resultados.
    /// </summary>
    /// <typeparam name="T">Tipo de dato esperado en el resultado de la operación.</typeparam>
    [DataContract]
    public class BaseResponse<T>
    {
        /// <summary>Código numérico que representa el estado de la transacción.</summary>
        [DataMember(Name = "response_code", Order = 0)]
        public int ResponseCode { get; set; }

        /// <summary>Indicador booleano simplificado del éxito de la operación.</summary>
        [DataMember(Name = "is_success", Order = 1)]
        public bool IsSuccess { get; set; }

        /// <summary>Mensaje descriptivo sobre el resultado de la operación.</summary>
        [DataMember(Name = "message", Order = 2)]
        public string Message { get; set; } = string.Empty;

        /// <summary>Colección de errores detallados, útil para validaciones múltiples.</summary>
        [DataMember(Name = "errors", Order = 3)]
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>Objeto con los datos resultantes de la solicitud.</summary>
        [DataMember(Name = "result", Order = 4)]
        public T Result { get; set; }
    }

    /// <summary>
    /// Clase de utilidad para la creación estandarizada de instancias de <see cref="BaseResponse{T}"/>.
    /// Implementa el patrón Factory para simplificar la construcción de respuestas desde la capa de servicio.
    /// </summary>
    public static class Response
    {
        /// <summary>
        /// Crea una respuesta de éxito con un resultado y código opcional.
        /// </summary>
        /// <typeparam name="T">Tipo de resultado.</typeparam>
        /// <param name="result">Datos resultantes de la operación.</param>
        /// <param name="message">Mensaje informativo.</param>
        /// <param name="code">Código de éxito (por defecto Success 200).</param>
        /// <returns>Instancia de BaseResponse configurada como exitosa.</returns>
        public static BaseResponse<T> Success<T>(T result, string message = "Operación exitosa", StatusCode code = StatusCode.Success)
        {
            return new BaseResponse<T>()
            {
                IsSuccess = true,
                ResponseCode = (int)code,
                Result = result,
                Message = message
            };
        }

        /// <summary>
        /// Crea una respuesta de error con información detallada para el cliente.
        /// </summary>
        /// <typeparam name="T">Tipo de resultado esperado (se retornará el valor por defecto).</typeparam>
        /// <param name="message">Descripción del error.</param>
        /// <param name="code">Código de fallo correspondiente (Ej: 400, 404, 500).</param>
        /// <param name="errors">Lista detallada de fallos de validación o excepciones.</param>
        /// <returns>Instancia de BaseResponse configurada como fallida.</returns>
        public static BaseResponse<T> Failure<T>(string message, StatusCode code, List<string> errors = null)
        {
            return new BaseResponse<T>()
            {
                IsSuccess = false,
                ResponseCode = (int)code,
                Result = default(T),
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}