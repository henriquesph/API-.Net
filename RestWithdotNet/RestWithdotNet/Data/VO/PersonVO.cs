/*using System.Text.Json.Serialization;*/ // custom serialization

using RestWithDotNet.Hypermedia;
using RestWithDotNet.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithDotNet.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
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
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}