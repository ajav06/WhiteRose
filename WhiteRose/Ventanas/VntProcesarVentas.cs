using System;
using Gtk;
using System.Timers;

namespace WhiteRose
{
	public partial class VntProcesarVentas : Validaciones
	{
		Timer ValidarBoton;
		ConexVenta cod = new ConexVenta ();
		Cliente Cli;
		bool EstadoDetalle;

		/***************************
		* CONSTRUCTOR Y DESTRUCTOR *
		****************************/

		public VntProcesarVentas () 

		{
			this.Build ();
			EstadoDetalle=false;
			cod.NuevoCodFactura (EntNroFact);
			ColorearControles ();
			TvServiciosPagar.Model = cod.GetDetFact (); //cod.CargarDetFacturas (EntNroFact.Text);
			LlenarTreeViewDetFactura ();
			LblFechaHora.Text = DateTime.Now.ToString ();
			EntFecha.Text = DateTime.Now.ToString ("dd/MM/yyyy");
			EntHora.Text = DateTime.Now.ToString ("hh:mm tt");
			EntSubtotal.Text = "0,00 Bs.";
			CalcularPrecios ();
			new TmrFechaHora (ref EntFecha, ref EntHora, ref LblFechaHora);
			EntRifCliente.ChildFocus (DirectionType.Up);
			ValidarBoton = new Timer();
			ValidarBoton.Elapsed+= new ElapsedEventHandler(OnValidarBotonElapsed);
			ValidarBoton.Interval = 100;
			ValidarBoton.Enabled = true;

		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			if (cod.Mensaje ("¿Desea volver al menú principal?\n Puede tener información sin guardar.",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes)
				this.Destroy ();
		}

		/****************************************
		* MÉTODOS DE LAS SEÑALES DE LOS BOTONES *
		*****************************************/

		protected void OnBtnBuscarClienteClicked (object sender, EventArgs e)
		{
			Cli = cod.BuscarCliente (EntRifCliente.Text);

			if (Cli.GetRif () == "N") {
				if (cod.Mensaje ("Cliente no encontrado. ¿Desea incluirlo en el sistema?", ButtonsType.YesNo, MessageType.Info) == ResponseType.Yes) {
					VntActualizarCliente ActCli = new VntActualizarCliente (EntRifCliente.Text);
					ActCli.Show ();
					EntRifCliente.ChildFocus (DirectionType.Down);
				}
			} else {
				EntNombreCliente.Text=Cli.GetNombre ();
				EntDireccionCliente.Text=Cli.GetDireccion ();
				EntTlfCliente.Text=Cli.GetTelefono ();
				BtnBuscarCliente.Sensitive=false;
				EntCodServ.ChildFocus (DirectionType.Down);
			}
		}

		protected void OnBtnBuscarServicioClicked (object sender, EventArgs e)
		{
			VntBuscarServicio Buscar = new VntBuscarServicio(ref EntCodServ);
			Buscar.Show();
		}

		protected void OnBtnAnadirServClicked (object sender, EventArgs e)
		{
			int cont = 0;
			TreeIter iter;
			ListStore otro = cod.GetDetFact ();
			if (otro.IterNChildren()>0) {
				if (otro.GetIterFirst (out iter)) {
					do {
						TreeIter iter2;
						if (otro.GetIterFirst (out iter2)) {
							if (EntCodServ.Text==otro.GetValue(iter,0).ToString()) {
								cont=+1;
								cod.Mensaje("El servicio ya fue añadido.\nSi desea actualizar la cantidad, haga doble click sobre el servicio\nen el listado de abajo.",ButtonsType.Ok,MessageType.Error);
								EntCodServ.ChildFocus (DirectionType.Up);
							}
						}
					} while (otro.IterNext(ref iter));
				}
			}
			AñadirServicios (cont);
		}

		protected void OnBtnActualizarServClicked (object sender, EventArgs e)
		{
			TreeIter iter;
			TreeModel model;
			string smonto="";
			double monto=0;

			if (TvServiciosPagar.Selection.GetSelected(out model,out iter)) 
				smonto = (string)model.GetValue (iter, 4);

			smonto = smonto.Remove (smonto.Length-4);
			monto=Convert.ToDouble (smonto);

			ListStore ls = cod.GetDetFact();
			TreePath[] tp = TvServiciosPagar.Selection.GetSelectedRows ();
			ls.GetIter (out iter, tp[0]);
			ls.Remove (ref iter);
			TvServiciosPagar.Model = ls;
			cod.SetDetFact(ls);
			cod.ReducirAcumSubtotal (monto);
			cod.NuevoDetFact (EntCodServ.Text, Convert.ToInt16 (EntCantServ.Text),EntSubtotal,TipoVenta (),TvServiciosPagar);
			LimpiarValidarEntServicios ();
			EntSubtotal.Text=cod.GetSst ().ToString ("N")+ " Bs.";
			CalcularPrecios ();
			ValidarBoton.Enabled = true;
		}

		protected void OnBtnEliminarServClicked (object sender, EventArgs e)
		{
			TreeModel model;
			string smonto="";
			double monto=0;
			TreeIter iter;

			if (TvServiciosPagar.Selection.GetSelected(out model,out iter)) 
				smonto = (string)model.GetValue (iter, 4);

			smonto = smonto.Remove (smonto.Length-4);
			monto=Convert.ToDouble (smonto);

			ListStore ls = cod.GetDetFact();
			TreePath[] tp = TvServiciosPagar.Selection.GetSelectedRows ();
			ls.GetIter (out iter, tp[0]);
			ls.Remove (ref iter);
			TvServiciosPagar.Model = ls;
			cod.SetDetFact(ls);
			cod.ReducirAcumSubtotal (monto);
			LimpiarValidarEntServicios ();
			EntSubtotal.Text=cod.GetSst ().ToString ("N")+ " Bs.";
			CalcularPrecios ();
			ValidarBoton.Enabled=true;
		}

		protected void OnBtnCancelarActualizacionClicked (object sender, EventArgs e)
		{
			BtnActualizarServ.Sensitive = false;
			BtnEliminarServ.Sensitive = false;
			BtnCancelarActualizacion.Sensitive = false;
			BtnAnadirServ.Sensitive = true;
			ValidarBoton.Enabled = true;
			EntCodServ.Text = "";
			EntCantServ.Text = "";
			EntCodServ.ChildFocus (DirectionType.Down);
		}

		protected void OnBtnGuardarClicked (object sender, EventArgs e)
		{
			try {
				cod.AñadirFactura(EntRifCliente.Text,Convert.ToString(TipoVenta()),DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss"),EntSubtotal.Text,EntPorcDesc.Text,EntIva1.Text);				
				GuardarDatosDetFact();
				LimpiarFactura();
				cod.Mensaje("La Factura se ha Guardado con Exito",ButtonsType.Ok,MessageType.Info);
				cod.LimpiarDetFact();
			} catch (Exception ex) {
				cod.Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);	
			}
		}

		protected void OnBtnLimpiarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea limpiar todos los campos?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) {
				LimpiarFactura ();
			}
		}

		protected void OnBtnSalirClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea salir del sistema?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) 
				Application.Quit ();
		}

		protected void OnBtnCancelarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea volver al menú principal?\n Puede tener información sin guardar.",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes)
				this.Destroy ();
		}

		/**********************************
		* MÉTODOS DE CÁLCULO / AUXILIARES *
		***********************************/

		protected void AñadirServicios(int c){
			if (c==0) {
				if (cod.BuscarServicio (EntCodServ.Text)) {
					cod.NuevoDetFact (EntCodServ.Text, Convert.ToInt16(EntCantServ.Text), EntSubtotal, TipoVenta (), TvServiciosPagar);
					CalcularPrecios ();
					EstadoDetalle = true;
				} else {
					cod.Mensaje ("Servicio no encontrado.",ButtonsType.Ok,MessageType.Info);
					EntCodServ.ChildFocus (DirectionType.Up);
				}
					EntCodServ.Text="";
					EntCantServ.Text="";
			}
		}

		protected void CalcularPrecios ()
		{
			double subtotal, porc, montodesc, basei, iva, total;
			string st = EntSubtotal.Text.Remove (EntSubtotal.Text.Length - 4);

			subtotal = porc = montodesc = basei = iva = total = 0;

			subtotal = Convert.ToDouble (st);
			if (EntPorcDesc.Text != "") {
				porc = Convert.ToDouble (EntPorcDesc.Text);
			} else porc = 0;

			montodesc=subtotal*porc/100;
			basei=subtotal-montodesc;
			if (EntIva1.Text!="")
				iva=basei*Convert.ToDouble(EntIva1.Text)/100;
			else 
				iva=0;
			total=basei+iva;

			EntMontoDesc.Text = montodesc.ToString ("N") + " Bs.";
			EntBaseImp.Text = basei.ToString ("N") + " Bs.";
			EntIva.Text = iva.ToString ("N") + " Bs.";
			EntTotalPagar.Text = total.ToString ("N") + " Bs.";
		}

		protected void GuardarDatosDetFact(){
			TreeIter iter;
			ListStore otro = cod.GetDetFact ();
			if (otro.GetIterFirst (out iter)) {
				do {
					TreeIter iter2;
					if (otro.GetIterFirst (out iter2)) {
						cod.AñadirDetFact(otro.GetValue(iter,0).ToString(),otro.GetValue(iter,2).ToString(),otro.GetValue(iter,3).ToString());
					}
				} while (otro.IterNext(ref iter));
			}
		}

		protected int TipoVenta(){
			if (RbDetal.Active) {
				return 1;
			} else {
				return 2;
			}
		}

		/***************************************
		* MÉTODOS PROPIOS DEL TREEVIEW (TABLA) *
		****************************************/

		public void LlenarTreeViewDetFactura(){
			TvServiciosPagar.AppendColumn ("Código", new CellRendererText (), "text", 0);
			TvServiciosPagar.AppendColumn ("Descripción", new CellRendererText (), "text", 1);
			TvServiciosPagar.AppendColumn ("Cantidad", new CellRendererText (), "text", 2);
			TvServiciosPagar.AppendColumn ("Precio", new CellRendererText (),"text", 3);
			TvServiciosPagar.AppendColumn ("Subtotal", new CellRendererText (),"text", 4);
		}

		protected void OnTvServiciosPagarRowActivated (object o, RowActivatedArgs args)
		{
			TreeModel model;
			TreeIter iter;
			if (TvServiciosPagar.Selection.GetSelected(out model,out iter)) {
				EntCodServ.Text = (string)model.GetValue (iter, 0);
				EntCantServ.Text = (string)model.GetValue (iter, 2);
			}
			BtnActualizarServ.Sensitive=true;
			BtnEliminarServ.Sensitive=true;
			BtnCancelarActualizacion.Sensitive = true;
			BtnAnadirServ.Sensitive=false;
			ValidarBoton.Enabled = false;
		}

		/************************************
		* SEÑALES DE CHANGED PARA LOS ENTRY *
		* (VALIDACIONES DE SUS CONTENIDOS)  *
		*************************************/

		protected void OnEntPorcDescChanged (object sender, EventArgs e)
		{
			ValidarSoloNroDecimal (EntPorcDesc);
			CalcularPrecios ();
		}

		protected void OnEntIva1Changed (object sender, EventArgs e)
		{
			ValidarSoloNroDecimal (EntIva1);
			CalcularPrecios ();
		}

		protected void OnEntRifClienteChanged (object sender, EventArgs e)
		{
			ValidarAlfanumerico(EntRifCliente);
		}

		protected void OnEntCantServChanged (object sender, EventArgs e)
		{
			ValidarNro(EntCantServ);
		}

		protected void OnEntCodServChanged (object sender, EventArgs e)
		{
			ValidarAlfanumerico (EntCodServ);
		}

		/***********************
		* OTROS MÉTODOS VARIOS *
		************************/

		protected void LimpiarValidarEntServicios ()
		{
			BtnActualizarServ.Sensitive=false;
			BtnEliminarServ.Sensitive=false;
			BtnAnadirServ.Sensitive=true;
			EntCodServ.Text = "";
			EntCantServ.Text = "";
			EntCodServ.ChildFocus (DirectionType.Up);
		}

		protected void LimpiarFactura(){
			RbDetal.Active=true;
			EntNroFact.Text=EntRifCliente.Text=EntNombreCliente.Text=EntDireccionCliente.Text=EntTlfCliente.Text=EntCodServ.Text=EntCantServ.Text=EntMontoDesc.Text=EntBaseImp.Text=EntIva.Text=EntTotalPagar.Text="";
			EntPorcDesc.Text="0";
			EntSubtotal.Text = "0,00 Bs.";
			CalcularPrecios ();
			cod.NuevoCodFactura (EntNroFact);

			ListStore Vacio = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string));
			TvServiciosPagar.Model = Vacio;
			cod.LimpiarDetFact();
			BtnBuscarCliente.Sensitive=true;
			EstadoDetalle = false;
		}

		protected void OnValidarBotonElapsed(object sender, EventArgs e)
		{
			ValidarRadioBUnaVez (cod.GetDetFact (), RbDetal, RbMayor);
			if (EntRifCliente.Text != "" && EntNombreCliente.Text == "")
				BtnBuscarCliente.Sensitive = true;
			else 
				BtnBuscarCliente.Sensitive = false;

			if (EntCodServ.Text != "" && EntCantServ.Text != "")
				BtnAnadirServ.Sensitive = true;
			else 
				BtnAnadirServ.Sensitive = false;

			if (EntNombreCliente.Text!="" && EstadoDetalle)
				BtnGuardar.Sensitive = true;
			else
				BtnGuardar.Sensitive = false;
		}

		#region Coloreado de Controles
		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			this.ModifyBg (StateType.Normal, Est.Grey);
			Est.EstilizarTreeView (TvServiciosPagar, Est.Grey);
			Est.EstilizarLabel (LblNroFact, Est.SmokyBlack);
			Est.EstilizarLabel (LblFecha, Est.SmokyBlack);
			Est.EstilizarLabel (LblRif, Est.SmokyBlack);
			Est.EstilizarLabel (LblNombreCliente, Est.SmokyBlack);
			Est.EstilizarLabel (LblNombreEmpresa, Est.SmokyBlack);
			Est.EstilizarLabel (LblHora, Est.SmokyBlack);
			Est.EstilizarLabel (LblDireccionCliente, Est.SmokyBlack);
			Est.EstilizarLabel (LblTelefono, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora, Est.SmokyBlack);
			Est.EstilizarLabel (LblIdCliente, Est.SmokyBlack);
			Est.EstilizarLabel (LblServicios, Est.SmokyBlack);
			Est.EstilizarLabel (LblServiciosAPagar, Est.SmokyBlack);
			Est.EstilizarLabel (LblMontosPagar, Est.SmokyBlack);
			Est.EstilizarLabel (LblTipoVenta, Est.SmokyBlack);
			Est.EstilizarLabel (LblCodigoServ, Est.SmokyBlack);
			Est.EstilizarLabel (LblCantidad, Est.SmokyBlack);
			Est.EstilizarLabel (LblSubtotal, Est.SmokyBlack);
			Est.EstilizarLabel (LblBaseImponible, Est.SmokyBlack);
			Est.EstilizarLabel (LblPorcDesc, Est.SmokyBlack);
			Est.EstilizarLabel (LblMonto, Est.SmokyBlack);
			Est.EstilizarLabel (LblMontosPagar1, Est.SmokyBlack);
			Est.EstilizarLabel (LblIva, Est.SmokyBlack);
			Est.EstilizarBoton (BtnBuscarCliente, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnBuscarServicio, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnGuardar, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnAnadirServ, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnActualizarServ, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnEliminarServ, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnLimpiar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnCancelarActualizacion, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnCancelar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnSalir, Est.SalmonPink, 14);
			Est.EstilizarFrame (frame1, Est.Iceberg);
			Est.EstilizarFrame (frame10, Est.Iceberg);
			Est.EstilizarFrame (frame9, Est.Iceberg);
			Est.EstilizarFrame (frame8, Est.Iceberg);
			Est.EstilizarFrame (frame7, Est.Iceberg);
			Est.EstilizarFrame (frame6, Est.Iceberg);
			Est.EstilizarFrame (frame5, Est.Iceberg);
			Est.EstilizarRadioButton (RbDetal, Est.SmokyBlack);
			Est.EstilizarRadioButton (RbMayor, Est.SmokyBlack);
		}
		#endregion
	}
}

