using System;

namespace CapaEntidad
{

    [System.ComponentModel.Bindable(true)]

    public class CECliente
    {
        public string Shipment { get; set; }
        public DateTime DateTime { get; set; }
        public string Archivo { get; set; }
    }
}
