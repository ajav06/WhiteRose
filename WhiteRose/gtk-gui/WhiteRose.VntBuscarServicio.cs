
// This file has been generated by the GUI designer. Do not modify.
namespace WhiteRose
{
	public partial class VntBuscarServicio
	{
		private global::Gtk.VBox vbox12;
		
		private global::Gtk.HBox hbox7;
		
		private global::Gtk.VBox vbox13;
		
		private global::Gtk.Label LblCodigo;
		
		private global::Gtk.Label LblDescripcion;
		
		private global::Gtk.VBox vbox14;
		
		private global::Gtk.Entry EntCodigo;
		
		private global::Gtk.Entry EntDescripcion;
		
		private global::Gtk.Frame frame5;
		
		private global::Gtk.Alignment GtkAlignment;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.TreeView TvServicios;
		
		private global::Gtk.Label LblServicios;
		
		private global::Gtk.HBox hbox6;
		
		private global::Gtk.Button BtnCancelar;
		
		private global::Gtk.Fixed fixed5;
		
		private global::Gtk.Button BtnAceptar;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget WhiteRose.VntBuscarServicio
			this.Name = "WhiteRose.VntBuscarServicio";
			this.Title = global::Mono.Unix.Catalog.GetString ("VntBuscarServicio");
			this.TypeHint = ((global::Gdk.WindowTypeHint)(4));
			this.WindowPosition = ((global::Gtk.WindowPosition)(2));
			this.BorderWidth = ((uint)(6));
			// Container child WhiteRose.VntBuscarServicio.Gtk.Container+ContainerChild
			this.vbox12 = new global::Gtk.VBox ();
			this.vbox12.Name = "vbox12";
			this.vbox12.Spacing = 6;
			// Container child vbox12.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox ();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.vbox13 = new global::Gtk.VBox ();
			this.vbox13.Name = "vbox13";
			this.vbox13.Homogeneous = true;
			this.vbox13.Spacing = 9;
			// Container child vbox13.Gtk.Box+BoxChild
			this.LblCodigo = new global::Gtk.Label ();
			this.LblCodigo.Name = "LblCodigo";
			this.LblCodigo.Xalign = 1F;
			this.LblCodigo.LabelProp = global::Mono.Unix.Catalog.GetString ("Código:");
			this.vbox13.Add (this.LblCodigo);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox13 [this.LblCodigo]));
			w1.Position = 0;
			// Container child vbox13.Gtk.Box+BoxChild
			this.LblDescripcion = new global::Gtk.Label ();
			this.LblDescripcion.Name = "LblDescripcion";
			this.LblDescripcion.Xalign = 1F;
			this.LblDescripcion.LabelProp = global::Mono.Unix.Catalog.GetString ("Descripción:");
			this.vbox13.Add (this.LblDescripcion);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox13 [this.LblDescripcion]));
			w2.Position = 1;
			this.hbox7.Add (this.vbox13);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.vbox13]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.vbox14 = new global::Gtk.VBox ();
			this.vbox14.Name = "vbox14";
			this.vbox14.Homogeneous = true;
			this.vbox14.Spacing = 9;
			// Container child vbox14.Gtk.Box+BoxChild
			this.EntCodigo = new global::Gtk.Entry ();
			this.EntCodigo.CanFocus = true;
			this.EntCodigo.Name = "EntCodigo";
			this.EntCodigo.IsEditable = true;
			this.EntCodigo.MaxLength = 3;
			this.EntCodigo.InvisibleChar = '•';
			this.vbox14.Add (this.EntCodigo);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox14 [this.EntCodigo]));
			w4.Position = 0;
			// Container child vbox14.Gtk.Box+BoxChild
			this.EntDescripcion = new global::Gtk.Entry ();
			this.EntDescripcion.CanFocus = true;
			this.EntDescripcion.Name = "EntDescripcion";
			this.EntDescripcion.IsEditable = true;
			this.EntDescripcion.MaxLength = 45;
			this.EntDescripcion.InvisibleChar = '•';
			this.vbox14.Add (this.EntDescripcion);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox14 [this.EntDescripcion]));
			w5.Position = 1;
			this.hbox7.Add (this.vbox14);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.vbox14]));
			w6.Position = 1;
			this.vbox12.Add (this.hbox7);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox12 [this.hbox7]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox12.Gtk.Box+BoxChild
			this.frame5 = new global::Gtk.Frame ();
			this.frame5.Name = "frame5";
			this.frame5.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child frame5.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			this.GtkAlignment.BorderWidth = ((uint)(12));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.TvServicios = new global::Gtk.TreeView ();
			this.TvServicios.CanFocus = true;
			this.TvServicios.Name = "TvServicios";
			this.GtkScrolledWindow.Add (this.TvServicios);
			this.GtkAlignment.Add (this.GtkScrolledWindow);
			this.frame5.Add (this.GtkAlignment);
			this.LblServicios = new global::Gtk.Label ();
			this.LblServicios.Name = "LblServicios";
			this.LblServicios.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Servicios</b>");
			this.LblServicios.UseMarkup = true;
			this.frame5.LabelWidget = this.LblServicios;
			this.vbox12.Add (this.frame5);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox12 [this.frame5]));
			w11.Position = 1;
			// Container child vbox12.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Homogeneous = true;
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.BtnCancelar = new global::Gtk.Button ();
			this.BtnCancelar.CanFocus = true;
			this.BtnCancelar.Name = "BtnCancelar";
			this.BtnCancelar.UseUnderline = true;
			this.BtnCancelar.Label = global::Mono.Unix.Catalog.GetString ("Cancelar");
			this.hbox6.Add (this.BtnCancelar);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.BtnCancelar]));
			w12.Position = 0;
			// Container child hbox6.Gtk.Box+BoxChild
			this.fixed5 = new global::Gtk.Fixed ();
			this.fixed5.Name = "fixed5";
			this.fixed5.HasWindow = false;
			this.hbox6.Add (this.fixed5);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.fixed5]));
			w13.Position = 1;
			// Container child hbox6.Gtk.Box+BoxChild
			this.BtnAceptar = new global::Gtk.Button ();
			this.BtnAceptar.HeightRequest = 40;
			this.BtnAceptar.CanFocus = true;
			this.BtnAceptar.Name = "BtnAceptar";
			this.BtnAceptar.UseUnderline = true;
			this.BtnAceptar.Label = global::Mono.Unix.Catalog.GetString ("Aceptar");
			this.hbox6.Add (this.BtnAceptar);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.BtnAceptar]));
			w14.Position = 2;
			this.vbox12.Add (this.hbox6);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox12 [this.hbox6]));
			w15.Position = 2;
			w15.Expand = false;
			w15.Fill = false;
			this.Add (this.vbox12);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 689;
			this.DefaultHeight = 406;
			this.Show ();
			this.EntCodigo.Changed += new global::System.EventHandler (this.OnEntCodigoChanged);
			this.EntDescripcion.Changed += new global::System.EventHandler (this.OnEntDescripcionChanged);
			this.TvServicios.RowActivated += new global::Gtk.RowActivatedHandler (this.OnTvServiciosRowActivated);
			this.BtnCancelar.Clicked += new global::System.EventHandler (this.OnBtnCancelarClicked);
			this.BtnAceptar.Clicked += new global::System.EventHandler (this.OnBtnAceptarClicked);
		}
	}
}
