using UnityEngine;
using System.Collections;

public class reticle : MonoBehaviour {

	private static string SKELETON_NAME="Exit";

	public Camera cameraFacing;
	private Vector3 originalScale;
	
	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		float distance;
		if (Physics.Raycast (new Ray (cameraFacing.transform.position,
		                              cameraFacing.transform.rotation * Vector3.forward),
		                     out hit)) {
			distance = hit.distance;
			if(hit.collider.gameObject.name=="Exit")
				hit.collider.gameObject.GetComponent<onExitCollision>().rayHit.isHit=true;
			else if(hit.collider.gameObject.name=="Play")
				hit.collider.gameObject.GetComponent<onPlayHit>().rayHit.isHit=true;
			else if(hit.collider.gameObject.name=="ChangeRoom")
				hit.collider.gameObject.GetComponent<onChangeRoomHit>().rayHit.isHit=true;
//			else
//				Debug.Log( hit.collider.name);
		} else {

			distance = cameraFacing.farClipPlane * 0.95f;
		}
		transform.position = cameraFacing.transform.position +
			cameraFacing.transform.rotation * Vector3.forward * distance;
		transform.LookAt (cameraFacing.transform.position);
		transform.Rotate (0.0f, 180.0f, 0.0f);
//		if (distance < 10.0f) {
//			distance *= 1 + 5*Mathf.Exp (-distance);
//		}
//		transform.localScale = originalScale * distance;

	}

//
//	void OnTriggerEnter(Collider other) {
//		//Debug.Log ("if I saw you in heaven");
//	}
//
//		

}
