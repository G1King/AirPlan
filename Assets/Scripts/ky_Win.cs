using UnityEngine;
using System.Collections;

public class ky_Win : MonoBehaviour {

	// Use this for initialization

	public Texture winBg;
	void Start () {
	
	}
	void OnGUI(){
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), winBg);

		if (Input.anyKeyDown) {
		Application.LoadLevel ("Level1");
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
