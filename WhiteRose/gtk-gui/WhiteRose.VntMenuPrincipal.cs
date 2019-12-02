
// This file has been generated by the GUI designer. Do not modify.
namespace WhiteRose
{
	public partial class VntMenuPrincipal
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.Image ImgLogo;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Label LblBienvenida;
		
		private global::Gtk.Frame frame1;
		
		private global::Gtk.Alignment GtkAlignment;
		
		private global::Gtk.Table table1;
		
		private global::Gtk.Button BtnAcercaDe;
		
		private global::Gtk.Button BtnActualizarClientes;
		
		private global::Gtk.Button BtnActualizarServicios;
		
		private global::Gtk.Button BtnActualizarUsuario;
		
		private global::Gtk.Button BtnCerrarSesion;
		
		private global::Gtk.Button BtnConsultarFactura;
		
		private global::Gtk.Button BtnProcesarVenta;
		
		private global::Gtk.Button BtnReportes;
		
		private global::Gtk.Button BtnSalir;
		
		private global::Gtk.Label LblMenuPpal;
		
		private global::Gtk.VBox vbox6;
		
		private global::Gtk.HSeparator hseparator2;
		
		private global::Gtk.HBox hbox2;
		
		private global::Gtk.Frame frame3;
		
		private global::Gtk.Alignment GtkAlignment1;
		
		private global::Gtk.Label LblFechaHora;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget WhiteRose.VntMenuPrincipal
			this.Name = "WhiteRose.VntMenuPrincipal";
			this.Title = global::Mono.Unix.Catalog.GetString ("WhiteRose");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Container child WhiteRose.VntMenuPrincipal.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			this.vbox1.BorderWidth = ((uint)(12));
			// Container child vbox1.Gtk.Box+BoxChild
			this.ImgLogo = new global::Gtk.Image ();
			this.ImgLogo.Name = "ImgLogo";
			this.vbox1.Add (this.ImgLogo);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.ImgLogo]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.LblBienvenida = new global::Gtk.Label ();
			this.LblBienvenida.Name = "LblBienvenida";
			this.LblBienvenida.LabelProp = global::Mono.Unix.Catalog.GetString ("¡Bienvenido, Usuario!");
			this.LblBienvenida.Justify = ((global::Gtk.Justification)(2));
			this.hbox1.Add (this.LblBienvenida);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.LblBienvenida]));
			w2.Position = 0;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.BorderWidth = ((uint)(1));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			this.GtkAlignment.BorderWidth = ((uint)(6));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(9)), ((uint)(1)), true);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(10));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.BtnAcercaDe = new global::Gtk.Button ();
			this.BtnAcercaDe.CanFocus = true;
			this.BtnAcercaDe.Name = "BtnAcercaDe";
			this.BtnAcercaDe.UseUnderline = true;
			this.BtnAcercaDe.Label = global::Mono.Unix.Catalog.GetString ("Acerca De");
			this.table1.Add (this.BtnAcercaDe);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.BtnAcercaDe]));
			w4.TopAttach = ((uint)(6));
			w4.BottomAttach = ((uint)(7));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnActualizarClientes = new global::Gtk.Button ();
			this.BtnActualizarClientes.HeightRequest = 40;
			this.BtnActualizarClientes.Sensitive = false;
			this.BtnActualizarClientes.CanFocus = true;
			this.BtnActualizarClientes.Name = "BtnActualizarClientes";
			this.BtnActualizarClientes.UseUnderline = true;
			this.BtnActualizarClientes.Label = global::Mono.Unix.Catalog.GetString ("Actualizar Clientes");
			this.table1.Add (this.BtnActualizarClientes);
			// Container child table1.Gtk.Table+TableChild
			this.BtnActualizarServicios = new global::Gtk.Button ();
			this.BtnActualizarServicios.Sensitive = false;
			this.BtnActualizarServicios.CanFocus = true;
			this.BtnActualizarServicios.Name = "BtnActualizarServicios";
			this.BtnActualizarServicios.UseUnderline = true;
			this.BtnActualizarServicios.Label = global::Mono.Unix.Catalog.GetString ("Actualizar Servicios");
			this.table1.Add (this.BtnActualizarServicios);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.BtnActualizarServicios]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnActualizarUsuario = new global::Gtk.Button ();
			this.BtnActualizarUsuario.Sensitive = false;
			this.BtnActualizarUsuario.CanFocus = true;
			this.BtnActualizarUsuario.Name = "BtnActualizarUsuario";
			this.BtnActualizarUsuario.UseUnderline = true;
			this.BtnActualizarUsuario.Label = global::Mono.Unix.Catalog.GetString ("Actualizar Usuarios");
			this.table1.Add (this.BtnActualizarUsuario);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.BtnActualizarUsuario]));
			w7.TopAttach = ((uint)(2));
			w7.BottomAttach = ((uint)(3));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnCerrarSesion = new global::Gtk.Button ();
			this.BtnCerrarSesion.CanFocus = true;
			this.BtnCerrarSesion.Name = "BtnCerrarSesion";
			this.BtnCerrarSesion.UseUnderline = true;
			this.BtnCerrarSesion.Label = global::Mono.Unix.Catalog.GetString ("Cerrar Sesión");
			this.table1.Add (this.BtnCerrarSesion);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.BtnCerrarSesion]));
			w8.TopAttach = ((uint)(7));
			w8.BottomAttach = ((uint)(8));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnConsultarFactura = new global::Gtk.Button ();
			this.BtnConsultarFactura.Sensitive = false;
			this.BtnConsultarFactura.CanFocus = true;
			this.BtnConsultarFactura.Name = "BtnConsultarFactura";
			this.BtnConsultarFactura.UseUnderline = true;
			this.BtnConsultarFactura.Label = global::Mono.Unix.Catalog.GetString ("Consultar Factura");
			this.table1.Add (this.BtnConsultarFactura);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.BtnConsultarFactura]));
			w9.TopAttach = ((uint)(4));
			w9.BottomAttach = ((uint)(5));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnProcesarVenta = new global::Gtk.Button ();
			this.BtnProcesarVenta.Sensitive = false;
			this.BtnProcesarVenta.CanFocus = true;
			this.BtnProcesarVenta.Name = "BtnProcesarVenta";
			this.BtnProcesarVenta.UseUnderline = true;
			this.BtnProcesarVenta.Label = global::Mono.Unix.Catalog.GetString ("Procesar Ventas");
			this.table1.Add (this.BtnProcesarVenta);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1 [this.BtnProcesarVenta]));
			w10.TopAttach = ((uint)(3));
			w10.BottomAttach = ((uint)(4));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnReportes = new global::Gtk.Button ();
			this.BtnReportes.Sensitive = false;
			this.BtnReportes.CanFocus = true;
			this.BtnReportes.Name = "BtnReportes";
			this.BtnReportes.UseUnderline = true;
			this.BtnReportes.Label = global::Mono.Unix.Catalog.GetString ("Reportes de Ventas");
			this.table1.Add (this.BtnReportes);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1 [this.BtnReportes]));
			w11.TopAttach = ((uint)(5));
			w11.BottomAttach = ((uint)(6));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnSalir = new global::Gtk.Button ();
			this.BtnSalir.CanFocus = true;
			this.BtnSalir.Name = "BtnSalir";
			this.BtnSalir.UseUnderline = true;
			this.BtnSalir.Label = global::Mono.Unix.Catalog.GetString ("Salir");
			this.table1.Add (this.BtnSalir);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1 [this.BtnSalir]));
			w12.TopAttach = ((uint)(8));
			w12.BottomAttach = ((uint)(9));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			this.GtkAlignment.Add (this.table1);
			this.frame1.Add (this.GtkAlignment);
			this.LblMenuPpal = new global::Gtk.Label ();
			this.LblMenuPpal.Name = "LblMenuPpal";
			this.LblMenuPpal.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Menú Principal</b>");
			this.LblMenuPpal.UseMarkup = true;
			this.frame1.LabelWidget = this.LblMenuPpal;
			this.vbox2.Add (this.frame1);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.frame1]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			this.vbox1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.vbox2]));
			w16.Position = 1;
			w16.Expand = false;
			w16.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.vbox6 = new global::Gtk.VBox ();
			this.vbox6.Name = "vbox6";
			this.vbox6.Spacing = 6;
			// Container child vbox6.Gtk.Box+BoxChild
			this.hseparator2 = new global::Gtk.HSeparator ();
			this.hseparator2.Name = "hseparator2";
			this.vbox6.Add (this.hseparator2);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.hseparator2]));
			w17.Position = 0;
			w17.Expand = false;
			w17.Fill = false;
			// Container child vbox6.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.frame3 = new global::Gtk.Frame ();
			this.frame3.Name = "frame3";
			this.frame3.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child frame3.Gtk.Container+ContainerChild
			this.GtkAlignment1 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment1.Name = "GtkAlignment1";
			this.GtkAlignment1.LeftPadding = ((uint)(12));
			// Container child GtkAlignment1.Gtk.Container+ContainerChild
			this.LblFechaHora = new global::Gtk.Label ();
			this.LblFechaHora.Name = "LblFechaHora";
			this.LblFechaHora.LabelProp = global::Mono.Unix.Catalog.GetString ("Fecha y Hora");
			this.GtkAlignment1.Add (this.LblFechaHora);
			this.frame3.Add (this.GtkAlignment1);
			this.hbox2.Add (this.frame3);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.frame3]));
			w20.Position = 0;
			this.vbox6.Add (this.hbox2);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.hbox2]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			this.vbox1.Add (this.vbox6);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.vbox6]));
			w22.Position = 2;
			w22.Expand = false;
			w22.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 292;
			this.DefaultHeight = 608;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.BtnSalir.Clicked += new global::System.EventHandler (this.OnBtnSalirClicked);
			this.BtnReportes.Clicked += new global::System.EventHandler (this.OnBtnReportesClicked);
			this.BtnProcesarVenta.Clicked += new global::System.EventHandler (this.OnBtnProcesarVentaClicked);
			this.BtnConsultarFactura.Clicked += new global::System.EventHandler (this.OnBtnConsultarFacturaClicked);
			this.BtnCerrarSesion.Clicked += new global::System.EventHandler (this.OnBtnCerrarSesionClicked);
			this.BtnActualizarUsuario.Clicked += new global::System.EventHandler (this.OnBtnActualizarUsuarioClicked);
			this.BtnActualizarServicios.Clicked += new global::System.EventHandler (this.OnBtnActualizarServiciosClicked);
			this.BtnActualizarClientes.Clicked += new global::System.EventHandler (this.OnBtnActualizarClientesClicked);
			this.BtnAcercaDe.Clicked += new global::System.EventHandler (this.OnBtnAcercaDeClicked);
		}
	}
}
