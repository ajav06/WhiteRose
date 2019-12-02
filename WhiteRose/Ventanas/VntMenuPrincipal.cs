using System;
using Gtk;
using System.Timers;

namespace WhiteRose
{
	public partial class VntMenuPrincipal : Gtk.Window
	{
		ConexBase cod;
		Usuario usuario;

		public VntMenuPrincipal (Usuario u) :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			ColorearControles();
			string path = Environment.CurrentDirectory + "/panda.png";
			//ImgLogo.Pixbuf = new Gdk.Pixbuf(path);
			LblFechaHora.Text = DateTime.Now.ToString ();
			new TmrFechaHora(ref LblFechaHora);
			cod = new ConexBase();
			usuario = u;
			LblBienvenida.Text = "¡Bienvenido, " + usuario.GetNombre () + "!";
			ValidarBotonesUsuario();
			LblBienvenida.ChildFocus (DirectionType.Up);
		}

		#region Coloreado de los Controles

		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			this.ModifyBg (StateType.Normal, Est.Snow);
			Est.EstilizarLabel (LblBienvenida, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora, Est.SmokyBlack);
			Est.EstilizarLabel (LblMenuPpal, Est.SmokyBlack);
			Est.EstilizarBoton (BtnActualizarClientes, Est.Iceberg, 14);
			Est.EstilizarBoton (BtnProcesarVenta, Est.Iceberg, 14);
			Est.EstilizarBoton (BtnActualizarServicios, Est.Iceberg, 14);
			Est.EstilizarBoton (BtnActualizarUsuario, Est.Iceberg, 14);
			Est.EstilizarBoton (BtnConsultarFactura, Est.Iceberg, 14);
			Est.EstilizarBoton (BtnReportes, Est.Iceberg, 14);
			Est.EstilizarBoton (BtnCerrarSesion, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnSalir, Est.SalmonPink, 14);
			Est.EstilizarBoton (BtnAcercaDe,Est.AliceBlue,14);
			Est.EstilizarFrame (frame1, Est.NonPhotoBlue);
			Est.EstilizarFrame (frame3, Est.NonPhotoBlue);
		}

		#endregion

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			if (cod.Mensaje ("¿Desea salir del sistema?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) 
				Application.Quit ();
		}

		protected void OnBtnSalirClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea salir del sistema?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) 
				Application.Quit ();
		}

		protected void OnBtnActualizarClientesClicked (object sender, EventArgs e)
		{
			VntActualizarCliente Actualizar;
			Actualizar = new VntActualizarCliente ();
			Actualizar.Show ();
		}

		protected void OnBtnProcesarVentaClicked (object sender, EventArgs e)
		{
			VntProcesarVentas Procesar;
			Procesar = new VntProcesarVentas();
			Procesar.Show ();
		}

		protected void OnBtnCerrarSesionClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Está seguro de cerrar sesión?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
				VntLogin Login = new VntLogin();
				Login.Show ();
				this.Destroy ();
			}
		}

		protected void ValidarBotonesUsuario ()
		{
			if (usuario.GetTipoU () == 1) {
				BtnActualizarClientes.Sensitive = true;
				BtnProcesarVenta.Sensitive = true;
				BtnActualizarServicios.Sensitive=true;
				BtnConsultarFactura.Sensitive=true;
				BtnActualizarUsuario.Visible=true;
				BtnActualizarUsuario.Sensitive=true;
				BtnReportes.Sensitive = true;
			} else if (usuario.GetTipoU () == 2) {
				BtnActualizarClientes.Sensitive = true;
				BtnProcesarVenta.Sensitive = true;
				BtnConsultarFactura.Sensitive=true;
			} else if (usuario.GetTipoU () == 3) {
				BtnActualizarClientes.Sensitive = true;
			}
		}

		protected void OnBtnConsultarFacturaClicked (object sender, EventArgs e)
		{
			VntConsultarFactura Consulta = new VntConsultarFactura();
			Consulta.Show ();
		}

		protected void OnBtnActualizarUsuarioClicked (object sender, EventArgs e)
		{
			VntActualizarUsuarios ActUsuario = new VntActualizarUsuarios(usuario);
			ActUsuario.Show ();
		}

		protected void OnBtnActualizarServiciosClicked (object sender, EventArgs e)
		{
			VntActualizarServicios ActServicios = new VntActualizarServicios();
			ActServicios.Show ();
		}

		protected void OnBtnReportesClicked (object sender, EventArgs e)
		{
			VntReportesVentas ReportVta = new VntReportesVentas ();
			ReportVta.Show ();
		}

		protected void OnBtnAcercaDeClicked (object sender, EventArgs e)
		{
			Estilo est = new Estilo();
			AboutDialog AcercaDe = new AboutDialog ();
			AcercaDe.ProgramName = "WhiteRose";
			AcercaDe.Version = "1.0";
			AcercaDe.Copyright = "(c) EcoTech Team - 2017"; 
			AcercaDe.Website = "http://www.imdb.com/title/tt4158110/?ref_=ttep_ep_tt";
			AcercaDe.WebsiteLabel = "Mr. Robot";
			AcercaDe.Authors = new string[] {"Albert Acevedo C.I. 26710983","Gabriel Roa C.I. 25919459","Marielba Maldonado C.I. 26710983","Rubén Gutiérrez C.I. 25147289","Sección 1 - Programación II"};
			AcercaDe.ModifyBg (StateType.Normal,est.LigthSteelBlue);
			AcercaDe.Comments = "Sistema de Facturación para la empresa Mr. Robot \n Programado como proyecto final de la asignatura Programación II \n Lapso 2017-1 - Barquisimeto, diciembre del 2017";
			AcercaDe.Run ();
			AcercaDe.Destroy ();
		}
	}
}

