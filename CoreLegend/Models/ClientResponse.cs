using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;


namespace CoreLegend.Models
{
    public class ClientResponse
    {
        public HttpResponseMessage SuccessResponse(IEnumerable<Employee> apiResponse)
        {
            
            HttpRequestMessage request = new HttpRequestMessage();
            var model = JsonConvert.SerializeObject(apiResponse);
            ////_logHelper.LogInfo("Response sent back to UI/App :", model);
            //var response = request.CreateResponse(System.Net.HttpStatusCode.OK);
            StringContent content = new StringContent(model, System.Text.Encoding.UTF8, "application/json");
            return new HttpResponseMessage(HttpStatusCode.OK) { Content =content };            
        }
    }


}
