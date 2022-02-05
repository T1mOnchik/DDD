using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    // Start is called before the first frame update
    //public AudioClip otherClip;
    public List<AudioClip> musicList;
    public List<AudioClip> guitarMusicList;
    private AudioSource mAudio;
    // IEnumerator Start()
    // {
    //     audio = GetComponent<AudioSource>();

    //     audio.Play();
    //     yield return new WaitForSeconds(audio.clip.length);
    //     audio.clip = otherClip;
    //     audio.Play();
    // }
    void Start()
    {
        mAudio = GetComponent<AudioSource>();
        StartCoroutine("MusicPlayer");
    }
    public IEnumerator MusicPlayer()
    {   
        //audio.volume = 0;
        int i = 0;
        while(true)
        {
            if(i < 5)
            {
                mAudio.clip = musicList[i];
                mAudio.Play();
                yield return new WaitForSeconds(mAudio.clip.length);
                i++;
            }
            else
            {
                i = 0;
            }
            yield return null;
        }
    }
    public void GuitarMusicPlayer(int track)
    {   Debug.Log("111");
        StopCoroutine("MusicPlayer");
        mAudio.clip = guitarMusicList[track];
        mAudio.Play();
    }
}
