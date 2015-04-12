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

	public static void LayoutTextMeshes (TextMesh[] textMeshes, Vector3 topLeft, float maxWidth)
	{
		Vector3 currentPos = new Vector3 (topLeft.x, topLeft.y, 0.0f);
		
		foreach (TextMesh textMesh in textMeshes) {
			textMesh.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
			TextUtils.WrapTextMesh(textMesh, maxWidth);
			currentPos.y -= textMesh.GetComponent<Renderer>().bounds.size.y;
		}
	}

}
