using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    public float minTimeToWait = 20, maxTimeToWait = 100;
    public AudioSource musicPlay;
    public AudioClip[] music;
    bool coroutineActive;

    private void Awake()
    {
        music = Resources.LoadAll<AudioClip>("Sounds/BGM");

    }
    void Start()
    {

        musicPlay = this.GetComponent<AudioSource>();
        if (!musicPlay.isPlaying)
        { 
        musicPlay.clip = GetRandomSong();      
        }
    }

    private AudioClip GetRandomSong()
    {
        return music[Random.Range(0, music.Length)];
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
        yield return new WaitForSeconds(Random.Range(minTimeToWait,maxTimeToWait));
        musicPlay.Play();
        coroutineActive = false;
    }
}