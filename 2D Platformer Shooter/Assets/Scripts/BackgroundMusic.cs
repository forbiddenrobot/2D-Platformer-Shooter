using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] bgMusic;
    private int currentTrackIndex;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentTrackIndex = Random.Range(0, bgMusic.Length);
        audioSource.clip = bgMusic[currentTrackIndex];
        audioSource.Play();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentTrackIndex = (currentTrackIndex + 1) % bgMusic.Length;
            audioSource.clip = bgMusic[currentTrackIndex];
            audioSource.Play();
        }
    }

    public void NextSong()
    {
        currentTrackIndex = (currentTrackIndex + 1) % bgMusic.Length;
        audioSource.clip = bgMusic[currentTrackIndex];
        audioSource.Play();
    }

    public void PreviousSong()
    {
        if (currentTrackIndex == 0)
        {
            currentTrackIndex = bgMusic.Length - 1;
        }
        else
        {
            currentTrackIndex--;
        }
        audioSource.clip = bgMusic[currentTrackIndex];
        audioSource.Play();
    }
}
