﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class onExitCollision : MonoBehaviour {
	
	public RayHit rayHit =new RayHit();

	public Slider slider;
	void Start(){
		rayHit.slider = slider;
	}

	void Update(){

		if(rayHit.update())
			Application.Quit ();
	}
}

