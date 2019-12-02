using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Data;

namespace WhiteRose
{
	public class ConexConsultaFactura : ConexBase
	{
		

		public ConexConsultaFactura ()
		{
		}

		public FacturaVenta DevolverFactura (string cod)
		{ //Método que devuelve la factura buscada.
			FacturaVenta fv = new FacturaVenta ();
			Cliente cli = new Cliente ();
			if (ExistenciaFactura (cod)) {
				cmd = new MySqlCommand ("select VenRIFCli, VenTipoVenta, VenFechaFact, VenSubTotalFact, VenPorcDesc, VenPorcIVA from tfactventa where VenNroFact = @nrof and VenEstatus='A'", con);
				cmd.Parameters.AddWithValue ("@nrof", cod);

				try {
					con.Open ();
					read = cmd.ExecuteReader ();
					while (read.Read ()) {
						fv.SetNroFact (cod);
						cli.SetRif (read [0].ToString ());
						fv.SetTipoV (Convert.ToInt16 (read [1]));
						fv.SetFechaFact (read[2].ToString ());
						fv.SetSubTotal (Convert.ToDouble (read [3]));
						fv.SetPorcDesc (Convert.ToDouble (read [4]));
						fv.SetPorcIva (Convert.ToDouble (read [5]));
					}
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Close ();
				}

				cmd = new MySqlCommand ("select * from tclientes where CliRIF=@rif", con);
				cmd.Parameters.AddWithValue ("@rif", cli.GetRif ());

				try {
					con.Open ();
					read = cmd.ExecuteReader ();
					while (read.Read ()){
						cli.SetNombre (read[1].ToString ());
						cli.SetDireccion (read[2].ToString ());
						cli.SetTelefono (read[3].ToString ());
					}
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Close ();
				}
				fv.SetCliente (cli);
			} 
			return fv;
		}

		public ListStore DevolverDetFactura (string cod)
		{ //Método que devuelve el detalle de la factura buscada.
			ListStore ls = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			cmd = new MySqlCommand ("select DetCodServ, DetCantidad, DetPrecio, ServDesc from tdetfactura,tservicios where DetNroFactura = @nro and ServCod=DetCodServ and DetEstatus = 'A'", con);
			cmd.Parameters.AddWithValue ("@nro",cod);

			try {
				con.Open ();
				read = cmd.ExecuteReader ();
				while (read.Read ()){
					double st = (Convert.ToDouble (read[1])*Convert.ToDouble (read[2]));
					ls.AppendValues(read [0].ToString(),read[3].ToString (),read[1].ToString (),read.GetDecimal(read.GetOrdinal("DetPrecio")).ToString("N")+ " Bs.",st.ToString ("N") + " Bs.");
				}
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}
			return ls;
		}

		public bool ExistenciaFactura (string cod)
		{ //Método que determina si una factura existe o no.
			cmd = new MySqlCommand ("select count(VenNroFact) from tfactventa where VenNroFact=@nrof and VenEstatus='A'", con);
			cmd.Parameters.AddWithValue ("@nrof", cod);

			try {
				con.Open ();
				if (Convert.ToInt16(cmd.ExecuteScalar ())==1)
					return true;
				else
					return false;
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}
			return false;
		}
	}
}

