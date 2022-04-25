using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;
using CapaDatos;
using Newtonsoft.Json;
using CapaPresentacion.Model.Viaje;

namespace CapaPresentacion
{
    public partial class frClientes : Form
    {
        CNCliente cNCliente = new CNCliente();

        public frClientes()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataSet dt = new DataSet();

            bool resultado;
            CECliente cECliente = new CECliente();
            cECliente.Shipment = (string)txtId.Text;

            resultado = cNCliente.validarDatos(cECliente);

            if (resultado == false)
            {
                return;
            }

           else 
            {
                cECliente.Shipment = txtId.Text;
                CDCLiente cCDCLiente = new CDCLiente();
               
                ds = cCDCLiente.Buscar(cECliente.Shipment);
                gridDatos.DataSource = ds.Tables["tbl"];
                cargarDetalles(cECliente.Shipment);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //Borrar datos escritos en busqueda de Shipment
            txtId.Text = String.Empty;

            //Actualizacion de Datos en Transplace
            cNCliente.pruebaMySql();
            //cargarDatos();

            //cargador datos en el Data Grid
            gridDatos.DataSource = cNCliente.ObtenerDatos();
            CDCLiente cdCLiente = new CDCLiente();

            cargarDatos();
        }

        private void frClientes_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            gridDatos.DataSource = cNCliente.ObtenerDatos().Tables["tbl"];
            lblViajesTotales.Text = (cNCliente.ObtenerDatos().Tables["tbl"].Rows.Count).ToString();
        }

        private void cargarDetalles(string shipment)
        {
            gridDetalles.DataSource = cNCliente.BuscarDatos(shipment).Tables["dtll"];
        }

        private void gridDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Viaje> viajes = JsonConvert.DeserializeObject<List<Viaje>>(JsonConvert.SerializeObject(gridDatos.DataSource, Formatting.Indented));
            cargarDetalles(viajes[e.RowIndex].shipment);
        }
    }
}
