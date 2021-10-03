using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public class BranchProvider : IBranchProvider
    {
        private readonly PostgreSqlContext _context;

        public BranchProvider(PostgreSqlContext context)
        {
            _context = context;
        }


        public List<Branch> GetAllBranchs()
        {

            return _context.branch.ToList();
   
        }

        public Branch GetBranch(string cinema_name)
        {
            return _context.branch.FirstOrDefault(c => c.cinema_name == cinema_name);
        }

        public void DeleteBranch(string cinema_name)
        {
            var branch = _context.branch.FirstOrDefault(c => c.cinema_name == cinema_name);
            _context.branch.Remove(branch);
            _context.SaveChanges();
        }

        public void UpdateBranch(Branch branch)
        {
            _context.branch.Update(branch);
            _context.SaveChanges();
        }

        public void InsertBranch(Branch branch)
        {
            _context.branch.Add(branch);
            _context.SaveChanges();
        }

    }
        
}
