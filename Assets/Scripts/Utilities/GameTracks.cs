using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameTracks : SingletonScriptableObject<GameTracks>
{
    public List<PlayableTrack> tracks = new List<PlayableTrack>();
}