using UnityEngine;
using System.Collections;

public class TextAnimation : MonoBehaviour {

	public delegate void AnimationCompletedHandler(TextAnimation animation);

	public event AnimationCompletedHandler animationCompleted;

	public SegmentContainer segmentContainer;
	public GameObject largeTextPrefab;
	public Rect bounds;

	public void StartAnimation (SegmentContainer segmentContainer)
	{
		this.segmentContainer = segmentContainer;

		for (int i = 0; i < segmentContainer.Segments.Count; i++) {
			Segment segment = segmentContainer.Segments[i];
			GameObject gameObject = Instantiate(largeTextPrefab);
			
			TextMesh textMesh = gameObject.GetComponent<TextMesh>();
			textMesh.text = segment.Text;
			textMesh.transform.parent = transform;
		}
		
		float maxWidth = bounds.width;
		TextMesh[] textMeshes = GetComponentsInChildren<TextMesh> ();
		TextUtils.LayoutTextMeshes (textMeshes, new Vector3(bounds.xMin, bounds.yMin, 0.0f), maxWidth);
	}

	void Start ()
	{

	}

	void Update ()
	{
	
	}
}
