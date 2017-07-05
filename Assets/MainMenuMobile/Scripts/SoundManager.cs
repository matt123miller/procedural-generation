using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    
    //NOTES FOR CHANGING VOLUME IN SCRIPT:
    //
    //If you want to mute a specific channel/audio group, first
    //In editor, click on group and then in inspector right click on volume
    //& select "Expose volume to script" 
    //Check the exposed parameters list found with the channels and rename exposed value as appropriate
    //then you can use in script via SetFloat
    //
    //Volume works in dB as seen in editor
    //0dB is not mute! -80dB is mute

    private static SoundManager instance;
    private bool musicOn;
    private bool sfxOn;
    public AudioMixerGroup musicAudioChannel;
    public AudioMixerGroup sfxMusicChannel;


    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindWithTag("GlobalGameManager").GetComponent<SoundManager>();
            }

            return instance;
        }
    }

    public bool MusicOn
    {
        get
        {
            return musicOn;
        }

        set
        {
            musicOn = value;
        }
    }

    public bool SfxOn
    {
        get
        {
            return sfxOn;
        }

        set
        {
            sfxOn = value;
        }
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        ChangeMusicVol();
        ChangeSFXVol();
	}

    public void ToggleMusic()
    {
        if(MusicOn)
        {
            MusicOn = false;
        }
        else
        {
            MusicOn = true;
        }
        
    }

    public void ChangeMusicVol()
    {
        // if (MusicOn)
        // {
        //     musicAudioChannel.audioMixer.SetFloat("MUSIC", 0.0f);
        // }
        // else
        // {
        //     musicAudioChannel.audioMixer.SetFloat("MUSIC", -80.0f);
        // }
    }

    public void ToggleSFX()
    {
        if (SfxOn)
        {
            SfxOn = false;
        }
        else
        {

            SfxOn = true;
        }
    }

    public void ChangeSFXVol()
    {
        if (SfxOn)
        {
            sfxMusicChannel.audioMixer.SetFloat("SFX", 0.0f);
        }
        else
        {
            sfxMusicChannel.audioMixer.SetFloat("SFX", -80.0f);
        }
        
    }

    public void saveTempSettings()
    {
        PlayerPrefs.SetString("MusicOn", MusicOn.ToString());
        PlayerPrefs.SetString("SFXOn", SfxOn.ToString());
        print("SAVED: MUSIC = " + MusicOn.ToString() + " SFX = " + SfxOn.ToString());
    }

    public bool convertSoundStrToBool(string _boolString)
    {
        if (_boolString == "False")
        {
            return false;
        }

        return true;
    }
}
