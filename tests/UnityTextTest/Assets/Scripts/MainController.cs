using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

[RequireComponent(typeof(TextAnimation))]
public class MainController : MonoBehaviour {
	
	JSONLoader<SegmentContainer> loader;
	TextAnimation textAnimation;

	void Start ()
	{
		loader = new JSONLoader<SegmentContainer> ();

		textAnimation = GetComponent<TextAnimation> ();
		textAnimation.animationCompleted += AnimationCompleted;

		LoadNextAnimation ();
	}

	void LoadNextAnimation ()
	{
		StartCoroutine (loader.Load ("http://localhost:8000/sample-segments.json", LoadSuccess, LoadError));
	}

	void LoadSuccess (SegmentContainer container)
	{
		Debug.Log ("Loaded " + container.Segments.Count + " segments");

		textAnimation.StartAnimation (container);
	}

	void LoadError (string error)
	{
		Debug.Log ("Error loading files: " + error);
	}

	void AnimationCompleted (TextAnimation animation)
	{
		LoadNextAnimation ();
	}

}
