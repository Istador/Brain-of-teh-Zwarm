using UnityEngine;

public interface Glyph {
	void Draw(float size, Vector2 pos);
	float Width(float size);
	float Height(float size);
}
