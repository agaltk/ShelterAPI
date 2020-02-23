using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;

namespace ShelterAPI.Data
{
    public class ShelterContext :DbContext
    {
        

        public ShelterContext(DbContextOptions<ShelterContext> options)
           : base(options)
        {
        }
        public DbSet<ShelterApi.Models.Animal> Animal { get; set; }

    }
}
