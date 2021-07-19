using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public int levelMusicToPlay;

    private int currentTrack;

    public AudioMixerGroup musicMixer, SFXMixer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playMusic(levelMusicToPlay);
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.M))
        {
            //currentTrack++;
            //playMusic(currentTrack);
            playSFX(5);
        }  //testing purpose */
    }

    public void playMusic(int musicToPlay)
    {
        for(int i = 0; i < music.Length; i++)
        {
            music[i].Stop();
        }
        music[musicToPlay].Play();
    }
    public void playSFX(int SFXToPlay)
    {
        sfx[SFXToPlay].Play();
    }

    public void setMusic()
    {
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicVolSlider.value);
    }

    public void setSFX()
    {
        SFXMixer.audioMixer.SetFloat("SfxVol", UIManager.instance.sfxVolSlider.value);
    }
}
