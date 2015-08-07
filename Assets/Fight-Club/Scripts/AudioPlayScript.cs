using UnityEngine;
using System.Collections.Generic;


public class AudioPlayScript : MonoBehaviour {

    public AudioClip tick;
    public AudioClip clang;
    AudioSource audioSource;

    Dictionary <string,bool> soundsPlayed = new Dictionary<string,bool>();
    bool resetState=false;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource> ();
        soundsPlayed.Add ("Rock",false);
        soundsPlayed.Add ("Paper",false);
        soundsPlayed.Add ("Scissor",false);
        soundsPlayed.Add ("Shoot",false);
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void playSound(string sound){

        if(!soundsPlayed[sound]){
            if(sound=="Shoot")
                audioSource.PlayOneShot(clang);
            else
                audioSource.PlayOneShot(tick);
            resetState=false;
        }
        soundsPlayed[sound]=true;
    }

    public void reset(){
        if (resetState)
            return;

        soundsPlayed["Rock"]=false;
        soundsPlayed["Paper"]=false;
        soundsPlayed["Scissor"]=false;
        soundsPlayed["Shoot"]=false;

        resetState=true;
        
    }


}


