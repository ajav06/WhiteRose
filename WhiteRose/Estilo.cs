using System;
using Gtk;

namespace WhiteRose
{
	public class Estilo
	{ //Clase con colores y métodos para colorear
	  //distintos widgets.

		public Gdk.Color Aquamarine;
		public Gdk.Color ElectricBlue;
		public Gdk.Color AliceBlue;
		public Gdk.Color Grey;
		public Gdk.Color PaleOrange;
		public Gdk.Color Snow;
		public Gdk.Color Iceberg;
		public Gdk.Color PaleTurquoise;
		public Gdk.Color EnglishRed;
		public Gdk.Color LigthSteelBlue;
		public Gdk.Color LightCoral;
		public Gdk.Color SalmonPink;
		public Gdk.Color Eggshell;
		public Gdk.Color TeaGreen;
		public Gdk.Color CadetBlue;
		public Gdk.Color MagicMint;
		public Gdk.Color BabyPowder;
		public Gdk.Color SmokyBlack;
		public Gdk.Color DarkImperialBlue;
		public Gdk.Color Alabaster;
		public Gdk.Color NonPhotoBlue;
		public Gdk.Color Honeydew;
		public Pango.FontDescription Fuente;

		public Estilo ()
		{
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#59f0ff", ref ElectricBlue);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#ecf8f8", ref AliceBlue);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#64ffda", ref Aquamarine);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#eeeeee", ref Grey);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#a7ffeb", ref PaleTurquoise);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#6dc0d5", ref Iceberg);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#b8c5d6", ref LigthSteelBlue);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#fcfafa", ref Snow);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#fcdebe", ref PaleOrange);
			EnglishRed = new Gdk.Color();
			Gdk.Color.Parse ("#b24c51", ref EnglishRed);
			LightCoral = new Gdk.Color();
			Gdk.Color.Parse ("#f28086",ref LightCoral);
			SalmonPink = new Gdk.Color();
			Gdk.Color.Parse ("#ffa3a7", ref SalmonPink);
			Eggshell = new Gdk.Color();
			Gdk.Color.Parse ("#eaf0ce", ref Eggshell);
			TeaGreen = new Gdk.Color();
			Gdk.Color.Parse ("#d7e8ba", ref TeaGreen);
			CadetBlue = new Gdk.Color();
			Gdk.Color.Parse ("#4da1a9", ref CadetBlue);
			MagicMint = new Gdk.Color();
			Gdk.Color.Parse ("#a9f0d1", ref MagicMint);
			BabyPowder = new Gdk.Color();
			Gdk.Color.Parse ("#fcfcfc",ref BabyPowder);
			SmokyBlack = new Gdk.Color();
			Gdk.Color.Parse ("#0c090d",ref SmokyBlack);
			DarkImperialBlue = new Gdk.Color();
			Gdk.Color.Parse ("#08415c",ref DarkImperialBlue);
			Alabaster = new Gdk.Color();
			Gdk.Color.Parse ("#e6efe9",ref Alabaster);
			NonPhotoBlue = new Gdk.Color();
			Gdk.Color.Parse ("#9fdaea",ref NonPhotoBlue);
			Honeydew = new Gdk.Color();
			Gdk.Color.Parse ("#e5ffef", ref Honeydew);
			Fuente = Pango.FontDescription.FromString ("Sawasdee Bold 12");
		}

		public void EstilizarBoton(Button Boton, Gdk.Color Color, int Tamaño){
			Boton.ModifyBg (StateType.Normal, Color);
			Boton.ModifyBg (StateType.Active, Color);
			Boton.ModifyBg (StateType.Prelight, Color);
			Boton.Child.ModifyFg (StateType.Normal,SmokyBlack);
			Boton.Child.ModifyFont (Fuente);
		}

		public void EstilizarLabel(Label Lbl, Gdk.Color Color){
			Lbl.ModifyFg (StateType.Normal,Color);
			Lbl.ModifyFont (Fuente);
		}

		public void EstilizarFrame(Frame Frm, Gdk.Color Color){
			Frm.ModifyBg (StateType.Normal,Color);
		}

		public void EstilizarEntry(Entry Ent, Gdk.Color Color){
			Ent.ModifyBg (StateType.Normal, Color);
			Ent.ModifyBg (StateType.Active, Color);
			Ent.ModifyText (StateType.Normal, SmokyBlack);
			Ent.ModifyFont (Fuente);
		}

		public void EstilizarTreeView(TreeView Tv, Gdk.Color Color){
			Tv.ModifyBg (StateType.Normal, Color);
			Tv.ModifyBg (StateType.Active, Color);
			Tv.ModifyText (StateType.Normal, SmokyBlack);
			Tv.ModifyText (StateType.Active, SmokyBlack);
			Tv.ModifyFont (Fuente);
		}

		public void EstilizarRadioButton(RadioButton Rb, Gdk.Color Color){
			Rb.Child.ModifyFg (StateType.Normal, Color);
			Rb.Child.ModifyFg (StateType.Active, Color);
			Rb.Child.ModifyFont (Fuente);
		}
	}
}
