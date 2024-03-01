using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appFinalNet.Modelos
{
    public class Producto
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int precio { get; set; }

        public string imagen_url { get; set; }

        public override string ToString()
        {
            return nombre + " - " + descripcion;
        }

    }
}
