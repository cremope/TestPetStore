using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TestPetStore.Model.Controllers.Pet.Delete;
using TestPetStore.Utils;

namespace TestPetStore.Test.Controllers.Pet.Delete
{
    [Parallelizable(scope: ParallelScope.All)]
    public class Pet
    {
        private readonly Base BASE = new();
        private readonly ApiPetStore APIPETSTORE = new();
        private readonly TestPetStore.Test.Controllers.Pet.Post.Pet POSTPET = new();
        public Pet()
        {
        }

        [Test, Order(1)]
        [TestCase(10)]
        [Description("Valida retorno 200 - Exclui um pet de acordo com o id informado")]
        public void Retorno_200_DeletaPet(int id)
        {
            #region Data
            var Id = id;
            #endregion

            #region Adiciona pet
            POSTPET.Retorno_200_AdicionaNovoPet(Id); 
            #endregion

            #region Request Delete /pet/petId
            var response = APIPETSTORE.ExecutaDeletePet(Id, Method.Delete);
            #endregion

            #region Validation Response
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                BASE.Mensagem($@"[TestPetStore] - Status code: {(int)response.StatusCode}");
                var responseDeserializado = JsonConvert.DeserializeObject<ResponseDeletePet>(response.Content);
                Assert.IsNotNull(responseDeserializado);
                Assert.AreEqual(200, responseDeserializado.code);
                Assert.AreEqual("unknown", responseDeserializado.type);
                Assert.AreEqual(Id.ToString(), responseDeserializado.message);
                BASE.Mensagem($@"[TestPetStore] - Pet excluído com sucesso");
                BASE.Mensagem($@"[TestPetStore] - Id Pet excluido: {Id}");
            }
            #endregion
        }

        [Test, Order(2)]
        [Description("Valida retorno 404 - Quando id do pet informado não existe para excluir")]
        public void Retorno_404_DeletaPet()
        {
            #region Data
            var Id = 10;
            #endregion

            #region Adiciona pet
            POSTPET.Retorno_200_AdicionaNovoPet(Id);
            #endregion

            #region Request Delete /pet/petId
            APIPETSTORE.ExecutaDeletePet(Id, Method.Delete);
            var response = APIPETSTORE.ExecutaDeletePet(Id, Method.Delete);
            #endregion

            #region Validation Response
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                BASE.Mensagem($@"[TestPetStore] - Status code: {(int)response.StatusCode}");
                BASE.Mensagem($@"[TestPetStore] - Não foi encontrado o pet com o id: {Id}");
            }
            #endregion
        }
    }
}
