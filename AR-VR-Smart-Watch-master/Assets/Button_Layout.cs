using UnityEngine;
using System.Collections.Generic;
using Vuforia;

public class Button_Layout : MonoBehaviour, IVirtualButtonEventHandler
{
    private GameObject timeCube;
    private GameObject mapCube;
    private GameObject temperatureCube;
	private GameObject backCube;
    private GameObject timeText;
    private GameObject weatherText;
	private GameObject backText;
    private GameObject wallpaper1;
	private GameObject backButton;
    // Use this for initialization
    void Start () {
        // Search for all Children from this ImageTarget with type VirtualButtonBehaviour
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            // Register with the virtual buttons TrackableBehaviour
            vbs[i].RegisterEventHandler(this);
        }
        timeCube = transform.FindChild("TimeCube").gameObject;
        mapCube = transform.FindChild("MapCube").gameObject;
        temperatureCube = transform.FindChild("TemperatureCube").gameObject;
		backCube = transform.FindChild("backCube").gameObject;
        timeText = GameObject.Find("Time_text");
        weatherText = GameObject.Find("WeatherText");
        wallpaper1 = transform.FindChild("samuzai").gameObject;
		backText = GameObject.Find ("labelBack");
		backButton = transform.FindChild ("backbtn").gameObject;

        // Time cube is active by default
        mapCube.SetActive(false);
        temperatureCube.SetActive(false);
        timeText.SetActive(true);
        weatherText.SetActive(false);
        wallpaper1.SetActive(false);
		timeCube.SetActive(true);
		backCube.SetActive (false);
		backText.SetActive (false);
		backButton.SetActive (false);

    }

    /// Called when the virtual button has just been released:
    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        /*mapCube.SetActive(false);
        temperatureCube.SetActive(false);
        timeText.SetActive(true);
        weatherText.SetActive(false);
        wallpaper1.SetActive(false);
        timeCube.SetActive(true);*/
        Debug.Log("Button released!");
    }

    /// Called when the virtual button has just been pressed:
    void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "menuBtn1":
            mapCube.SetActive(true);
			    //activate back button
			backCube.SetActive (true);
			backText.SetActive (true);
			backButton.SetActive (true);

            temperatureCube.SetActive(false);
            weatherText.SetActive(false);
            wallpaper1.SetActive(false);
			timeCube.SetActive(false);
			timeText.SetActive(false);

            break;

            case "menuBtn2":
            weatherText.SetActive(true);
            temperatureCube.SetActive(true);
			//activate back button
			backCube.SetActive (true);
			backText.SetActive (true);
			backButton.SetActive (true);

            mapCube.SetActive(false);
            wallpaper1.SetActive(false);
			timeCube.SetActive(false);
			timeText.SetActive(false);

            break;

            case "menuBtn3":
            wallpaper1.SetActive(true);
			//activate back button
			backCube.SetActive (true);
			backText.SetActive (true);
			backButton.SetActive (true);

            temperatureCube.SetActive(false);
            mapCube.SetActive(false);
            weatherText.SetActive(false);
			timeCube.SetActive(false);
			timeText.SetActive(false);

            break;

		    case "back":
			    // activate time cube
			    // deactivate everything else
			backCube.SetActive (false);
			backText.SetActive (false);
			backButton.SetActive (false);
			temperatureCube.SetActive(false);
			weatherText.SetActive(false);
			wallpaper1.SetActive(false);
			mapCube.SetActive(false);

			timeCube.SetActive(true);
			timeText.SetActive(true);
			    break;
            default:
                throw new UnityException("Button not supported: " + vb.VirtualButtonName);
                break;
        }
    }
}
