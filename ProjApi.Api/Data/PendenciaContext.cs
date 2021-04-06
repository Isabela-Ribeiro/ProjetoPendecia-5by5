using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjApi.Api.Model;

    public class PendenciaContext : DbContext
    {
        public PendenciaContext (DbContextOptions<PendenciaContext> options)
            : base(options)
        {
        }

        public DbSet<ProjApi.Api.Model.Pendencia> Pendencia { get; set; }
    }
