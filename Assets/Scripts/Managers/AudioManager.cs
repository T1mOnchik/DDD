using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    // Start is called before the first frame update
    public static AudioManager instance;
    [SerializeField]private List<AudioClip> musicList;
    // public List<AudioClip> guitarMusicList;
    [SerializeField]private AudioClip guitarMusic;

    private AudioSource mAudio;
    private bool isPlay = true;
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
        if(instance != this)
            Destroy(instance);
        if(instance == null)
            instance = this;

        mAudio = GetComponent<AudioSource>();
        StartCoroutine("MusicPlayer");
    }
    public IEnumerator MusicPlayer()
    {   
        //audio.volume = 0;
        int i = 0;
        while(isPlay)
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

    public void LaunchGuitarMusicPlayer(float time)
    {   
        StartCoroutine(GuitarMusicPlayer(time));
    }

    private IEnumerator GuitarMusicPlayer(float time){
        isPlay = false;
        mAudio.clip = guitarMusic;
        mAudio.Play();
        yield return new WaitForSeconds(time);
        isPlay = true;
        yield break;
    }
}
