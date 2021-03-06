﻿using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	
	//Change scence
	public void Change(int sceneToChangeTo)
	{
		print ("StageClear : " + PlayerPrefs.GetInt("StageCleared"));
		print ("Scene : " + sceneToChangeTo);
		if (PlayerPrefs.GetInt("StageCleared") >= sceneToChangeTo - 3) {
			float fadeTime = GameObject.Find ("GameController").GetComponent<Fade> ().BeginFade (1);
			new WaitForSeconds (fadeTime);
			Application.LoadLevel (sceneToChangeTo);
		}
	}
}
