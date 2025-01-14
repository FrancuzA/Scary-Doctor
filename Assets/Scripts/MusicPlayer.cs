using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicPlayer : MonoBehaviour
{
    public List<EventReference> musicTracks; // List of FMOD events for music tracks
    public EventInstance currentTrack; // The currently playing FMOD event instance
    private int lastTrackIndex = -1; // Keeps track of the last played track index
    public float volume = 1.0f;

    void Start()
    {
        if (musicTracks == null || musicTracks.Count == 0)
        {
            Debug.LogError("Music tracks list is empty!");
            return;
        } 
    }

    public void PlayRandomTrack()
    {
        if (musicTracks.Count <= 1)
        {
            // If there's only one track, just play it
            lastTrackIndex = 0;
            PlayFMODTrack(musicTracks[0]);
            return;
        }

        int newTrackIndex;
        do
        {
            newTrackIndex = Random.Range(0, musicTracks.Count);
        } while (newTrackIndex == lastTrackIndex);

        lastTrackIndex = newTrackIndex;
        PlayFMODTrack(musicTracks[newTrackIndex]);
    }

    private void PlayFMODTrack(EventReference track)
    {
        // Stop the current track if it's playing
        if (currentTrack.isValid())
        {
            currentTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentTrack.release();
        }

        // Create and play the new track
        currentTrack = RuntimeManager.CreateInstance(track);
        SetVolume(volume);
        currentTrack.start();

        // Get the length of the track to schedule the next one
        currentTrack.getDescription(out var description);
        description.getLength(out var trackLength); // Length in milliseconds

        // Schedule the next track
        float trackLengthSeconds = trackLength / 1000f;
        Invoke(nameof(PlayRandomTrack), trackLengthSeconds);
    }

    private void OnDestroy()
    {
        // Stop and release the current track when the object is destroyed
        if (currentTrack.isValid())
        {
            currentTrack.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentTrack.release();
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume); // Ensure volume stays between 0.0 and 1.0
        if (currentTrack.isValid())
        {
            currentTrack.setVolume(volume);
        }
    }
}