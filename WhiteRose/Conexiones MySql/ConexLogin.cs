using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Data;

namespace WhiteRose
{
	public class ConexLogin : ConexBase
	{
		public ConexLogin ()
		{
		}

		public int IngresarSistema (string usuario, string Contrasena)
		{ //Ingreso al sistema codificado, devuelve un número dependiendo del estado del intento de login.
		  //Devuelve 1 si el login fue exitoso, 2 si la contraseña fue incorrecta, 3 si el usuario no existe.
			int r = 0;
			cmd = new MySqlCommand ("select count(UsuUsuario) from tusuarios where UsuUsuario=@user and UsuContraseña=@pwd and UsuEstatus='A'", con);
			cmd.Parameters.AddWithValue ("@user", usuario);
			cmd.Parameters.AddWithValue ("@pwd", Contrasena);


			try {
				con.Open ();

				if (Convert.ToInt16 (cmd.ExecuteScalar ()) == 1) {
					r = 1;
				}
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			} 

			if (r == 0) {
				cmd = new MySqlCommand ("select count(UsuUsuario) from tusuarios where UsuUsuario=@user and UsuEstatus='A'", con);
				cmd.Parameters.AddWithValue ("@user", usuario);

				try {
					con.Open ();
					if (Convert.ToInt16 (cmd.ExecuteScalar ()) == 1) {
						r = 2;
					} else {
						r = 3;
					}
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Close ();
				}
			}
			return r;
		}

		public string PistaUsuario (string usuario, string contrasena)
		{ //Método que devuelve la pista del usuario.
			string pista = "";
			cmd = new MySqlCommand ("select UsuPista from tusuarios where UsuUsuario=@user and UsuEstatus='A'", con);
			cmd.Parameters.AddWithValue ("@user", usuario);

			try {
				con.Open ();
				pista = cmd.ExecuteScalar ().ToString ();
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}
			return pista;
		}

		public void ConstruirUsuario (string usuario, string contrasena, ref string c, ref string n, ref string a, ref string ce, ref string fi, ref int ti)
		{ //Método que extrae todos los datos de un usuario de la base de datos
		  //y los vuelca al exterior para construir un objeto de la clase Usuario.
			cmd = new MySqlCommand ("select UsuCodigo, UsuNombre, UsuApellido, UsuCedula, UsuFechaIng, UsuTipoUsuario from tusuarios where UsuUsuario=@user and UsuContraseña=@pwd and UsuEstatus='A'", con);
			cmd.Parameters.AddWithValue ("@user",usuario);
			cmd.Parameters.AddWithValue ("@pwd",contrasena);
			cmd.CommandType = CommandType.Text;
			try {
				con.Open ();
				read = cmd.ExecuteReader ();
				while (read.Read ()){
					c=read ["UsuCodigo"].ToString ();
					n=read ["UsuNombre"].ToString ();
					a=read ["UsuApellido"].ToString ();
					ce=read ["UsuCedula"].ToString ();
					fi=read ["UsuFechaIng"].ToString ();
					ti=Convert.ToInt16 (read["UsuTipoUsuario"]);
				}
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}
		}
	}
}

