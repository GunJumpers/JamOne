using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //Song beats per minute
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;
    public AudioSource musicSource;
    public float[] beats;
    public float notesInAdvance = 1;
    public NoteSpawner NoteSpawner;
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;


    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        for (int i = 0; i < beats.Length; i++)
        {

            if ((int)songPositionInBeats % beats[i] == 0 && beats[i] != notesInAdvance)
            {
                notesInAdvance = beats[i];
                NoteSpawner.changeNoteTempo(Random.Range(1, 3), beats[i]);
                
            }
        }
        
    }

}
