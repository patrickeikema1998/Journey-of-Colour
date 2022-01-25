using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeExtractingState : SpikeBaseState
{
    //In this state, the spikes should extract and a sound should be played
    //When the spikes are extracted, a timer waits for a couple of seconds and switches state to retract again.

    CustomTimer holdTimer;
    AudioSource sound;
    public SpikeExtractingState(CustomTimer timer, AudioSource sound)
    {
        holdTimer = timer;
        this.sound = sound;
    }
    public override void Start(SpikeStateManager stateManager)
    {
        holdTimer.Reset();
        holdTimer.start = false;
        sound.Play();
    }
    public override void Update(SpikeStateManager stateManager)
    {
        if (holdTimer.finish) stateManager.SwitchState(stateManager.retractingState);
        MoveSpikes(stateManager);
    }

    void MoveSpikes(SpikeStateManager stateManager)
    {
        //moves spike untill they are retracted
        if (stateManager.rb.position.y < stateManager.extractedPosY)
        {
            stateManager.rb.velocity = new Vector3(stateManager.rb.velocity.x, stateManager.moveSpeed, stateManager.rb.velocity.z);
        }
        else
        {
            //if retracted, assure they are on the wanted pos and start the hold timer.
            stateManager.rb.position = new Vector3(stateManager.rb.position.x, stateManager.extractedPosY, stateManager.rb.position.z);
            stateManager.rb.velocity = Vector3.zero;
            holdTimer.start = true;
        }
    }


    public override void OnTriggerEnter(SpikeStateManager stateManager, Collider collider) { }

    public override void OnTriggerExit(SpikeStateManager stateManager, Collider collider) { }
}
