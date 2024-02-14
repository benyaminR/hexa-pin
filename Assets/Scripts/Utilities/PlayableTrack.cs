using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PlayableTrack
{
    public float pitch;
    public float volume;
    public List<AudioClip> clips;
    public String name;

    public AudioClip clip => clips[Random.Range(0, clips.Count)];
}