using System;
using Gtk;

namespace WhiteRose
{
	public partial class VntFechaCalendario : Gtk.Window
	{
		Entry F;

		/**************
		* CONSTRUCTOR *
		***************/

		public VntFechaCalendario (ref Entry Fecha) :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			ColorearControles ();
			F = Fecha;
		}

		/*********************************
		* SEÑALES CLICKED DE LOS BOTONES *
		**********************************/

		protected void OnBtnCancelarClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}

		protected void OnBtnAceptarClicked (object sender, EventArgs e)
		{
			F.Text = Calendario.Date.ToString ("dd/MM/yyyy");
			this.Destroy ();	
		}

		/***********************************
		* SEÑAL DOUBLECLICK DEL CALENDARIO *
		************************************/

		protected void OnCalendarioDaySelectedDoubleClick (object sender, EventArgs e)
		{
			F.Text = Calendario.Date.ToString ("dd/MM/yyyy");
			this.Destroy ();	
		}

		#region Coloreado de los Controles
		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			this.ModifyBg (StateType.Normal, Est.Alabaster);
			Calendario.ModifyBg (StateType.Normal, Est.Alabaster);
			Est.EstilizarBoton (BtnCancelar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnAceptar, Est.Aquamarine, 14);
		}
		#endregion
	}
}

