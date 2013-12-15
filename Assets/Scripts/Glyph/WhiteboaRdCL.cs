using UnityEngine;
using System.Collections.Generic;



public abstract class AbstractFont {
	public abstract float Width(char c, float size);
	public abstract float Height(char c, float size);
	public abstract void Draw(char c, float size, Vector2 pos);
	
	public static AbstractFont Selected = WhiteboaRdCL.I;
}



public class WhiteboaRdCL : AbstractFont {
	
	//abstand zwischen einzelnen Zeichen
	private const float charSpacing = 1.0f; // 1 pixel
	//pixel um die sich die Breite von einzelnen Zeichen erhöht
	private const float widthSpacing = charSpacing * 2.0f;
	
	
	
	private Texture2D font;
	
	
	
	private float sWidth;
	
	

	

	
	//offset from left to the start of character
	//calculated by: offset[px] / (image width of one char-field)[px] = off / 150
	private float[] charoffset = {
		
		0.22666667f, 0.25000000f, 0.23000000f, 0.23000000f, 0.24000000f,	// A-E
		0.27333333f, 0.12000000f, 0.19333333f, 0.38333333f, 0.23333333f,	// F-J
		0.15666667f, 0.20000000f, 0.03333333f, 0.06666667f, 0.15666667f,	// K-O
		0.22666667f, 0.13000000f, 0.23000000f, 0.20666667f, 0.13333333f,	// P-T
		0.18000000f, 0.15000000f, 0.05666667f, 0.19000000f, 0.23000000f,	// U-Y
		0.20000000f,														// X
		
		0.24000000f, 0.30666667f, 0.28000000f, 0.26666667f, 0.30000000f,	// a-e
		0.28000000f, 0.28666667f, 0.22000000f, 0.32666667f, 0.18666667f,	// f-j
		0.28666667f, 0.32000000f, 0.02000000f, 0.16666667f, 0.22000000f,	// k-o
		0.24666667f, 0.26000000f, 0.16666667f, 0.27333333f, 0.22666667f,	// p-t
		0.22000000f, 0.24666667f, 0.11333333f, 0.28000000f, 0.27333333f,	// u-y
		0.24666667f, 														// x
		
		0.19000000f, 0.27666667f, 0.23000000f, 0.30000000f, 0.18666667f,	// 0-4
		0.24666667f, 0.23000000f, 0.17000000f, 0.17333333f, 0.29000000f,	// 5-9
		
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// ...
		0f, 0f, 0f, 0f, 0f, 0f,						// ...
		
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// ...
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// ...
		0f, 0f, 0f, 0f, 0f, 0.25000000f,			// ..., Space
	};
	
	//width of char without whitespace
	//calculated by: 1 - offset[px] / (image width of one char-field)[px] * 2 = 1 - off / 75
	private float[] charscale = {
		0.54666667f, 0.50000000f, 0.54000000f, 0.54000000f, 0.52000000f,	// A-E
		0.45333333f, 0.76000000f, 0.61333333f, 0.23333333f, 0.53333333f,	// F-J
		0.68666667f, 0.60000000f, 0.93333333f, 0.86666667f, 0.68666667f,	// K-O
		0.54666667f, 0.74000000f, 0.54000000f, 0.58666667f, 0.73333333f,	// P-T
		0.64000000f, 0.70000000f, 0.88666667f, 0.62000000f, 0.54000000f,	// U-Y
		0.60000000f,														// X
		
		0.52000000f, 0.38666667f, 0.44000000f, 0.46666667f, 0.40000000f,	// a-e
		0.44000000f, 0.42666667f, 0.56000000f, 0.34666667f, 0.62666667f,	// f-j
		0.42666667f, 0.36000000f, 0.96000000f, 0.66666667f, 0.56000000f,	// k-o
		0.50666667f, 0.48000000f, 0.66666667f, 0.45333333f, 0.54666667f,	// p-t
		0.56000000f, 0.50666667f, 0.77333333f, 0.44000000f, 0.45333333f,	// u-y
		0.50666667f,														// x
		
		0.62000000f, 0.44666667f, 0.54000000f, 0.40000000f, 0.62666667f,	// 0-4
		0.50666667f, 0.54000000f, 0.66000000f, 0.65333333f, 0.42000000f,	// 5-9
		
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 	// ...
		1f, 1f, 1f, 1f, 1f, 1f,						// ...
		
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 	// ...
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 	// ...
		1f, 1f, 1f, 1f, 1f, 0.50000000f				// ..., Space
	};
	
	
	
	//to store char vectors, defining their offsets on the image
	private Vector2[] pos = new Vector2[4*26];
	
	
	
	private WhiteboaRdCL(){
		font = Resource.Texture["Font"];
		_width = (float)font.width / 26.0f;
		_height = (float)font.height / 4.0f;
		sWidth = _height / _width;
		_width *= sWidth;
		
		//Positionen initialisieren
		// 'A' = (0,0), 'B' = (1,0), ..., 'Z' = (25,0)
		// 'a' = (0,1), 'b' = (1,1), ..., 'z' = (25,1)
		// '0' = (0,2), '1' = (1,2), ..., '9' = (9,2)
		for(int i=0; i<4; i++) for(int j=0; j<26; j++)
			pos[i*26+j] = new Vector2(j, i);
	}
	
	
	
	private float _width;
	private float Width(float size){return _width * size;}
	private float Width(int index, float size){return Width(size) * charscale[index] + widthSpacing;}
	public override float Width(char c, float size){return Width(index(c), size);}
	
	
	
	private float _height;
	private float Height(float size){return _height * size;}
	private float Height(int index, float size){return Height(size);}
	public override float Height(char c, float size){return Height(index(c), size);}
	
	
	
	public override void Draw(char c, float size, Vector2 pos){
		int i = index(c);
		GUI.BeginGroup(new Rect(pos.x, pos.y, Width(i, size), Height(i, size)));
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
	
	
	
	private static Dictionary<float, Dictionary<int,Rect>> sizemap = new Dictionary<float, Dictionary<int,Rect>>();
	
	
	
	private Rect getCrop(int c, float size){
		//SubMap erstellen wenn nicht vorhanden
		if(!sizemap.ContainsKey(size))
			sizemap.Add(size, new Dictionary<int, Rect>());
		
		//Char noch nicht vorhanden
		if(!sizemap[size].ContainsKey(c)){
			Vector2 v = getPos(c);
			Rect r = new Rect(Width(size) * (-v.x - charoffset[c]) + charSpacing, -Height(size) * v.y, font.width*sWidth*size, font.height*size);
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
