using UnityEngine;
using UnityEngine.UI;

public class TestMatchMaker : Photon.MonoBehaviour
{
	private PhotonView myPhotonView;
	public Text numberOfPlayersText;
	public Text status;
	bool startGame = false;
	public GameObject player2;
	float timer=15.3f;
	bool disableUpdate=false;
	GameObject dataTransferObject;
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.2");
	}
	
	void OnJoinedLobby()
	{
		Debug.Log("JoinRandom");
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("JoinRandomFailed");
		PhotonNetwork.CreateRoom (null,true,true,2);
		
	}
	
	void OnJoinedRoom()
	{
		numberOfPlayersText.text = PhotonNetwork.room.playerCount.ToString();
		if (PhotonNetwork.room.playerCount == 2) {
			player2.SetActive(true);
			startGame = true;
		}
		PhotonNetwork.InstantiateInRoomOnly=true;
	}
	
	void OnPhotonPlayerDisconnected(){
		
		if (Application.loadedLevel==1)
			Application.LoadLevel ("demo");
		else if (Application.loadedLevel == 0) {
			player2.SetActive(false);
		}
		
	}
	
	void OnPhotonPlayerConnected(){
		numberOfPlayersText.text = PhotonNetwork.room.playerCount.ToString ();
		if(PhotonNetwork.room.playerCount==2)
			player2.SetActive(true);
		status.text = "Opponent Connected";
		startGame = true;
		
	}
	
	void Update(){
		
		
		if (startGame) {
			
			timer=timer-Time.deltaTime;
			
			if(timer<6 && timer>5)
				status.text="Game Starts in";
			else if(timer>1)
				status.text=(timer-(timer%1)).ToString();
			else
				Application.LoadLevel("test");
			
		}
	}
	
	
	
	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			Debug.Log ("Level one initialised");
			dataTransferObject = PhotonNetwork.Instantiate ("object", Vector3.zero,Quaternion.identity, 0);
			startGame = false;
		} else if (level == 0) { // switching form level 1 back to 0
			
			if (PhotonNetwork.room.playerCount == 1) 
				player2.SetActive (false);
			
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.JoinRandomRoom();
			
		}
		
		
	}
	
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		
	}
}


