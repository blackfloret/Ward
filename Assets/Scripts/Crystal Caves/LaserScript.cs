using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {
	public Transform startPoint;
	public Transform endPoint;
	public Player playerScript; 
	LineRenderer laserLine;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		laserLine = GetComponentInChildren<LineRenderer> ();
		laserLine.SetWidth (.2f, .2f);
	}
	
	// Update is called once per frame
	void Update () {
		laserLine.SetPosition (0, startPoint.position);
		laserLine.SetPosition (1, endPoint.position);
		if (Physics.Linecast(startPoint.position, endPoint.position, out hit))
		{
			if (hit.collider.gameObject.tag == "Player");
			{
				Debug.Log("Player Hit");
				damage();
			}	
		}
	}

	IEnumerator damage()
	{
		print("Start waiting");
		playerScript.adjustHealth(-2);
		yield return new WaitForSeconds(1);
		print("1 second has passed");
	}
}
