using UnityEngine;
using System.Collections;

public class TextUtils {
	
	public static void WrapTextMesh (TextMesh textMesh, float maxWidth)
	{
		string builder = "";
		string text = textMesh.text;
		string[] parts = text.Split(' ');

		textMesh.text = "";

		Renderer renderer = textMesh.GetComponent<Renderer> ();

		for (int i = 0; i < parts.Length; i++) {
			textMesh.text += parts[i] + " ";
			
			if (renderer.bounds.size.x >= maxWidth) {
				textMesh.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
			}
			
			builder = textMesh.text;
		}

		textMesh.text = textMesh.text.TrimEnd ();
	}

	public static void LayoutTextMeshes (TextMesh[] textMeshes, float maxWidth)
	{
		float maxWidth = (canvasBottomRight.x - canvasTopLeft.x);
		Vector3 currentPos = new Vector3 (canvasTopLeft.x, canvasTopLeft.y, 0.0f);
		
		foreach (Segment segment in container.Segments) {
			GameObject gameObject = Instantiate(largeTextPrefab);
			gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
			
			TextMesh textMesh = gameObject.GetComponent<TextMesh>();
			textMesh.text = segment.Text;
			TextUtils.WrapTextMesh(textMesh, maxWidth);
			
			currentPos.y -= textMesh.GetComponent<Renderer>().bounds.size.y;
		}
	}

}
