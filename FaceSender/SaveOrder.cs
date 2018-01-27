using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace FaceSender
{
    public static class SaveOrder
    {
        [FunctionName("SaveOrder")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",   "post", Route = null)]HttpRequestMessage req, 
            [Table("Orders", Connection="StorageConnection")]IAsyncCollector<PhotoOrder> ordersTable,
            TraceWriter log)
        {
            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Set name to query string or body data
            name = name ?? data?.name;

            var item = new PhotoOrder
            {
                PartitionKey = "1",
                RowKey = name,                
                Name = name
            };

            await ordersTable.AddAsync(item);

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);

            
        }
    }
}
