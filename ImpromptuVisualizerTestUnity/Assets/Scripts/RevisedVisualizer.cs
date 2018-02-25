using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevisedVisualizer : MonoBehaviour {
    public GameObject dotPrefab;
//    public BeatBallData data;
    List<GameObject> sixteenthObjects = new List<GameObject>();
    List<GameObject> beatObjects = new List<GameObject>();
//    List<GameObject> recordData = new List<GameObject>();
    List<GameObject> recordDSPData = new List<GameObject>();
    float mesureTimer = 0;
    public float distMultiplier = 0.2f;
    public Vector3 pos;
    public Vector3 BeatScale = new Vector3(0.04f, 0.04f, 0.04f);


    // Use this for initialization
    void Start() {
        BeatManager.EverySixteenth += EverySixteenth;
        BeatManager.EveryBeat += EveryBeat;
        BeatManager.EveryMeasure += EveryMeasure;
        BeatManager.EveryTwoMeasures += EveryTwoMeasures;
    }

    void EverySixteenth() {
        GameObject obj = Instantiate(dotPrefab, transform) as GameObject;
        obj.transform.localPosition = pos;

        sixteenthObjects.Add(obj);
        Debug.Log("sixteenth");
    }

    void EveryEighth() {
        

    }

    void EveryBeat() {
        GameObject obj = Instantiate(dotPrefab, transform) as GameObject;
        obj.transform.localPosition = pos;
        obj.transform.localScale = BeatScale;
        beatObjects.Add(obj);
    }

    void EveryMeasure() {
        GameObject obj = Instantiate(dotPrefab, transform) as GameObject;
        Vector3 p = pos + new Vector3(0, 0.05f, 0);
        obj.transform.localPosition = p;

        beatObjects.Add(obj);
    }

    void ClearTimeline() {
        sixteenthObjects.RemoveAll(obj => {
            Destroy(obj);
            return true;
        });

        beatObjects.RemoveAll(obj => {
            Destroy(obj);
            return true;
        });
    }

    void EveryTwoMeasures() {
        mesureTimer = 0;
        pos = Vector3.zero;
        ClearTimeline();
        /*
        if (data.timings.Count > 0) {
            print("Timings");
            recordDSPData.RemoveAll(obj => {
                Destroy(obj);
                return true;
            });
            CreateTimings();
        }
        */
    }

    void CreateTimings() {
        /*
        foreach (double t in data.timings) {
            GameObject obj = Instantiate(dotPrefab, transform) as GameObject;
            Vector3 p = new Vector3((float)t * distMultiplier, 0.3f, 0);
            obj.transform.localPosition = p;

            recordDSPData.Add(obj);
        }
        */

    }

    void OnDisable() {
        BeatManager.EverySixteenth -= EverySixteenth;
        BeatManager.EveryBeat -= EveryBeat;
        BeatManager.EveryTwoMeasures -= EveryTwoMeasures;
    }

    public void ClearRecordData() {
        /*
        recordData.RemoveAll(obj => {
            Destroy(obj);
            return true;
        });

        recordDSPData.RemoveAll(obj => {
            Destroy(obj);
            return true;
        });

        data.timings.Clear();
        */
    }

    // Update is called once per frame
    void Update() {
        mesureTimer += Time.deltaTime;
        pos = new Vector3(mesureTimer * distMultiplier, 0, 0);
    }
}