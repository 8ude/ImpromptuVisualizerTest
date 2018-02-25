using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentManager : MonoBehaviour {

    public static InstrumentManager instance;

    public AudioClip[] bassClips;
    public AudioClip[] toneClips;
    public AudioClip[] drumClips;

    public enum Instrument { Bass, Tone, Drum, Recording };
    public Instrument currentInstrument;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {

        currentInstrument = Instrument.Bass;
        
	}


    /// <summary>
    /// Returns an audio clip corresponding to the light level inputed (0-1 range)
    /// </summary>
    /// <returns>The bass clip</returns>
    /// <param name="lightLevel">Light level (0-1 range)</param>
    public AudioClip MapBass(float lightLevel) {

        for (int i = 0; i < bassClips.Length; i++) {
            if (lightLevel < ((1f / bassClips.Length) * (i + 1f))) {
                
                return bassClips[i];
            }
        }
        //should only be reached if light level > 1f
        return bassClips[bassClips.Length - 1];
        
    }

    /// <summary>
    /// Returns an audio clip corresponding to the light level inputed (0-1 range)
    /// </summary>
    /// <returns>The tone clip</returns>
    /// <param name="lightLevel">Light level (0-1 range)</param>
    public AudioClip MapTones(float lightLevel) {

        for (int i = 0; i < toneClips.Length; i++) {
            if (lightLevel < ((1f / toneClips.Length) * (i + 1f))) {
               
                return toneClips[i];
            }
        }
        //should only be reached if light level > 1f
        return toneClips[toneClips.Length - 1];

    }

    /// <summary>
    /// Returns an audio clip corresponding to the light level inputed (0-1 range)
    /// </summary>
    /// <returns>The drum clip</returns>
    /// <param name="lightLevel">Light level (0-1 range)</param>
    public AudioClip MapDrums(float lightLevel) {

        for (int i = 0; i < drumClips.Length; i++) {
            if (lightLevel < ((1f / drumClips.Length) * (i + 1f))) {
                return drumClips[i];
            }
        }
        //should only be reached if light level > 1f
        return drumClips[drumClips.Length - 1];

    }

}
