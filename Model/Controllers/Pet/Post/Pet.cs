using ExecuteService.Models.PetStore.Controllers;

namespace TestPetStore.Model.Controllers.Pet.Post
{
    public class ResponsePet
    {
        public int id { get; set; }
        public ResponseCategory category { get; set; }
        public string name { get; set; }
        public string[] photoUrls { get; set; }
        public List<ResponseTag> tags { get; set; }
        public string status { get; set; }
    }

    public class ResponseCategory
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ResponseTag
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
