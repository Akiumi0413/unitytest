using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance;
    public AudioSource audiosource;
    [SerializeField] private AudioClip jumpAudio,CoinAudio,HurtAudio;

    private void Awake()
    {
        instance=this;
    }
    public void jumpaudio()
    {
        audiosource.clip = jumpAudio;
        audiosource.Play();
    }

    public void hurtAudio()
    {
        audiosource.clip = HurtAudio;
        audiosource.Play();
    }

    public void coinaudio()
    {
        audiosource.clip = CoinAudio;
        audiosource.Play();
    }

}
