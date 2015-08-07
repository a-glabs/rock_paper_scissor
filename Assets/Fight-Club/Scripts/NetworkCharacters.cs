using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class NetworkCharacters : Photon.MonoBehaviour
{
	
	public ButtonDemoToggle playButton;
	GameObject leftHandModel ;
	GameObject rightHandModel ;
	AudioPlayScript audioPlayer;
	PoseManager poseManager ;
	HandController controller;
	Text result;
	string pose = "NULL";
	bool showdown = false;
	float time = 10f;
	Frame f;
	bool firstPlayer = false;
	
	void Awake ()
	{
		DontDestroyOnLoad (transform.gameObject);
	}
	
	void Start ()
	{
		audioPlayer = GameObject.Find ("SoundObject").GetComponent<AudioPlayScript> (); 
		controller = GameObject.Find ("HeadMountedHandController").GetComponent<HandController> ();
		leftHandModel = GameObject.Find ("SecondPlayerLeft");
		rightHandModel = GameObject.Find ("SecondPlayerRight");
		result = GameObject.Find ("ResultText").GetComponent<Text> ();
		poseManager = new PoseManager ();
		if (PhotonNetwork.playerList [0].name == PhotonNetwork.playerName) {
			firstPlayer = true;
			Debug.Log ("first player");
		}
		
	}
	/**
     * 
     * */
	void Update ()
	{
		
		time = time - Time.deltaTime;
		
		f = controller.GetFrame ();
		
		if (time > 5)
			result.text = "Get Ready";
		else if (time > 4)
			result.text = "";
		else if (time > 3) {
			result.text = "Rock";
			result.fontSize = 220 + (int)((time % 1) * 800);
			audioPlayer.playSound ("Rock");
		} else if (time > 2) {
			result.text = "Paper";
			result.fontSize = 220 + (int)((time % 1) * 800);
			audioPlayer.playSound ("Paper");
		} else if (time > 1) {
			
			result.text = "Scissor";
			result.fontSize = 220 + (int)((time % 1) * 800);
			audioPlayer.playSound ("Scissor");
		} else if (time > 0) {
			
			result.text = "Shoot";
			result.fontSize = 220 + (int)((time % 1) * 800);
			showdown = true;
			audioPlayer.playSound ("Shoot");
			
		}
		if (time < -10) { 
			result.fontSize = 220;
			if (time < -14)
				reset ();
			else
				result.text = "Another Bout Incoming";
		}
		
		
	}
	
	LeapHandWrapper getHand ()
	{
		
		if (f.Hands.Count != 1) {
			return null;
		}
		
		
		if (f.Hands [0].IsLeft) {
			return  new LeapHandWrapper (GameObject.Find ("ImageFullLeftHand(Clone)"), "Left", poseManager.detectGesture (f.Hands [0]));
		}
		
		if (f.Hands [0].IsRight) {
			return  new LeapHandWrapper (GameObject.Find ("ImageFullRightHand(Clone)"), "Right", poseManager.detectGesture (f.Hands [0]));
		}
		
		return null;
	}
	
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		
		if (stream.isWriting) {
			
			LeapHandWrapper realHand = getHand (); 
			
			if (realHand == null) {
				return;
			}
			
			Transform realHandContainer = realHand.hand.transform.FindChild ("HandContainer");
			
			stream.SendNext (realHand.orientation);
			stream.SendNext (realHand.pose.ToString ());
			
			stream.SendNext (realHandContainer.transform.rotation);
			
			for (int i = 0; i< realHandContainer.GetChild(0).childCount; i++) {
				
				Quaternion[] arr = new Quaternion[3];
				Transform finger = realHandContainer.GetChild (0).GetChild (i);
				
				Transform bone1 = finger;
				Transform bone2 = bone1.GetChild (0);
				Transform bone3 = bone2.GetChild (0);
				
				arr [0] = bone1.rotation;
				arr [1] = bone2.rotation;
				arr [2] = bone3.rotation;
				stream.SendNext (arr);
				
			}
			
			//stream.SendNext(playButton.ToggleState);
			//stream.SendNext(showdown);
			
			
		} else {
			
			string handOrientation = (string)stream.ReceiveNext (); 
			
			string otherPose = (string)stream.ReceiveNext ();
			
			Frame f = controller.GetFrame ();
			
			if (f.Hands.Count != 1) {
				return;
			}
			
			string pose = poseManager.detectGesture (f.Hands [0]).ToString ();
			
			Transform modelHandContainer = getHandContainer (handOrientation);
			
			modelHandContainer.transform.rotation = (Quaternion)stream.ReceiveNext ();
			
			for (int i = 0; i< modelHandContainer.GetChild(0).childCount; i++) {
				Quaternion[] data = (Quaternion[])stream.ReceiveNext ();
				
				Transform finger = modelHandContainer.GetChild (0).GetChild (i);
				
				Transform bone1 = finger;
				Transform bone2 = bone1.GetChild (0);
				Transform bone3 = bone2.GetChild (0);
				
				bone1.rotation = data [0];
				bone2.rotation = data [1];
				bone3.rotation = data [2];
			}
			
			
			if (showdown) {
				
				PoseManager.RESULT winStatus = poseManager.didIWin (pose, otherPose);
				if (winStatus != PoseManager.RESULT.BLANK) 
					if (winStatus == PoseManager.RESULT.INCORRECT_POSE)
						poseManager.reset ();
				result.text = winStatus.ToString();
				
			}
			
		}
	}
	
	Transform getHandContainer (string hand)
	{
		
		if (hand == "Left") {
			rightHandModel.gameObject.SetActive (false);
			leftHandModel.SetActive (true);
			return leftHandModel.transform.FindChild ("HandContainer"); 
		} else {
			rightHandModel.SetActive (true);
			leftHandModel.SetActive (false);
			return rightHandModel.transform.FindChild ("HandContainer"); 
		}
	}
	
	
	
	/**
     * Class is used store some extra details returned by leap hand
     */
	public class LeapHandWrapper
	{
		
		public GameObject hand;
		public string orientation;
		public PoseManager.POSE pose;
		
		public LeapHandWrapper (GameObject hand, string orientation, PoseManager.POSE pose)
		{
			this.hand = hand;
			this.orientation = orientation;
			this.pose = pose;
		}
		
	}
	
	public void reset ()
	{
		poseManager.reset ();
		time = 10f;
		showdown = false;
		audioPlayer.reset ();
	}
}

