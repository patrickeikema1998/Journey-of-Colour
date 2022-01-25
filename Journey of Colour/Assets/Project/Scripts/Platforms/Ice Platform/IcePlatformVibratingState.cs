using UnityEngine;

public class IcePlatformVibratingState : IcePlatformState
{
    CustomTimer switchToBreakingTimer;
    AudioSource crackSound;
    Vector3 initialPos;
    float shakeSpeed = 80f, shakeAmount = .03f;

    public IcePlatformVibratingState(float secondsUntilBreaking, AudioSource crackSound)
    {
        switchToBreakingTimer = new CustomTimer(secondsUntilBreaking);
        this.crackSound = crackSound;
    }

    public override void Start(IcePlatformStateManager stateManager)
    {
        initialPos = stateManager.transform.position;

        //need to reset for when it switches back to this state.
        switchToBreakingTimer.Reset();
        switchToBreakingTimer.start = true;

    }
    public override void Update(IcePlatformStateManager stateManager)
    {
        switchToBreakingTimer.Update();

        //The guard is to stop the sound from spamming.
        //It gives the player audio feedback the ice is cracking.
        if (!crackSound.isPlaying) crackSound.Play();

        //If it isn't time to break yet, it vibrates to show the player its cracking.
        if(!switchToBreakingTimer.finish) Vibrate(stateManager);
        else
        {
            //this resets the initial position. It stops the platform from moving small steps every time it 'respawns'.
            stateManager.transform.position = initialPos;

            crackSound.Stop();
            stateManager.SwitchState(stateManager.breakingState);
        }
    }
    public override void OnCollisionEnter(IcePlatformStateManager stateManager, Collision collision)
    {
    }
    public override void OnCollisionExit(IcePlatformStateManager stateManager, Collision collision)
    {
    }

    void Vibrate(IcePlatformStateManager stateManager)
    {
        //starts vibrating based on sinus waves.
        float posChange = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        Transform transform = stateManager.transform;
        transform.position = new Vector3(transform.position.x + posChange, transform.position.y, transform.position.z);
    }
}
