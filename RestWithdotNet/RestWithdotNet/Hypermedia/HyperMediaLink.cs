using System.Text;

namespace RestWithDotNet.Hypermedia
{
    public class HyperMediaLink
    {
        public string Rel { get; set; }
        // Barra / no .net é %2f, corrigindo abaixo
        private string href { get; set; }
        public string Href
        {
            get
            {
                // para evitar paralelismo e gerar urls com problemas
                object _lock = new object();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString();
                }

            }
            set
            {
                href = value;
            }
        }
        public string Type { get; set; }
        public string Action { get; set; }
    }
}
