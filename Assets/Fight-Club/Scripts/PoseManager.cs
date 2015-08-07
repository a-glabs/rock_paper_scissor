using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
using System.Linq;
//using 

public class PoseManager {

	public enum POSE {ROCK,PAPER,SCISSOR,NULL}; 
	public enum RESULT{ WIN,LOSE,TIE,INCORRECT_POSE,BLANK};

	private  List <RESULT> detectedPoseList= new List<RESULT>();
	

	public POSE detectGesture(Hand hand){

		if (hand.SphereRadius > 90) {
			return POSE.PAPER;
		} else if (hand.Fingers [1].IsExtended 
		           && hand.Fingers [2].IsExtended
		           && !hand.Fingers [0].IsExtended 
		           && !hand.Fingers [4].IsExtended
		           && !hand.Fingers [5].IsExtended) {
			return POSE.SCISSOR;
		} else if (hand.SphereRadius < 40) {
			return POSE.ROCK;
		} else {
			return POSE.NULL;
		}

	}

	public RESULT didIWin(string myPOSE,string otherPOSE){

		//Debug.Log ("my pose" + myPOSE + " other Pose" + otherPOSE);
		if (detectedPoseList.Count < 11) {

			if (myPOSE == "NULL" || otherPOSE == "NULL")
				detectedPoseList.Add (RESULT.INCORRECT_POSE);
			else if (myPOSE == otherPOSE)
				detectedPoseList.Add (RESULT.TIE);
			else if (myPOSE == "ROCK" && otherPOSE == "SCISSOR")
				detectedPoseList.Add (RESULT.WIN);
			else if (myPOSE == "PAPER" && otherPOSE == "ROCK")
				detectedPoseList.Add (RESULT.WIN);
			else if (myPOSE == "SCISSOR" && otherPOSE == "PAPER")
				detectedPoseList.Add (RESULT.WIN);
			else 
				detectedPoseList.Add (RESULT.LOSE);

			return RESULT.BLANK;

		} else
			return (RESULT) detectedPoseList.GroupBy (x => x).OrderByDescending (x => x.Count ()).First ().Key;
	}

	public void reset(){
		detectedPoseList.Clear ();
	}
}



