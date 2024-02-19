using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : SingletonMonoBehaviour<ParticleManager>
{
    public void SpawnAt(string name, Vector3 position, Color color)
    {
        var particlePref = Resources.Load<ParticleSystem>("Particles/" + name);
        var instance = Instantiate(particlePref, position, particlePref.transform.rotation);
    }
}
