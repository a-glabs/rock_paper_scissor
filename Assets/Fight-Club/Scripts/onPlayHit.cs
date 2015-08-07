using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class onPlayHit : MonoBehaviour {
	
	public RayHit rayHit =new RayHit();
	public Text activePlayersNumber;
	public Text status;

	public Slider slider;
	void Start(){
		rayHit.slider = slider;
	}

	void Update(){
	

		if(rayHit.update() || Input.GetKeyDown("p") )
			if(activePlayersNumber.text=="2")
				Application.LoadLevel("test");
			else
				status.text="waiting for player two to join";
				
	}
}

