using UnityEngine;

public class IcePlatformStationaryState : IcePlatformState
{
    //this state serves as an idle state.
    //It checks for collisions with the player and switches to vibrating state when necessary.

    CustomTimer switchToVibratingTimer, cancelVibratingTimer;
    float cancelVibratingTime = 1f;
    public IcePlatformStationaryState(float secondsUntilVibrating, float cancelVibratingTime)
    {
        switchToVibratingTimer = new CustomTimer(secondsUntilVibrating);
        cancelVibratingTimer = new CustomTimer(cancelVibratingTime);
    }

    public override void Start(IcePlatformStateManager stateManager)
    {
        //need to reset for when it switches back to this state.
        switchToVibratingTimer.Reset();
        switchToVibratingTimer.start = false;
    }
    public override void Update(IcePlatformStateManager stateManager)
    {
        switchToVibratingTimer.Update();
        cancelVibratingTimer.Update();

        if (switchToVibratingTimer.finish) stateManager.SwitchState(stateManager.vibratingState);
        else if (cancelVibratingTimer.finish)
        {
            //this counter is to cancel the vibrationTimer after a countdown.
            //It exists, so that the player cant cheat the countdown by simply continuously jumping on the platform.

            switchToVibratingTimer.Reset();
            switchToVibratingTimer.start = false;
            cancelVibratingTimer.Reset();
            cancelVibratingTimer.start = false;
        }
    }
    public override void OnCollisionEnter(IcePlatformStateManager stateManager, Collision collision)
    {
        //when colliding with player it starts the vibration counter
        if(collision.gameObject.tag == "Player")
        {
            switchToVibratingTimer.start = true;
            cancelVibratingTimer.Reset();
            cancelVibratingTimer.start = false;
        }
    }
    public override void OnCollisionExit(IcePlatformStateManager stateManager, Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cancelVibratingTimer.start = true;
        }
    }
}
