using System;
using Gtk;
using System.Data;
using System.Timers;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public partial class VntReportesVentas : Validaciones
	{
		ConexReportes cod = new ConexReportes();

		/***************************
		* CONSTRUCTOR Y DESTRUCTOR *
		****************************/

		public VntReportesVentas ()
		{
			this.Build ();
			ColorearControles ();
			EntFechaF.Text = DateTime.Now.ToString ("d");
			LblFechaHora.Text = DateTime.Now.ToString ();
			new TmrFechaHora(ref LblFechaHora);
			cod.LimpiaFact ();
			cod.LimpiarDetFact ();
			LlenarTreeViewFact ();
			LlenarTreeViewDeta ();
			TvFacturas.Model = cod.GetReptFact ();
			TvDetalles.Model = cod.GetReptDeta ();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			if (cod.Mensaje ("¿Desea volver al menú principal?\n Puede tener información sin guardar.",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes)
				this.Destroy ();
		}

		/****************************************
		* MÉTODOS DE LAS SEÑALES DE LOS BOTONES *
		*****************************************/

		protected void OnBtnBuscarFactClicked (object sender, EventArgs e)
		{
			if (Convert.ToDateTime (EntFechaI.Text) > Convert.ToDateTime (EntFechaF.Text)) {
				cod.Mensaje ("La fecha de inicio no puede ser posterior a la fecha de final.", ButtonsType.Ok, MessageType.Info);
				EntFechaI.Text = "";
				EntFechaI.ChildFocus (DirectionType.Down);
			} else {
				string FechaI = Convert.ToDateTime (EntFechaI.Text).ToString ("yyyy-MM-dd");
				string FechaF = Convert.ToDateTime (EntFechaF.Text).ToString ("yyyy-MM-dd");
				cod.LimpiaFact ();
				cod.DevolverFactura (FechaI, FechaF);
				TvFacturas.Model = cod.GetReptFact ();
			}
		}

		protected void OnBtnBuscarServClicked (object sender, EventArgs e)
		{
			string Codigo = EntCodServ.Text;

			if (cod.ExistenciaServicio (Codigo)) {
				LimpiarDetFac ();
				cod.DevolverDetFacturas (Codigo);
				TvDetalles.Model = cod.GetReptDeta ();
			} else {
				cod.Mensaje ("Servicio no encontrado.",ButtonsType.Ok,MessageType.Info);
				EntCodServ.Text="";
				EntCodServ.ChildFocus (DirectionType.Up);
			}
		}

		protected void OnBtnVerCalendarioFechaIClicked (object sender, EventArgs e)
		{
			VntFechaCalendario Fec = new VntFechaCalendario(ref EntFechaI);
			Fec.Show ();
		}

		protected void OnBtnVerCalendarioFechaFClicked (object sender, EventArgs e)
		{
			VntFechaCalendario Fec = new VntFechaCalendario(ref EntFechaF);
			Fec.Show ();
		}


		protected void OnBtnLimpiarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea limpiar todos los campos?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) {
				LimpiarFac ();
				LimpiarDetFac ();
			}
		}

		protected void OnBtnCancelarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea volver al menú principal?\n Puede tener información sin guardar.",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes)
				this.Destroy ();
		}

		protected void OnBtnSalirClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea salir del sistema?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) 
				Application.Quit ();
		}

		/***************************************
		* MÉTODOS PROPIOS DEL TREEVIEW (TABLA) *
		****************************************/

		protected void OnTvFacturasRowActivated (object o, Gtk.RowActivatedArgs args)
		{
			TreeModel model;
			TreeIter iter;
			if (TvFacturas.Selection.GetSelected(out model, out iter)) {
				LimpiarDetFac ();
				cod.CargarDetFacturas(model.GetValue (iter, 0).ToString());
				TvDetalles.Model = cod.GetReptDeta1 ();
			}
			notebook2.NextPage ();
		}

		public void LlenarTreeViewFact(){
			TvFacturas.AppendColumn ("Número de Factura", new CellRendererText (), "text",0);
			TvFacturas.AppendColumn ("Rif del Cliente", new CellRendererText (), "text", 1);
			TvFacturas.AppendColumn ("Tipo de Venta", new CellRendererText (), "text", 2);
			TvFacturas.AppendColumn ("Fecha", new CellRendererText (), "text", 3);
			TvFacturas.AppendColumn ("SubTotal", new CellRendererText (), "text", 4);
			TvFacturas.AppendColumn ("% Descuento", new CellRendererText (), "text", 5);
			TvFacturas.AppendColumn ("% Iva", new CellRendererText (), "text", 6);
		}

		public void LlenarTreeViewDeta(){
			TvDetalles.AppendColumn ("Número de la Factura", new CellRendererText (), "text",0);
			TvDetalles.AppendColumn ("Código del Servicio", new CellRendererText (), "text",1);
			TvDetalles.AppendColumn ("Cantidad", new CellRendererText (), "text", 2);
			TvDetalles.AppendColumn ("Precio", new CellRendererText (), "text", 3);
		}

		/**********************
		* MÉTODOS DE LIMPIEZA *
		***********************/

		protected void LimpiarFac(){
			EntFechaI.Text="";
			EntFechaF.Text = DateTime.Now.ToString ("d");
			ListStore Vacio = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			TvFacturas.Model = Vacio;
			cod.LimpiaFact ();
		}

		protected void LimpiarDetFac(){
			EntCodServ.Text="";
			ListStore Vacio = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			TvDetalles.Model = Vacio;
			cod.LimpiarDetFact1 ();
			cod.LimpiarDetFact ();
		}

		/************************************
		* SEÑALES DE CHANGED PARA LOS ENTRY *
		* (VALIDACIONES DE SUS CONTENIDOS)  *
		*************************************/

		protected void OnEntCodServChanged (object sender, EventArgs e)
		{
			ValidarAlfanumerico (EntCodServ);
		}

		#region Coloreado de los Controles
		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			this.ModifyBg (StateType.Normal, Est.Grey);
			notebook2.ModifyBg (StateType.Normal, Est.Snow);
			Est.EstilizarTreeView (TvFacturas, Est.Grey);
			Est.EstilizarTreeView (TvDetalles, Est.Grey);
			Est.EstilizarLabel (LblEstadisticas1, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaF, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaI, Est.SmokyBlack);
			Est.EstilizarLabel (LblReportesServicios1, Est.SmokyBlack);
			Est.EstilizarLabel (LblReportesServicios2, Est.SmokyBlack);
			Est.EstilizarLabel (LblReportesVentas, Est.SmokyBlack);
			Est.EstilizarLabel (LblReportesVentas1, Est.SmokyBlack);
			Est.EstilizarLabel (LblServicios, Est.SmokyBlack);
			Est.EstilizarLabel (LblDatosS, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaC, Est.SmokyBlack);
			Est.EstilizarBoton (BtnVerCalendarioFechaF, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnVerCalendarioFechaI, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnBuscarServ, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnBuscarFact, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnCancelar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnSalir, Est.SalmonPink, 14);
			Est.EstilizarBoton (BtnLimpiar, Est.PaleOrange, 14);
			Est.EstilizarFrame (frame1, Est.Iceberg);
			Est.EstilizarFrame (frame2, Est.Iceberg);
			Est.EstilizarFrame (frame3, Est.Iceberg);
			Est.EstilizarFrame (frame5, Est.Iceberg);
			Est.EstilizarFrame (frame7, Est.Iceberg);
			Est.EstilizarFrame (frame4, Est.Iceberg);
		}
		#endregion
	}
}

