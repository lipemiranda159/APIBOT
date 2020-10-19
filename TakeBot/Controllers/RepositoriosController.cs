using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TakeAPI.Models;

namespace TakeBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RepositoriosController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(IntegrarAPIGitHub());
            }
            catch (Exception)
            {
                return BadRequest();
            }            
        }

        public List<RepositorioModel> IntegrarAPIGitHub()
        {
            List<RepositorioModel> repositorio = new List<RepositorioModel>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                HttpResponseMessage response = client.GetAsync("https://api.github.com/orgs/takenet/repos").Result;

                response.EnsureSuccessStatusCode();
                string conteudo = response.Content.ReadAsStringAsync().Result;

                dynamic resultado = JsonConvert.DeserializeObject(conteudo);
                
                foreach (var item in resultado)
                {
                    RepositorioModel repos = new RepositorioModel();

                    repos.NomeCompleto = item.full_name;
                    repos.DescricaoRepositorio = item.description;
                    repos.Imagem = item.owner.avatar_url;
                    repos.DataCriacao = item.created_at;
                    repos.linguagemRepositorio = item.language.ToString();

                    repositorio.Add(repos);
                }

                return repositorio.Where(b => b.linguagemRepositorio.Equals("C#")).OrderBy(a => a.DataCriacao).Take(5).ToList();
            }
        }
    }
}
