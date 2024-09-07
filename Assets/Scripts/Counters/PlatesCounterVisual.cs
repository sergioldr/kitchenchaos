using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectsList;

    private void Awake()
    {
        plateVisualGameObjectsList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateTaken += PlatesCounter_OnPlateTaken;
    }

    private void PlatesCounter_OnPlateTaken(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectsList[plateVisualGameObjectsList.Count - 1];
        plateVisualGameObjectsList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = plateVisualGameObjectsList.Count * 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY, 0);
        plateVisualGameObjectsList.Add(plateVisualTransform.gameObject);
    }
}
