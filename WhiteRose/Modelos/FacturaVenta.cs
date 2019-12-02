using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace WhiteRose
{
	public class FacturaVenta
	{
		string nrofact;
		Cliente cli;
		int tipov;
		string fechafact;
		double subtotal;
		double porcdesc;
		double porciva;

		public FacturaVenta(){
			nrofact="";
		}

		public FacturaVenta (string n, Cliente c, int t, string f, double st, double pd, double pi)
		{
			nrofact = n;
			cli= c;
			tipov = t;
			fechafact = f;
			subtotal = st;
			porcdesc = pd;
			porciva = pi;
		}

		public string GetNroFact(){
			return nrofact;
		}

		public Cliente GetCliente(){
			return cli;
		}

		public int GetTipoV (){
			return tipov;
		}

		public string GetFechaFact (){
			return fechafact;
		}

		public double GetSubTotal(){
			return subtotal;
		}

		public double GetPorcDesc(){
			return porcdesc;
		}

		public double GetPorcIva (){
			return porciva;
		}

		public void SetNroFact(string n){
			nrofact=n;
		}

		public void SetCliente(Cliente c){
			cli=c;
		}

		public void SetTipoV (int t){
			tipov = t;
		}

		public void SetFechaFact (string f){
			fechafact=f;
		}

		public void SetSubTotal(double st){
			subtotal=st;
		}

		public void SetPorcDesc(double pd){
			porcdesc=pd;
		}

		public void SetPorcIva (double pi){
			porciva=pi;
		}
	}
}

