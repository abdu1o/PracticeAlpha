using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
   public AudioClip[] sounds;
   private AudioSource audioSrc => GetComponent<AudioSource>();

   public void PlaySound(AudioClip clip, float volume = 1f, bool is_destroy = false, float p1 = 0.85f, float p2 = 1.2f)
   {
        audioSrc.pitch = Random.Range(p1, p2);
        audioSrc.PlayOneShot(clip, volume);
   }
}
