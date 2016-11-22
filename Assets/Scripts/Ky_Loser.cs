using UnityEngine;
using System.Collections;

public class Ky_Loser : MonoBehaviour {

	// Use this for initialization
	public Texture loseBg;
	void Start () {
	
	}
	void OnGUI (){
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), loseBg);
		if(Input.anyKeyDown){
		Application.LoadLevel ("Level1");
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
