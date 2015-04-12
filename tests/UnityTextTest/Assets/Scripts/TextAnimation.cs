using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TextAnimation : MonoBehaviour {

	public delegate void AnimationCompletedHandler(TextAnimation animation);

	public event AnimationCompletedHandler animationCompleted;
	
	public GameObject largeTextPrefab;
	public Rect bounds;
	public float scrollSpeed = 1.0f;

//	int currentSegmentIndex = 0;
	GameObject[] textObjects;
	SegmentContainer segmentContainer;

	public void StartAnimation (SegmentContainer segmentContainer)
	{
		this.segmentContainer = segmentContainer;
//		currentSegmentIndex = 0;

		Vector3 currentPos = new Vector3 (bounds.xMin, bounds.y - bounds.height, 0.0f);
//		Vector3 currentPos = new Vector3 (bounds.xMin, bounds.y, 0.0f);
		float maxWidth = bounds.width;
		textObjects = new GameObject[segmentContainer.Segments.Count];

		for (int i = 0; i < segmentContainer.Segments.Count; i++) {
			Segment segment = segmentContainer.Segments[i];
			GameObject gameObject = Instantiate(largeTextPrefab);
			
			TextMesh textMesh = gameObject.GetComponent<TextMesh>();
			textMesh.text = segment.Text;
			textMesh.transform.parent = transform;

			textMesh.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
			TextUtils.WrapTextMesh(textMesh, maxWidth);
			textObjects[i] = textMesh.gameObject;

			Renderer renderer = textMesh.GetComponent<Renderer>();
//			Color color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.0f);
//			renderer.material.color = color;

			currentPos.y -= renderer.bounds.size.y;
		}

//		StartCoroutine (AnimateInNextSegment ());
	}

	void FixedUpdate()
	{
		if (segmentContainer == null || segmentContainer.Segments == null || segmentContainer.Segments.Count == 0) {
			return;
		}

		for (int i = 0; i < segmentContainer.Segments.Count; i++) {
			GameObject textObject = textObjects[i];
			Renderer renderer = textObject.GetComponent<Renderer> ();
			
			Vector3 targetPosition = textObject.transform.position;
			targetPosition.y += scrollSpeed * Time.fixedDeltaTime;
			
			textObject.transform.position = targetPosition;

			if (i == segmentContainer.Segments.Count - 1 && targetPosition.y - renderer.bounds.size.y > bounds.yMin) {
				FinishAnimation();
			}
		}
	}

	void FinishAnimation()
	{
		Debug.Log("animation completed");

		foreach (GameObject textObject in textObjects) {
			Destroy (textObject);
		}

		textObjects = null;
		segmentContainer = null;

		animationCompleted(this);
	}

	/// <summary>
	/// Animates in the next segments. Calls itself recursively when done or
	/// triggers AnimationCompleted() when all segments are off screen.
	/// </summary>
	/*IEnumerator AnimateInNextSegment()
	{
		Segment currentSegment = segmentContainer.Segments [currentSegmentIndex];
		GameObject currentTextObject = textObjects [currentSegmentIndex];
		Renderer currentRenderer = currentTextObject.GetComponent<Renderer> ();
		
		const float fadeInDuration = 1.0f;
		const float fadeOutDuration = 1.0f;
		const float moveDuration = 1.0f;
		const float delayPerItem = 0.01f;

		float stayDuration = (currentSegment.End - currentSegment.Begin) * 0.001f;

		float top = bounds.y;
		float bottom = bounds.y - bounds.height;
		float moveDistance = currentRenderer.bounds.size.y;
		string easeType = "easeInOutQuad";

		Debug.Log ("bottom: " + bottom);

		// get start delay
		float delay = currentSegment.Begin * 0.001f;

		if (currentSegmentIndex > 0) {
			Segment previousSegment = segmentContainer.Segments [currentSegmentIndex - 1];
			delay -= previousSegment.End * 0.001f;
			delay = Mathf.Max(0.0f, delay);

			Debug.Log ("delay: " + delay);
		}

		// safety net
		DOTween.CompleteAll ();
		
		for (int i = 0; i < segmentContainer.Segments.Count; i++) {
			GameObject textObject = textObjects[i];
			Renderer renderer = textObject.GetComponent<Renderer> ();

			Vector3 targetPosition = textObject.transform.position;
			targetPosition.y += moveDistance;

			Sequence sequence = DOTween.Sequence();

//			if (textObject.transform.position.y + renderer.bounds.size.y < bottom) {
			if (i == currentSegmentIndex) {
//				Hashtable fadeArgs = new Hashtable();
//				fadeArgs.Add ("alpha", 1.0f);
//				fadeArgs.Add ("delay", delay);
//				fadeArgs.Add ("time", fadeInDuration);
//				fadeArgs.Add ("easeType", easeType);
//				iTween.FadeTo (textObject, fadeArgs);
			}
//
//			if (targetPosition.y - renderer.bounds.size.y > top) {
//				Hashtable fadeArgs = new Hashtable();
//				fadeArgs.Add ("alpha", 0.0f);
//				fadeArgs.Add ("delay", delay);
//				fadeArgs.Add ("time", fadeOutDuration);
//				fadeArgs.Add ("easeType", easeType);
//				iTween.FadeTo (textObject, fadeArgs);
//			}
			
//			Hashtable moveArgs = new Hashtable();
//			moveArgs.Add ("easeType", easeType);
//			moveArgs.Add ("delay", delay);
//			moveArgs.Add ("time", moveDuration);
//			moveArgs.Add ("position", targetPosition);
//			iTween.MoveTo (textObject, moveArgs);

//			textObject.transform.DOMove ();
		}

		yield return new WaitForSeconds(stayDuration + delay);

		currentSegmentIndex++;

		if (currentSegmentIndex >= segmentContainer.Segments.Count) {
			animationCompleted (this);
		} else {
			StartCoroutine(AnimateInNextSegment());
		}
	}*/
}
