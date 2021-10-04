using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CineTec.Models;

namespace CineTec.Context
{
    public class CRUDContext:DbContext
    {

        public CRUDContext(DbContextOptions<CRUDContext> options): base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branches { get; set; }

    }
}
