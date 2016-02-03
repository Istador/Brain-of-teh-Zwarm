using UnityEngine;
using System;
using System.Collections.Generic;

public class GBounds : Glyph {
	
	private Glyph g;

	public double maxWidth {get; set;}
	public double maxHeight {get; set;}


	public GBounds(Glyph g, double width, double height){
		this.g = g;
		this.maxWidth = width;
		this.maxHeight = height;
	}

	private Dictionary<double, Dictionary<double, Dictionary<double, double[]>>> cache =
		new Dictionary<double, Dictionary<double, Dictionary<double, double[]>>>();

	private double[] getCache(double size){
		if (! cache.ContainsKey(maxWidth)) {
			cache.Add(maxWidth, new Dictionary<double, Dictionary<double, double[]>>());
		}

		if (! cache[maxWidth].ContainsKey(maxHeight)) {
			cache[maxWidth].Add(maxHeight, new Dictionary<double, double[]>());
		}

		if (! cache[maxWidth][maxHeight].ContainsKey(size)) {
			double corrSize = size;

			//width not bigger than max width
			double width = g.Width(corrSize);
			if (width > maxWidth) {
				corrSize *= maxWidth / width;
			}

			//height not bigger than max height
			double height = g.Height(corrSize);
			if (height > maxHeight) {
				corrSize *= maxHeight / height;
			}

			//Endwerte
			width = g.Width(corrSize);
			height = g.Height(corrSize);

			cache[maxWidth][maxHeight].Add(size, new double[]{corrSize, width, height});
		}
		return cache[maxWidth][maxHeight][size];
	}

	public void Draw(double size, Vector2 pos){
		//zeichnen
		g.Draw(getCache(size)[0], pos);

	}

	public double Width(double size){
		return getCache(size)[1];
	}

	public double Height(double size){
		return getCache(size)[2];
	}

}