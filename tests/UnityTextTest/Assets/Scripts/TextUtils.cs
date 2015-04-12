using UnityEngine;
using System.Collections;

public class TextUtils {
	
	public static void WrapTextMesh (TextMesh textMesh, float maxWidth)
	{
		string builder = "";
		string text = textMesh.text;
		string[] parts = text.Split(' ');

		textMesh.text = "";

		for (int i = 0; i < parts.Length; i++) {
			textMesh.text += parts[i] + " ";
			
			if (textMesh.GetComponent<Renderer>().bounds.extents.x >= maxWidth) {
				textMesh.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
			}
			
			builder = textMesh.text;
		}
	}
	
}
