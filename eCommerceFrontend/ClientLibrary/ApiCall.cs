using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{
    public class ApiCall
    {
        public string? Type { get; set; }
        public HttpClient? Client { get; set; }
        public string? Route { get; set; }
        public dynamic? Model { get; set; }
        public string? Id { get; set; }
        public static void ToString(Guid id) => id.ToString(); 
    }
}
