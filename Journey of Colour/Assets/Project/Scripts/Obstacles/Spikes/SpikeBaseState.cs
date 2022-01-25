
using UnityEngine;

public abstract class SpikeBaseState
{
    //this class serves as a base state class. Here its possible to add demanding functionality for each states.
    public abstract void Start(SpikeStateManager stateManager);
    public abstract void Update(SpikeStateManager stateManager);
    public abstract void OnTriggerEnter(SpikeStateManager stateManager, Collider collider);
    public abstract void OnTriggerExit(SpikeStateManager stateManager, Collider collider);
}
