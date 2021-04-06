using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProjApi.Api;
using ProjApi.Api.Model;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace ProjApi.Tests
{
    public class PendenciaIntegrateTest
    {
        public HttpClient _pendencia { get; }
        public PendenciaIntegrateTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _pendencia = appFactory.CreateClient();
            _pendencia.DefaultRequestHeaders.Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async void GetAllClients()
        {
            var response = await _pendencia.GetAsync("/api/Pendencia");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetCountClients()
        {
            var response = await _pendencia.GetAsync("/api/Pendencia");
            var pendencias = JsonConvert.DeserializeObject<Pendencia[]>(await response.Content.ReadAsStringAsync());
            Assert.True(pendencias.Length >= 1);
        }

        [Fact]
        public async void PostClient()
        {

            Pendencia pendencia = new Pendencia()
            {
                Data = "20/20/2015",
                Descricao = "Eletronica",
               
            };

            var jsonContent = JsonConvert.SerializeObject(pendencia);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _pendencia.PostAsync("/api/Pendencia", contentString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
    }
}