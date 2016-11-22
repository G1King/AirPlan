using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float minSpeed;
	public float maxSpeed;
    public static int enemyAmout = 0;
	private float currentSpeed;
	private float x, y, z;


	// Use this for initialization
	void Start ()
	{
		SetPositionSpeed ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		float speed = minSpeed * Time.deltaTime;
		transform.Translate (Vector3.down * speed,Space.World);
		if (transform.position.y < -4.3f) {
			SetPositionSpeed ();
			Ky_Players.missed++;

		}
		
	}

	public void SetPositionSpeed ()
	{
        

		x = Random.Range (-4.6f, 2.2f);
		y = 4.0f;
		z = 0.0f;
		
		transform.position = new Vector3 (x, y, z);

	}
}
