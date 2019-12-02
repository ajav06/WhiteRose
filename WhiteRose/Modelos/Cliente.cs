using System;

namespace WhiteRose
{
	public class Cliente
	{
		string rif;
		string nombre;
		string direccion;
		string telefono;

		public Cliente ()
		{
			rif = "N";
		}

		public Cliente (string r, string n, string d, string t)
		{
			rif = r;
			nombre = n;
			direccion = d;
			telefono = t;
		}

		public string GetRif(){
			return rif;
		}

		public string GetNombre(){
			return nombre;
		}

		public string GetDireccion(){
			return direccion;
		}

		public string GetTelefono (){
			return telefono;
		}

		public void SetRif(string r){
			rif = r;
		}

		public void SetNombre(string n){
			nombre = n;
		}

		public void SetDireccion(string d){
			direccion = d;
		}

		public void SetTelefono(string t){
			telefono = t;
		}
	}
}

