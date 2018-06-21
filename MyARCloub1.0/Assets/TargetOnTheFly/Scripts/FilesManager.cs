//=============================================================================================================================
//
// Copyright (c) 2015-2018 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================

using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using EasyAR;


public class FilesManager : MonoBehaviour
{
    private string MarksDirectory;
	[HideInInspector]
	public bool isWriting = false;
    private TargetOnTheFly ui;

    void Awake()
    {
        ui = FindObjectOfType<TargetOnTheFly>();
        MarksDirectory = Application.persistentDataPath + "/Picture/";
		//MarksDirectory = Application.dataPath + "/Picture/";
		Debug.Log ("ddms:" + MarksDirectory);
    }

    public void StartTakePhoto()
    {
		Debug.Log ("ddms: " + "StartTakePhoto");
		if (!Directory.Exists(MarksDirectory))
            Directory.CreateDirectory(MarksDirectory);
        if (!isWriting)
            StartCoroutine(ImageCreate());
    }

    IEnumerator ImageCreate()
    {
        isWriting = true;
        yield return new WaitForEndOfFrame();
		Debug.Log ("ddms: " + "ImageCreate");
		FindObjectOfType<HandlePicture> ().Click ();
		Debug.Log ("ddms: " + "Click Over");
	
    }

    public Dictionary<string, string> GetDirectoryName_FileDic()
    {
        if (!Directory.Exists(MarksDirectory))
            return new Dictionary<string, string>();
        return GetAllImagesFiles(MarksDirectory);
    }

    private Dictionary<string, string> GetAllImagesFiles(string path)
    {
		Debug.Log ("ddms:Pictures are finding");
		Dictionary<string, string> imgefilesDic = new Dictionary<string, string>();

        foreach (var file in Directory.GetFiles(path))
        {
			if (Path.GetExtension (file) == ".jpg" || Path.GetExtension (file) == ".bmp" || Path.GetExtension (file) == ".png") {
				imgefilesDic.Add (Path.GetFileNameWithoutExtension (file), file);
				Debug.Log ("ddms:Pictures are fonund");
			}
			else {
				Debug.Log ("ddms:No pictures are fonund");
			}
        }
        return imgefilesDic;
    }

    public void ClearTexture()
    {
        Dictionary<string, string> imageFileDic = GetAllImagesFiles(MarksDirectory);
        foreach (var path in imageFileDic)
            File.Delete(path.Value);
    }
}
