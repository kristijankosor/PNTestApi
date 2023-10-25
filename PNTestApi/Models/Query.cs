using System.Net.NetworkInformation;

namespace PNTestApi.Models
{
    public class Query
    {
        public int Id { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
