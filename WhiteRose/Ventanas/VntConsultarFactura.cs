using System;
using Gtk;
using System.Data;
using System.Timers;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public partial class VntConsultarFactura : Validaciones
	{
		Timer ValidarBoton;
		ConexConsultaFactura cod;
		FacturaVenta fv;

		/***************************
		* CONSTRUCTOR Y DESTRUCTOR *
		****************************/

		public VntConsultarFactura () 
		{
			this.Build ();
			cod = new ConexConsultaFactura ();
			ColorearControles ();
			LblFechaHora.Text = DateTime.Now.ToString ();
			new TmrFechaHora (ref LblFechaHora);
			TvServiciosPagados.AppendColumn ("Código", new CellRendererText (), "text", 0);
			TvServiciosPagados.AppendColumn ("Descripción", new CellRendererText (), "text", 1);
			TvServiciosPagados.AppendColumn ("Cantidad", new CellRendererText (), "text", 2);
			TvServiciosPagados.AppendColumn ("Precio", new CellRendererText (), "text", 3);
			TvServiciosPagados.AppendColumn ("Subtotal", new CellRendererText (), "text", 4);
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

		protected void OnBtnBuscarClicked (object sender, EventArgs e)
		{
			fv = cod.DevolverFactura (EntNroFact.Text);
			if (fv.GetNroFact () == "") {
				cod.Mensaje ("Factura no encontrada.", ButtonsType.Ok, MessageType.Info);
				EntNroFact.ChildFocus (DirectionType.Up);
			} else {
				DateTime Fecha = Convert.ToDateTime (fv.GetFechaFact ());
				EntFechaE.Text = Fecha.ToString("dd/MM/yyyy");
				EntHoraE.Text = Fecha.ToString ("hh:mm:ss tt");


				if (fv.GetTipoV () == 1) 
					RbDetal.Active=true;
				else
					RbMayor.Active=true;

				EntRifCliente.Text=fv.GetCliente ().GetRif ();
				EntNombreCliente.Text=fv.GetCliente ().GetNombre ();
				EntDireccionCliente.Text=fv.GetCliente ().GetDireccion ();
				EntTlfCliente.Text=fv.GetCliente ().GetTelefono ();
				TvServiciosPagados.Model=cod.DevolverDetFactura(fv.GetNroFact ());
				EntSubtotal.Text=fv.GetSubTotal ().ToString ("N") + " Bs.";
				EntPorcDesc.Text=fv.GetPorcDesc ().ToString ();
				EntIva1.Text=fv.GetPorcIva ().ToString ();
				CalcularPrecios ();
			}
		}

		/****************************************
		* CÁLCULOS DE LOS PRECIOS DE LA FACTURA *
		*****************************************/

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
			iva=basei*Convert.ToDouble(EntIva1.Text)/100;
			total=basei+iva;

			EntMontoDesc.Text = montodesc.ToString ("N") + " Bs.";
			EntBaseImp.Text = basei.ToString ("N") + " Bs.";
			EntIva.Text = iva.ToString ("N") + " Bs.";
			EntTotalPagar.Text = total.ToString ("N") + " Bs.";
		}

		/***************
		* VALIDACIONES *
		****************/

		protected void OnEntNroFactChanged (object sender, EventArgs e)
		{
			ValidarNro (EntNroFact);
		}

		protected void OnValidarBotonElapsed(object sender, EventArgs e)
		{
			if (EntNroFact.Text != "")
				BtnBuscar.Sensitive = true;
			else 
				BtnBuscar.Sensitive = false;
		}

		#region Coloreado de los controles
		public void ColorearControles(){
			Estilo Est = new Estilo();
			this.ModifyBg (StateType.Normal,Est.Grey);
			Est.EstilizarTreeView (TvServiciosPagados, Est.Grey);
			Est.EstilizarLabel (LblNroFact,Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaE,Est.SmokyBlack);
			Est.EstilizarLabel (LblFecha,Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora,Est.SmokyBlack);
			Est.EstilizarLabel (LblHoraE,Est.SmokyBlack);
			Est.EstilizarLabel (LblRif,Est.SmokyBlack);
			Est.EstilizarLabel (LblNombreCliente,Est.SmokyBlack);
			Est.EstilizarLabel (LblDireccionCliente,Est.SmokyBlack);
			Est.EstilizarLabel (LblTelefono,Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora,Est.SmokyBlack);
			Est.EstilizarLabel (LblIdCliente,Est.SmokyBlack);
			Est.EstilizarLabel (LblServiciosAPagar,Est.SmokyBlack);
			Est.EstilizarLabel (LblMontosPagar,Est.SmokyBlack);
			Est.EstilizarLabel (LblTipoV,Est.SmokyBlack);
			Est.EstilizarLabel (LblSubtotal,Est.SmokyBlack);
			Est.EstilizarLabel (LblBaseImponible,Est.SmokyBlack);
			Est.EstilizarLabel (LblPorcDesc,Est.SmokyBlack);
			Est.EstilizarLabel (LblMonto,Est.SmokyBlack);
			Est.EstilizarLabel (LblMontosPagar1,Est.SmokyBlack);
			Est.EstilizarLabel (LblIva,Est.SmokyBlack);
			Est.EstilizarBoton (BtnBuscar,Est.PaleTurquoise,14);
			Est.EstilizarBoton (BtnCancelar,Est.PaleOrange,14);
			Est.EstilizarBoton (BtnSalir,Est.SalmonPink,14);
			Est.EstilizarFrame (frame1,Est.Iceberg);
			Est.EstilizarFrame (frame2,Est.Iceberg);
			Est.EstilizarFrame (frame8,Est.Iceberg);
			Est.EstilizarFrame (frame7,Est.Iceberg);
			Est.EstilizarFrame (frame6, Est.Iceberg);
			Est.EstilizarRadioButton (RbDetal, Est.SmokyBlack);
			Est.EstilizarRadioButton (RbMayor, Est.SmokyBlack);
		}
		#endregion
	}
}

