using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private List<AudioSource> _sources = new List<AudioSource>();
    private List<AudioClip> _clips = new List<AudioClip>();




    private void Awake()
    {
        _sources = new List<AudioSource>();
        for (int i = 0; i < 25; i++)
        {
            var source = new GameObject("Source " + i, typeof(AudioSource));
            _sources.Add(source.GetComponent<AudioSource>());
        }

        _clips = Resources.LoadAll("Sounds", typeof(AudioClip)).Cast<AudioClip>().ToList();

    }


    public void PlayOneShot(String name, float vol = 1f, float pitch = 1f, bool looping = false)
    {
        var isSoundOn = PlayerPrefs.GetInt("isSoundOn", 1);
        if (isSoundOn == 0) return;

        var clip = GetClipByName(name);
        if (clip == null)
        {
            Debug.LogError("Clip not found: " + name);
            return;
        }

        for (var i = 0; i < _sources.Count; i++)
        {
            var source = _sources[i];
            if (!source.isPlaying)
            {
                source.pitch = pitch >= 2 ? 2 : pitch;
                source.volume = vol;
                source.clip = clip;
                source.Play();
                source.loop = looping;
                return;
            }
        }
    }

    private AudioClip GetClipByName(string name)
    {
        foreach (var clip in _clips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
}