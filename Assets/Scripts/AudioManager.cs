using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterAudio;
    public Slider audioSlider;
    // Start is called before the first frame update
    void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat("MasterAudio", 1f);
        masterAudio.SetFloat("MasterAudio", Mathf.Log10(audioSlider.value) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterAudio", volume);
        masterAudio.SetFloat("MasterAudio", Mathf.Log10(volume) * 20);
    }
}
