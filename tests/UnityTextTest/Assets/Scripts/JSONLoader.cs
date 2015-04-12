using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class JSONLoader<ResultType> {

	public delegate void SuccessCallback(ResultType result);
	public delegate void ErrorCallback(string error);

	public IEnumerator Load (string URL, SuccessCallback successCallback, ErrorCallback errorCallback)
	{
		Debug.Log ("loading: " + URL);

		WWW www = new WWW (URL);
		yield return www;

		if (www.error != null) {
			Debug.Log ("error loading: " + URL);
			errorCallback (www.error);
			yield break;
		}

		ResultType result = JsonConvert.DeserializeObject<ResultType> (www.text);

		successCallback (result);
	}
}
