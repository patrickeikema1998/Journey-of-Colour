using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatformStateManager : MonoBehaviour
{
    [SerializeField] float secondsUntilVibrating, secondsUntilBreaking, cancelVibratingTime, respawnTime;
    [SerializeField] AudioSource crackSound, breakSound;
    [SerializeField] public GameObject platform;

    IcePlatformState currentState;
    public IcePlatformStationaryState stationaryState;
    public IcePlatformVibratingState vibratingState;
    public IcePlatfromBreakAndRespawnState breakingState;

    void Start()
    {
        //creates the states
        stationaryState = new IcePlatformStationaryState(secondsUntilVibrating, secondsUntilBreaking);
        vibratingState = new IcePlatformVibratingState(secondsUntilBreaking, crackSound);
        breakingState = new IcePlatfromBreakAndRespawnState(breakSound, respawnTime);
        //sets the current state to the first state and calls on the start method of this state.
        currentState = stationaryState;
        currentState.Start(this);
    }

    void Update()
    {
        currentState.Update(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(this, collision);
    }

    //this method switches the current state to a new state and calls the start method of this state.
    public void SwitchState(IcePlatformState state)
    {
        currentState = state;
        state.Start(this);
    }
}
