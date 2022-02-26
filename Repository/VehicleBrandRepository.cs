using ServitalleresWeb.Models;
using ServitalleresWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServitalleresWeb.Repository
{
   
    public class VehicleBrandRepository : Repository<VehicleBrand>, IVehicleBrandRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public VehicleBrandRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
