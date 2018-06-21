using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

public class HandlePicture : MonoBehaviour {
	[DllImport("MyAkaze")]  
	private static extern void AkazeFeatures(string input_img_name,string output_img_name);  
	[DllImport("MyAkaze")]  
	private static extern void AkazeDiscriptor(string input_img_name,string output_img_name);  
	// Use this for initialization
	// Use this for initialization
	
	//private bool ready_handle = false;
	//private UnityAction action;
	private float wid = Screen.width;
	private float hgt = Screen.height;
	private string original_path;
	private string handled_path;
	private Thread thread;
	//[SerializeField]
	//private Button CaptureButton;
 
	void Start () {
		Debug.Log ("ddms: " + "HandlePicture");
		original_path = Application.persistentDataPath + "/TempPicture/ScreenShot.jpg";
		handled_path = Application.persistentDataPath + "/TempPicture/HandledScreenShot.jpg";
		thread = new Thread (new ThreadStart(Calculate));
	}
	
	// Update is called once per frame
	void Update () {
		
			//AkazeDiscriptor (Application.persistentDataPath+"/ScreenShot.jpg",Application.persistentDataPath+"/HandledScreenShot.jpg");
	}

	public void Calculate(){
		Debug.Log ("ddms: " + "Calculate");
		AkazeDiscriptor(original_path,handled_path);
		thread.Abort ();
		FindObjectOfType<FilesManager> ().isWriting = false;
	}

	public void Click(){
		Debug.Log ("ddms: " + "Click");
		if(!File.Exists(Application.persistentDataPath + "/TempPicture")){
			Directory.CreateDirectory(Application.persistentDataPath + "/TempPicture");
			Debug.Log ("ddms: " + "Create folder...");
		}   
		Texture2D t = new Texture2D(1440,2880);  
		t.ReadPixels( new Rect(0,0,wid,hgt),0,0,false);  
		Debug.Log ("ddms:height=" + hgt + " width=" + wid);
		t.Apply();  
		byte[] byt=t.EncodeToJPG();  
		File.WriteAllBytes(original_path,byt);
		thread.Start ();

	}
}
