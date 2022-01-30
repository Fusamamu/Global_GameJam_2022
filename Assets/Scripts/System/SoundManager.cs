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

    private Tween tween1;
    private Tween tween2;
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
        Debug.Log("PlayPair");
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
        bgmSource1.Stop();
        bgmSource2.Stop();
        bgmSource1.Play();
        bgmSource2.Play();

        bgmSource1.loop = true;
        bgmSource2.loop = true;
        bgmSource1.volume = 1;
        bgmSource2.volume = 0;
    }

    public void SwapBGM()
    {
        tween1.Kill();
        tween2.Kill();
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
            tween1 = bgmSource1.DOFade(1, 0.5f);
            tween2 = bgmSource2.DOFade(0, 0.5f);
        }
        else
        {
            tween1 = bgmSource1.DOFade(0, 0.5f);
            tween2 = bgmSource2.DOFade(1, 0.5f);
        }
    }

}
