using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeStateManager : MonoBehaviour
{
    //changable variables
    [SerializeField] float secondsToSpikes;
    [SerializeField] float moveDistance;
    [SerializeField] public float moveSpeed;
    [SerializeField] AudioSource soundExtract, soundRetract;

    [HideInInspector] public CustomTimer holdStateTimer;
    CustomTimer betweenDamageTimer;

    [HideInInspector] public float retractedPosY, extractedPosY;
    [HideInInspector] public Rigidbody rb;

    SpikeBaseState currentState;
    public SpikeExtractingState extractingState;
    public SpikeRetractingState retractingState;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //timers
        holdStateTimer = new CustomTimer(secondsToSpikes);
        holdStateTimer.start = true;

        //creates states
        extractingState = new SpikeExtractingState(holdStateTimer, soundExtract);
        retractingState = new SpikeRetractingState(holdStateTimer, soundRetract);
        //sets the current state to the first state and calls on the start method of this state.
        currentState = retractingState;
        currentState.Start(this);

        //determines the wanted positions.
        extractedPosY = rb.position.y;
        retractedPosY = extractedPosY - moveDistance;

    }

    private void Update()
    {
        currentState.Update(this);
        holdStateTimer.Update(); 
    }

    private void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(this, collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        currentState.OnTriggerExit(this, collider);
    }

    //this method switches the current state to a new state and calls the start method of this state.
    public void SwitchState(SpikeBaseState state)
    {
        currentState = state;
        state.Start(this);
    }
}
