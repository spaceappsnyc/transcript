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

		float maxWidth = 0.5f * (canvasBottomRight.x - canvasTopLeft.x);
		Vector3 currentPos = new Vector3 (canvasTopLeft.x, canvasTopLeft.y, 0.0f);
		
		foreach (Segment segment in container.Segments) {
			GameObject gameObject = Instantiate(largeTextPrefab);
			gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);

			TextMesh textMesh = gameObject.GetComponent<TextMesh>();
			textMesh.text = segment.Text;
			TextUtils.WrapTextMesh(textMesh, maxWidth);

			currentPos.y -= textMesh.GetComponent<Renderer>().bounds.extents.y;
		}
	}

	void LoadError (string error)
	{
		Debug.Log ("Error loading files: " + error);
	}

}
