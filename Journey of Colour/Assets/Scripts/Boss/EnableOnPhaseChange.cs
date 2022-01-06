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
        if (phase == this.phase) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
