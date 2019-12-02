using System;
using Gtk;
using System.Data;
using System.Timers;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public partial class VntActualizarUsuarios : Validaciones
	{
		Timer ValidarBotones;
		ConexUsuario cod = new ConexUsuario();
		Usuario us;

		/***************************
		* CONSTRUCTOR Y DESTRUCTOR *
		****************************/

		public VntActualizarUsuarios (Usuario u) 
		{
			this.Build ();
			ColorearControles ();
			LblFechaHora.Text = DateTime.Now.ToString ();
			cod.NuevoCod (EntCodigo, "A");
			new TmrFechaHora (ref LblFechaHora);
			EntFechaIng.Text = DateTime.Now.ToString ("dd/MM/yyyy");
			ValidarBotones = new Timer ();
			ValidarBotones.Elapsed += new ElapsedEventHandler (OnValidarBotonesElapsed);
			ValidarBotones.Interval = 100;
			ValidarBotones.Enabled = true;
			LlenarTreeView ();
			TvUsuarios.Model = cod.CargarUsuarios ();
			us = u;
			EntCodigoReg.Text = us.GetCodigo();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			if (cod.Mensaje ("¿Desea volver al menú principal?\n Puede tener información sin guardar.", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) 
				this.Destroy ();
		}

		/****************************************
		* MÉTODOS DE LAS SEÑALES DE LOS BOTONES *
		*****************************************/

		protected void OnBtnModificarClicked (object sender, EventArgs e)
		{
			ModificarUsuario ();
		}

		protected void OnBtnEliminarClicked (object sender, EventArgs e)
		{
			EliminarUsuario ();
		}

		protected void OnBtnIncluirClicked (object sender, EventArgs e)
		{
			IncluirUsuario ();
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

		protected void OnBtnVerClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea ver la contraseña?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) 
				EntContrasena.Visibility=true;
		}

		protected void OnBtnLimpiarClicked (object sender, EventArgs e)
		{
			if (cod.Mensaje ("¿Desea limpiar todos los campos?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) 
				Limpiar();
		}

		/************************************
		* SEÑALES DE CHANGED PARA LOS ENTRY *
		* (VALIDACIONES DE SUS CONTENIDOS)  *
		*************************************/

		protected void OnEntUsuarioChanged (object sender, EventArgs e)
		{
			ValidarLetras (EntUsuario);
		}

		protected void OnEntNombreChanged (object sender, EventArgs e)
		{
			ValidarLetras (EntNombre);

		}

		protected void OnEntApellidoChanged (object sender, EventArgs e)
		{
			ValidarLetras (EntApellido);
		}

		protected void OnEntCedulaChanged (object sender, EventArgs e)
		{
			ValidarNro(EntCedula);
		}

		protected void OnEntCodigoRegChanged (object sender, EventArgs e)
		{
			ValidarAlfanumerico (EntCodigoReg);
		}

		protected void OnValidarBotonesElapsed (object sender, EventArgs e)
		{
			if ((EntNombre.Text != "") && (EntCedula.Text != "") && (EntApellido.Text != "") && (EntUsuario.Text != "")&& (EntPista.Text != "")&& (EntContrasena.Text != "")) 
				BtnIncluir.Sensitive = true;
			else
				BtnIncluir.Sensitive = false;

			if ((EntCodigo.Text != "") || (EntNombre.Text != "") || (EntCedula.Text != "") || (EntApellido.Text != "") || (EntUsuario.Text != "") || (EntPista.Text != "") || (EntContrasena.Text != ""))
				BtnLimpiar.Sensitive = true;
			else
				BtnLimpiar.Sensitive = false;
		}

		/***************************
		* MÉTODOS DE ACTUALIZACIÓN *
		****************************/

		protected void IncluirUsuario ()
		{
			int e = cod.ExistenciaUsuario (EntUsuario.Text);
			if (e == 0) {
				if (cod.Mensaje ("¿Desea incluir al usuario?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
					cod.NuevoUsuario (EntCodigo.Text, EntUsuario.Text, EntContrasena.Text, EntNombre.Text, EntApellido.Text, EntCedula.Text, EntFechaIng.Text, EntCodigoReg.Text, TipodeUsuario (), EntEmail.Text, EntPista.Text);
					ActualizarTvClientes ();
					EntCodigoReg.Text = us.GetCodigo ();
					EntFechaIng.Text = DateTime.Now.ToString ("dd/MM/yyyy");
					Limpiar ();
				}
			} else if (e == 1) {
				cod.Mensaje ("El nombre usuario ya existe.", ButtonsType.Ok, MessageType.Info);
				EntUsuario.ChildFocus (DirectionType.Down);
			} else if (e == 2) {
				cod.Mensaje ("El usuario que está intentando ingresar está registrado en la base de datos como eliminado.\nPara mayor información, póngase en contacto con el administrador del sistema.",ButtonsType.Ok,MessageType.Info);
				EntUsuario.ChildFocus (DirectionType.Down);
			}
		}

		protected void ModificarUsuario ()
		{
			if (cod.Mensaje ("¿Desea modificar al Usuario?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
				cod.ActualizarUsuario (EntCodigo.Text, EntUsuario.Text, EntContrasena.Text, EntNombre.Text, EntApellido.Text, EntCedula.Text, EntEmail.Text, EntPista.Text);
				ActualizarTvClientes ();
				EntCodigoReg.Text = us.GetCodigo ();
				EntFechaIng.Text = DateTime.Now.ToString ("dd/MM/yyyy");
				Limpiar ();
			}
		}

		protected void EliminarUsuario ()
		{
			if (us.GetCodigo () != EntCodigo.Text) {
				if (cod.Mensaje ("¿Desea eliminar al usuario?", ButtonsType.YesNo, MessageType.Question) == ResponseType.Yes) {
					cod.EliminacionLogicaUsuario (EntCodigo.Text);
					ActualizarTvClientes ();
					EntCodigoReg.Text = us.GetCodigo ();
					EntCodigo.ChildFocus (DirectionType.Up);
					EntCodigoReg.Text = us.GetCodigo ();
					EntFechaIng.Text = DateTime.Now.ToString ("dd/MM/yyyy");
					Limpiar ();
				}
			} else {
				cod.Mensaje ("No es posible eliminar su propio usuario.",ButtonsType.Ok,MessageType.Info);
			}
		}

		/***************************************
		* MÉTODOS PROPIOS DEL TREEVIEW (TABLA) *
		****************************************/

		public void LlenarTreeView(){
			TvUsuarios.AppendColumn ("Codigo", new CellRendererText (), "text",0);
			TvUsuarios.AppendColumn ("Nombre de Usuario", new CellRendererText (), "text", 1);
			TvUsuarios.AppendColumn ("Nombre", new CellRendererText (), "text", 3);
			TvUsuarios.AppendColumn ("Apellido", new CellRendererText (), "text", 4);
			TvUsuarios.AppendColumn ("Cedula", new CellRendererText (), "text", 5);
			TvUsuarios.AppendColumn ("Fecha de Ingreso", new CellRendererText (), "text", 6);
			TvUsuarios.AppendColumn ("Codigo del Registrador", new CellRendererText (), "text", 7);
			TvUsuarios.AppendColumn ("Tipo de Usuario", new CellRendererText (), "text", 8);
			TvUsuarios.AppendColumn ("Correo Electronico", new CellRendererText (), "text", 9);
			TvUsuarios.AppendColumn ("Pista de contrs.", new CellRendererText (), "text", 10);
		}

		protected void ActualizarTvClientes(){
			TvUsuarios.Model = cod.CargarUsuarios ();
		}

		protected void OnTvUsuariosRowActivated (object o, RowActivatedArgs args)
		{
			TreeModel model;
			TreeIter iter;
			if (TvUsuarios.Selection.GetSelected(out model, out iter)) {
				EntCodigo.Text = (string)model.GetValue (iter, 0);
				EntUsuario.Text = (string)model.GetValue (iter, 1);
				EntContrasena.Text = (string)model.GetValue (iter, 2);
				EntNombre.Text = (string)model.GetValue (iter, 3);
				EntApellido.Text = (string)model.GetValue (iter, 4);
				EntCedula.Text = (string)model.GetValue (iter, 5);
				EntFechaIng.Text = (string)model.GetValue (iter, 6);
				EntCodigoReg.Text = (string)model.GetValue (iter, 7);
				EntEmail.Text = (string)model.GetValue (iter, 9);
				EntPista.Text = (string)model.GetValue (iter, 10);
			}

			string tp = (string)model.GetValue (iter, 8);

			ValidarBotones.Enabled = false;
			BtnModificar.Sensitive = true;
			BtnEliminar.Sensitive = true; 
			BtnIncluir.Sensitive = false;
			BtnLimpiar.Sensitive = true;
			RbAdministrador.Sensitive = false;
			RbVendedor.Sensitive = false;
			RbActualizador.Sensitive = false;

			if (tp == "1")
				RbAdministrador.Active = true;
			else if (tp == "2")
				RbVendedor.Active = true;
			else
				RbActualizador.Active = true;
		}

		/***********************************************
		* VALIDACIONES VARIAS SOBRE EL TIPO DE USUARIO *
		*                 Y SU CÓDIGO                  *
		************************************************/

		protected int TipodeUsuario(){
			if (RbAdministrador.Active)
				return 1;
			else if (RbVendedor.Active)
				return 2;
			else
				return 3;
		}

		protected void OnRbActualizadorClicked (object sender, EventArgs e)
		{
			if (BtnModificar.Sensitive==false)
				cod.NuevoCod (EntCodigo, "B");
		}

		protected void OnRbVendedorClicked (object sender, EventArgs e)
		{
			if (BtnModificar.Sensitive==false)
				cod.NuevoCod (EntCodigo, "V");
		}

		protected void OnRbAdministradorClicked (object sender, EventArgs e)
		{			
			if (BtnModificar.Sensitive==false)
				cod.NuevoCod (EntCodigo, "A");
		}

		/*********************
		* MÉTODO DE LIMPIEZA *
		**********************/

		protected void Limpiar() { 
			EntUsuario.Text = "";
			EntContrasena.Text = "";
			EntNombre.Text = "";
			EntApellido.Text = "";
			EntCedula.Text = "";
			EntFechaIng.Text = "";
			EntCodigoReg.Text = "";
			EntEmail.Text = "";
			EntPista.Text = "";
			EntUsuario.ChildFocus (DirectionType.Up);
			EntCodigoReg.Text = us.GetCodigo();
			EntFechaIng.Text = DateTime.Now.ToString ("dd/MM/yyyy");
			RbAdministrador.Active = true;
			cod.NuevoCod (EntCodigo,"A");
			ValidarBotones.Enabled = true;
			BtnModificar.Sensitive = false;
			BtnEliminar.Sensitive = false;
			RbAdministrador.Sensitive = true;
			RbVendedor.Sensitive = true;
			RbActualizador.Sensitive = true;
		}

		#region Coloreado de los Controles
		public void ColorearControles ()
		{
			Estilo Est = new Estilo ();
			this.ModifyBg (StateType.Normal, Est.Grey);
			Est.EstilizarTreeView (TvUsuarios, Est.Grey);
			Est.EstilizarLabel (LblApellido, Est.SmokyBlack);
			Est.EstilizarLabel (LblCedula, Est.SmokyBlack);
			Est.EstilizarLabel (LblCodigo, Est.SmokyBlack);
			Est.EstilizarLabel (LblCodUsuarioAnadio, Est.SmokyBlack);
			Est.EstilizarLabel (LblContrasena, Est.SmokyBlack);
			Est.EstilizarLabel (LblCorreo, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaIng, Est.SmokyBlack);
			Est.EstilizarLabel (LblNombre, Est.SmokyBlack);
			Est.EstilizarLabel (LblFechaHora, Est.SmokyBlack);
			Est.EstilizarLabel (LblPista, Est.SmokyBlack);
			Est.EstilizarLabel (LblTipoUsuario, Est.SmokyBlack);
			Est.EstilizarLabel (LblUsuario, Est.SmokyBlack);
			Est.EstilizarLabel (LblUsuariosSistema, Est.SmokyBlack);
			Est.EstilizarBoton (BtnIncluir, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnModificar, Est.PaleTurquoise, 14);
			Est.EstilizarBoton (BtnEliminar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnLimpiar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnCancelar, Est.PaleOrange, 14);
			Est.EstilizarBoton (BtnSalir, Est.SalmonPink, 14);
			Est.EstilizarFrame (frame1, Est.Iceberg);
			Est.EstilizarFrame (frame2, Est.Iceberg);
			Est.EstilizarRadioButton (RbActualizador, Est.SmokyBlack);
			Est.EstilizarRadioButton (RbAdministrador, Est.SmokyBlack);
			Est.EstilizarRadioButton (RbVendedor, Est.SmokyBlack);
		}
		#endregion
	}
}
