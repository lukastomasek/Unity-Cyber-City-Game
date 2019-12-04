using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterSounds : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 1.1f)]
    private float volume = 1f;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float pitch = 1f;

    private float soundEffect;
     
    public AudioClip punchClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayPunchSound()
    {
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(pitch - 0.5f, pitch + 0.5f);

        audioSource.clip = punchClip;
        audioSource.Play();
    }


}
