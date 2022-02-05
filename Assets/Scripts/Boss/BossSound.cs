using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;

    public float timeBetweenSoundEffects;
    private float nextSoundEffectTime;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Time.time >= nextSoundEffectTime)
        {
            int randomNumber = Random.Range(0, clips.Length);
            audioSource.clip = clips[randomNumber];
            audioSource.Play();
            nextSoundEffectTime=Time.time+timeBetweenSoundEffects;
        }
    }
}
