using System;
using System.Timers;
using Gtk;

namespace WhiteRose
{
	public partial class VntSplashScreen : Gtk.Window
	{
		Timer TmpTemporizador;

		public VntSplashScreen () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			TmpTemporizador = new Timer();
			TmpTemporizador.Elapsed += new ElapsedEventHandler(CerrarSplashScreen);
			TmpTemporizador.Interval = 1000;
			TmpTemporizador.Enabled = true;
			//string path = Environment.CurrentDirectory + "/panda.png";
			//ImgSplashScreen.Pixbuf = new Gdk.Pixbuf(path);
		}

			protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void CerrarSplashScreen (object sender, ElapsedEventArgs e)
		{
				VntLogin Login = new VntLogin ();
				Login.Show ();
				TmpTemporizador.Enabled = false;
				this.Destroy ();
		}
	}
}

