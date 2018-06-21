//=============================================================================================================================
//
// Copyright (c) 2015-2018 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using EasyAR;

public class ImageTargetManager : MonoBehaviour
{
    private Dictionary<string, DynamicImageTagetBehaviour> imageTargetDic = new Dictionary<string, DynamicImageTagetBehaviour>();
    private FilesManager pathManager;

    void Start()
    {
        if (!pathManager)
            pathManager = FindObjectOfType<FilesManager>();
    }

    void Update()
    {
		
		if(FindObjectOfType<TargetOnTheFly>().ShowModel){
			Debug.Log ("ddms: " + FindObjectOfType<TargetOnTheFly>().ShowModel);
			var imageTargetName_FileDic = pathManager.GetDirectoryName_FileDic();
			Debug.Log ("ddms: Loading0...");
            foreach (var obj in imageTargetName_FileDic.Where(obj => !imageTargetDic.ContainsKey(obj.Key)))
            {
				Debug.Log ("ddms: " + obj.Key);
				//main factor!!!!!!!
				GameObject imageTarget = new GameObject(obj.Key);
                var behaviour = imageTarget.AddComponent<DynamicImageTagetBehaviour>();
                behaviour.Name = obj.Key;
                behaviour.Path = obj.Value.Replace(@"\", "/");
                behaviour.Storage = StorageType.Absolute;
                behaviour.Bind(ARBuilder.Instance.ImageTrackerBehaviours[0]);
                imageTargetDic.Add(obj.Key, behaviour);
            }
			Debug.Log ("ddms: Loading1...");
			FindObjectOfType<TargetOnTheFly> ().ShowModel = false;
		}
    }

    public void ClearAllTarget()
    {
        foreach (var obj in imageTargetDic)
            Destroy(obj.Value.gameObject);
        imageTargetDic = new Dictionary<string, DynamicImageTagetBehaviour>();
    }
}

