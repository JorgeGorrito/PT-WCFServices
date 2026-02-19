using AutoMapper;
using PruebaTecnica.BDO.Enums;
using PruebaTecnica.BDO.Exceptions;
using PruebaTecnica.Business.DataContracts.Responses;
using System;
using System.Collections.Generic;

namespace PruebaTecnica.Business.Services
{
    public static class ServiceHandler
    {
        public static BaseResponse<T> Handle<T>(string serviceName, Func<BaseResponse<T>> action)
        {
            try
            {
                return action();
            }
            catch (ValidationErrorException ex)
            {
                return HandleValidationError<T>(ex);
            }
            catch (NotFoundException ex)
            {
                return HandleNotFound<T>(ex);
            }
            catch (Exception ex)
            {
                return HandleGenericError<T>(serviceName, ex);
            }
        }

        private static BaseResponse<T> HandleValidationError<T>(ValidationErrorException ex) =>
            Response.Failure<T>("Error de validación en los datos.", StatusCode.ValidationError, new List<string> { ex.Message });

        private static BaseResponse<T> HandleNotFound<T>(NotFoundException ex) =>
            Response.Failure<T>("Recurso no encontrado.", StatusCode.NotFound, new List<string> { ex.Message });

        private static BaseResponse<T> HandleGenericError<T>(string name, Exception ex)
        {
            System.Diagnostics.Trace.WriteLine($"[Error Grave en {name}]: {ex}");
            return Response.Failure<T>("Algo salió mal.", StatusCode.UnhandledError, new List<string> { ex.Message });
        }
    }
}