using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public interface IClientProvider
    {
        List<Client> GetAllClients();
        Client GetClient(int id);
        void DeleteClient(int id);
        void UpdateClient(Client client);
        void InsertClient(Client client);
        
        
    }
}
