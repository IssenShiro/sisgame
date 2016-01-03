using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	// Attribute in gameplay
	public List<GameObject> ListStars = new List<GameObject> ();
	public List<Sprite> ListSprites = new List<Sprite> ();
	public List<int> ListStarsCount = new List<int> ();
	public List<int> ListActvatedStars = new List<int> ();
	public List<List<int>> ListCorrectStars = new List<List<int>> ();
	
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
	int ActivatedStars = 0;
	
	Vector2[,] StarPosistion;

	// Use this for initialization
	void Start () {
		Star = ListStars [0];
		CurrentStar = Star.GetComponent<StarScript> ();
		LightCurrentStar = Star.GetComponent<LineRenderer> ();

		//Stage = PlayerPrefs.GetInt("Stage");
		Stage = Application.loadedLevel - 2;
		PlayerPrefs.SetInt("StageCleared", 1);
		//CurrentStarsCount = ListStarsCount[Stage];

		List<int> dummyCombination = new List<int> ();
		dummyCombination.Add (0);
		ListCorrectStars.Add (dummyCombination);

		List<int> firstStage = new List<int> ();
		firstStage.Add (0);
		firstStage.Add (1);
		firstStage.Add (2);
		firstStage.Add (3);
		firstStage.Add (4);
		ListCorrectStars.Add (firstStage);

		List<int> secondStage = new List<int> ();
		secondStage.Add (0);
		secondStage.Add (1);
		secondStage.Add (2);
		secondStage.Add (3);
		secondStage.Add (5);
		secondStage.Add (6);
		secondStage.Add (4);
		ListCorrectStars.Add (secondStage);

		List<int> thirdStage = new List<int> ();
		thirdStage.Add (0);
		thirdStage.Add (1);
		thirdStage.Add (2);
		thirdStage.Add (3);
		thirdStage.Add (6);
		thirdStage.Add (7);
		thirdStage.Add (4);
		thirdStage.Add (5);
		ListCorrectStars.Add (thirdStage);
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentStar.isStar) {
			if(Input.GetMouseButtonDown(0))
			{
				RemoveCollidersRecursively();
				ClickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				StartPoint = Star.transform.position;
				ClickPoint.z = 10;
				EndPoint = ClickPoint;
				LightCurrentStar.SetPosition(1, EndPoint);
				//print ("Location EndPoint x : " + EndPoint.x + " Location EndPoint y : " + EndPoint.y);
				AddColliderToLine();
			}
		}
	}
	
	public void ActiveInitStar(int StarId){
		if (ListCorrectStars [Stage][ActivatedStars] == StarId) {
			CurrentStar.isStar = false;
			Star = ListStars [StarId];
			CurrentStar = ListStars [StarId].GetComponent<StarScript> ();
			LightCurrentStar = ListStars [StarId].GetComponent<LineRenderer> ();
			CurrentStar.isStar = true;
			CurrentStar.isActive = true;
			ActivatedStars++;
			print ("CorrectStars : " + ListCorrectStars [Stage].Count + " Activated Stars : " + ActivatedStars);
		}

		if (ListCorrectStars [Stage].Count == ActivatedStars) {
			PlayerPrefs.SetInt("StageCleared", Stage);
			print(Application.loadedLevel);
			Application.LoadLevel(1);
		}
	}

	public void changeSprite(int StarId){
		if (ListCorrectStars [Stage] [ActivatedStars] == StarId) {
			if (ListStars [StarId].GetComponent<SpriteRenderer> ().sprite == ListSprites [0]) {
				StartCoroutine (SpriteLoader (StarId));
			} else {
				ListStars [StarId].GetComponent<SpriteRenderer> ().sprite = ListSprites [0];
			}
		}
	}

	public void ActivatePortal(int portalId) {
		if (ListCorrectStars [Stage][ActivatedStars] == portalId) {
			CurrentStar.isStar = false;
			/*
			Star = ListStars [portalId];
			CurrentStar = ListStars [portalId].GetComponent<StarScript> ();
			LightCurrentStar = ListStars [portalId].GetComponent<LineRenderer> (); */
			CurrentStar.isStar = true;
			CurrentStar.isActive = true;
			ActivatedStars++;
			print ("CorrectStars : " + ListCorrectStars [Stage].Count + " Activated Stars : " + ActivatedStars);
		}
		
		if (ListCorrectStars [Stage].Count == ActivatedStars) {
			PlayerPrefs.SetInt("StageCleared", Stage);
			print(Application.loadedLevel);
			Application.LoadLevel(1);
		}
	}

	public void TeleportTo(int portalId) {
		if (ListCorrectStars [Stage][ActivatedStars] == portalId) {
			CurrentStar.isStar = false;
			Star = ListStars [portalId];
			CurrentStar = ListStars [portalId].GetComponent<StarScript> ();
			LightCurrentStar = ListStars [portalId].GetComponent<LineRenderer> ();
			CurrentStar.isStar = true;
			CurrentStar.isActive = true;
			ActivatedStars++;
			print ("CorrectStars : " + ListCorrectStars [Stage].Count + " Activated Stars : " + ActivatedStars);
		}
		
		if (ListCorrectStars [Stage].Count == ActivatedStars) {
			PlayerPrefs.SetInt("StageCleared", Stage);
			print(Application.loadedLevel);
			Application.LoadLevel(1);
		}
	}
	
	public void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Star") {
			print ("collide");
		} else {
			print ("line dude");
		}
	}
	
	
	private void AddColliderToLine()
	{
		BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider> ();
		col.transform.parent = LightCurrentStar.transform; // Collider is added as child object of line
		float lineLength = Vector3.Distance (StartPoint, EndPoint); // length of line
		col.size = new Vector3 (lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
		Vector3 midPoint = (StartPoint + EndPoint)/2;
		col.transform.position = midPoint; // setting position of collider object
		
		// Following lines calculate the angle between startPos and endPos
		/*
		float angle = (Mathf.Abs (StartPoint.y - EndPoint.y) / Mathf.Abs (StartPoint.x - EndPoint.x));
		
		if((StartPoint.y<EndPoint.y && StartPoint.x>EndPoint.x) || (EndPoint.y<StartPoint.y && EndPoint.x>StartPoint.x))
		{
			angle*=-1;
		}
		
		angle = Mathf.Rad2Deg * Mathf.Atan (angle); 
		col.transform.Rotate (0, 0, angle); */

		print ("Location collider x : " + col.transform.position.x + " Location collider y : " + col.transform.position.y);
		//col.enabled{};
	}

	private void RemoveCollidersRecursively() {
		var children = new List<GameObject>();

		foreach (Transform child in LightCurrentStar.transform) children.Add(child.gameObject);
		children.ForEach (child => Destroy (child));
	}

	IEnumerator SpriteLoader (int StarId) 
	{
		ListStars [StarId].GetComponent<SpriteRenderer> ().sprite = ListSprites [1];
		yield return new WaitForSeconds(1);
		ListStars [StarId].GetComponent<SpriteRenderer> ().sprite = ListSprites [2];
	}
	
}
