using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class SedeCliente
    {
        public string pais { get; set; }
        public string departamento { get; set; }
    }

    public class SedeClienteCons
    {
        public string pais { get; set; }
        public string departamento { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string telefono { get; set; }
        public string contacto { get; set; }
    }
}
