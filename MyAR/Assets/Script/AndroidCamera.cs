using UnityEngine;  
using System.Collections;  
using System.IO;  
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Runtime.Serialization;  
using System.Runtime .Serialization.Formatters.Binary;  
using System.Threading;  
public class AndroidCamera : MonoBehaviour   
{  
	public string deviceName;  
	private WebCamTexture tex;  
	public GameObject CameraImage;

	void Start(){
		StartCoroutine ("start");
		Application.targetFrameRate = 100;
	}


	void Update(){
		
	}


	public IEnumerator start()  
	{  
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);  
		if (Application.HasUserAuthorization(UserAuthorization.WebCam))  
		{  
			WebCamDevice[] devices = WebCamTexture.devices;  
			deviceName = devices[0].name;  
			tex = new WebCamTexture (deviceName,1920,1080);
			Debug.Log ("ddms:" + deviceName);
			CameraImage.GetComponent<Image> ().canvasRenderer.SetTexture (tex);
			CameraImage.transform.Rotate (new Vector3(0,0,0f));
			tex.Play();  
		}  
	}  

}  