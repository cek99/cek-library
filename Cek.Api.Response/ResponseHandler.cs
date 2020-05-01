using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace Cek.Api.Response
{
        public class ResponseDetail
        {
            //Error response details for bad requests 
            public const string BadRequestCode = "400";
            public const string BadRequestMessage = "Bad Request";
            public const string BadRequestDetail = "Provide request with correct parameters";

            //Error response details for success request
            public const string SuccessCode = "200";
            public const string SuccessMessage = "Success";
            public const string SuccessDetail = "Success";

            public const string SaveSuccess = "Save Successfully";
            public const string UpdateSuccess = "Update Successfully";
            public const string RetreaveSuccess = "Retreave Successfully";
            public const string EmptyResultFound = "No result found from the data base";
            public const string SqlErrorMessage = "Error in sql";
            public const string Invalid = "Invalid parameter found in data base";

            //Error response details for internal server status 
            public const string InternalServerErrorCode = "500";
            public const string InternalServerErrorMessage = "Internal Server Error";
            public const string InternalServerErrorDetail = "Internal Server Error";
        }
        public class ErrorMessages
        {
            public const string IsRequired = " is Required";
        }
        public class ResposnseType
        {
            public const string BadRequest = "BadRequest";
            public const string Success = "Success";
            public const string ServerError = "ServerError";
            public const string SqlError = "SqlError";
            public const string UserDefineError = "UserDefineError";
            public const string EmptyResult = "EmptyResult";
        }
        public class Response<T> where T : class, new()
        {
            public Response()
            {
                Data = new T();
                Status = new ResponseStatus();
            }
            public T Data { get; set; }
            public ResponseStatus Status { get; set; }
        }
        public class ResponseList<T> where T : class, new()
        {
            public ResponseList()
            {
                Data = new List<T>();
                Status = new ResponseStatus();
            }
            public List<T> Data { get; set; }
            public ResponseStatus Status { get; set; }
        }
        public class EmptyResponse
        {
            public bool Validity { get; set; } = true;
            public string Message { get; set; }
        }
        public class ResponseStatus
        {
            //public bool Status { get; set; }
            public string Message { get; set; }
            public Dictionary<string, string> Errors { get; set; }
        }
        public class ResponseHandler
        {
            public static Response<T> Get<T>(string type, string detail = null, T response = null, Dictionary<string, string> errors = null)
             where T : class, new()
            {
                var apiResponse = new Response<T>();
                    apiResponse.Data = response;
                    apiResponse.Status = new ResponseStatus() { Message = detail != null ? detail : GetResponseMessage(type), Errors = errors };
                return apiResponse;
            }
            public static ResponseList<T> GetList<T>(string type, string detail = null, List<T> response = null, Dictionary<string, string> errors = null)
               where T : class, new()
            {
                var apiResponse = new ResponseList<T>();
                    apiResponse.Data = response;
                    apiResponse.Status = new ResponseStatus() { Message = detail != null ? detail : GetResponseMessage(type), Errors = errors };
                return apiResponse;
            }
            public static Dictionary<string, string> ValidateModelState(ModelStateDictionary modelState)
            {
                Dictionary<string, string> defaultErrors = new Dictionary<string, string>();
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        if (!defaultErrors.ContainsKey(state.Key))
                        {
                            defaultErrors.Add(state.Key, state.Key + ErrorMessages.IsRequired);
                        }
                    }
                }
                return defaultErrors;
         }
            private static string GetResponseMessage(string type)
             {
                    string message = string.Empty;
                    switch (type)
                    {
                        case ResposnseType.BadRequest:
                            message = ResponseDetail.BadRequestMessage;
                            break;
                        case ResposnseType.ServerError:
                            message = ResponseDetail.InternalServerErrorMessage;
                            break;
                        case ResposnseType.Success:
                            message = ResponseDetail.SuccessMessage;
                            break;
                        case ResposnseType.SqlError:
                            message = ResponseDetail.SqlErrorMessage;
                            break;
                        default:
                            message = ResponseDetail.InternalServerErrorMessage;
                            break;
                    }
                    return message;
             }

        }
    }


