using UnityEngine;
using System.Collections;

public class TimeScript : MonoBehaviour {

	public string currentTime;
	// Use this for initialization
	void Start () {
		currentTime = System.DateTime.Now.ToString ("HH:mm:ss");
		// Debug.Log ("Time recorded in start is " + currentTime);
		GameObject.Find ("Time_text").GetComponent<TextMesh> ().text = currentTime;

	}

	// Update is called once per frame
	void Update () {
		currentTime = System.DateTime.Now.ToString ("HH:mm:ss");
		// Debug.Log ("Time recorded in update is " + currentTime);
		GameObject.Find ("Time_text").GetComponent<TextMesh> ().text = currentTime;
	}
}
