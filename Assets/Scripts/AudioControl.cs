
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource maintheme;
    public AudioSource gameovers;
    public AudioSource[] soundeffects;

    public enum Sfx { idle, back, updown, driving, explosion };

    public void GameOverBgm()
    {
        maintheme.Stop();
        gameovers.Play();
    }

    public void MenuBgm()
    {
        if (gameovers.isPlaying)
            gameovers.Stop();
    }

    public void LevelBgm()
    {
        maintheme.Play();
    }

    public void SfxPlay(int sound)
    {
        soundeffects[sound].Play();
    }

	public void SfxPlay(Sfx sound) {
		Debug.Log("Playing " + sound + " " + (int)sound);
		SfxPlay((int)sound);
	}
 
	public void SfxStop(int sound)
    {
        soundeffects[sound].Stop();
    }

	public void SfxStop(Sfx sound) {
		SfxStop((int)sound);
	}

    public bool SfxPlaying(int sound)
    {
        return soundeffects[sound].isPlaying;
    }
   
    public void SetMasterVolume(float nvol)
    {
        mixer.SetFloat("masterVolume", Mathf.Clamp(nvol, -80f, 20f));
    }

    public void SetBgmVolume(float nvol)
    {
        mixer.SetFloat("bgmVolume", Mathf.Clamp(nvol, -80f, 20f));
    }

    public void SetSfxVolume(float nvol)
    {
        mixer.SetFloat("sfxVolume", Mathf.Clamp(nvol, -80f, 20f));
    }
}

