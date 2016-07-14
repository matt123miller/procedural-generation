using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour
{
	public Renderer textureRender;

	public void DrawTexture(Texture2D texture) {

		int width = texture.width;
		int height = texture.height;

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;

		textureRender.sharedMaterial.mainTexture = texture;
		textureRender.transform.localScale = new Vector3 (width, 1, height);
	}
}
