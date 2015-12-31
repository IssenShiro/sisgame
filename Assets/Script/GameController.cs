using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	GameObject initStar;
	StarScript firstStar;
	LineRenderer xrayInitStar;
	Vector3 endPoint;
	Vector3 startPoint;
	Vector3 finalPoint;
	int xLimit;
	int yLimit;

	// Use this for initialization
	void Start () {
		initStar = GameObject.Find ("InitStar");
		firstStar = initStar.GetComponent<StarScript> ();
		xrayInitStar = initStar.GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
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

				/*
				if(endPoint.y < startPoint.y)
					xLimit = -1;
				else
					xLimit = 1; 
				*/
				// y = m x

				float xHasil = 600f * xLimit;
				float yHasil = 600f * yLimit * ((endPoint.y - startPoint.y) / (endPoint.x - startPoint.x));

				finalPoint = new Vector3(xHasil, yHasil);
				print ("Lokasi endpoint hasil klik x : " + endPoint.x + " y : " + endPoint.y);
				print ("Lokasi endpoint akhir : " + xHasil + " y : " + yHasil);
				finalPoint.z = 10;

				xrayInitStar.SetPosition(1, finalPoint);
			}
		}
	}

	public void ActiveInitStar() {
		firstStar.isStar = true;
	}
}
