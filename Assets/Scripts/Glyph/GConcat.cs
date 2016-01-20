using UnityEngine;
using System.Collections.Generic;

public enum VertAlignment { none, top, middle, bottom }

public class GConcat : Glyph {
	
	private List<Glyph> glyphs = new List<Glyph>();


	protected GConcat(params Glyph[] gls){
		Size = 1.0;
		Alignment = VertAlignment.top;
		AutoAlignment = HorAlignment.none;
		foreach(Glyph g in gls)
			this.glyphs.Add(g);
	}



	public double Size { get; set;}
	public double Spacing { get; set; }
	public VertAlignment Alignment { get; set; }
	public HorAlignment AutoAlignment { get; set; }

	public GConcat Resize(double size){ Size = size; return this; }
	public GConcat Space(double spacing){ Spacing = spacing; return this; }
	public GConcat Align(VertAlignment alignment){ Alignment = alignment; return this; }
	public GConcat AutoAlign(HorAlignment alignment){ AutoAlignment = alignment; return this; }



	public void Draw(double size, Vector2 pos){
		double s = size * Size;
		double xref = pos.x;
		double yref = pos.y;
		double max_w = (AutoAlignment != HorAlignment.none ? MaxWidth(s) : 0.0);

		if (Alignment == VertAlignment.middle) { yref += Height(size) * 0.5; }
		else if (Alignment == VertAlignment.bottom) { yref += Height(size); }

		foreach(Glyph g in glyphs){
			double w = g.Width(s);
			double h = g.Height(s);

			double x = xref;
			if (AutoAlignment == HorAlignment.center) { x += (max_w - w) * 0.5; }
			else if (AutoAlignment == HorAlignment.right) { x += (max_w - w); }

			double y = yref;
			if (Alignment == VertAlignment.middle) { y -= h * 0.5; }
			else if (Alignment == VertAlignment.bottom) { y -= h; }

			g.Draw(s, new Vector2((float)x, (float)y));

			xref += (AutoAlignment != HorAlignment.none ? max_w : w) + Spacing * s;
		}
	}
	
	private double MaxWidth(double size){
		double w = 0.0;
		foreach(Glyph g in glyphs){
			double nw = g.Width(size);
			if(nw > w) w = nw;
		}
		return w;
	}

	private double MaxHeight(double size){
		double h = 0.0;
		foreach(Glyph g in glyphs){
			double nh = g.Height(size);
			if(nh > h) h = nh;
		}
		return h;
	}

	public double Width(double size){
		double s = size * Size;
		double w = 0.0;

		if (AutoAlignment != HorAlignment.none) {
			w = MaxWidth(s) * glyphs.Count;
		}
		else {
			foreach(Glyph g in glyphs)
				w += g.Width(s);
		}
		return w + Spacing * s * System.Math.Max(0, glyphs.Count - 1);
	}
	
	
	
	public double Height(double size){
		return MaxHeight(size * Size);
	}


	
	public static GConcat Concat(params Glyph[] glyphs){
		return new GConcat(glyphs);
	}



}
