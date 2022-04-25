using System;
using CapaEntidad;
using System.Windows.Forms;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CNCliente
    {
        CDCLiente cDCLiente = new CDCLiente();

        public bool validarDatos(CECliente cliente) 
        {
            bool resultado = true;
            if (cliente.Shipment == string.Empty) 
            {
                resultado = false;
                MessageBox.Show("Verifica que el numero sea correcto");
            }
            return resultado; 
        }

        public void pruebaMySql()
        {
            cDCLiente.pruebaConexion();
        }

        public DataSet ObtenerDatos() 
        {
            return cDCLiente.Listar();
        }

        public DataSet BuscarDatos(string shipment)
        {
            return cDCLiente.Detalles(shipment);
        }


    }
}
