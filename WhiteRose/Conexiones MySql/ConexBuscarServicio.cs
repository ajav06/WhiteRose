using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public class ConexBuscarServicio : ConexBase
	{
		public ListStore Servicios;

		public ConexBuscarServicio ()
		{
			Servicios = CargarServicios();
		}

		public ListStore CargarServicios()
		{ //Carga los servicios que están registrados en la base de datos.
			ListStore tservicios = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			cmd = new MySqlCommand("Select ServCod, ServDesc, ServPrecioDetal, ServPrecioMayor from tservicios where ServEstatus='A'",con);

			try {
				con.Open();
				read = cmd.ExecuteReader();
				while (read.Read()) {
					tservicios.AppendValues((string) read ["ServCod"],(string) read ["ServDesc"],read ["ServPrecioDetal"].ToString (),read ["ServPrecioMayor"].ToString ());
				}
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			} finally{
				cmd.Dispose ();
				con.Dispose ();
			}
			return tservicios;
		}

		public ListStore BuscarServicio(string codigo, string desc)
		{ //Busca servicios similares a un código y/o una descripción dados.
			ListStore tservicios = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			using (con) {
				cmd = new MySqlCommand("Select ServCod, ServDesc, ServPrecioDetal, ServPrecioMayor from tservicios where ServCod like '%"+codigo+"%' and ServDesc like '%"+desc+"%' and ServEstatus='A'",con);
				cmd.CommandType = CommandType.Text;
				try {
					con.Open();
					read = cmd.ExecuteReader();
					while (read.Read()) {
						tservicios.AppendValues((string) read [0],(string) read [1],read [2].ToString (),read [3].ToString ());
					}
					read.Close();
				} catch (Exception ex) {
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
				}
				return tservicios;
			}
		}
	}
}

