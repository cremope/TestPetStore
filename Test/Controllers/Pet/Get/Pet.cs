using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TestPetStore.Model.Controllers.Pet.Post;
using TestPetStore.Utils;

namespace TestPetStore.Test.Controllers.Pet.Get
{
    [Parallelizable(scope: ParallelScope.All)]
    public class Pet
    {
        private readonly Base BASE = new();
        private readonly ApiPetStore APIPETSTORE = new();
        private readonly TestPetStore.Test.Controllers.Pet.Post.Pet POSTPET = new();
        private readonly TestPetStore.Test.Controllers.Pet.Delete.Pet DELETEPET = new();
        public Pet()
        {
        }

        [Test, Order(1)]
        [TestCase(10)]
        [Description("Valida retorno 200 - Consulta pet de acordo com id informado")]
        public void Retorno_200_ConsultaPet(int id)
        {
            #region Data
            var Id = id;
            #endregion

            #region Adiciona pet
            POSTPET.Retorno_200_AdicionaNovoPet(Id);
            #endregion

            #region Request GET /pet/petId
            var response = APIPETSTORE.ExecutaGetPet(Id, Method.Get);
            #endregion

            #region Validation Response
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                BASE.Mensagem($@"[TestPetStore] - Status code: {(int)response.StatusCode}");
                var responseDeserializado = JsonConvert.DeserializeObject<ResponsePet>(response.Content);
                Assert.IsNotNull(responseDeserializado);
                Assert.AreEqual(Id, responseDeserializado.id);
                BASE.Mensagem($@"[TestPetStore] - Pet consultado com sucesso");
                BASE.Mensagem($@"[TestPetStore] - Pet consultado: {response.Content}");
            }
            #endregion
        }

        [Test, Order(2)]
        [Description("Valida retorno 404 - Quando id do pet informado não existe para ser consultar")]
        public void Retorno_404_ConsultaPet()
        {
            #region Data
            int Id = 10;
            #endregion

            #region Deleta pet
            DELETEPET.Retorno_200_DeletaPet(Id);
            #endregion

            #region Request Get /pet/petId
            var response = APIPETSTORE.ExecutaGetPet(Id, Method.Get);
            #endregion

            #region Validation Response
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                BASE.Mensagem($@"[TestPetStore] - Status code: {(int)response.StatusCode}");
                BASE.Mensagem($@"[TestPetStore] - Não foi encontrado pet com o id: {Id}");
            }
            else
            {
                Assert.Fail($"Foi encontrado um pet com o Id: {Id} - Onde deveria retornar que ele não existe");
            }   
            #endregion
        }
    }
}
