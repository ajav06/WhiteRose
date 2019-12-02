using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public class ConexVenta: ConexBase
	{
		ListStore DetFact;
		double sst;
		string nro;

		public ConexVenta ()
		{
			DetFact = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			sst=0;
		}

				/********************
				* VARIABLES Y OTROS *
				********************/

		public ListStore GetDetFact(){
			return DetFact;
		}

		public void SetDetFact (ListStore ls)
		{
			DetFact = ls;
		}

		public double GetSst ()
		{
			return sst;
		}

		public void SetSst (double m)
		{
			sst=m;
		}

		public void LimpiarDetFact(){
			ListStore Vacio = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			DetFact = Vacio;
			sst=0;
		}

		public void ReducirAcumSubtotal (double Monto)
		{
			sst-=Monto;
		}

				/********************
				* TREEVIEW Y OTROS *
				********************/

		public ListStore LLenarDetFact(Entry entnr,Entry entcod,Entry entcan, string prc)
		{ //Añade un artículo al detalle factura.
			ListStore tdetfactura = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			tdetfactura.AppendValues(entnr.Text,entcod.Text,entcan.Text,prc);
			return tdetfactura;
		}

		public double PrecioServicio(string cod, int tip)
		{ //Devuelve el precio de un servicio dependiendo de si es mayor o detal.
			string precD = "SELECT ServPrecioDetal from tservicios where ServCod='"+cod+"'";
			string precM = "SELECT ServPrecioMayor from tservicios where ServCod='"+cod+"'";
			string prec = "0";
			if (tip == 1) {
				cmd = new MySqlCommand (precD, con);
				try {
					con.Open ();
					read = cmd.ExecuteReader ();
					while (read.Read ()) {
						prec = read ["ServPrecioDetal"].ToString();
					}
				} catch (Exception ex) {
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
					prec="0";
				}finally{
					cmd.Dispose ();
					con.Close ();
				}
			} else{
				cmd = new MySqlCommand (precM, con);
				cmd.CommandType = CommandType.Text;
				try {
					con.Open ();
					read = cmd.ExecuteReader ();
					while (read.Read ()) {
						prec = read ["ServPrecioMayor"].ToString();
					}
				} catch (Exception ex) {
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
					prec = "0";
				}finally{
					cmd.Dispose ();
					con.Close ();
				}
			}
			return Convert.ToDouble(prec);
		}

				/*********************
				* MÉTODOS DE FACTURA *
				*********************/

		public Cliente BuscarCliente(string rif)
		{ //Devuelve un cliente dado.
			Cliente Cli = new Cliente();
			cmd = new MySqlCommand ("Select CliNombre,CliDir,CliTlf from tclientes where CliRIF = @rif and CliEstatus='A'", con);
			cmd.Parameters.AddWithValue ("@rif",rif);
			try {
				con.Open ();
				read = cmd.ExecuteReader ();
				while (read.Read ()) {
					Cli.SetRif (rif);
					Cli.SetNombre ((string)read ["CliNombre"]);
					Cli.SetDireccion((string)read ["CliDir"]);
					Cli.SetTelefono((string)read ["CliTlf"]);
				}
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			}finally{
				cmd.Dispose ();
				con.Close ();
			}
			return Cli;
		}

		public bool BuscarServicio (string cod)
		{ //Verifica la existencia de un servicio dado.
			bool Resultado = false;
			cmd = new MySqlCommand ("select count(ServCod) from tservicios where ServCod=@cod and ServEstatus='A'", con);
			cmd.Parameters.AddWithValue ("@cod", cod);

			try {
				con.Open ();
				if (Convert.ToInt16(cmd.ExecuteScalar ()) == 1)
					Resultado=true;
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}
			return Resultado;
		}

		public void NuevoCodFactura(Entry entCod)
		{ //Genera automáticamente el código de una nueva factura.
			nro="";
			using(con){
				cmd = new MySqlCommand ("SELECT MAX(VenNroFact) from tfactventa", con);
				try {
					con.Open();
					entCod.Text = nro =(Convert.ToInt32(cmd.ExecuteScalar())+1).ToString("D5");
				} catch {
					entCod.Text = nro = 1.ToString("D5");
				} finally {
					cmd.Dispose ();
					con.Close ();
				}
			}
		}

		public void AñadirFactura (string rif, string tp, string fec, string entsub, string entdesc, string entiva )
		{ //Guarda la factura en la base de datos.
			int fa = Convert.ToInt32 (nro);
			string st = entsub.Remove (entsub.Length - 4);
			double sub = Convert.ToDouble (st);
			decimal iva = (Convert.ToDecimal (entiva));
			decimal desc = (Convert.ToDecimal (entdesc));
			using (con) {
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "INSERT INTO tfactventa (VenNroFact,VenRIFCli,VenTipoVenta,VenFechaFact,VenSubTotalFact,VenPorcDesc,VenPorcIVA) values (@nro,@rif,@tip,@fecha,@sub,@desc,@iva)";
				cmd.Parameters.AddWithValue ("@nro", fa);
				cmd.Parameters.AddWithValue ("@rif", rif);
				cmd.Parameters.AddWithValue ("@tip", tp);
				cmd.Parameters.AddWithValue ("@fecha", fec);
				cmd.Parameters.AddWithValue ("@sub", sub);
				cmd.Parameters.AddWithValue ("@desc", desc);
				cmd.Parameters.AddWithValue ("@iva", iva);
				try {
					con.Open();
					cmd.ExecuteNonQuery();
				} catch (Exception ex) {
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
				} finally{
					cmd.Dispose ();
					con.Dispose ();
				}
			}
		}

				/****************************
				* MÉTODOS DE DETALLEFACTURA *
				****************************/

		public void NuevoDetFact(string codS, int cant, Entry entSubT, int tip, TreeView tr1 )
		{ //Método que consulta y almacena un producto para la lista de Detalle Factura
			double prec = PrecioServicio(codS,tip);
			string des="";

			cmd = new MySqlCommand("Select ServDesc from tservicios where ServEstatus='A' and ServCod=@codS",con);
			cmd.Parameters.AddWithValue ("@codS",codS);
			cmd.CommandType = CommandType.Text;
			try {
				con.Open();
				des = cmd.ExecuteScalar().ToString();
				Mensaje ("test",ButtonsType.Ok,MessageType.Info);
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			} finally{
				cmd.Dispose ();
				con.Dispose ();
			}

			double st = cant*prec;
			sst+=st;

			DetFact.AppendValues(codS.ToUpper(),des,cant.ToString(),prec.ToString("N") + " Bs.",st.ToString("N") + " Bs.");
			tr1.Model = DetFact;
			entSubT.Text = sst.ToString ("N")+" Bs.";
		}

		public void AñadirDetFact(string codS,string can,string pre)
		{ //Método que incluye los Detalle Factura en la Base de Datos
			int fa = Convert.ToInt16 (nro);
			int cant = Convert.ToInt16 (can);
			string pc = pre.Remove (pre.Length - 4);
			double prec = Convert.ToDouble (pc);
			using (con) {
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "INSERT INTO tdetfactura (DetNroFactura,DetCodServ,DetCantidad,DetPrecio) values (@detcodf,@detcods,@detcan,@detprec)";
				cmd.Parameters.AddWithValue ("@detcodf", fa);
				cmd.Parameters.AddWithValue ("@detcods", codS);
				cmd.Parameters.AddWithValue ("@detcan", cant);
				cmd.Parameters.AddWithValue ("@detprec", prec);
				try {
					con.Open ();
					cmd.ExecuteNonQuery ();
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Dispose ();
					cmd.Parameters.Clear ();
				}
			}
		}
	}
}