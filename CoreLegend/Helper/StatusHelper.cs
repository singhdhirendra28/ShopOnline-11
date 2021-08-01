using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLegend.Helper
{
    public static class CommonStatusHelper
    {
        private static Dictionary<string, string> statusCodeAndDescription { get; set; }
        private static Dictionary<string, System.Net.HttpStatusCode> httpStatusCodeMapping { get; set; }
        static CommonStatusHelper()
        {
            if (statusCodeAndDescription == null)
                statusCodeAndDescription = new Dictionary<string, string>();


            statusCodeAndDescription.Add("200", "OK");

            statusCodeAndDescription.Add("1000", "System Error.");
            statusCodeAndDescription.Add("1001", "Invalid request input.");
            statusCodeAndDescription.Add("1002", "Can not fulfill the order.");
            statusCodeAndDescription.Add("1003", "Empty cart.");
            statusCodeAndDescription.Add("1004", "Product is not found.");

            #region HTTP status code mapping
            if (httpStatusCodeMapping == null)
                httpStatusCodeMapping = new Dictionary<string, System.Net.HttpStatusCode>();

            httpStatusCodeMapping.Add(ErrorMappingCode.Success, System.Net.HttpStatusCode.OK);
            httpStatusCodeMapping.Add(ErrorMappingCode.UnknownError, System.Net.HttpStatusCode.InternalServerError);
            httpStatusCodeMapping.Add(ErrorMappingCode.InvalidRequestInput, System.Net.HttpStatusCode.BadRequest);
            httpStatusCodeMapping.Add(ErrorMappingCode.CanNotFullfillOrder, System.Net.HttpStatusCode.ExpectationFailed);
            httpStatusCodeMapping.Add(ErrorMappingCode.CartIsEmpty, System.Net.HttpStatusCode.NotFound);
            httpStatusCodeMapping.Add(ErrorMappingCode.ProductNotAvailable, System.Net.HttpStatusCode.NotFound);

            #endregion

        }
        public static string ServiceStatusDesctiption(string statusCode)
        {
            string statusDesc = string.Empty;
            if (statusCodeAndDescription != null)
                statusDesc = statusCodeAndDescription[statusCode];

            return statusDesc;
        }



        public static System.Net.HttpStatusCode GetHTTPStatusCode(string oneClickStatusCode)
        {
            System.Net.HttpStatusCode statusCode = System.Net.HttpStatusCode.OK;
            if (httpStatusCodeMapping != null)
                statusCode = httpStatusCodeMapping[oneClickStatusCode];
            return statusCode;
        }
    }

    public sealed class ErrorMappingCode
    {
        public const string Success = "200";
        public const string UnknownError = "1000";
        public const string InvalidRequestInput = "1001";
        public const string CanNotFullfillOrder = "1002";
        public const string CartIsEmpty = "1003";
        public const string ProductNotAvailable = "1004";

    }  
    public class ApiResponse
    {
        public string StatusCode { get; set; }
        public object Response { get; set; }
        public ApiResponse(string code,object response)
        {
            this.StatusCode = code;
            this.Response = response;
        }

    }    
}
