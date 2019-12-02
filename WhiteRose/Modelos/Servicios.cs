using System;
using Gtk;

namespace WhiteRose
{
	public class Servicios
	{
		string Codigo;
		string CodigoDpto;
		string Descripcion;
		double Costo;
		double PrecioD;
		double PrecioM;

		public Servicios ()
		{
		}

		public Servicios(string c, string cd, string d, double co, double pd, double pm)
		{
			Codigo=c;
			CodigoDpto=cd;
			Descripcion=d;
			Costo=co;
			PrecioD=pd;
			PrecioM=pm;
		}

		public void SetCodigo(string c){
			Codigo=c;
		}

		public void SetCodigoDpto(string cd){
			CodigoDpto=cd;
		}

		public void SetDescripcion(string d){
			Descripcion=d;
		}

		public void SetCosto(double c){
			Costo=c;
		}

		public void SetPrecioD(double p){
			PrecioD=p;
		}

		public void SetPrecioM(double p){
			PrecioM=p;
		}

		public string GetCodigo(){
			return Codigo;
		}

		public string GetCodigoDpto(){
			return CodigoDpto;
		}

		public string GetDescripcion(){
			return Descripcion;
		}

		public double GetCosto(){
			return Costo;
		}

		public double GetPrecioD(){
			return PrecioD;
		}

		public double GetPrecioM(){
			return PrecioM;
		}
	}
}

