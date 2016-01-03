﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	// Attribute in gameplay
	public List<GameObject> ListStars = new List<GameObject> ();
	public List<Sprite> ListSprites = new List<Sprite> ();
	public List<int> ListStarsCount = new List<int> ();
	
	GameObject Star;
	
	StarScript CurrentStar;
	StarScript LinkedStar;
	
	LineRenderer LightCurrentStar;
	
	Vector3 StartPoint;
	Vector3 ClickPoint;
	Vector3 EndPoint;
	
	int XLimit;
	int YLimit;
	int CurrentStarsCount;
	int Stage;
	
	Vector2[,] StarPosistion;

	//Change scence
	public void ChangeScene(int sceneToChangeTo)
	{
		float fadeTime = GameObject.Find("GameController").GetComponent<Fade>().BeginFade(1);
		new WaitForSeconds(fadeTime);
		Application.LoadLevel(sceneToChangeTo);
	}
	
	// Use this for initialization
	void Start () {
		Star = ListStars [0];
		CurrentStar = Star.GetComponent<StarScript> ();
		LightCurrentStar = Star.GetComponent<LineRenderer> ();
		Stage = PlayerPrefs.GetInt("Stage");
		
		CurrentStarsCount = ListStarsCount[Stage];
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentStar.isStar) {
			if(Input.GetMouseButtonDown(0))
			{
				ClickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				StartPoint = Star.transform.position;
				ClickPoint.z = 10;
				EndPoint = ClickPoint;
				LightCurrentStar.SetPosition(1, EndPoint);
				addColliderToLine();
			}
		}
	}
	
	public void ActiveInitStar(int StarId){
		CurrentStar.isStar = false;
		Star = ListStars [StarId];
		CurrentStar = ListStars [StarId].GetComponent<StarScript> ();
		LightCurrentStar = ListStars[StarId].GetComponent<LineRenderer> ();
		CurrentStar.isStar = true;
		CurrentStar.isActive = true;
	}
	
	public void changeSprite(int StarId){
		if (ListStars [StarId].GetComponent<SpriteRenderer> ().sprite == ListSprites [1]) {
			ListStars [StarId].GetComponent<SpriteRenderer> ().sprite = ListSprites [2];
		} else {
			ListStars [StarId].GetComponent<SpriteRenderer> ().sprite = ListSprites [1];
		}
	}
	
	public void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Star") 
		{
			print ("collide");
		}
	}
	
	
	private void addColliderToLine()
	{
		BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider> ();
		col.transform.parent = LightCurrentStar.transform; // Collider is added as child object of line
		float lineLength = Vector3.Distance (StartPoint, EndPoint); // length of line
		col.size = new Vector3 (lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
		Vector3 midPoint = (StartPoint + EndPoint)/2;
		col.transform.position = midPoint; // setting position of collider object
		
		// Following lines calculate the angle between startPos and endPos
		float angle = (Mathf.Abs (StartPoint.y - EndPoint.y) / Mathf.Abs (StartPoint.x - EndPoint.x));
		
		if((StartPoint.y<EndPoint.y && StartPoint.x>EndPoint.x) || (EndPoint.y<StartPoint.y && EndPoint.x>StartPoint.x))
		{
			angle*=-1;
		}
		
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);
		col.transform.Rotate (0, 0, angle);
	}
	
}
