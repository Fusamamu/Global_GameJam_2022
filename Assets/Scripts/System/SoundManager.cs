using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource1;
    [SerializeField] private AudioSource bgmSource2;
    

    public void PlaySFX(AudioClip _audio)
    {
        if (!sfxSource)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        sfxSource.PlayOneShot(_audio);
        
    }

    public void PlayBGM(AudioClip _audio)
    {
        if (!bgmSource1)
        {
            bgmSource1 = gameObject.AddComponent<AudioSource>();
        }
        if (!bgmSource2)
        {
            bgmSource2 = gameObject.AddComponent<AudioSource>();
        }
        bgmSource1.clip = _audio;
        bgmSource2.Stop();
        bgmSource1.Play();
    }

    public void PlayPairBGM(AudioClip _audio1, AudioClip _audio2)
    {
        if (!bgmSource1)
        {
            bgmSource1 = gameObject.AddComponent<AudioSource>();
        }
        if (!bgmSource2)
        {
            bgmSource2 = gameObject.AddComponent<AudioSource>();
        }
        
        bgmSource1.clip = _audio1;
        bgmSource2.clip = _audio2;
        bgmSource1.Play();
        bgmSource2.Play();
    }

    public void SwapBGM()
    {
        if (!bgmSource1)
        {
            bgmSource1 = gameObject.AddComponent<AudioSource>();
        }
        if (!bgmSource2)
        {
            bgmSource2 = gameObject.AddComponent<AudioSource>();
        }
        
        if (GameManager.Instance.currentFilter == Filter.Normal)
        {
            bgmSource1.DOFade(1, 0.5f);
            bgmSource2.DOFade(0, 0.5f);
        }
        else
        {
            bgmSource1.DOFade(0, 0.5f);
            bgmSource2.DOFade(1, 0.5f);
        }
    }

}
