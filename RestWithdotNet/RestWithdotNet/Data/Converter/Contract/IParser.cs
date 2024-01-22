using System.Collections.Generic;

namespace RestWithDotNet.Data.Converter.Contract
{
    public interface IParser<O, D> // 2 tipos genéricos - Orgigem e destino
    {
        D Parse(O origin);

        List<D> Parse(List<O> origin);
    }
}