using UnityEngine;  
using UnityEngine.Networking;
using System.Collections;     
using System.Collections.Generic;  
using System.IO;
public class WWWTransport : MonoBehaviour {     

	private Toolkit tool = new Toolkit();
	private bool readyDownload = false;
	private List<string> previousImageNameList = new List<string> ();
	private List<string> downloadImageNameList = new List<string> ();

	public static List<string> imageNameList = new List<string> ();

	void Start ()     
	{      
		if(!readyDownload){
			SendImage ();
		}
	}     

	void Update ()     
	{     
		if (readyDownload) {
			StartCoroutine (GETimages("http://39.106.27.58:8088/imagemark?imagename="));
		}
	}     
		
     
	public void SendImage()     
	{     

		//登录请求 POST 把参数写在字典用 通过www类来请求     
		byte[] tempImage = File.ReadAllBytes(Application.persistentDataPath + "/TempPicture/HandledScreenShot.jpg");

		StartCoroutine(POSTimageName("http://39.106.27.58:8088/akaze",tempImage));

	}     
	//WWW	    
	IEnumerator POSTimageName(string url, byte[] bytes)     
	{     


		WWWForm form = new WWWForm();
		form.AddField ("username","SanJi");
		form.AddBinaryData ("file", bytes);
		WWW www = new WWW(url, form);
		string result = "";
		previousImageNameList = tool.deepCopyList (imageNameList);	

		yield return www;     

		if (www.error != null)     
		{     
			//POST请求失败     
			Debug.Log("ddms:error is :"+ www.error);     

		} else  
		{     
			//POST请求成功     
			Debug.Log("ddms:request ok");
			Debug.Log("ddms:return list of name:" + www.text);

			result = www.text;

			result = result.Replace("[","");
			result = result.Replace("]","");
			result = result.Replace(" ","");
			foreach(string name in result.Split(',')){
				imageNameList.Add (name);
			}

			tool.displayList (previousImageNameList,"previousImageNameList");
			tool.displayList (imageNameList,"imageNameList");

			readyDownload = true;
		}     
	}     

	//UnityWebRequest
	IEnumerator GETimages(string url){
		
		readyDownload = false;    
		int num = 0;

		downloadImageNameList = tool.FindReaptedImage (previousImageNameList,imageNameList);
		tool.displayList (downloadImageNameList,"downloadImageNameList");

		foreach(string name in downloadImageNameList){
			using(UnityWebRequest www = UnityWebRequest.Get(url + name)){
				yield return www.SendWebRequest();
				byte[] data = www.downloadHandler.data;
				File.WriteAllBytes (Application.persistentDataPath + "/Picture/Mark" + num + ".jpg",data);
				num++;
			}
		}

	}






}
