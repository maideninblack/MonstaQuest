using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public MonsterDialiogue monster;
    public enum MixerGroup { Master, Music, SFX };
    public MixerGroup mixerGroup;
    public AudioSource audio;
    [SerializeField]private bool playingAll = false;
    
    public AudioClip[] sfxClips;
    public AudioClip[] ambientClips;
    public AudioClip[] musicClips;
    private AudioSource musicSource;
    private AudioSource ambientSource;
    private int counter = 0;

    #region SFX
    //3D
    public void PlaySFX(int clip)
    {
        PlaySFX(clip, 1, 1);
    }
    public void PlaySFX(int clip, float volume, float pitch)
    {
        Play(sfxClips[clip], volume, pitch, false, false, "SFX");
    }
    //2D
    public void Play2DSFX(int clip)
    {
        Play2DSFX(clip, 1, 1);
    }
    public void Play2DSFX(int clip, float volume, float pitch)
    {
        Play(sfxClips[clip], volume, pitch, false, true, "SFX");
    }    
    #endregion

    #region Music
    public void PlayMusic(int clip)
    {
        PlayMusic(clip, 1, true);
    }
    public void PlayMusic(int clip, float volume, bool loop)
    {
        Play(musicClips[clip], volume, 1, loop, true, "Music");
    }
    public void StopMusic()
    {
        musicSource.TryStop();
        Destroy(musicSource);
    }
    #endregion

    #region Ambient // SFX Loop
    public void PlayAmbient(int clip)
    {
        PlayAmbient(clip, 1);
    }
    public void PlayAmbient(int clip, float volume)
    {
        Play(ambientClips[clip], volume, 1, true, true, "Ambient");
    }
    public void StopAmbient()
    {
        ambientSource.TryStop();
        Destroy(ambientSource);
    }
    #endregion

    public void Play(AudioClip audio, float volume, float pitch, bool loop, bool audio2D, string groupName)
    {
        AudioSource source = this.gameObject.AddAudioSource();
        source.Play(audio, volume, pitch, loop, audio2D, groupName);

        if(!loop) Destroy(source, audio.length);
        else
        {
            if(groupName == "Music")
            {
                if(musicSource != null) StopMusic();
                musicSource = source;
            }
            else
            {
                if(ambientSource != null) StopAmbient();
                ambientSource = source;
            }
        }
    }

   

    public void PlayAll()
    {
        Debug.Log("Play all sfx");
        counter = 0;
        StartCoroutine(PlaySongs());
    }
    public IEnumerator PlaySongs()
    {
        
       
        Debug.Log("Entered while cycle");
        while(counter <= MonsterQuestLogic.chordsSelection.Count)
        {
            
            PlaySFX(MonsterQuestLogic.chordsSelection[counter].id - 1 );
            Debug.Log("playing selected " + MonsterQuestLogic.chordsSelection[counter].id);
            counter++;
            Debug.Log("Contador = " + counter);
            yield return new WaitForSeconds(0.75f);  
        }

        if (MonsterQuestLogic.success && MonsterQuestLogic.chordsSelection.Count == 5 && counter >= 5)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAA");
            monster.TriggerEndDialogue();
        }
        else if(MonsterQuestLogic.success && MonsterQuestLogic.chordsSelection.Count == 5)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAA");
            monster.TriggerEndDialogue();
        }
        yield return null;

    }
    
   
}
