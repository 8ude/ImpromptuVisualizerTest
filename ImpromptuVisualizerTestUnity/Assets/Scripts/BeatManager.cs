using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour {

    Beat.Clock clock;

    public delegate void MeasureRepeat();
    public static event MeasureRepeat EveryMeasure;

    public delegate void TwoMeasureRepeat();
    public static event TwoMeasureRepeat EveryTwoMeasures;

	public delegate void BeatEvent();
	public static event BeatEvent EveryBeat;
	public static event BeatEvent EveryEighth;
    public static event BeatEvent EverySixteenth;

    bool atSixteenth = false;
    bool atEighth = false;
    bool atBeat = false;
    bool atMeasure = false;
    bool atSecondMeasure = false;
	

	public double timeSinceMeasure;
	public double timeSinceTwoMeasure;

	public static BeatManager Instance;

	void Awake(){
		if (Instance != null) {
			Debug.LogWarning ("BeatManager is a singleton, make sure you don't have onether BeatManager in scene");	
		} else {
			Instance = this;
		}
	}

	void OnDestroy(){
		Destroy (Instance);
	}

	// Use this for initialization
	void Start () {
		

        clock = Beat.Clock.Instance;
        clock.Measure += OnMeasure;
		clock.Beat += OnBeat;
		clock.Eighth += OnEighth;
        clock.Sixteenth += OnSixteenth;
	}

	public double getTimeSinceMesure(int mesureCount){
		switch (mesureCount) {
		case 1 : {
				return timeSinceMeasure;
				break;	
			}
		case 2 : {
				return timeSinceTwoMeasure;
				break;
			}
		default :
			{
				return timeSinceMeasure;
				break;
			}
		}	
	}
	// Update is called once per frame
	void FixedUpdate () {

		// mesure order from long to short

        if (atMeasure == true) {
            if (atSecondMeasure == false) {
				// todo : move to event somehow because update is not acurate
				timeSinceTwoMeasure = AudioSettings.dspTime;
				if(EveryTwoMeasures!=null) EveryTwoMeasures();

                atSecondMeasure = true;
            } else {
                atSecondMeasure = false;
            }
            atMeasure = false;

			if(EveryMeasure != null) EveryMeasure();
        }

		if (atBeat) {
			if (EveryBeat != null)
				EveryBeat();
			atBeat = false;
		}
		if (atEighth) {
			if (EveryEighth != null)
				EveryEighth ();
			atEighth = false;
		}
        if (atSixteenth) {
            if(EverySixteenth != null) {
                EverySixteenth();
            }
            atSixteenth = false;
        }
	}

	void OnBeat(Beat.Args args){
		atBeat = true;
	}

    void OnMeasure(Beat.Args args) {
		timeSinceMeasure = AudioSettings.dspTime;
        atMeasure = true;
    }

	void OnEighth(Beat.Args args) {
		atEighth = true;
	}

    void OnSixteenth(Beat.Args args) {
        atSixteenth = true;
    }


    void OnDisable() {
        clock.Measure -= OnMeasure;
		clock.Beat -= OnBeat;
		clock.Eighth -= OnEighth;
        clock.Sixteenth -= OnSixteenth;
    }


}
