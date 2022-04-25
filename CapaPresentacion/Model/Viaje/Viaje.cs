using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Model.Viaje
{
    class Viaje
    {
        public int id_archivo { get; set; }
        public string shipment { get; set; }
        public int id_pedido { get; set; }
        public int no_viaje { get; set; }
        public string id_unidad { get; set; }
        public string fecha_real_viaje  { get; set; }
        public string fecha_estatus_viaje { get; set; }
        public string eta_programada { get; set; }
        public string desc_ruta { get; set; }
        public int NoControl { get; set; }        
    }
}
