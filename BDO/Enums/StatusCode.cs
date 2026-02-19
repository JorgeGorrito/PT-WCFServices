using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BDO.Enums
{
    /// <summary>
    /// Representa el estado de una operación de negocio o transacción.
    /// Se utilizan valores estándar compatibles con HTTP para facilitar la integración.
    /// </summary>
    public enum StatusCode
    {
        Success = 200,         // OK
        Created = 201,         // Recurso creado
        ValidationError = 400, // Bad Request (Datos inválidos)
        NotFound = 404,        // No encontrado
        UnhandledError = 500   // Error interno del servidor
    }
}
