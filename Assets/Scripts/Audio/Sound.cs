using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
   public AudioClip clip;
   public string name;
   [Range(0,1)]
   public float volume;
   [Range(0,3)]
   public float pitch;

   public AudioSource source;

   public bool loop;
}
