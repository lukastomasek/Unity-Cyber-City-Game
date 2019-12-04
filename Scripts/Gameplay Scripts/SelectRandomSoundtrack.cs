using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomSoundtrack : MonoBehaviour
{
   
    public List<GameObject> soundTracks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        var randomSoundrack = Random.Range(0, soundTracks.Count);

        soundTracks[randomSoundrack].gameObject.SetActive(true);


        //foreach(var sound in soundTracks)
        //{
        //    var random = Random.Range(0, soundTracks.Count);
        //    soundTracks[random].gameObject.SetActive(true);
        //}
        

    }

  
}
