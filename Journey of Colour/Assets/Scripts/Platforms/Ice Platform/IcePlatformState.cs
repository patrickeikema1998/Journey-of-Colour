using UnityEngine;

public abstract class IcePlatformState
{
    //this class serves as a base state class. Here its possible to add demanding functionality for each states.
    public abstract void Start(IcePlatformStateManager stateManager);
    public abstract void Update(IcePlatformStateManager stateManager);
    public abstract void OnCollisionEnter(IcePlatformStateManager stateManager, Collision collision);
    public abstract void OnCollisionExit(IcePlatformStateManager stateManager, Collision collision);
}
