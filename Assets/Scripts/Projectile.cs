using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float projectileSpeed;
	
	private Transform myTransform;
	public GameObject explosionPrefab;
    private Enemy enemy;
    
	// Use this for initialization
	void Start () 
  {
	myTransform=transform;
	enemy = (Enemy)GameObject.Find("Enemy").GetComponent("Enemy");
	
	}
	
	// Update is called once per frame
	void Update () {
		
	float amtToMove=projectileSpeed*Time.deltaTime;
		myTransform.Translate(Vector3.up*amtToMove);
		if(myTransform.position.y >= 4.0f)
		{
			Destroy(gameObject);
		}
		
	}
	void OnTriggerEnter(Collider otherObject)
	{
		
		
		if(otherObject.tag == "enemy" )
		{
			Debug.Log ("yes");
			Destroy(this.gameObject);
            Enemy.enemyAmout++;
			
		GameObject explosion=(GameObject)Instantiate(explosionPrefab,enemy.transform.position,enemy.transform.rotation);
        if (Enemy.enemyAmout % 10 == 0)
        {
            enemy.minSpeed += .5f;
            enemy.maxSpeed += .5f;
        }
            enemy.SetPositionSpeed();
			Destroy(explosion.gameObject,0.7f);
			Ky_Players.score+=100;
			if (Ky_Players.score >= 100)
            {
				Application.LoadLevel("Win");
                Enemy.enemyAmout = 0;
				Ky_Players.score = 0;
            }
			
			
		}
		
	}
  
   
	
}
