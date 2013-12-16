using UnityEngine;

public interface Glyph {
	void Draw(double size, Vector2 pos);
	double Width(double size);
	double Height(double size);
}
