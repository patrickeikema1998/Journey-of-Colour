using UnityEngine;

public class IcePlatfromBreakAndRespawnState : IcePlatformState
{
    AudioSource breakSound;
    CustomTimer respawnTimer;
    public IcePlatfromBreakAndRespawnState(AudioSource breakSound, float respawnTime)
    {
        this.breakSound = breakSound;
        respawnTimer = new CustomTimer(respawnTime);
    }
    public override void Start(IcePlatformStateManager stateManager)
    {
        breakSound.Play();
        SetActivePlatform(false, stateManager);

        //need to reset for when it switches back to this state.
        respawnTimer.Reset();
        respawnTimer.start = true;
    }
    public override void Update(IcePlatformStateManager stateManager)
    {
        respawnTimer.Update();

        if (respawnTimer.finish)
        {
            SetActivePlatform(true, stateManager);
            stateManager.SwitchState(stateManager.stationaryState);
        }
    }
    public override void OnCollisionEnter(IcePlatformStateManager stateManager, Collision collision)
    {
    }
    public override void OnCollisionExit(IcePlatformStateManager stateManager, Collision collision)
    {
    }

    void SetActivePlatform(bool setActive, IcePlatformStateManager stateManager)
    {
        //disables or enables the collider and renderers.
        if (setActive)
        {
            stateManager.platform.SetActive(true);
            stateManager.GetComponent<BoxCollider>().enabled = true;
        } else
        {
            stateManager.platform.SetActive(false);
            stateManager.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
