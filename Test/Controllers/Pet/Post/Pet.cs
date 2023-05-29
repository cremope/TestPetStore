using ExecuteService.Models.PetStore.Controllers;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TestPetStore.Model.Controllers.Pet.Post;
using TestPetStore.Utils;

namespace TestPetStore.Test.Controllers.Pet.Post
{
    [Parallelizable(scope: ParallelScope.All)]
    public class Pet
    {
        private readonly Base BASE = new();
        private readonly ApiPetStore APIPETSTORE = new();
        public Pet()
        {
        }

        [Test, Order(1)]
        [TestCase(10)]
        [Description("Valida retorno 200 - Adiciona novo pet")]
        public void Retorno_200_AdicionaNovoPet(int id)
        {
            #region Data
            int Id = id;
            int IdCategory = 1;
            string NameCategory = "Cachorro";
            string Name = "Igor";
            string UrlPhoto = "https://www.adoropets.com.br/wp-content/uploads/2021/04/how-to-draw-a-dog-19.jpeg";
            int IdTag = 1;
            string NameTag = "Cachorro";
            string Status = "available";

            #endregion

            #region Json Request
            var request = new PetRequest()
            {
                id = Id,
                category = new Category()
                {
                    id = IdCategory,
                    name = NameCategory
                },
                name = Name,
                photoUrls = new string[1] {UrlPhoto},
                tags = new List<Tag>()
                {
                    new Tag() 
                    {
                        id = IdTag,
                        name = NameTag
                    }
                },
                status = Status
            };
            #endregion

            #region Request Post /pet
            var response = APIPETSTORE.ExecutaPostPet(request, Method.Post);
            #endregion

            #region Validation Response
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                BASE.Mensagem($@"[TestPetStore] - Status code: {(int)response.StatusCode}");
                var responseDeserializado = JsonConvert.DeserializeObject<ResponsePet>(response.Content);
                Assert.AreEqual(Id, responseDeserializado.id);
                Assert.AreEqual(IdCategory, responseDeserializado.category.id);
                Assert.AreEqual(NameCategory, responseDeserializado.category.name);
                Assert.AreEqual(Name, responseDeserializado.name);
                Assert.AreEqual(UrlPhoto, responseDeserializado.photoUrls[0]);
                Assert.AreEqual(IdTag, responseDeserializado.tags.FirstOrDefault().id);
                Assert.AreEqual(NameTag, responseDeserializado.tags.FirstOrDefault().name);
                Assert.AreEqual(Status, responseDeserializado.status);
                BASE.Mensagem($@"[TestPetStore] - Novo pet adicionado com sucesso");
                BASE.Mensagem($@"[TestPetStore] - Pet adicionado: {response.Content}");
            }
            #endregion
        }

        [Test, Order(2)]
        [Description("Valida retorno 405 - Quando acionado endpoint com método diferente de Post")]

        public void Retorno_405_AdicionaNovoPet()
        {
            #region Data
            int Id = 2;
            int IdCategory = 2;
            string NameCategory = "Cachorro";
            string Name = "Igor";
            string UrlPhoto = "https://www.adoropets.com.br/wp-content/uploads/2021/04/how-to-draw-a-dog-19.jpeg";
            int IdTag = 2;
            string NameTag = "Cachorro";
            string Status = "available";
            #endregion

            #region Json Request
            var request = new PetRequest()
            {
                id = Id,
                category = new Category()
                {
                    id = IdCategory,
                    name = NameCategory
                },
                name = Name,
                photoUrls = new string[1] { UrlPhoto },
                tags = new List<Tag>()
                {
                    new Tag()
                    {
                        id = IdTag,
                        name = NameTag
                    }
                },
                status = Status
            };
            #endregion

            #region Request Get /pet
            var response = APIPETSTORE.ExecutaPostPet(request, Method.Get);
            #endregion

            #region Validation Response
            if (response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
            {
                BASE.Mensagem($@"[TestPetStore] - Status code: {(int)response.StatusCode}");
                BASE.Mensagem($@"[TestPetStore] - Não foi permitido executar o endpoint com o método Get: {response.Content}");
            }
            #endregion
        }
    }
}
