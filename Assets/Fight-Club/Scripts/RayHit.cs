using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//manages ray hit on an object

public class RayHit  {

	public Slider slider;
	public bool isHit =false;
	float stayTime=0; // the time the reticle has stayed inside the collider

	// Update is called once per frame
	public bool update () {
//		Debug.Log ("p");
		if (isHit) {
			if (stayTime > slider.maxValue) {
				return true;
			}

			stayTime += Time.deltaTime * 30;
			slider.value = stayTime;
			Debug.Log (stayTime);

		} else {
			slider.value=0;
			stayTime=0;
		}
		isHit = false;	
		return false;
	}

	public void reset(){
		stayTime = 0;
		isHit = false;
		slider.value = 0;
	}


}