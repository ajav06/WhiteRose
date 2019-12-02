using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public class ConexBase: Gtk.Window
	{	
		protected Estilo Est = new Estilo();
		protected MySqlConnection con;
		protected MySqlCommand cmd;
		protected MySqlDataReader read;

		public ConexBase (): base (Gtk.WindowType.Toplevel)
		{
			con = new MySqlConnection ("Server=localhost;Database=white_rose;UserId=root;password=060998");
		}

		public ResponseType Mensaje(string Msj, ButtonsType Tb, MessageType Tm)
		{
			MessageDialog Dialogo = new MessageDialog (this, DialogFlags.DestroyWithParent, Tm, Tb, Msj);
			Dialogo.ModifyBg (StateType.Normal,Est.LigthSteelBlue);
			ResponseType Respuesta = (ResponseType)Dialogo.Run ();
			Dialogo.Destroy ();
			return Respuesta;
		}
	}
}

