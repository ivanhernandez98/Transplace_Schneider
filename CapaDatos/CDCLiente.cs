using System;
using MySql.Data.MySqlClient;
using CapaEntidad;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CDCLiente
    {
        string CadenaConexion = "Server=192.168.40.1;User=sa;Password=SitioM1;database=hgdb_lis;";
        
        public void pruebaConexion()
        {
            SqlConnection sqlConnection = new SqlConnection(CadenaConexion);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexion " + ex.Message);
                return;
            }
            MessageBox.Show("Conectado correctamente");
            sqlConnection.Close();
        }

        public DataSet Buscar(string shipment)
        {
            DateTime datetime = DateTime.Now;
            CECliente cE = new CECliente();
            cE.DateTime = datetime;
            cE.Shipment = shipment;

            SqlConnection sqlConnection = new SqlConnection(CadenaConexion);
            sqlConnection.Open();
            //string query = "select shipment,id_archivo,id_pedido,no_viaje,id_unidad,id_remolque1,f_despachado,eta_programada,eta_definitiva,entidad_nombre,entidad_N401,parada_finaliza,estatus from edi_interfase_interprete_paradas_detalle where shipment = " + cE.Shipment ;
            string qry = "select DISTINCT eis.id_archivo, eis.shipment, eis.id_pedido, eis.no_viaje, tv.id_unidad, eis.id_remolque1, eis.fecha_real_viaje, eis.fecha_estatus_viaje, eis.status_viaje, eis.eta_programada, eis.entidad_nombre, eis.entidad_N401, eis.id_ruta, tr.desc_ruta, tr.tiempo_ruta, eis.origen, eis.destino, isnull(eis.ETA_entrega_no_control,0) As NoControl, tv.fecha_llego_destinatario As fecha_llega_destino, tv.fecha_real_fin_viaje As fecha_real_fin_viaje, eiipd.estatus_control, tv.tipo_serv From edi_interfase_seguimiento eis With( Nolock ), trafico_viaje tv With( Nolock ), trafico_ruta tr, edi_interfase_interprete_paradas_detalle eiipd With( Nolock )" +
                         @"Where eis.fecha_real_viaje BETWEEN CONVERT(VARCHAR(10),DATEADD(day, 1, getdate()-11), 110) AND CONVERT(VARCHAR(10),DATEADD(day, 1, getdate()+1), 110) And eis.no_viaje=tv.no_viaje And eis.status_viaje='A' And eis.id_ruta=tr.id_ruta And eis.id_archivo=eiipd.id_archivo And tv.id_area = 1 And eis.nur is NULL and eis.id_interfas=7 AND eis.shipment='"+ cE.Shipment + "' order by eis.fecha_real_viaje";
            
            //string query = "select f_despachado, eta_programada, id_cliente_stop, entidad_nombre As Origen, X3, X3_shipment_status,  X3_enviado, id_posicionX3, AF, eta_definitiva, AF_shipment_status, AF_enviado, id_posicionAF, eta_arrivo, X1, X1_shipment_status, X1_enviado, id_posicionX1,  no_embarque, tipo_serv From edi_interfase_interprete_paradas_detalle With( Nolock ) Where shipment="+cE.Shipment+" And parada=1";

            SqlDataAdapter adapter;
            DataSet ds = new DataSet();

            adapter = new SqlDataAdapter(qry, sqlConnection);
            adapter.Fill(ds, "tbl");

            return ds;
        }

        public DataSet Listar() 
        {
            SqlConnection sqlConnection = new SqlConnection(CadenaConexion);
            sqlConnection.Open();
            //string query = "select shipment,id_archivo,id_pedido,no_viaje,id_unidad,id_remolque1,f_despachado,eta_programada,eta_definitiva,entidad_nombre,entidad_N401,parada_finaliza,estatus from edi_interfase_interprete_paradas_detalle where YEAR(eta_programada) = 2022 and MONTH(eta_programada) = 04 and DAY(eta_programada) = 05";
            //string qry = "select DISTINCT eis.id_archivo, eis.shipment, eis.id_pedido, eis.no_viaje, tv.id_unidad, eis.fecha_real_viaje, eis.fecha_estatus_viaje, eis.eta_programada, tr.desc_ruta, isnull(eis.ETA_entrega_no_control,0) As NoControl, tv.fecha_llego_destinatario As fecha_llega_destino, tv.fecha_real_fin_viaje As fecha_real_fin_viaje, tv.tipo_serv From edi_interfase_seguimiento eis With( Nolock ), trafico_viaje tv With( Nolock ), trafico_ruta tr, edi_interfase_interprete_paradas_detalle eiipd With( Nolock )" +
            //            "Where eis.fecha_real_viaje BETWEEN CONVERT(VARCHAR(10),DATEADD(day, 1, getdate()-11), 110) AND CONVERT(VARCHAR(10),DATEADD(day, 1, getdate()+1), 110) And eis.no_viaje=tv.no_viaje And eis.status_viaje='A' And eis.id_ruta=tr.id_ruta And eis.id_archivo=eiipd.id_archivo And tv.id_area = 1 And eis.nur is NULL and eis.id_interfas=7 order by eis.fecha_real_viaje ";
            
            string qry = "select DISTINCT eis.id_archivo, eis.shipment, eis.id_pedido, eis.no_viaje, tv.id_unidad, eis.fecha_real_viaje, eis.fecha_estatus_viaje, eis.eta_programada, tr.desc_ruta, isnull(eis.ETA_entrega_no_control,0) As NoControl From edi_interfase_seguimiento eis With( Nolock ), trafico_viaje tv With( Nolock ), trafico_ruta tr, edi_interfase_interprete_paradas_detalle eiipd With( Nolock )" +
                        "Where eis.fecha_real_viaje BETWEEN CONVERT(VARCHAR(10),DATEADD(day, 1, getdate()-11), 110) AND CONVERT(VARCHAR(10),DATEADD(day, 1, getdate()+1), 110) And eis.no_viaje=tv.no_viaje And eis.status_viaje='A' And eis.id_ruta=tr.id_ruta And eis.id_archivo=eiipd.id_archivo And tv.id_area = 1 And eis.nur is NULL and eis.id_interfas=7 order by eis.fecha_real_viaje ";

            SqlDataAdapter adapter;
            DataSet ds = new DataSet();

            adapter = new SqlDataAdapter(qry, sqlConnection);
            adapter.Fill(ds,"tbl");

            return ds; 
        }

        public DataSet Detalles(string shipment)
        {
            SqlConnection sqlConnection = new SqlConnection(CadenaConexion);
            sqlConnection.Open();

            string qry = "Select shipment, f_despachado, eta_programada, id_cliente_stop, entidad_nombre As X3, X3_shipment_status, X3_enviado, id_posicionX3, AF, AF_enviado, id_posicionAF, no_embarque, X1, X1_enviado From edi_interfase_interprete_paradas_detalle With(Nolock)" +
                        " Where eta_programada BETWEEN CONVERT(VARCHAR(10),DATEADD(day, 1, getdate() - 11), 110)" +
                        " AND CONVERT(VARCHAR(10),DATEADD(day, 1, getdate() + 1), 110) and shipment=" + shipment;
                        //" AND f_despachado is NOT NULL";

            SqlDataAdapter adapter;
            DataSet ds = new DataSet();

            adapter = new SqlDataAdapter(qry, sqlConnection);
            adapter.Fill(ds, "dtll");

            return ds;
        }
    }
}
