using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableOnPhaseChange : MonoBehaviour
{
    [SerializeField]
    int phase;

    private void Awake()
    {
        SlimeBossController.PhaseChange.AddListener(CheckPhaseChange);
    }

    void CheckPhaseChange(int phase)
    {
        //enables the object if the required phase is activated
        if (phase == this.phase) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
