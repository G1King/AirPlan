using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Ky_MainMenu : MonoBehaviour {

	private string idrouceText = "按‘方向键’控制移动，‘空格键’ 来发射子弹";
	public Texture backGroudTt;
	// Use this for initialization
	void Start () {
	
	}
	void OnGUI(){
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backGroudTt);
		GUI.Label (new Rect (20, 20, 400, 250), idrouceText);
		if (Input.anyKeyDown) {
			// 第一个方法 过期了 已经 - - 
				Application.LoadLevel ("Level1");


			// 第二个方法 需要 引入 using UnityEngine.SceneManagement; 
//			SceneManager.LoadScene("Ky_Level_1");
		}
			
	}
	// Update is called once per frame
	void Update () {
	
	}
}
