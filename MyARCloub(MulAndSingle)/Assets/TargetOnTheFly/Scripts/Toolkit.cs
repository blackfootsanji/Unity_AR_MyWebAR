using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolkit{

	public Toolkit (){}


	/// <summary>
	/// remove repeated image 
	/// </summary>
	/// <returns>The image list removes the repeated image.</returns>
	/// <param name="templateList">Template list.</param>
	/// <param name="comparedList">Will be handled list.</param>
	public List<string> FindReaptedImage(List<string> templateList,List<string> comparedList){
		List<string> tempList = new List<string>();
		foreach(string name in comparedList){
			if (!templateList.Contains (name)) {
				tempList.Add (name);
			}
		}
		return tempList;
	}

	/// <summary>
	/// Deeply copy list(different address).
	/// </summary>
	/// <returns>The copy list.</returns>
	/// <param name="templateList">Template list.</param>
	public List<string> deepCopyList(List<string> templateList){
		List<string> tempList = new List<string>(templateList);
		return tempList;
	}


	public void displayList(List<string> templateList,string listName){
		foreach(string name in templateList){
			Debug.Log ("ddms:list name " + listName +" : " + name);
		}
	}


}
