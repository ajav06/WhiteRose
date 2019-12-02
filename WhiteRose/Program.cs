using System;
using Gtk;

namespace WhiteRose
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			VntLogin win = new VntLogin ();
			win.Show ();
			Application.Run ();
		}
	}
}
