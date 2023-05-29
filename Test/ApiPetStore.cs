using ExecuteService.Models.PetStore.Controllers;
using ExecuteService.Service.PetStore.Controllers;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TestPetStore.Utils;

namespace TestPetStore.Test
{
    public class ApiPetStore
    {
        private Pet PET = new();
        private readonly Base BASE = new();

        public ApiPetStore()
        {
        }

        public RestResponse ExecutaPostPet(PetRequest request, Method metodo)
        {
            BASE.Mensagem($"[TestPetStore] - Executando endpoint {metodo.ToString().ToUpper()} /pet");
            BASE.Mensagem($"[TestPetStore] - Request: {JsonConvert.SerializeObject(request)}");
            var response = PET.Post_Pet(request, metodo);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.MethodNotAllowed)
            {
                Assert.Fail($@"[TestPetStore] - Ocorreu um erro ao executar o endpoint Post /pet
                Status code: {response.StatusDescription}
                Response: {response.Content}");
            }

            return response;
        }

        public RestResponse ExecutaGetPet(object id, Method metodo)
        {
            BASE.Mensagem($"[TestPetStore] - Executando endpoint {metodo.ToString().ToUpper()} /pet/petId");
            BASE.Mensagem($"[TestPetStore] - Id informado: {id}");
            var response = PET.Get_Pet(id, metodo);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && 
                response.StatusCode != System.Net.HttpStatusCode.NotFound 
               )
            {
                Assert.Fail($@"[TestPetStore] - Ocorreu um erro ao executar o endpoint Get /pet/petId
                Status code: {response.StatusDescription}
                Response: {response.ResponseUri}");
            }

            return response;
        }

        public RestResponse ExecutaDeletePet(object id, Method metodo)
        {
            BASE.Mensagem($"[TestPetStore] - Executando endpoint {metodo.ToString().ToUpper()} /pet/petId");
            BASE.Mensagem($"[TestPetStore] - Id informado: {id}");
            var response = PET.Delete_Pet(id, metodo);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.NotFound 
               )
            {
                Assert.Fail($@"[TestPetStore] - Ocorreu um erro ao executar o endpoint Delete /pet/petId
                Status code: {response.StatusDescription}
                Response: {response.ResponseUri}");
            }

            return response;
        }
    }
}
