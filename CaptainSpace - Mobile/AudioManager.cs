using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public const string prefAudioMute = "prefAudioMute";

    [SerializeField] private Image muteButtonImage;
    [SerializeField] private Sprite audioON;
    [SerializeField] private Sprite audioOFF;

    [SerializeField] private Sound[] sounds;

    private void Awake() 
    {
        Instance = this;
        
        if (PlayerPrefs.HasKey(prefAudioMute))
            AudioListener.volume = PlayerPrefs.GetFloat(prefAudioMute);

        UpdateMuteButtonImgae();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLoop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.volume = s.volume;

            if (s.playOnAwake)
                s.source.Play();
        }    
    }

    private void UpdateMuteButtonImgae()
    {
        if (AudioListener.volume == 0)
            muteButtonImage.sprite = audioOFF;
        else
            muteButtonImage.sprite = audioON;
    }

    public void PlayClip(string _clipName)
    {
        Sound soundToPlay = Array.Find(sounds, dummySound => dummySound.clipName == _clipName);

        if (soundToPlay != null)
            soundToPlay.source.Play();
    }

    public void StopClip(string _clipName)
    {
        Sound soundToStop = Array.Find(sounds, dummySound => dummySound.clipName == _clipName);

        if (soundToStop != null)
            soundToStop.source.Stop();
    }

    public void ToggleMute()
    {
        if (AudioListener.volume == 1)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;

        PlayerPrefs.SetFloat(prefAudioMute, AudioListener.volume);

        UpdateMuteButtonImgae();
    }
}
