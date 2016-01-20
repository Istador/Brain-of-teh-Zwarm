using UnityEngine;
using System.Collections.Generic;

public enum HorAlignment { none, left, center, right }

public class GVConcat : Glyph {
	
	private List<Glyph> glyphs = new List<Glyph>();


	protected GVConcat(params Glyph[] gls){
		Alignment = HorAlignment.left;

		foreach(Glyph g in gls)
			this.glyphs.Add(g);
	}



	public double Spacing { get; set; }
	public HorAlignment Alignment { get; set; }

	public GVConcat Space(double spacing){ Spacing = spacing; return this; }
	public GVConcat Align(HorAlignment alignment){ Alignment = alignment; return this; }



	public void Draw(double size, Vector2 pos){
		double xref = pos.x;
		double yref = pos.y;
		if (Alignment == HorAlignment.center) { xref += Width(size) * 0.5; }
		else if (Alignment == HorAlignment.right) { xref += Width(size); }

		foreach(Glyph g in glyphs){
			double w = g.Width(size);
			double h = g.Height(size);

			double x = xref;
			if (Alignment == HorAlignment.center) { x -= w * 0.5; }
			else if (Alignment == HorAlignment.right) { x -= w; }

			g.Draw(size, new Vector2((float)x, (float)yref));

			yref += h + Spacing * size;
		}
	}
	
	
	
	public double Width(double size){
		double w = 0.0;
		foreach(Glyph g in glyphs){
			double nw = g.Width(size);
			if(nw > w) w = nw;
		}
		return w;
	}
	
	
	
	public double Height(double size){
		double h = 0.0;
		foreach(Glyph g in glyphs)
			h += g.Height(size);
		return h + Spacing * size * System.Math.Max(0, glyphs.Count - 1);
	}


	
	public static GVConcat Concat(params Glyph[] glyphs){
		return new GVConcat(glyphs);
	}



}
