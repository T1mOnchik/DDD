using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Runtime;
using System;

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
        ShuffleMusicList();
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
            if(i < musicList.Count)
            {
                mAudio.clip = musicList[i];
                mAudio.Play();
                yield return new WaitForSeconds(mAudio.clip.length);
                i++;
            }
            else
            {
                ShuffleMusicList();
                i = 0;
            }
            yield return null;
        }
    }

    private void ShuffleMusicList()
    {   
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = musicList.Count;
        while (n > 1)
        {
            n--;
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            AudioClip value = musicList[k];
            musicList[k] = musicList[n];
            musicList[n] = value;
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
