using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ServitalleresWeb.Models;
using ServitalleresWeb.Repository.IRepository;

namespace ServitalleresWeb.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public ClientRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
