using UnityEngine;
using System.Collections;

public class Ky_Players : MonoBehaviour {

	// Use this for initialization
	enum State {
	   Playing,// 开打状态
	   Boom,//爆炸
	   Invincible // 隐身状态
	}
	private State state = State.Playing;// initial
	private float shipInvisibleTime = 1.5f;// 隐身时间
	private float shipMoveOnToScreenSpeed = 30;// 爆炸后 移动到屏幕上的速度
	private float flickerSpeed = 0.2f;//飞机闪烁的频率
	private float numberOfTimesToFlicke = 8;//闪烁的时间
	private float flickeCount;// 记录闪烁的时间

	public GameObject bulletPrefabs;// 子弹预设
	public GameObject boomPrefabs;// 爆炸后飞机的预设
	public float bulletForPlaneDistance;
	public float playerSpeed;//飞机的速度
	public static int score = 0; // 得到的数
	public static int missed = 0;// 没有打到的敌人数量
	public static int life = 3;// 飞机的复活次数 默认 为 3

	void Start () {
		StartCoroutine (InitFlicker());
	}
	//初始化闪烁
	IEnumerator InitFlicker (){
		state = State.Invincible;

		while (flickeCount < numberOfTimesToFlicke) {
		  // flicker 
			//一个泛型 public T ganmeObject<T>()  T:Component Renderer 渲染器组件
			gameObject.GetComponent<Renderer> ().enabled = !gameObject.GetComponent<Renderer> ().enabled;
			if (gameObject.GetComponent<Renderer> ().enabled == true) {
				flickeCount++;
			}
			yield return new WaitForSeconds (flickerSpeed);
		}
		flickeCount = 0;
		state = State.Playing;//起飞 ----
	}
	// Update is called once per frame
	void Update () {
		if (state != State.Boom) {
			//GetAxis -- GetAxisRaw 
			float hMove = Input.GetAxisRaw("Horizontal") * playerSpeed;//水平移动
			float vMove = Input.GetAxisRaw ("Vertical") * playerSpeed;//垂直移动
		     

			if (Input.GetKey (KeyCode.UpArrow)) {
				// 向上 Time.deltaTime 从上一帧到现在所经历的时间 以秒为单位
				transform.Translate (Vector3.up * Time.deltaTime * vMove);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				transform.Translate (Vector3.down * Time.deltaTime * -vMove);
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.Translate (Vector3.left * Time.deltaTime * -hMove);
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				transform.Translate (Vector3.right * Time.deltaTime * hMove);
			}
			if (transform.position.x >= 4.8f) {
				transform.position = new Vector3 (4.8f, transform.position.y, transform.position.z);
			}
			if (transform.position.x <= -4.8f) {
				transform.position = new Vector3 (-4.8f, transform.position.y, transform.position.z);
			}
			if (transform.position.y >= 4.0f) {
				transform.position = new Vector3 (transform.position.x, 4.0f, transform.position.z);
			
			}
			if (transform.position.y <= -4f) {
				transform.position = new Vector3 (transform.position.x, -4f, transform.position.z);
			}
//						if(Input.GetKey(KeyCode.Space)) 
			   if (Input.GetButtonDown("Jump")) 
			{
				//点击了space 发射子弹  预设子弹 Quaternion.identity  初始角度 0 ， 0 ，0
				Vector3 positionF = new Vector3 (transform.position.x, transform.position.y + bulletForPlaneDistance, transform.position.z);
			    Instantiate (bulletPrefabs, positionF, Quaternion.identity);
			}
		}

	}
  

	void OnGUI(){
				
		setUI ();
	}
	void setUI(){
		GUI.Label (new Rect (10, 10, 120, 20), "Score:" + Ky_Players.score.ToString ());
		GUI.Label (new Rect (10, 30, 60, 20), "Life:" + Ky_Players.life.ToString ());
		GUI.Label (new Rect (10, 50, 60, 20), "Missed:" + Ky_Players.missed.ToString ());
	}
	//// 敌人 和 飞机 相撞 就会触发 触发器
	void OnTriggerEnter (Collider otherObject) {
		if (otherObject.tag == "enemy" && state == State.Playing) {
			Ky_Players.life--;
	 		DestroyPlnae ();//Boom
			StartCoroutine (DestroyPlnae());
		}
	
	}
	IEnumerator DestroyPlnae(){
		state = State.Boom;
		//初始化 预设
		GameObject boom =(GameObject) Instantiate(boomPrefabs,transform.position,Quaternion.identity);
		gameObject.GetComponent<Renderer> ().enabled = false;//plane  hide
		Destroy (boom.gameObject, 0.7f);
		transform.position = new Vector3 (0, -4f, transform.position.z);
		yield return new WaitForSeconds (shipInvisibleTime);
		if (Ky_Players.life <= 0) {
			Application.LoadLevel ("Lose");
			Enemy.enemyAmout = 0;
		} else
		
		{
			gameObject.GetComponent<Renderer> ().enabled = true;
			while (transform.position.y < -4f) {
				float mv = shipMoveOnToScreenSpeed * Time.deltaTime;
				transform.position = new Vector3 (0, transform.position.y + mv, transform.position.z);
				yield return 0;
			}

			state = State.Invincible;
			while (flickeCount < numberOfTimesToFlicke) {
				gameObject.GetComponent<Renderer> ().enabled = !gameObject.GetComponent<Renderer> ().enabled;
				if(gameObject.GetComponent<Renderer>().enabled == true){
					flickeCount++;
				}
				yield return new WaitForSeconds (flickerSpeed);

			}
			flickeCount = 0;
			state = State.Playing;
		}
	}
}
