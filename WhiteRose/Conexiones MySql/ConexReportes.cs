using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public class ConexReportes : ConexBase
	{
		ListStore ReptFact;
		ListStore ReptDeta;
		ListStore ReptDeta1;

		public ConexReportes ()
		{
			ReptDeta = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			ReptDeta1 = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			ReptFact = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
		}

		/***********************************
		* MÉTODOS PROPIOS DE LOS LISTSTORE *
		************************************/

		public ListStore GetReptFact(){
			return ReptFact;
		}

		public ListStore GetReptDeta(){
			return ReptDeta;
		}

		public ListStore GetReptDeta1(){
			return ReptDeta1;
		}

		public void LimpiarDetFact(){
			ListStore Vacio = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));;
			ReptDeta = Vacio;
		}

		public void LimpiarDetFact1(){
			ListStore Vacio = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));;
			ReptDeta1 = Vacio;
		}

		public void LimpiaFact(){
			ListStore Vacio = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			ReptFact = Vacio;
		}

		/****************
		* OTROS MÉTODOS *
		*****************/

		public bool ExistenciaServicio (string cod)
		{ //Verifica si un servicio existe o no.
			int n=0;
			cmd = new MySqlCommand ("select count(ServCod) from tservicios where ServCod=@cod", con);
			cmd.Parameters.AddWithValue ("@cod", cod);

			try {
				con.Open ();
				n = Convert.ToInt16 (cmd.ExecuteScalar ());
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}

			if (n==0)
				return false;
			else
				return true;
		}

		public void CargarDetFacturas(string nro)
		{ //Carga el detalle de una factura dada.
			cmd = new MySqlCommand("Select DetNroFactura,DetCodServ,DetCantidad,DetPrecio from tdetfactura where DetEstatus='A' and DetNroFactura='"+nro+"'",con);
			try {
				con.Open();
				read = cmd.ExecuteReader();
				while (read.Read()) {
					ReptDeta1.AppendValues(read [0].ToString(),read [1].ToString(),read [2].ToString(),(read [3].ToString()+ " Bs."));
				}
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			} finally{
				cmd.Dispose ();
				con.Dispose ();
			}
		}

		public void DevolverFactura (string FechaI,string FechaF)
		{ //Carga los datos de las facturas que estén ubicadas en un intervalo de tiempo dado.
			cmd = new MySqlCommand ("select * from tfactventa where VenEstatus='A' and VenFechaFact >= @fi and VenFechaFact <= date_add('"+FechaF+"',interval 1 day)", con);
			cmd.Parameters.AddWithValue ("@fi", FechaI);
				try {
					con.Open ();
					read = cmd.ExecuteReader ();
					while (read.Read ()) {
					ReptFact.AppendValues(read [0].ToString (), read [1].ToString (), read [2].ToString(),Convert.ToDateTime(read[3]).ToString ("d"), (read [4].ToString()+ " Bs."), read [5].ToString(), read [6].ToString());
				}
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Close ();
				}
		}

		public void DevolverDetFacturas(string nro)
		{ //Carga el detalle de una factura dada.
			cmd = new MySqlCommand("Select DetNroFactura,DetCodServ,DetCantidad,DetPrecio from tdetfactura where DetEstatus='A' and DetCodServ='"+nro+"'",con);
			try {
				con.Open();
				read = cmd.ExecuteReader();
				while (read.Read()) {
					ReptDeta.AppendValues(read [0].ToString(),read [1].ToString(),read [2].ToString(),(read [3].ToString()+ " Bs."));
				}
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			} finally{
				cmd.Dispose ();
				con.Dispose ();
			}
		}
	}
}

