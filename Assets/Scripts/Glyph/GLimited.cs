using UnityEngine;
using System.Collections.Generic;

public class GLimited : Glyph {
		
		private double maxWidth;
		private double maxHeight;
		
		private Glyph g;
		
		public GLimited(double width, double height, Glyph g){
			this.maxWidth = width;
			this.maxHeight = height;
			this.g = g;
		}
		
		private Dictionary<double, object[]> cache = new Dictionary<double, object[]>();
		
		public void Draw(double size, Vector2 pos){
			if(!cache.ContainsKey(size)){
				
				//Ausmaße des Content Bereichs
				double contWidth = Width(size);
				double contHeight = Height(size);
				
				double corrSize = size;
				
				//width not bigger than max width
				double width = g.Width(corrSize);
				if(width > contWidth)
					corrSize *= contWidth/width;
				
				//height not bigger than max height
				double height = g.Height(corrSize);
				if(height > contHeight)
					corrSize *= contHeight/height;
				
				//Endwerte
				width = g.Width(corrSize);
				height = g.Height(corrSize);
				
				//mittig ausrichten
				float left = (float)((contWidth - width)/2.0 );
				float top = (float)((contHeight - height)/2.0 );
				
				cache.Add(size, new object[]{corrSize, new Vector2(left, top)});
			}
			
			//zeichnen
			object[] c = cache[size];
			g.Draw((double)c[0], pos + (Vector2)c[1]);
			
		}
		
		public double Width(double size){
			return maxWidth * size;
		}
		
		public double Height(double size){
			return maxHeight * size;
		}
		
	}