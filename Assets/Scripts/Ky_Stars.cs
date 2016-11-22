using UnityEngine;
using System.Collections;

public class Ky_Stars : MonoBehaviour {

	// Use this for initialization
	public float starSpeed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float move = starSpeed * Time.deltaTime;
		transform.Translate (Vector3.down * move,Space.World);
		if (transform.position.y <= -2.1f) {
			transform.position = new Vector3 (transform.position.x, 2.2f, transform.position.z);
		}
	}
}
