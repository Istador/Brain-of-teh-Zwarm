using UnityEngine;
using System.Collections.Generic;



public abstract class AbstractFont {
	public abstract double Width(char c, double size);
	public abstract double Height(char c, double size);
	public abstract void Draw(char c, double size, Vector2 pos);
	
	public static AbstractFont Selected = WhiteboaRdCL.I;
}



public class WhiteboaRdCL : AbstractFont {
	
	//abstand zwischen einzelnen Zeichen
	private const double charSpacing = 1.0; // 1 pixel
	//pixel um die sich die Breite von einzelnen Zeichen erhöht
	private const double widthSpacing = charSpacing * 2.0;
	
	
	
	private Texture2D font;
	
	
	
	private double sWidth;
	
	

	

	
	//offset from left to the start of character
	//calculated by: offset[px] / (image width of one char-field)[px] = off / 150
	private double[] charoffset = {
		
		0.22666667, 0.25000000, 0.23000000, 0.23000000, 0.24000000,	// A-E
		0.27333333, 0.12000000, 0.19333333, 0.38333333, 0.23333333,	// F-J
		0.15666667, 0.20000000, 0.03333333, 0.06666667, 0.15666667,	// K-O
		0.22666667, 0.13000000, 0.23000000, 0.20666667, 0.13333333,	// P-T
		0.18000000, 0.15000000, 0.05666667, 0.19000000, 0.23000000,	// U-Y
		0.20000000,													// X
		
		0.24000000, 0.30666667, 0.28000000, 0.26666667, 0.30000000,	// a-e
		0.28000000, 0.28666667, 0.22000000, 0.32666667, 0.18666667,	// f-j
		0.28666667, 0.32000000, 0.02000000, 0.16666667, 0.22000000,	// k-o
		0.24666667, 0.26000000, 0.16666667, 0.27333333, 0.22666667,	// p-t
		0.22000000, 0.24666667, 0.11333333, 0.28000000, 0.27333333,	// u-y
		0.24666667, 												// x
		
		0.19000000, 0.27666667, 0.23000000, 0.30000000, 0.18666667,	// 0-4
		0.24666667, 0.23000000, 0.17000000, 0.17333333, 0.29000000,	// 5-9
		
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,		// ...
		0, 0, 0, 0, 0, 0,					// ...
		
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,		// ...
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,		// ...
		0, 0, 0, 0, 0, 0.25000000,			// ..., Space
	};
	
	//width of char without whitespace
	//calculated by: 1 - offset[px] / (image width of one char-field)[px] * 2 = 1 - off / 75
	private double[] charscale = {
		0.54666667, 0.50000000, 0.54000000, 0.54000000, 0.52000000,	// A-E
		0.45333333, 0.76000000, 0.61333333, 0.23333333, 0.53333333,	// F-J
		0.68666667, 0.60000000, 0.93333333, 0.86666667, 0.68666667,	// K-O
		0.54666667, 0.74000000, 0.54000000, 0.58666667, 0.73333333,	// P-T
		0.64000000, 0.70000000, 0.88666667, 0.62000000, 0.54000000,	// U-Y
		0.60000000,													// X
		
		0.52000000, 0.38666667, 0.44000000, 0.46666667, 0.40000000,	// a-e
		0.44000000, 0.42666667, 0.56000000, 0.34666667, 0.62666667,	// f-j
		0.42666667, 0.36000000, 0.96000000, 0.66666667, 0.56000000,	// k-o
		0.50666667, 0.48000000, 0.66666667, 0.45333333, 0.54666667,	// p-t
		0.56000000, 0.50666667, 0.77333333, 0.44000000, 0.45333333,	// u-y
		0.50666667,													// x
		
		0.62000000, 0.44666667, 0.54000000, 0.40000000, 0.62666667,	// 0-4
		0.50666667, 0.54000000, 0.66000000, 0.65333333, 0.42000000,	// 5-9
		
		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 	// ...
		1, 1, 1, 1, 1, 1,				// ...

		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 	// ...
		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 	// ...
		1, 1, 1, 1, 1, 0.50000000		// ..., Space
	};
	
	
	
	//to store char vectors, defining their offsets on the image
	private Vector2[] pos = new Vector2[4*26];
	
	
	
	private WhiteboaRdCL(){
		font = Resource.Texture["Font"];
		_width = (double)font.width / 26.0f;
		_height = (double)font.height / 4.0f;
		sWidth = _height / _width;
		_width *= sWidth;
		
		//Positionen initialisieren
		// 'A' = (0,0), 'B' = (1,0), ..., 'Z' = (25,0)
		// 'a' = (0,1), 'b' = (1,1), ..., 'z' = (25,1)
		// '0' = (0,2), '1' = (1,2), ..., '9' = (9,2)
		for(int i=0; i<4; i++) for(int j=0; j<26; j++)
			pos[i*26+j] = new Vector2(j, i);
	}
	
	
	
	private double _width;
	private double Width(double size){return _width * size;}
	private double Width(int index, double size){return Width(size) * charscale[index] + widthSpacing*size;}
	public override double Width(char c, double size){return Width(index(c), size);}
	
	
	
	private double _height;
	private double Height(double size){return _height * size;}
	private double Height(int index, double size){return Height(size);}
	public override double Height(char c, double size){return Height(index(c), size);}
	
	
	
	public override void Draw(char c, double size, Vector2 pos){
		int i = index(c);
		GUI.BeginGroup(new Rect(pos.x, pos.y, (float)Width(i, size), (float)Height(i, size)));
		GUI.DrawTexture(getCrop(i, size), font);
		GUI.EndGroup();
		//Debug.Log(getCrop(c, size) + " : " + Width(c, size) + "x" + Height(c, size) );
	}
	
	
	private int index(char c){
		if('A' <= c && c <= 'Z') return (c-'A');
		if('a' <= c && c <= 'z') return 26+(c-'a');
		if('0' <= c && c <= '9') return 2*26+(c-'0');
		else return 4*26-1;
	}
	
	
	private Vector2 getPos(int index){
		return pos[index];
	}
	
	private Vector2 getPos(char c){
		return pos[index(c)];
	}
	
	
	
	private static Dictionary<double, Dictionary<int,Rect>> sizemap = new Dictionary<double, Dictionary<int,Rect>>();
	
	
	
	private Rect getCrop(int c, double size){
		//SubMap erstellen wenn nicht vorhanden
		if(!sizemap.ContainsKey(size))
			sizemap.Add(size, new Dictionary<int, Rect>());
		
		//Char noch nicht vorhanden
		if(!sizemap[size].ContainsKey(c)){
			Vector2 v = getPos(c);
			Rect r = new Rect((float)(Width(size) * (-v.x - charoffset[c]) + charSpacing*size), (float)(-Height(size) * v.y), (float)(font.width*sWidth*size), (float)(font.height*size));
			sizemap[size].Add(c, r);
		}
		
		return sizemap[size][c];
	}
	
	
	
	/**
	 * Singleton
	*/
	private static WhiteboaRdCL instance = null;
	public static WhiteboaRdCL Instance{get{
			if(instance == null)
				instance = new WhiteboaRdCL();
			return instance;
		}}
	public static WhiteboaRdCL I{get{return Instance;}}
}
