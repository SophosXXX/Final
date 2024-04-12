using UnityEngine;
using System.Collections.Generic;
public class LightingController : MonoBehaviour
{
    public List<LightTemperature> lightTemperatures; // List of LightTemperature scripts
    public AudioSource buttonSoundSource;
    public AudioClip buttonSound;
    

     public void Button1Clicked()
    {
        PlayButtonSound();
        foreach (var light in lightTemperatures)
        {
            light.temperature = 15000f; // Set the temperature to your desired value
        }
    }

    public void Button2Clicked()
    {
        PlayButtonSound();
        foreach (var light in lightTemperatures)
        {
            light.temperature = 6500f; // Set the temperature to your desired value
        }
    }

    public void Button3Clicked()
    {
        PlayButtonSound();
        foreach (var light in lightTemperatures)
        {
            light.temperature = 1000f; // Set the temperature to your desired value
        }
    }
    

    private void PlayButtonSound()
    {
        buttonSoundSource.PlayOneShot(buttonSound);
    }
}