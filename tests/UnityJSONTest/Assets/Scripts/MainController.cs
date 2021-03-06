﻿using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class MainController : MonoBehaviour {

	JSONLoader<SegmentContainer> loader;

	void Start ()
	{
		loader = new JSONLoader<SegmentContainer> ();
		StartCoroutine (loader.Load ("http://localhost:8000/sample-segments.json", LoadSuccess, LoadError));
	}

	void LoadSuccess (SegmentContainer container)
	{
		Debug.Log ("Loaded " + container.Segments.Count + " segments");
	}

	void LoadError (string error)
	{
		Debug.Log ("Error loading files: " + error);
	}

}
