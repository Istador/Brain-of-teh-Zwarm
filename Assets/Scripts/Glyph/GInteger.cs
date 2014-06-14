using UnityEngine;

public class GInteger : Glyph, IObserver {

	private int? _value = null;
	public int? Value {
		get { return _value; }
		private set {
			//bei gleichem Wert keine Änderung
			if(_value == value) return;
			//Wert setzen
			_value = value;

			if(Value != null)
				str = GString.GetString(Value.ToString());
			else
				str = GString.GetString("");
		}
	}
	
	Glyph str;

	private string msg;

	//Konstruktor
	public GInteger(string msg){
		this.msg = msg;

		Value = (int?) Observer.I.Add(msg, this);
	}

	//Destruktor
	~GInteger(){
		Observer.I.Remove(msg, this);
	}

	public void ObserveUpdate(string msg, object x){
		if(msg != this.msg) return;
		Value = (int) x;
	}



	public void Draw(double size, Vector2 pos){
		if(str != null)
			str.Draw(size, pos);
	}



	public double Width(double size){
		if(str == null) return 0.0;
		return str.Width(size);
	}



	public double Height(double size){
		if(str == null) return 0.0;
		return str.Height(size);
	}


}
