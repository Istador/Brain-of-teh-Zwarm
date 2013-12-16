using UnityEngine;
using System;

public class GInteger : Glyph {

	Func<int?> f;
	int? option = null;

	Glyph str;

	public GInteger(Func<int?> f){
		this.f = f;
		recheck();
	}

	private bool recheckNeeded = true;

	private void recheck(){
		if(recheckNeeded){
			option = f();
			if(option != null)
				str = GString.GetString(option.ToString());
			else
				str = null;
			recheckNeeded = false;
		}
	}

	public void Draw(double size, Vector2 pos){
		recheck();
		if(str != null)
			str.Draw(size, pos);
		recheckNeeded = true;
	}

	public double Width(double size){
		recheck();
		if(str == null) return 0.0;
		return str.Width(size);
	}

	public double Height(double size){
		recheck();
		if(str == null) return 0.0;
		return str.Height(size);
	}
}
