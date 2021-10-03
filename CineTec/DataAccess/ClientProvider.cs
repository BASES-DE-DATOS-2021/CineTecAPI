using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public class ClientProvider : IClientProvider
    {
        private readonly PostgreSqlContext _context;

        public ClientProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        public List<Client> GetAllClients()
        {

            return _context.client.ToList();
   
        }

        public Client GetClient(int id)
        {
            return _context.client.FirstOrDefault(c => c.id == id);
        }

        public void DeleteClient(int id)
        {
            var client = _context.client.FirstOrDefault(t => t.id == id);
            _context.client.Remove(client);
            _context.SaveChanges();
        }

        public void UpdateClient(Client client)
        {
            _context.client.Update(client);
            _context.SaveChanges();
        }

        public void InsertClient(Client client)
        {
            _context.client.Add(client);
            _context.SaveChanges();
        }
    }
}
