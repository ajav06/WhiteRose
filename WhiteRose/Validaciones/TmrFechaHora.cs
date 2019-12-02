using System;
using Gtk;
using System.Timers;

namespace WhiteRose
{
	public class TmrFechaHora:Timer
	{
		Timer FechaHora;
		Label LblHora;
		Entry EntFecha;
		Entry EntHora;

		public TmrFechaHora (ref Label Lbl)
		{
			FechaHora = new Timer ();
			FechaHora.Elapsed += new ElapsedEventHandler(ActualizarHora);
			FechaHora.Interval = 1000;
			FechaHora.Enabled = true;
			LblHora = Lbl;
		}

		public TmrFechaHora (ref Entry Fecha, ref Entry Hora, ref Label Lbl)
		{
			FechaHora = new Timer ();
			FechaHora.Elapsed += new ElapsedEventHandler(ActualizarFechaHoraFactura);
			FechaHora.Interval = 1000;
			FechaHora.Enabled = true;
			EntFecha = Fecha;
			EntHora = Hora;
			LblHora = Lbl;
		}

		protected void ActualizarHora(object sender, ElapsedEventArgs e){
			LblHora.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
		}

		protected void ActualizarFechaHoraFactura(object sender, ElapsedEventArgs e){
			LblHora.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
			EntFecha.Text = DateTime.Now.ToString ("dd/MM/yyyy");
			EntHora.Text = DateTime.Now.ToString ("hh:mm tt");
		}
	}
}

