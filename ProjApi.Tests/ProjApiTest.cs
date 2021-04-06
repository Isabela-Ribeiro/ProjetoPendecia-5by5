using Microsoft.EntityFrameworkCore;
using ProjApi.Api.Controllers;
using ProjApi.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjApi.Tests
{
   
    public class ProjApiTest
    {
        private DbContextOptions<PendenciaContext> options;

        private void InitializeDataBase()
        {
             options = new DbContextOptionsBuilder<PendenciaContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

             using (var context = new PendenciaContext(options))
            {
                context.Pendencia.Add(new Pendencia { Id = 1, Descricao = "Comida", Data = "06/05/2021" });
                context.Pendencia.Add(new Pendencia { Id = 2, Descricao = "Roupa", Data = "10/08/1950" });
                context.Pendencia.Add(new Pendencia { Id = 3, Descricao = "Brinquedos",Data= "02/03/1986" });
                context.SaveChanges();
            }

        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController PendenciaController = new PendenciaController(context);
                IEnumerable<Pendencia> pendencias = PendenciaController.GetPendencia().Result.Value;
    
                Assert.Equal(3, pendencias.Count());
            }

        
        }
        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            using(var context = new PendenciaContext(options))
            {
                int pendenciaId = 2;
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pendencia = pendenciaController.GetPendencia(pendenciaId).Result.Value;
                Assert.Equal(2,pendencia.Id);
            }
        }

        [Fact]
        public void Create()
        {
                InitializeDataBase();

                Pendencia pendencia = new Pendencia()
                {
                    Id = 4,
                    Descricao = "Eletronico",
                    Data="12/02/2021"

                };

            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.PostPendencia(pendencia).Result.Value;
                Assert.Equal(4, pend.Id);
            }
        }
         [Fact]
        public void Update()
        {
            InitializeDataBase();

            Pendencia pendencia = new Pendencia()
            {
                Id = 3,
                Descricao = "Eletronico",
                Data = "12/02/2020"
            };

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia ped = pendenciaController.PutPendencia(3, pendencia).Result.Value;
                Assert.Equal("Eletronico", ped.Descricao);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
               PendenciaController PendenciaController = new PendenciaController(context);
               Pendencia pendencia = PendenciaController.DeletePendencia(2).Result.Value;
                Assert.Null(pendencia);
            }
        }
    }
}
