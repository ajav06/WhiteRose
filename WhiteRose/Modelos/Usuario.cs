using System;

namespace WhiteRose
{
	public class Usuario
	{
		string codigo;
		string nombre;
		string apellido;
		string cedula;
		string fechaing;
		int tipoU;

		public Usuario (string c, string n, string a, string ce, string fi, int t)
		{
			codigo = c;
			nombre = n;
			apellido = a;
			cedula = ce;
			fechaing = fi;
			tipoU = t;
		}

		public string GetCodigo(){
			return codigo;
		}

		public string GetNombre(){
			return nombre;
		}

		public string GetApellido (){
			return apellido;
		}

		public string GetCedula(){
			return cedula;
		}

		public string GetFechaIng(){
			return fechaing;
		}

		public int GetTipoU(){
			return tipoU;
		}
	}
}

