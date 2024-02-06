using System.Collections.Generic;

namespace RestWithDotNet.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    { 
        List<HyperMediaLink> Links { get; set; }
    }
}
