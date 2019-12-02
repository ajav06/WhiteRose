using System;
using Gtk;
using System.Timers;

namespace WhiteRose
{
	public partial class VntLogin : Validaciones
	{
		Timer ValidarBotones;
		ConexLogin cod;

		/***************************
		* CONSTRUCTOR Y DESTRUCTOR *
		****************************/

		public VntLogin () 
		{
			this.Build ();
			ColorearControles();
			cod = new ConexLogin();
			LblFechaHora.Text = DateTime.Now.ToString ();
			new TmrFechaHora (ref LblFechaHora);
			ValidarBotones = new Timer ();
			ValidarBotones.Elapsed += new ElapsedEventHandler (OnValidarBotonesElapsed);
			ValidarBotones.Interval = 100;
			ValidarBotones.Enabled = true;
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			if (cod.Mensaje ("¿Desea salir del sistema?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes)
				Application.Quit ();
		}

		/****************************************
		* MÉTODOS DE LAS SEÑALES DE LOS BOTONES *
		*****************************************/

		protected void OnBtnAccederClicked (object sender, EventArgs e)
		{
			int r = cod.IngresarSistema (EntUsuario.Text, EntContrasena.Text);
			if (r == 1) {
				cod.Mensaje ("¡Bienvenido al sistema!",ButtonsType.Ok,MessageType.Info);
				string c="", n="", a="", ce="", fi="";
				int ti=0;
				cod.ConstruirUsuario(EntUsuario.Text, EntContrasena.Text, ref c, ref n, ref a, ref ce, ref fi, ref ti);
				Usuario u = new Usuario(c,n,a,ce,fi,ti);
				VntMenuPrincipal Menu = new VntMenuPrincipal (u);
				Menu.Show ();
				this.Destroy ();
			} else if (r == 2) {
				cod.Mensaje ("Contraseña incorrecta.", ButtonsType.Ok, MessageType.Error);
			} else if (r == 3) {
				cod.Mensaje ("Usuario no encontrado.", ButtonsType.Ok, MessageType.Error);
			} else {
				cod.Mensaje ("Error desconocido.",ButtonsType.Ok,MessageType.Error);
			}
		}

		protected void OnBtnSalirClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea salir del sistema?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) 
				Application.Quit ();
		}

		protected void OnBtnOlvideMiContrasenaClicked (object sender, EventArgs e)
		{
			if (cod.IngresarSistema (EntUsuario.Text,EntContrasena.Text)==2)
				cod.Mensaje ("La pista de su usuario es: \n '"+cod.PistaUsuario (EntUsuario.Text,EntContrasena.Text)+"'",ButtonsType.Ok,MessageType.Info);
			else
				cod.Mensaje ("En caso de no poder acceder al sistema, contacte al administrador del mismo.",ButtonsType.Ok,MessageType.Info);
		}

		/***********************************
		* VALIDACIÓN DEL BOTÓN DE INGRESAR *
		*           Y DE LOS ENTRY         *
		************************************/

		protected void OnValidarBotonesElapsed(object sender, EventArgs e)
		{	
			if ((EntUsuario.Text != "") && (EntContrasena.Text != "")) {
				BtnAcceder.Sensitive = true;
			} else {
				BtnAcceder.Sensitive = false;
			}
		}

		protected void OnEntUsuarioChanged (object sender, EventArgs e)
		{
			ValidarAlfanumerico(EntUsuario);
		}

		#region Coloreado de los Controles
		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			this.ModifyBg (StateType.Normal, Est.AliceBlue);
			Est.EstilizarBoton (BtnAcceder, Est.Aquamarine, 14);
			Est.EstilizarLabel (LblIngreso, Est.SmokyBlack);
			Est.EstilizarLabel (LblUsuario, Est.SmokyBlack);
			Est.EstilizarLabel (LblContrasena, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora, Est.SmokyBlack);
			Est.EstilizarBoton (BtnOlvideMiContrasena, Est.MagicMint, 14);
			Est.EstilizarBoton (BtnSalir, Est.SalmonPink, 14);
			Est.EstilizarFrame (frame3, Est.ElectricBlue);
			Est.EstilizarFrame (frame4, Est.ElectricBlue);
			Est.EstilizarEntry (EntUsuario, Est.SalmonPink);
			Est.EstilizarEntry (EntContrasena, Est.SalmonPink);
		}
		#endregion
	}
}
