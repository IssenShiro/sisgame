using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	//Change scence
	public void Change(int sceneToChangeTo)
	{
		float fadeTime = GameObject.Find("GameController").GetComponent<Fade>().BeginFade(1);
		new WaitForSeconds(fadeTime);
		Application.LoadLevel(sceneToChangeTo);
	}
}
