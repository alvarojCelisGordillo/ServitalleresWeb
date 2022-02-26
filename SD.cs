using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServitalleresWeb
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44328/";
        public static string VehicleBrandPath = APIBaseUrl + "api/VehicleBrands/";
        public static string ClientPath = APIBaseUrl + "api/Clients/";
        public static string AccountAPIPath = APIBaseUrl + "api/Users/";
    }
}
