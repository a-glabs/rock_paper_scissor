using UnityEngine;
using System.Collections;
using Leap;

public class handTest : MonoBehaviour {
	//DElete this scripts 
	//This was used to test leap hadn to handmodel update 
	// Use this for initialization
	public HandController controller;
	const float MM_TO_M = 0.001f;
	public HandModel model;
	public Transform modelHandContainer;

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		Frame f = controller.GetFrame();
		Hand hand;

		if (f.Hands.Count < 1)
			return;

		hand = f.Hands [0];
		//Do left ot righty hand

		GameObject realHandLeft = GameObject.Find ("ImageFullLeftHand(Clone)");
 		Transform realHandContainer = realHandLeft.transform.FindChild("HandContainer");

        modelHandContainer.transform.rotation = realHandContainer.transform.rotation;

		Transform[] fingers = new Transform[5];

		for (int i = 0; i< realHandContainer.GetChild(0).childCount; i++) {
			fingers[i]=realHandContainer.GetChild(0).GetChild(i);
		}

	}




//	void processFingers(Transform handContainer,int count){
//
//		Transform handBone1= handContainer.GetChild(0).GetChild(count);
//		Transform handBone2 = handBone1.GetChild (0);
//		Transform handBone3 = handBone2.GetChild (0);
//
////		bones[1].rotation = Quaternion.Euler (handBone1.rotation.eulerAngles);
////		bones[2].rotation = Quaternion.Euler (handBone2.rotation.eulerAngles);
////		bones[3].rotation = Quaternion.Euler (handBone3.rotation.eulerAngles);
//
//		bones[1].localPosition = handBone1.localPosition;
//		bones[2].localPosition = handBone2.localPosition;
//		bones[3].localPosition = handBone3.localPosition;
//
//		setBoneRotation (bones[1],handBone1);
//		setBoneRotation (bones[2],handBone2);
//		setBoneRotation (bones[3],handBone3);
//
//	}
//
//	void setHandRotation(Transform handContainer){
//		model.transform.rotation = new Quaternion (
//			 handContainer.rotation.y
//		  	, handContainer.rotation.z
//		 	, handContainer.rotation.x
//			, handContainer.rotation.w);
//
//
//	}
//
//	void setPalmRotation(Transform handContainer){
//		model.palm.rotation = new Quaternion (
//			handContainer.rotation.x
//			, handContainer.rotation.z
//			, handContainer.rotation.y
//			, handContainer.rotation.w);
//		
//		
//	}
//
//	void setBoneRotation(Transform modelBone,Transform dataBone){
//		modelBone.rotation = new Quaternion (
//			 dataBone.rotation.x
//			,dataBone.rotation.z
//			,dataBone.rotation.y
//			,dataBone.rotation.w);
//	}
//


}
