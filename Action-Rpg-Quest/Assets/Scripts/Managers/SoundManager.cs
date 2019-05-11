using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public AudioSource playerAudioSource;
        public AudioSource musicAudioSource;
        public float lowPitchRange = .95f;
        public float highPitchRange = 1.05f;


        //Used to play single sound clips.
        public void PlaySingle(AudioClip clip)
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            playerAudioSource.clip = clip;

            //Play the clip.
            playerAudioSource.Play();
        }


        //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
        public void RandomizeSfx(params AudioClip[] clips)
        {
            //Generate a random number between 0 and the length of our array of clips passed in.
            int randomIndex = Random.Range(0, clips.Length);

            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            //Set the pitch of the audio source to the randomly chosen pitch.
            playerAudioSource.pitch = randomPitch;

            //Set the clip to the clip at our randomly chosen index.
            playerAudioSource.clip = clips[randomIndex];

            //Play the clip.
            playerAudioSource.Play();
        }
    }
}