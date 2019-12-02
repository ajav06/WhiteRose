using System;
using Gtk;
using System.Timers;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public partial class VntActualizarServicios : Validaciones
	{
		Timer ValidarBotones;
		ConexServicios cod = new ConexServicios();

		/***************************
		* CONSTRUCTOR Y DESTRUCTOR *
		****************************/

		public VntActualizarServicios () 
		{
			this.Build ();
			ColorearControles();
			CbDepartamento.Model = cod.CargarDescDpto ();
			new TmrFechaHora (ref LblFechaHora);
			LblFechaHora.Text = DateTime.Now.ToString ();
			LlenarTreeView ();
			TvServicios.Model = cod.CargarServicios ();
			ValidarBotones = new Timer ();
			ValidarBotones.Elapsed += new ElapsedEventHandler(OnValidarBotonesElapsed);
			ValidarBotones.Interval = 100;
			ValidarBotones.Enabled = true;
			CbDepartamento.Active = 0;
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			if (cod.Mensaje ("¿Desea volver al menú principal?\n Puede tener información sin guardar.", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) 
				this.Destroy ();
		}

		/****************************************
		* MÉTODOS DE LAS SEÑALES DE LOS BOTONES *
		*****************************************/

		protected void OnBtnIncluirClicked (object sender, EventArgs e)
		{
			int c = cod.VerificarExistenciaServicio (EntCodigo.Text);
			if (c == 0) {
				if (cod.Mensaje ("¿Desea incluir el servicio?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
					Servicios Serv = new Servicios(EntCodigo.Text,cod.CodDpto (CbDepartamento.Active),EntDescripcion.Text,Convert.ToDouble(EntCosto.Text),Convert.ToDouble (EntPrecioD.Text),Convert.ToDouble (EntPrecioM.Text));
					cod.NuevoServicio (Serv);
				}
			} else if (c == 1) {
				cod.Mensaje ("El servicio fue eliminado.\n Pónganse en contacto con el administrador del sistema.", ButtonsType.Ok, MessageType.Info);
			} else if (c == 2) {
				cod.Mensaje ("El servicio ya existe.",ButtonsType.Ok,MessageType.Info);
			}
			Limpiar ();
		}

		protected void OnBtnModificarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea actualizar al Servicio?\n¡Ojo! Esta es una acción que no podrá deshacer.", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
				Servicios Serv = new Servicios(EntCodigo.Text,cod.CodDpto (CbDepartamento.Active),EntDescripcion.Text,Convert.ToDouble(EntCosto.Text),Convert.ToDouble (EntPrecioD.Text),Convert.ToDouble (EntPrecioM.Text));
				cod.ModificarServicios (Serv);
				Limpiar ();
			}
		}

		protected void OnBtnEliminarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea eliminar al servicio?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
				cod.EliminacionLogicaServicio (EntCodigo.Text);
				Limpiar ();
			}
		}

		protected void OnBtnLimpiarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea limpiar todos los campos?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) {
				Limpiar ();
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

		/************************************
		* SEÑALES DE CHANGED PARA LOS ENTRY *
		* (VALIDACIONES DE SUS CONTENIDOS)  *
		*************************************/

		protected void OnValidarBotonesElapsed (object sender, EventArgs e)
		{
			if ((EntCodigo.Text != "") && (EntPrecioM.Text != "") && (EntPrecioD.Text != "") && (EntCosto.Text != "")) 
				BtnIncluir.Sensitive = true;
			else 
				BtnIncluir.Sensitive = false;

			if ((EntCodigo.Text != "") || (EntPrecioM.Text != "") || (EntPrecioD.Text != "") || (EntCosto.Text != ""))
				BtnLimpiar.Sensitive = true;
			else
				BtnLimpiar.Sensitive = false;
		}

		protected void OnEntCostoChanged (object sender, EventArgs e)
		{
			ValidarSoloNroDecimal (EntCosto);
		}

		protected void OnEntPrecioDChanged (object sender, EventArgs e)
		{
			ValidarSoloNroDecimal (EntPrecioD);
		}

		protected void OnEntPrecioMChanged (object sender, EventArgs e)
		{
			ValidarSoloNroDecimal (EntPrecioM);
		}

		protected void OnEntCodigoChanged (object sender, EventArgs e)
		{
			ValidarAlfanumerico (EntCodigo);
			TvServicios.Model=cod.BuscarServicio(EntCodigo.Text);
		}

		protected void OnEntRifChanged (object sender, EventArgs e)
		{
			TvServicios.Model=cod.BuscarServicio (EntCodigo.Text.Trim ());
		}

		/***************************************
		* MÉTODOS PROPIOS DEL TREEVIEW (TABLA) *
		****************************************/

		protected void ActualizarTvServicios(){
			TvServicios.Model = cod.CargarServicios();
		}

		protected void OnTvServiciosRowActivated (object o, RowActivatedArgs args)
		{
			TreeModel model;
			TreeIter iter;
			if (TvServicios.Selection.GetSelected (out model, out iter)) {
				EntCodigo.Text = (string)model.GetValue (iter, 0);

				string dpto = (string)model.GetValue (iter, 1);
				var dptos = cod.CargarDescDpto ();
				int indice = 0;

				foreach (object[] row in dptos) {
					if (dpto == row [0].ToString ()) {
						CbDepartamento.Active = indice;
						break;
					}
					indice++;
				}

				EntDescripcion.Text = (string)model.GetValue (iter, 2);
				EntCosto.Text = (string)model.GetValue (iter, 3);
				EntPrecioD.Text = (string)model.GetValue (iter, 4);
				EntPrecioM.Text = (string)model.GetValue (iter, 5);
				EntCodigo.IsEditable = false;
				ValidarBotones.Enabled=false;
				BtnEliminar.Sensitive=true;
				BtnModificar.Sensitive=true;
				BtnLimpiar.Sensitive=true;
			}
		}

		public void LlenarTreeView()
		{
			TvServicios.AppendColumn ("Codigo", new CellRendererText (), "text", 0);
			TvServicios.AppendColumn ("Departamento", new CellRendererText (), "text", 1);
			TvServicios.AppendColumn ("Descripcion", new CellRendererText (), "text", 2);
			TvServicios.AppendColumn ("Costo", new CellRendererText (), "text", 3);
			TvServicios.AppendColumn ("Precio Detal", new CellRendererText (), "text", 4);
			TvServicios.AppendColumn ("Precio al Mayor", new CellRendererText (), "text", 5);
		}

		/***********************
		* OTROS MÉTODOS VARIOS *
		************************/

		protected void Limpiar(){
			EntCodigo.Text = EntDescripcion.Text = EntCosto.Text = EntPrecioD.Text = EntPrecioM.Text = "";
			EntCodigo.ChildFocus (DirectionType.Up);
			CbDepartamento.Active = 0;
			ValidarBotones.Enabled=true;
			EntCodigo.IsEditable = false;
			ActualizarTvServicios ();
			BtnModificar.Sensitive=false;
			BtnEliminar.Sensitive=false;
			EntCodigo.IsEditable=true;
		}

		#region Coloreado de los Controles
		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			this.ModifyBg (StateType.Normal, Est.Grey);
			Est.EstilizarTreeView (TvServicios, Est.Grey);
			Est.EstilizarLabel (LblCodigoServ, Est.SmokyBlack);
			Est.EstilizarLabel (LblCodDpto, Est.SmokyBlack);
			Est.EstilizarLabel (LblCosto, Est.SmokyBlack);
			Est.EstilizarLabel (LblDescripcion, Est.SmokyBlack);
			Est.EstilizarLabel (LblPrecioDetal, Est.SmokyBlack);
			Est.EstilizarLabel (LblPrecioMayor, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora, Est.SmokyBlack);
			Est.EstilizarLabel (LblServiciosActuales, Est.SmokyBlack);
			Est.EstilizarBoton (BtnIncluir, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnModificar, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnEliminar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnLimpiar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnCancelar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnSalir, Est.SalmonPink, 14);
			Est.EstilizarFrame (frame1, Est.Iceberg);
			Est.EstilizarFrame (frame2, Est.Iceberg);
		}
		#endregion
	}
}

