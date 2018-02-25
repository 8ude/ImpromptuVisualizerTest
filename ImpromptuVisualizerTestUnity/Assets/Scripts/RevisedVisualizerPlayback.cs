using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class RevisedVisualizerPlayback : MonoBehaviour {

    public List<MBT> mbtInput;
    public List<double> beatTimesInput;
    public List<double> beatTimesUpdated;
    public int playIndex = 0;
    double currentMeasureStartTime = 0d;
    AudioSource[] mySources;

    AudioSource availableSource;

    bool endSequence;

    void Awake() {
        mySources = GetComponents<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        
        endSequence = false;

        if (mbtInput.Count != beatTimesInput.Count) {
            Debug.LogWarning("mbt and time inputs are not the same length!");
        }

        beatTimesUpdated = new List<double>();
        playIndex = 0;
        endSequence = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        //find available audioSource
        foreach(AudioSource source in mySources) {
            if(!source.isPlaying) {
                availableSource = source;
                break;
            }
        }

        if (!endSequence && AudioSettings.dspTime >= beatTimesUpdated[playIndex] && beatTimesUpdated.Count > 1 ) {
            playIndex++;
            availableSource.PlayScheduled(beatTimesUpdated[playIndex]);

            if (playIndex >= beatTimesUpdated.Count - 1) {
                endSequence = true;
                playIndex = 0;
            } 
        }
    }

    public void ResetMeasure() {

        foreach (AudioSource source in mySources) {
            if (!source.isPlaying) {
                availableSource = source;
                break;
            }
        }

        playIndex = 0;
        //update currentMeasureStartTime
        currentMeasureStartTime = AudioSettings.dspTime;
        beatTimesUpdated.Clear();
        for (int i = 0; i < mbtInput.Count; i++) {
            
            //update mbt to reflect currentMeasure
            mbtInput[i].Measure = Clock.Instance.GetMBT().Measure;
            //convert mbt to dsp time, add to updated array
            beatTimesUpdated.Add(MBTToDSPTime(mbtInput[i]) + Clock.Instance.StartTime);
            beatTimesUpdated.Sort();

        }

        Debug.Log("DSP time is " + AudioSettings.dspTime);

        availableSource.PlayScheduled(beatTimesUpdated[playIndex]);

        endSequence = false;

    }

    void OnEnable() {
        BeatManager.EveryMeasure += ResetMeasure;
    }

    void OnDisable() {
        BeatManager.EveryMeasure -= ResetMeasure;
    }

    private void OnAudioFilterRead(float[] data, int channels) {
        if (mbtInput[playIndex].CompareTo(Clock.Instance.GetMBT()) == 0) {
            Debug.Log("maybe this will work instead");
        }
    }

    double MBTToDSPTime(MBT input) {
        double dspTime = (input.Measure - 2)  * Clock.Instance.MeasureLengthD();
        dspTime += (input.Beat - 1) * Clock.Instance.BeatLengthD();
        dspTime += (input.Tick - 1) * Clock.Instance.TickLength;
        return dspTime;

    }
}
