/*using System.Text.Json.Serialization;*/ // custom serialization

namespace RestWithDotNet.Data.VO
{
    public class PersonVO
    {
        /*[JsonPropertyName("code")] */// mudando a serialização no Json - personalizando o nome do campo exibido
        // servve tanto para ler os dados quanto para persistir no BD
        public long Id { get; set; }
        //[JsonPropertyName("name")]
        public string FirstName { get; set; }
        //[JsonPropertyName("last_name")]
        public string LastName { get; set; }
        //[JsonIgnore] // não vai ser serializado - não aparece no Json
        public string Adress { get; set; }
        //[JsonPropertyName("sex")]
        public string Gender { get; set; }
    }
}