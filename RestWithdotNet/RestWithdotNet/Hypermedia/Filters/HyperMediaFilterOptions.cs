using RestWithDotNet.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithDotNet.Hypermedia.Filters
{
    // filters: servem para interceptar as requisições e criar os Links (HATEOAS)
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>(); // interface sendo declarada como lista?
    }
}
