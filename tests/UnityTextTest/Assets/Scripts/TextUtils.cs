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
	
}
