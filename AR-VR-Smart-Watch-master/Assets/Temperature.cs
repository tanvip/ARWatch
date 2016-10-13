using UnityEngine;
using System.Collections;
using System.Net;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


public class Temperature : MonoBehaviour {

    IEnumerator GetWeather()
    {
        float lat, lon;
        string returnStr = "";
        Debug.LogError("starting location service");
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("location service off");
            yield break;
        }
        else
        {
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

            // Now, get weather condition

            string weatherUrl = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=1&APPID=1de6a0c05df14e5b8c3763d4ad38ff98&mode=xml", Uri.EscapeDataString(lat.ToString()), Uri.EscapeDataString(lon.ToString()));
            var requestWUri = string.Format(weatherUrl);

            var Wrequest = WebRequest.Create(requestWUri);
            var Wresponse = (HttpWebResponse)Wrequest.GetResponse();
            if (Wresponse.StatusCode != HttpStatusCode.OK)
            {
                returnStr = "Failure from server with response code " + Wresponse.StatusCode;
                GameObject.Find("Weather_text").GetComponent<TextMesh>().text = returnStr;
            }
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Wresponse.GetResponseStream());
            try
            {
                var Wxdoc = xmldoc.GetElementsByTagName("response").Item(0).ChildNodes.Item(2).ChildNodes.Item(0);

                /*if ((Wxdoc.Element("weatherdata").IsEmpty) || (Wxdoc.Element("weatherdata") == null))
                {
                    returnStr = "No forecast records found. Enter valid city";
                    GameObject.Find("Weather_text").GetComponent<TextMesh>().text = returnStr;
                }*/

                // Loop can help in futureto iterate over multiple outputs: In case of 10 days weather forecast or so
                foreach (XElement xChild in Wxdoc)
                {
                    var day = xChild.Attribute("day").Value;
                    var temperature = xChild.Element("temperature").Attribute("day").Value;
                    //var humidity = xChild.Element("humidity").Attribute("value").Value;
                    //var humidityUnits = xChild.Element("humidity").Attribute("unit").Value;
                    //var clouds = xChild.Element("clouds").Attribute("value").Value;
                    //var windSpeedMps = xChild.Element("windSpeed").Attribute("mps").Value;
                    //var windSpeedName = xChild.Element("windSpeed").Attribute("name").Value;
                    returnStr = "Day: " + day + "\n" + "Temperature: " + temperature + "\n";
                    GameObject.Find("Weather_text").GetComponent<TextMesh>().text = returnStr;
                }
            }
            catch (Exception e)
            {
                returnStr = e.ToString();
                GameObject.Find("Weather_text").GetComponent<TextMesh>().text = returnStr;
            }
        }
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(GetWeather());
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(GetWeather());
    }
}
