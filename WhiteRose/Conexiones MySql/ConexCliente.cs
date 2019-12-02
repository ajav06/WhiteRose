using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public class ConexCliente: ConexBase
	{
		public ConexCliente ()
		{

		}

		public ListStore CargarClientes()
		{ //Método que carga todos los clientes en un ListStore
			ListStore tclientes = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			cmd = new MySqlCommand("Select CliRIF,CliNombre,CliDir,CliTlf from tclientes where CliEstatus='A'",con);
			try {
				con.Open();
				read = cmd.ExecuteReader();
				while (read.Read()) {
					tclientes.AppendValues((string) read ["CliRIF"],(string) read ["CliNombre"],(string) read ["CliDir"],(string) read ["CliTlf"]);
				}
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			} finally{
				cmd.Dispose ();
				con.Close ();
			}
			return tclientes;
		}

		public int VerificarExistenciaCliente (string rif)
		{ //Método codificado que verifica si un cliente existe y si este fue eliminado o no.
		  //Devuelve entero, 0 si no existe, 1 si existe y fue eliminado, 2 si existe y está activo
			int n = 0;
			char e = '\n';

			cmd = new MySqlCommand ("select count(CliRif) from tclientes where CliRif=@rif", con);
			cmd.Parameters.AddWithValue ("@rif", rif);
			cmd.CommandType = CommandType.Text;
			try {
				con.Open ();
				n = Convert.ToInt16 (cmd.ExecuteScalar ());
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}

			if (n == 1) {
				cmd = new MySqlCommand ("select CliEstatus from tclientes where CliRif=@rif", con);
				cmd.Parameters.AddWithValue ("@rif", rif);
				cmd.CommandType = CommandType.Text;
				try {
					con.Open ();
					e = Convert.ToChar(cmd.ExecuteScalar ());
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Close ();
				}
			}

			if (e=='A')
				n++;

			return n;
		}

		public ListStore BuscarUsuario (string rif)
		{ //Método que busca un cliente con un rif similar al dado.
		  //Devuelve un listado con los clientes que cumplen esa condición.
			ListStore tclientes = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			using (con) {
				cmd = new MySqlCommand ("Select CliRIF,CliNombre,CliDir,CliTlf from tclientes where CliRIF LIKE'" + rif + "%' and CliEstatus = 'A'", con);
				cmd.CommandType = CommandType.Text;
				try {
					con.Open ();
					read = cmd.ExecuteReader ();
					while (read.Read ()) {
						tclientes.AppendValues ((string)read ["CliRIF"], (string)read ["CliNombre"], (string)read ["CliDir"], (string)read ["CliTlf"]);
					}
					read.Close ();
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Close ();
				}
				return tclientes;
			}
		}

		public void NuevoCliente (Cliente cli)
		{ //Método que ingresa un nuevo cliente a la base de datos.
			using (con) {
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "INSERT INTO tclientes (CliRIF,CliNombre,CliDir,CliTlf) values (@clirif,@clinom,@clidir,@clitlf)";
				cmd.Parameters.AddWithValue ("@clirif", cli.GetRif ());
				cmd.Parameters.AddWithValue ("@clinom", cli.GetNombre ());
				cmd.Parameters.AddWithValue ("@clidir", cli.GetDireccion ());
				cmd.Parameters.AddWithValue ("@clitlf", cli.GetTelefono ());
				try {
					con.Open ();
					cmd.ExecuteNonQuery ();
					Mensaje ("",ButtonsType.Ok,MessageType.Info);
				} catch (Exception ex) {
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
					return;
				} finally {
					cmd.Parameters.Clear ();
					cmd.Dispose ();
					con.Close ();
				}
			}
		}

		public void ModificarCliente(Cliente cli)
		{ //Método que modifica un cliente en la base de datos.
			cmd = new MySqlCommand ("UPDATE tclientes SET CliNombre = ?nom, CliDir = ?dir, CliTlf=?tlf where CliRIF = ?rif", con);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue ("?nom",cli.GetNombre ());
			cmd.Parameters.AddWithValue ("?dir",cli.GetDireccion ());
			cmd.Parameters.AddWithValue ("?tlf",cli.GetTelefono ());
			cmd.Parameters.AddWithValue ("?rif",cli.GetRif ());
			try {
				con.Open();
				cmd.ExecuteNonQuery();
				Mensaje ("Modificación realizada con éxito.",ButtonsType.Ok,MessageType.Info);
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
				return;
			} finally{
				cmd.Parameters.Clear ();
				cmd.Dispose ();
				con.Close ();
			}
		}

		public void EliminacionLogicaCliente(string rif)
		{ //Método que elimina un cliente mediante el Estatus.
			cmd = new MySqlCommand ("UPDATE tclientes SET CliEstatus = 'E' where CliRIF=@rif", con);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue ("@rif",rif);
			try {
				con.Open();
				cmd.ExecuteNonQuery();
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			} finally{
				cmd.Dispose ();
				con.Close ();
			}
		}


	}
}

