using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextTrip.Models
{

    public class TypeAheadRoot
    {
        public int StatusCode { get; set; }
        public object Message { get; set; }
        public int ExecutionTime { get; set; }
        public ResponseData[] ResponseData { get; set; }
    }

    public class ResponseData
    {
        public string Name { get; set; }
        public string SiteId { get; set; }
        public string Type { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
    }

}
