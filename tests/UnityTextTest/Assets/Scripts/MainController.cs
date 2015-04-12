using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class MainController : MonoBehaviour {

	public GameObject largeTextPrefab;
	public Vector3 canvasTopLeft;
	public Vector3 canvasBottomRight;

	JSONLoader<SegmentContainer> loader;

	void Start ()
	{
		loader = new JSONLoader<SegmentContainer> ();
		StartCoroutine (loader.Load ("http://localhost:8000/sample-segments.json", LoadSuccess, LoadError));
	}

	void LoadSuccess (SegmentContainer container)
	{
		Debug.Log ("Loaded " + container.Segments.Count + " segments");

		for (int i = 0; i < container.Segments.Count; i++) {
			Segment segment = container.Segments[i];
			GameObject gameObject = Instantiate(largeTextPrefab);

			TextMesh textMesh = gameObject.GetComponent<TextMesh>();
			textMesh.text = segment.Text;
			textMesh.transform.parent = transform;
		}
		
		float maxWidth = (canvasBottomRight.x - canvasTopLeft.x);
		TextMesh[] textMeshes = GetComponentsInChildren<TextMesh> ();
		TextUtils.LayoutTextMeshes (textMeshes, canvasTopLeft, maxWidth);
	}

	void LoadError (string error)
	{
		Debug.Log ("Error loading files: " + error);
	}

}
