using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {

	//Attributes
	public int idStar;
	public bool isStar;
	public bool isActive;
	public LineRenderer xray;
	Vector3 startPoint;

	// Use this for initialization
	void Start () {
		xray = GetComponent<LineRenderer> ();
		startPoint = transform.position;
		startPoint.z = 10;
		xray.SetPosition (0, startPoint);
		xray.SetPosition (1, startPoint);
		xray.SetWidth (10, 10);
		isStar = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
