using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;

    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        int randomNumber=Random.Range(0,clips.Length);
        audioSource.clip=clips[randomNumber];
        audioSource.Play();
    }

    
}
