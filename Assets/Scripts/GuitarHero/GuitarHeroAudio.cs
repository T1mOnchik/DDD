using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GuitarHeroAudio : MonoBehaviour
{
    public List<AudioClip> musicList;
    private AudioSource mAudio;
    public int track;
    // Start is called before the first frame update
    void Start()
    {
        mAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MusicPlayer()
    {   Debug.Log("111");
        mAudio.clip = musicList[track];
        mAudio.Play();
    }
}
