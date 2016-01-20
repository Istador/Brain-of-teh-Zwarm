using UnityEngine;
using System.Collections.Generic;

public class GImage : Glyph {

	private Texture2D img;


	public double Size { get; set;}


	public GImage(Texture2D img, double size = 1.0){
		this.img = img;
		Size = size;
	}



	public void Draw(double size, Vector2 pos){
		Rect r = new Rect(pos.x, pos.y, (float)Width(size), (float)Height(size));
		GUI.DrawTexture(r, img, ScaleMode.ScaleToFit);
	}



	private Dictionary<double, double> widths = new Dictionary<double, double>();
	public double Width(double size){
		double s = size * Size;
		if(!widths.ContainsKey(s)){
			double w = ((double)img.width) * s;
			widths.Add(s, w);
		}
		return widths[s];
	}
	
	
	
	private Dictionary<double, double> heights = new Dictionary<double, double>();
	public double Height(double size){
		double s = size * Size;
		if(!heights.ContainsKey(s)){
			double h = ((double)img.height) * s;
			heights.Add(s, h);
		}
		return heights[s];
	}

}
