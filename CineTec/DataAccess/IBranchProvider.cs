using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.DataAccess
{
    public interface IBranchProvider
    {

       
        List<Branch> GetAllBranchs();
        Branch GetBranch(string cinema_name);
        void DeleteBranch(string cinema_name);
        void UpdateBranch(Branch branch);
        void InsertBranch(Branch branch);

   
        
        
    }
}
