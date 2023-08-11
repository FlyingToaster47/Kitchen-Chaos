using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesCounterVisual : MonoBehaviour {

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;


    private List<GameObject> plateVisualObjectList;

    private void Awake() {
        plateVisualObjectList = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnPlateSpawned += PlateSpawned;
        platesCounter.OnPlateRemoved += PlateRemoved;
    }

    private void PlateSpawned(object sender, System.EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualObjectList.Count, 0);

        plateVisualObjectList.Add(plateVisualTransform.gameObject);

    }
    private void PlateRemoved(object sender, System.EventArgs e) {
        GameObject plateObject = plateVisualObjectList[plateVisualObjectList.Count - 1];
        plateVisualObjectList.Remove(plateObject);
        Destroy(plateObject);
    }

}
