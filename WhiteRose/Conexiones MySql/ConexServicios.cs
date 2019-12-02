using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public class ConexServicios : ConexBase
	{
		string[] CodigosDptos;

		public ConexServicios ()
		{
			CodigosDptos = new string[100];
		}

		public ListStore CargarServicios ()
		{ //Método que carga todos los servicios en un ListStore
			ListStore tservicios = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			cmd = new MySqlCommand("Select ServCod, DptoDesc ,ServDesc,ServCosto,ServPrecioDetal,ServPrecioMayor from tservicios,tdepartamentos where DptoCod=ServCodpto and ServEstatus='A'and DptoEstatus='A'",con);
			cmd.CommandType = CommandType.Text;
			try {
				con.Open();
				read = cmd.ExecuteReader();
				while (read.Read()) {
					tservicios.AppendValues(read [0].ToString(),read [1].ToString(),read [2].ToString(),read [3].ToString(),read [4].ToString(),read [5].ToString());
				}
			} 
			catch(Exception ex) 
		 	{
				Mensaje(ex.Message , ButtonsType.Ok , MessageType.Error);
			}
			finally
			{
				read.Close ();
				cmd.Dispose ();
				con.Dispose ();
			}
			return tservicios;
		}

		public ListStore CargarDescDpto ()
		{ //Método que carga los departamentos existentes en un lsitado.
			int cont = 0;
			ListStore tservicios = new ListStore (typeof(string));
			cmd = new MySqlCommand("Select DptoDesc,DptoCod from tdepartamentos where DptoEstatus='A'",con);
			cmd.CommandType = CommandType.Text;
			try {
				con.Open();
				read = cmd.ExecuteReader();
				while (read.Read()) {
					tservicios.AppendValues(read [0].ToString());
					CodigosDptos[cont]=read [1].ToString();
					cont++;
				}
			} 
			catch(Exception ex) 
			{
				Mensaje(ex.Message , ButtonsType.Ok , MessageType.Error);
			}
			finally
			{
				read.Close ();
				cmd.Dispose ();
				con.Close ();
			}
			return tservicios;
		}

		public int VerificarExistenciaServicio (string cod)
		{ //Método codificado que verifica si un cliente existe y si este fue eliminado o no.
		  //Devuelve entero, 0 si no existe, 1 si existe y fue eliminado, 2 si existe y está activo
			int n = 0;
			char e = '\n';

			cmd = new MySqlCommand ("select count(ServCod) from tservicios where ServCod=@cod", con);
			cmd.Parameters.AddWithValue ("@cod", cod);
			cmd.CommandType = CommandType.Text;
			try {
				con.Open ();
				n = Convert.ToInt16 (cmd.ExecuteScalar ());
			} catch (Exception ex) {
				Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
			} finally {
				cmd.Dispose ();
				con.Close ();
			}

			if (n == 1) {
				cmd = new MySqlCommand ("select ServEstatus from tservicios where ServCod=@cod", con);
				cmd.Parameters.AddWithValue ("@cod", cod);
				cmd.CommandType = CommandType.Text;
				try {
					con.Open ();
					e = Convert.ToChar(cmd.ExecuteScalar ());
				} catch (Exception ex) {
					Mensaje (ex.Message, ButtonsType.Ok, MessageType.Error);
				} finally {
					cmd.Dispose ();
					con.Close ();
				}
			}

			if (e=='A')
				n++;

			return n;
		}

		public ListStore BuscarServicio(string cod)
		{ //Método que busca un servicio con un código similar al código dado
		  //Devuelve un listado con los servicios que cumplan con esa condición.
			ListStore tservicios = new ListStore (typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
			using (con) {
			cmd = new MySqlCommand ("Select ServCod, DptoDesc ,ServDesc,ServCosto,ServPrecioDetal,ServPrecioMayor from tservicios,tdepartamentos where DptoCod=ServCodpto and ServEstatus='A'and DptoEstatus='A' and ServCod LIKE'" + cod + "%'", con);
			cmd.CommandType = CommandType.Text;
				try 
				{
					con.Open();
					read = cmd.ExecuteReader();
					while (read.Read()) {
						tservicios.AppendValues((string) read [0],read   [1].ToString(),read [2].ToString(),read  [3].ToString(),read [4].ToString(),read [5].ToString());
					}
					read.Close();
				}
				catch (Exception ex) 
				{
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
				}
				return tservicios;
			}
		}

		public void NuevoServicio (Servicios Serv)
		{ //Método que ingresa un nuevo servicio a la base de datos.
			using (con) 
			{
				cmd = new MySqlCommand("INSERT INTO tservicios (ServCod,ServCodpto,ServDesc,ServCosto,ServPrecioDetal,ServPrecioMayor,ServEstatus) values (@cod,@dpt,@des,@costo,@pred,@prem,'A')",con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue ("@cod", Serv.GetCodigo ());
				cmd.Parameters.AddWithValue ("@dpt", Serv.GetCodigoDpto ());
				cmd.Parameters.AddWithValue ("@des", Serv.GetDescripcion ());
				cmd.Parameters.AddWithValue ("@costo", Serv.GetCosto ());
				cmd.Parameters.AddWithValue ("@pred", Serv.GetPrecioD ());
				cmd.Parameters.AddWithValue ("@prem", Serv.GetPrecioM ());
				try 
				{
					con.Open ();
					cmd.ExecuteNonQuery ();
					Mensaje ("Servicio añadido con éxito.",ButtonsType.Ok,MessageType.Info);
				} 
				catch (Exception ex) 
				{
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
					return;
				} 
				finally 
				{
					cmd.Dispose ();
					con.Close ();
				}
			}
		}

		public void ModificarServicios(Servicios Serv)
		{ //Método que modifica un servicio en la base de datos.
			cmd = new MySqlCommand ("UPDATE tservicios SET ServCosto = ?cos, ServCodpto = ?dpt, ServDesc=?des, ServPrecioDetal=?pred,  ServPrecioMayor=?prem where ServCod = @cod and ServEstatus='A'", con);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue ("@cod",Serv.GetCodigo ());
			cmd.Parameters.AddWithValue ("?cos", Serv.GetCosto ());
			cmd.Parameters.AddWithValue ("?dpt", Serv.GetCodigoDpto ());
			cmd.Parameters.AddWithValue ("?des", Serv.GetDescripcion ());
			cmd.Parameters.AddWithValue ("?pred", Serv.GetPrecioD ());
			cmd.Parameters.AddWithValue ("?prem", Serv.GetPrecioM ());
			try {
				con.Open();
				cmd.ExecuteNonQuery();
				Mensaje ("Servicio modificado con éxito.",ButtonsType.Ok,MessageType.Info);
			} catch (Exception ex) {
				Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
				return;
			} finally{
				cmd.Dispose ();
				con.Close ();
			}
		}

		public void EliminacionLogicaServicio(string cod)
		{ //Método que elimina un servicio mediante el Estatus.
				cmd = new MySqlCommand ("UPDATE tservicios SET ServEstatus = 'E' where ServCod=@cod", con);
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue ("@cod",cod);
				try 
				{
					con.Open();
					cmd.ExecuteNonQuery();
					Mensaje ("Servicio eliminado con éxito.",ButtonsType.Ok,MessageType.Info);
				} 
			 	catch (Exception ex) 
				{
					Mensaje(ex.Message,ButtonsType.Ok,MessageType.Error);
					return;
				} 
				finally
				{
					cmd.Dispose ();
					con.Dispose ();
				}
		}

		public string CodDpto (int Desc)
		{ //Retorna el código del departamento en una posición dada.
			return CodigosDptos [Desc];
		}
	}
}


