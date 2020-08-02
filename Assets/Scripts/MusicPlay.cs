using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    public float timeToWait = 20;
    public AudioSource musicPlay;
    public AudioClip[] music;
    int randomSong;
    bool coroutineActive;

    private AudioClip GetRandomSong()
    {
        return music[Random.Range(0, music.Length)];
    }
    void Start()
    {
        musicPlay = this.GetComponent<AudioSource>();
        if (!musicPlay.isPlaying)
        { 
        musicPlay.clip = GetRandomSong();      
        }
    }
    void Update()
    {
        if (!musicPlay.isPlaying && !coroutineActive )
        {
            musicPlay.clip = GetRandomSong();
            StartCoroutine (MusicPlayo());
            coroutineActive = true;
        }
        
    }
    IEnumerator MusicPlayo()
    {
        yield return new WaitForSeconds(Random.Range(10,100));
        musicPlay.Play();
        coroutineActive = false;
    }
}