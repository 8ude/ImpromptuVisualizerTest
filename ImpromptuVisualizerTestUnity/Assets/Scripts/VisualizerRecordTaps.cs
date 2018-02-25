using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;


public class VisualizerRecordTaps : MonoBehaviour {

    public List<MBT> inputMBT;
    public List<double> inputClockTime;

    public MBT startMBT;
    double measureStartTime;
    double recordingEndTime;

    public bool isRecording = false;

    public GameObject recordedLoopPrefab;

    public void OnTap() {
        if (!isRecording) {
            StartRecording();
        }
        else {
            RecordTapTime();
        }
    }
    public void StartRecording() {
        Debug.Log("start recording");
        inputClockTime = new List<double>();
        inputClockTime.Add(Clock.Instance.Time);
        startMBT = Clock.Instance.GetMBT();
        //roundTickToSixteenth
        startMBT.Tick = Mathf.RoundToInt(startMBT.Tick / 16f) * 16;
        Debug.Log("tick: " + startMBT.Tick);
        inputMBT = new List<MBT>();
        inputMBT.Add(startMBT);

        isRecording = true;
        recordingEndTime = Clock.Instance.Time + Clock.Instance.MeasureLengthD();


    }

    public void RecordTapTime() {
        
        inputClockTime.Add(Clock.Instance.Time);
        MBT nextMBT = Clock.Instance.GetMBT();
        //roundTickToSixteenth
        nextMBT.Tick = Mathf.RoundToInt(nextMBT.Tick / 16f) * 16;
        Debug.Log("next tick: " + nextMBT.Tick);
        inputMBT.Add(nextMBT);
    }






    public void EndRecording() {

        isRecording = false;
        //sets the measure component of MBT to zero
        NormalizeMBT();

        List<double> outputClockTime = new List<double>();
        foreach(MBT mbt in inputMBT) {
            outputClockTime.Add(MBTToClockTime(mbt));
        }

        GameObject newLoopPrefab = Instantiate(recordedLoopPrefab, transform.position, Quaternion.identity);
        newLoopPrefab.GetComponent<RevisedVisualizerPlayback>().beatTimesInput = outputClockTime;
        newLoopPrefab.GetComponent<RevisedVisualizerPlayback>().mbtInput = inputMBT;

    }
	

    double MBTToClockTime (MBT input) {
        double clockTime = input.Measure * Clock.Instance.MeasureLengthD();
        clockTime += input.Beat * Clock.Instance.BeatLengthD();
        clockTime += input.Tick * Clock.Instance.TickLength;
        return clockTime;
                              
    }

    private void FixedUpdate() {
        if (isRecording && Clock.Instance.Time >= recordingEndTime) {
            isRecording = false;
            EndRecording();
            Debug.Log("end recording");
        }
    }

    public void NormalizeMBT() {
        for (int i = 0; i < inputMBT.Count; i++) {
            inputMBT[i].Measure = 0;
        }
    }
}
