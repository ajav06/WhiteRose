using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public class ConexUsuario : ConexBase
	{
		public ConexUsuario ()
		{

		}

		public int ExistenciaUsuario (string usu)
		{ //Verifica si un usuario existe o no, devolviendo un código.
		  //0 = no existe, 1 = existe, 2 = fue eliminado.
			int n = 0;
			char e = '\n';
			cmd = new MySqlCommand ("select count(UsuCodigo) from tusuarios where UsuUsuario = @usu", con);
			cmd.Parameters.AddWithValue ("@usu", usu);

			try {
				con.Open ();
				n = Convert.ToInt16(cmd.ExecuteScalar ());
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}

			if (n == 1) {
				cmd = new MySqlCommand("select UsuEstatus from tusuarios where UsuUsuario = @usu",con);
				cmd.Parameters.AddWithValue ("@usu",usu);

				try {
					con.Open ();
					e = Convert.ToChar(cmd.ExecuteScalar ());
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Close ();
				}

				if (e=='I')
					n++;
			}

			return n;
		}

		public void NuevoCod(Entry entCod, string Cod)
		{ //Genera un nuevo código de usuario para poder incluir un usuario nuevo sin problemas.
			cmd = new MySqlCommand ("SELECT MAX(UsuCodigo) from tusuarios where UsuCodigo LIKE'" + Cod + "%'", con);
			cmd.CommandType = CommandType.Text;
			try {
				con.Open();
				entCod.Text =Cod+((Convert.ToInt16 (cmd.ExecuteScalar().ToString().Substring(1,4)))+1).ToString("D4");
			} catch {
				entCod.Text = Cod+1.ToString("D4");
			} finally {
				cmd.Dispose ();
				con.Close();
			}
		}

		public ListStore CargarUsuarios()
		{ //Carga el listado actual con todos los usuarios.
			ListStore tusuarios = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string),typeof(string), typeof(string), typeof(string), typeof(string),typeof(string), typeof(string),typeof(string));
			cmd = new MySqlCommand ("Select UsuCodigo,UsuUsuario,UsuContraseña,UsuNombre,UsuApellido,UsuCedula,UsuFechaIng,UsuCodUsuarioAnadido,UsuTipoUsuario,UsuCorreo,UsuPista from tusuarios where UsuEstatus='A'", con);
			try {
				con.Open();
				read = cmd.ExecuteReader();
				while (read.Read()) {
					tusuarios.AppendValues((string) read [0].ToString(),(string) read[1].ToString(),(string) read[2].ToString (),(string) read[3].ToString(),(string) read[4].ToString(),(string) read[5].ToString(),Convert.ToDateTime(read[6]).ToString("d"),(string) read[7].ToString(),(string) read[8].ToString(),(string) read[9].ToString(),(string) read[10].ToString());
				}
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
			} finally{
				cmd.Dispose ();
				con.Close ();
			}
			return tusuarios;
		}

		public void NuevoUsuario (string entcod,string entusu, string entco, string entnom,string entape,string entced,string entfech,string entusuan,int ttp,string entcorr,string entpis)
		{ //Ingresa un nuevo usuario en la base de datos.
			using (con) {
					cmd.Connection = con;
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "INSERT INTO tusuarios (UsuCodigo,UsuUsuario,UsuContraseña,UsuNombre,UsuApellido,UsuCedula,UsuFechaIng,UsuCodUSuarioAnadido,UsuTipoUsuario,UsuCorreo,UsuPista,UsuEstatus) values(@usuCod,@usuUsu,@usuCon,@usuNom,@usuApe,@UsuCed,@UsuFech,@UsuCodUSua,@UsuTip,@UsuCor,@usuPis,@UsuEstatus)";
					cmd.Parameters.AddWithValue ("@usuCod",entcod);
					cmd.Parameters.AddWithValue ("@usuUsu",entusu);
					cmd.Parameters.AddWithValue ("@usuCon",entco);
					cmd.Parameters.AddWithValue ("@usuNom",entnom);
					cmd.Parameters.AddWithValue ("@usuApe",entape);
					cmd.Parameters.AddWithValue ("@usuCed",entced);
					cmd.Parameters.AddWithValue ("@usuFech",Convert.ToDateTime(entfech));
					cmd.Parameters.AddWithValue ("@usuCodUsua",entusuan);
					cmd.Parameters.AddWithValue ("@usuTip",ttp);
					cmd.Parameters.AddWithValue ("@usuCor",entcorr);
					cmd.Parameters.AddWithValue ("@usuPis",entpis);
					cmd.Parameters.AddWithValue ("@UsuEstatus","A");
					try {
						con.Open();
						cmd.ExecuteNonQuery ();
					} catch (Exception ex) {
						Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
					} finally {
						cmd.Parameters.Clear ();
						cmd.Dispose ();
						con.Close ();
					}
			}
		}

		public void EliminacionLogicaUsuario(string cod)
		{ //Elimina un usuario mediante el estatus.
				cmd = new MySqlCommand ("UPDATE tusuarios SET UsuEstatus = 'I' where UsuCodigo = @cod", con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue ("@cod",cod);
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

		public void ActualizarUsuario(string cod, string usu, string co, string nom, string ape, string ced, string corr,string pis)
		{ //Actualiza un usuario.
				cmd = new MySqlCommand ("UPDATE tusuarios SET UsuUsuario=?Usuario, UsuContraseña=?Contrasena, UsuNombre=?Nombre, UsuApellido=?Ape, UsuCedula=?Ced, UsuCorreo=?Corr, UsuPista=?Pis where UsuCodigo = @cod and UsuEstatus='A'",con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue ("@cod",cod);
				cmd.Parameters.AddWithValue ("?Usuario",usu);
				cmd.Parameters.AddWithValue ("?Contrasena",co);
				cmd.Parameters.AddWithValue ("?Nombre",nom);
				cmd.Parameters.AddWithValue ("?Ape",ape);
				cmd.Parameters.AddWithValue ("?Ced",ced);
				cmd.Parameters.AddWithValue ("?Corr",corr);
				cmd.Parameters.AddWithValue ("?Pis",pis);
				try {
					con.Open();
					cmd.ExecuteNonQuery();
				} catch (Exception ex) {
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
				} finally{
					cmd.Parameters.Clear ();
					cmd.Dispose ();
					con.Close ();
			}
		}
	}
}