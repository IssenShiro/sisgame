using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	GameObject initStar;
	public List<GameObject> liststar = new List<GameObject> ();
	public List<Sprite> starsprite = new List<Sprite> ();
	StarScript firstStar;
	LineRenderer xrayInitStar;
	Vector3 endPoint;
	Vector3 startPoint;
	Vector3 finalPoint;
	int xLimit;
	int yLimit;
	int starcount;
	List<int> liststarcount = new List<int> ();

	int stage;
	Vector2[,] starposition;

    //Change scence
    public void ChangeScene(int sceneToChangeTo)
    {
        float fadeTime = GameObject.Find("GameController").GetComponent<Fade>().BeginFade(1);
        new WaitForSeconds(fadeTime);
        Application.LoadLevel(sceneToChangeTo);
    }


    // Use this for initialization
    void Start () {
		initStar = GameObject.Find ("InitStar");
		firstStar = initStar.GetComponent<StarScript> ();
		xrayInitStar = initStar.GetComponent<LineRenderer> ();
		stage = PlayerPrefs.GetInt ("stage");
			
		liststarcount.Add (0);
		liststarcount.Add (4);
		liststarcount.Add (5);
		liststarcount.Add (6);
		liststarcount.Add (6);

		starcount = liststarcount[stage];
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if star is being star
		if (firstStar.isStar) 
		{
			// On mouse down new line will be created
			if(Input.GetMouseButtonDown(0))
			{
				endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				startPoint = initStar.transform.position;
				endPoint.z = 10;
				finalPoint = endPoint;
				xrayInitStar.SetPosition(1,finalPoint);
				addColliderToLine();
			}
		}

		/*
		if (firstStar.isStar) {
			if(Input.GetMouseButtonDown(0)) {
				endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				print ("Lokasi startpoint x : " + startPoint.x + " y : " + startPoint.y);
				print ("Lokasi endpoint x : " + endPoint.x + " y : " + endPoint.y);
				startPoint = initStar.transform.position;

				if(endPoint.x < startPoint.x) {
					xLimit = -1;
					yLimit = -1;
				}
				else {
					yLimit = 1; 
					xLimit = 1;
				}

				///*
				if(endPoint.y < startPoint.y)
					xLimit = -1;
				else
					xLimit = 1; 
				//
				// y = m x

				float gradien = (Mathf.Abs (startPoint.y - endPoint.y) / Mathf.Abs (startPoint.x - endPoint.x));

				if((startPoint.y<endPoint.y && startPoint.x>endPoint.x) || (endPoint.y<startPoint.y && endPoint.x>startPoint.x))
				{
					gradien*=-1;
				}
				//gradien = Mathf.Rad2Deg * Mathf.Atan (gradien);
			

				float xHasil = endPoint.x;
				float yHasil = endPoint.y;

				//float xHasil = endPoint.x;
				//float yHasil = endPoint.y;

				finalPoint = new Vector3(xHasil, yHasil);
				print ("Lokasi endpoint hasil klik x : " + endPoint.x + " y : " + endPoint.y);
				print ("Lokasi endpoint akhir : " + xHasil + " y : " + yHasil);
				finalPoint.z = 10;

				xrayInitStar.SetPosition(1, finalPoint);
			}
		} */
	}

	public void ActiveInitStar() 
	{
		firstStar.isStar = true;
	}

	public void changeSprite(int starId)
	{
		if (liststar [starId].GetComponent<SpriteRenderer> ().sprite == starsprite [1]) {
			liststar [starId].GetComponent<SpriteRenderer> ().sprite = starsprite [2];
		} else {
			liststar [starId].GetComponent<SpriteRenderer> ().sprite = starsprite [1];
		}
	}

	public void OnCollisionEnter(Collision col)
	{
		
	}


	private void addColliderToLine()
	{
		BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider> ();
		col.transform.parent = xrayInitStar.transform; // Collider is added as child object of line
		float lineLength = Vector3.Distance (startPoint, finalPoint); // length of line
		col.size = new Vector3 (lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
		Vector3 midPoint = (startPoint + finalPoint)/2;
		col.transform.position = midPoint; // setting position of collider object

		// Following lines calculate the angle between startPos and endPos
		float angle = (Mathf.Abs (startPoint.y - finalPoint.y) / Mathf.Abs (startPoint.x - finalPoint.x));
		if((startPoint.y<finalPoint.y && startPoint.x>finalPoint.x) || (endPoint.y<finalPoint.y && endPoint.x>finalPoint.x))
		{
			angle*=-1;
		}
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);
		col.transform.Rotate (0, 0, angle);
	}
}
