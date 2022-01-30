using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class FinishedTest : MonoBehaviour
{
    [HideInInspector] float passedTime, distance;
    Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        AnalyticsEvent.Custom("FinishedLevel", new Dictionary<string, object>
            {
                { "Scene",  SceneManager.GetActiveScene().name},
                { "Time",  Time.timeSinceLevelLoad},
            });
    }
}
