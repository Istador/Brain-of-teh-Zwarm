using UnityEngine;
using System.Collections.Generic;

public class GImage : Glyph {

	private Texture2D img;



	public GImage(Texture2D img){
		this.img = img;
	}



	public void Draw(double size, Vector2 pos){
		Rect r = new Rect(pos.x, pos.y, (float)Width(size), (float)Height(size));
		GUI.DrawTexture(r, img, ScaleMode.ScaleToFit);
	}



	private Dictionary<double, double> widths = new Dictionary<double, double>();
	public double Width(double size){
		if(!widths.ContainsKey(size)){
			double w = ((double)img.width) * size;
			widths.Add(size, w);
		}
		return widths[size];
	}
	
	
	
	private Dictionary<double, double> heights = new Dictionary<double, double>();
	public double Height(double size){
		if(!heights.ContainsKey(size)){
			double h = ((double)img.height) * size;
			heights.Add(size, h);
		}
		return heights[size];
	}

}
