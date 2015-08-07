using UnityEngine;
using UnityEngine.UI;

public class TestMatchMaker1 : Photon.MonoBehaviour
{
	private PhotonView myPhotonView;
	//public Text numberOfPlayersText;
	GameObject firstPerson ;

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
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
		PhotonNetwork.Instantiate ("object", Vector3.zero, Quaternion.identity, 0);
		//numberOfPlayersText.text = PhotonNetwork.room.playerCount.ToString();

	}

	void OnPhotonPlayerConnected(){
		//numberOfPlayersText.text = PhotonNetwork.room.playerCount.ToString ();
	}



	void OnLevelWasLoaded(int level) {
//		if (level == 1) {
//			Debug.Log ("Level one initialised");
//			
//		}
		
	}

	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

	}
}
