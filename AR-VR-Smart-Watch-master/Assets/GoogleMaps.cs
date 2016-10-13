using UnityEngine;
using System.Collections;

public class GoogleMaps : MonoBehaviour {

	string subject = "WORD-O-MAZE";
	string body = "PLAY THIS AWESOME. GET IT ON THE PLAYSTORE";
	string key = "AIzaSyC57-8x2IyIGaWHJcqq6xnUAjMh4wIffKU"; //put your own API key here.
	string url = "";
	/// <summary>
	/// Langitude/latitude of area. default Karachi is set
	/// </summary>
	public float lat =  24.917828f;

	public float lon = 67.097096f;
	LocationInfo li;
	/// <summary>
	/// Maps on Google Maps have an integer 'zoom level' which defines the resolution of the current view.
	/// Zoom levels between 0 (the lowest zoom level, in which the entire world can be seen on one map) and
	/// 21+ (down to streets and individual buildings) are possible within the default roadmap view.
	/// </summary>
	public int zoom = 14;
	bool loadingMap;
	/// <summary>
	/// not more then 640 * 640
	/// </summary>
	///
	public int mapWidth = 640;
	public int mapHeigh = 640;

	public enum mapType { roadmap, satellite, hybrid, terrain };
	public mapType mapSelected;
	/// <summary>
	/// scale can be 1,2 for free plan and can also be 4 for paid
	/// </summary>
	public int scale;
	IEnumerator GetGoogleMap()
	{

		Debug.Log("starting google maps");

			GameObject gameObject = GameObject.Find ("Cube1");
			/*url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
				"&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeigh + "&scale=" + scale
				+"&maptype=" + mapSelected +
				"&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=" + key;
			*/
			url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=13&size=600x300&maptype=roadmap"
					+ "&markers=color:blue%7Clabel:%7C" + lat + "," + lon
					+ "&key=" + key;
			loadingMap = true;
			WWW www = new WWW(url);
			yield return www;
			loadingMap = false;
			//Assign downloaded map texture to any gameobject e.g., plane
			if (gameObject != null) {
				gameObject.GetComponent<Renderer> ().material.mainTexture = www.texture;
			} else {
				Debug.Log ("game object null");
			}

	}


 /*public int callShareApp(){
  AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
  //AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
  //AndroidJavaObject currentActivity = new AndroidJavaObject("com.dikshay.arwatch.MainActivity");
  AndroidJavaClass jc = new AndroidJavaClass("com.dikshay.arwatch.MainActivity");
  int ret = jc.CallStatic<int>("shareText");
  return ret;
 }*/
	// Use this for initialization
	void Start () {
/*
	if(ret == 0)
	{
		StartCoroutine(setLocation());
		StartCoroutine(GetGoogleMap());
	}*/
	StartCoroutine(setLocation());
	StartCoroutine(GetGoogleMap());
	}

	// Update is called once per frame
	void Update () {
		//StartCoroutine(GetGoogleMap());
	StartCoroutine(setLocation());
	StartCoroutine(GetGoogleMap());
	}
	IEnumerator setLocation()
	{
		Debug.LogError("starting location service");
		if (!Input.location.isEnabledByUser)
		{
			Debug.Log("location service off");
			yield break;}
		else{
			Debug.Log("location service on");
		}
		Input.location.Start();
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
		if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }
		        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			lat = Input.location.lastData.latitude;
			lon = Input.location.lastData.longitude;


		}
		// Stop service if there is no need to query location updates continuously
        //Input.location.Stop();


	}

}
