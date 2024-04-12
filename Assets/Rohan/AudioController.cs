using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource bgmSource; // The AudioSource component attached to your BGM object
    public AudioClip[] bgmClips; // An array of your 3 BGM tracks

    public void StopMusic()
    {
        bgmSource.Stop();
    }

    public void PlayMusic(int trackNumber)
    {
        if (bgmSource.isPlaying && bgmSource.clip == bgmClips[trackNumber])
        {
            StopMusic();
        }
        else
        {
            StopMusic();
            bgmSource.clip = bgmClips[trackNumber];
            bgmSource.Play();
        }
    }

    public void Button1Clicked()
    {
        PlayMusic(0); // Play the first BGM track
    }

    public void Button2Clicked()
    {
        PlayMusic(1); // Play the second BGM track
    }

    public void Button3Clicked()
    {
        PlayMusic(2); // Play the third BGM track
    }
}