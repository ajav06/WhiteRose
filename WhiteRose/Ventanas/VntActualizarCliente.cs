using System;
using Gtk;
using System.Timers;

namespace WhiteRose {

	public partial class VntActualizarCliente : Validaciones
		{
		Timer ValidarBoton;
		ConexCliente cod = new ConexCliente();

		/******************************************
		* CONSTRUCTOR CON SOBRECARGA PARA USAR AL *
		*    AL INCLUIR UN NUEVO CLIENTE DESDE    *
		*      LA INTERFAZ DE PROCESAR VENTA      *
		*******************************************/

		public VntActualizarCliente () 
		{
			this.Build ();
			ColorearControles ();
			LlenarTreeView ();
			TvClientes.Model = cod.CargarClientes ();
			LblFechaHora.Text = DateTime.Now.ToString ();
			new WhiteRose.TmrFechaHora(ref LblFechaHora);
			ValidarBoton = new Timer();
			ValidarBoton.Elapsed+= new ElapsedEventHandler(OnValidarBotonElapsed);
			ValidarBoton.Interval = 100;
			ValidarBoton.Enabled = true;
		}

		public VntActualizarCliente (string Rif) 
		{
			this.Build ();
			ColorearControles ();
			LlenarTreeView ();
			TvClientes.Model = cod.CargarClientes ();
			LblFechaHora.Text = DateTime.Now.ToString ();
			EntRif.Text = Rif;
			new WhiteRose.TmrFechaHora(ref LblFechaHora);
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

		protected void OnBtnLimpiarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea limpiar todos los campos?",ButtonsType.YesNo,MessageType.Question) == ResponseType.Yes) {
				Limpiar ();
			}		
		}

		protected void OnBtnIncluirClicked (object sender, EventArgs e)
		{
			int c = cod.VerificarExistenciaCliente (EntRif.Text);
			if (c == 0) {
				if (cod.Mensaje ("¿Desea incluir al cliente?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
					Cliente cli = new Cliente(EntRif.Text,EntNombre.Text,EntDireccion.Text,EntTelefono.Text);
					cod.NuevoCliente (cli);
				}
			} else if (c == 1) {
				cod.Mensaje ("El cliente fue eliminado.\n Pónganse en contacto con el administrador del sistema.", ButtonsType.Ok, MessageType.Info);
			} else if (c == 2) {
				cod.Mensaje ("El cliente ya existe.",ButtonsType.Ok,MessageType.Info);
			}
			Limpiar ();
		}

		protected void OnBtnModificarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea actualizar al cliente?\n¡Ojo! Esta es una acción que no podrá deshacer.", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
				Cliente cli = new Cliente(EntRif.Text,EntNombre.Text,EntDireccion.Text,EntTelefono.Text);
				cod.ModificarCliente (cli);
				Limpiar ();
			}
		}

		protected void OnBtnEliminarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea eliminar al cliente?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
				cod.EliminacionLogicaCliente (EntRif.Text);
				Limpiar ();
			}
		}


		/***************************************
		* MÉTODOS PROPIOS DEL TREEVIEW (TABLA) *
		****************************************/

		protected void OnTvClientesRowActivated (object o, RowActivatedArgs args)
		{
			TreeModel model;
			TreeIter iter;
			if (TvClientes.Selection.GetSelected(out model, out iter)) {
				EntRif.Text = (string)model.GetValue (iter, 0);
				EntNombre.Text = (string)model.GetValue (iter, 1);
				EntDireccion.Text = (string)model.GetValue (iter, 2);
				EntTelefono.Text = (string)model.GetValue (iter, 3);
			}
			ValidarBoton.Enabled = false;
			BtnIncluir.Sensitive = false;
			BtnModificar.Sensitive = true;
			BtnEliminar.Sensitive = true;
			BtnLimpiar.Sensitive = true;
		}

		/************************************
		* SEÑALES DE CHANGED PARA LOS ENTRY *
		* (VALIDACIONES DE SUS CONTENIDOS)  *
		*************************************/

		protected void OnEntRifChanged (object sender, EventArgs e)
		{
			ValidarAlfanumerico (EntRif);
			TvClientes.Model=cod.BuscarUsuario (EntRif.Text.Trim ());
		}

		protected void OnEntNombreChanged (object sender, EventArgs e)
		{
			ValidarLetras (EntNombre);
		}

		protected void OnEntTelefonoChanged (object sender, EventArgs e)
		{
			ValidarNro (EntTelefono);
		}

		/**************************************
		* EVENTO ELAPSED DEL TIMER QUE VALIDA *
		*             LOS BOTONES             *
		***************************************/

		protected void OnValidarBotonElapsed (object sender, EventArgs e)
		{
			if (EntRif.Text != "" && EntNombre.Text != "" && EntDireccion.Text != "" && EntTelefono.Text != "")
				BtnIncluir.Sensitive = true;
			else
				BtnIncluir.Sensitive = false;

			if (EntRif.Text != "" || EntNombre.Text != "" || EntDireccion.Text != "" || EntTelefono.Text != "")
				BtnLimpiar.Sensitive = true;
			else
				BtnLimpiar.Sensitive = false;
		}

		/***********************
		* OTROS MÉTODOS VARIOS *
		************************/

		protected void Limpiar(){
			EntRif.Text = EntNombre.Text = EntDireccion.Text = EntTelefono.Text = "";
			EntRif.ChildFocus (DirectionType.Up);
			ValidarBoton.Enabled = true;
			BtnEliminar.Sensitive = false;
			BtnModificar.Sensitive = false;
			ActualizarTvClientes ();
		}

		public void LlenarTreeView(){
			TvClientes.AppendColumn ("Rif", new CellRendererText (), "text",0);
			TvClientes.AppendColumn ("Nombre", new CellRendererText (), "text", 1);
			TvClientes.AppendColumn ("Dirección", new CellRendererText (), "text", 2);
			TvClientes.AppendColumn ("Nro. Teléfono", new CellRendererText (), "text", 3);
		}

		protected void ActualizarTvClientes(){
			TvClientes.Model = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
			TvClientes.Model = cod.CargarClientes();
		}

		#region Coloreado de los Controles
		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			Est.EstilizarLabel (LblClientes, Est.SmokyBlack);
			Est.EstilizarLabel (LblRif, Est.SmokyBlack);
			Est.EstilizarLabel (LblNombre, Est.SmokyBlack);
			Est.EstilizarLabel (LblDireccion, Est.SmokyBlack);
			Est.EstilizarLabel (LblTelefono, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora, Est.SmokyBlack);
			Est.EstilizarBoton (BtnIncluir, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnModificar, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnEliminar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnLimpiar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnCancelar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnSalir, Est.SalmonPink, 14);
			Est.EstilizarFrame (frame2, Est.Iceberg);
			Est.EstilizarFrame (frame4, Est.Iceberg);
		}
		#endregion
	}
}