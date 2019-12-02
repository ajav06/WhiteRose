using System;
using Gtk;

namespace WhiteRose
{
	public class Validaciones : Gtk.Window
	{
		public Validaciones() : base(Gtk.WindowType.Toplevel)
		{
		}
		public void ValidarNro(Entry ent){
			string cadena = ent.Text;
			int x;
			for (x = 0; x < cadena.Length; x++){
				if(cadena[x] >= '0' && cadena[x]<='9'){}
				else
					ent.Text=cadena.Substring(0,cadena.Length - 1);

			}
		}
		public void ValidarSoloNroDecimal(Entry ent)
		{
			string cadena = ent.Text;
			int x;
			int cont=0;
			for (x = 0; x < cadena.Length; x++)
			{
				if (cadena[x] >= '0' && cadena[x] <= '9' || cadena[x]=='.' || cadena[x]==','){
					if (cadena[x] == ','|| cadena[x]=='.'){
						if(cont>=1){
							ent.Text=cadena.Substring(0,cadena.Length - 1);
						}
						else if(cadena[x] == ','){
							cont++;
						}
						else if(cadena[x] == '.'){
							ent.Text = cadena.Substring(0, cadena.Length - 1) + ',';
							cont++;
						}
					}
				}

				else
					ent.Text=cadena.Substring(0,cadena.Length - 1);
			}
		}
		public void ValidarLetras(Entry ent)
		{
			string cadena = ent.Text;
			int x;
			for (x = 0; x < cadena.Length; x++)
			{
				if (cadena[x] >= 'A' && cadena[x] <= 'Z' || cadena[x] >= 'a' && cadena[x] <= 'z' || cadena[x] == ' ') { }
				else
					ent.Text = cadena.Substring(0, cadena.Length - 1);
			}
		}
		public void ValidarEntry(Entry ent1, Entry ent2)
		{
			if (ent1.Text == "")
				ent2.IsEditable = false;
			else
				ent2.IsEditable = true;
		}

		public void ValidarAlfanumerico(Entry ent)
		{
			string cadena = ent.Text;
			int x;
			for (x = 0; x < cadena.Length; x++)
			{
				if (cadena[x] >= 'A' && cadena[x] <= 'Z' || cadena[x] >= 'a' && cadena[x] <= 'z' || cadena[x] == ' '|| cadena[x] >= '0' && cadena[x] <= '9' ) { }
				else
					ent.Text = cadena.Substring(0, cadena.Length - 1);
			}
		}

		public void ValidarRadioBUnaVez(ListStore list,RadioButton rb1,RadioButton rb2){
			if (list.IterNChildren () > 0) {
				rb1.Sensitive = false;
				rb2.Sensitive = false;
			} else {
				rb1.Sensitive = true;
				rb2.Sensitive = true;
			}		
		}

		}
}

