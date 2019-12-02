using System;
using Gtk;

namespace WhiteRose
{
	public partial class VntBuscarServicio : Validaciones
	{
		ConexBuscarServicio cod;
		Entry EntCod;

		/**************
		* CONSTRUCTOR *
		***************/

		public VntBuscarServicio (ref Entry EntCodigoP)
		{
			Build ();
			ColorearControles ();
			cod = new ConexBuscarServicio();
			LlenarTreeview ();
			EntCod = EntCodigoP;
			EntCodigo.Text = EntCod.Text;
			TvServicios.Model = cod.BuscarServicio (EntCodigo.Text,EntDescripcion.Text);
		}

		/****************************************
		* MÉTODOS DE LAS SEÑALES DE LOS BOTONES *
		*****************************************/

		protected void OnBtnAceptarClicked (object sender, EventArgs e)
		{
			EntCod.Text = EntCodigo.Text;
			this.Destroy ();
		}

		protected void OnBtnCancelarClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}

		/*******************************
		* SEÑALES CHANGED DE LOS ENTRY *
		********************************/

		protected void OnEntCodigoChanged (object sender, EventArgs e)
		{
			ValidarAlfanumerico (EntCodigo);
			TvServicios.Model = cod.BuscarServicio (EntCodigo.Text, EntDescripcion.Text);
		}

		protected void OnEntDescripcionChanged (object sender, EventArgs e)
		{
			TvServicios.Model = cod.BuscarServicio (EntCodigo.Text, EntDescripcion.Text);
		}

		/**********************************
		* MÉTODOS PROPIOS DE LOS TREEVIEW *
		***********************************/

		protected void OnTvServiciosRowActivated (object o, RowActivatedArgs args)
		{
			TreeModel model;
			TreeIter iter;
			if (TvServicios.Selection.GetSelected(out model, out iter)) 
				EntCod.Text = (string)model.GetValue (iter, 0);
			this.Destroy ();
		}

		protected void LlenarTreeview ()
		{
			TvServicios.AppendColumn ("Código", new CellRendererText (), "text", 0);
			TvServicios.AppendColumn ("Descripción", new CellRendererText (), "text", 1);
			TvServicios.AppendColumn ("Precio Detal", new CellRendererText (), "text", 2);
			TvServicios.AppendColumn ("Precio Mayor", new CellRendererText (), "text", 3);
		}

		#region Coloreado de los Controles
		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			this.ModifyBg (StateType.Normal, Est.Alabaster);
			Est.EstilizarTreeView (TvServicios, Est.Grey);
			Est.EstilizarLabel (LblServicios, Est.SmokyBlack);
			Est.EstilizarLabel (LblCodigo, Est.SmokyBlack);
			Est.EstilizarLabel (LblDescripcion, Est.SmokyBlack);
			Est.EstilizarBoton (BtnCancelar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnAceptar, Est.Iceberg, 14);
			Est.EstilizarFrame (frame5, Est.Aquamarine);
		}
		#endregion
	}
}

