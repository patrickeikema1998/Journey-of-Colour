using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] float thrownObjectForce, stationaryEnemyForce;
    [Range(0f, 3f)] public float timeToVisible, fadeTime, timeToInvisible;
    [Range(0f, 5f)] public float playerHeight;
    //private float alphaValue;
    private bool start, finished, deflected;

    private CharacterController controller;
    //private CustomTimer fadeTimer;
    private FadeTimer fadeTimer;

    private GameObject player;
    //private Material[] materials;
    //https://www.youtube.com/watch?v=_w7GU2NIxUE
    //https://answers.unity.com/questions/1100879/push-object-in-opposite-direction-of-collision.html

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        fadeTimer = new FadeTimer(fadeTime, timeToVisible, timeToInvisible, this.gameObject);
        fadeTimer.Reset();
    }
    private void Update()
    {
        if (transform != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (playerHeight / 2), player.transform.position.z);
        }

        if (fadeTimer.finished)
        {
            Destroy(this.gameObject);
        }

        fadeTimer.Update();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bullet")
        {
            Redirect(other);
        }
        else { }
    }

    void Redirect(Collider collider)
    {
        GameObject gameObject = collider.gameObject;
        Vector3 dir = new Vector3(
            gameObject.transform.position.x - player.transform.position.x,
            0,
            gameObject.transform.position.z - player.transform.position.z
            ).normalized;

        switch (gameObject.tag)
        {
            case "Enemy":
                switch (gameObject.GetComponent<Rigidbody>())
                {
                    case null:
                        switch (gameObject.GetComponent<Rigidbody>())
                        {
                            case null:
                                break;
                            default:
                                gameObject.transform.forward = (new Vector3(
                                dir.x,
                                dir.y,
                                0)
                                );
                                controller.SimpleMove(gameObject.transform.forward * stationaryEnemyForce);
                                break;
                        }
                        break;
                    default:
                        dir = new Vector3(dir.x, dir.y, 0);
                        gameObject.GetComponent<Rigidbody>().AddForce(dir * stationaryEnemyForce, ForceMode.Impulse);
                        break;
                }
                break;
            case "Bullet":
                if (gameObject.GetComponent<SpearBehavior>() != null && !deflected && !gameObject.GetComponent<SpearBehavior>().onGround)
                {
                    gameObject.GetComponent<SpearBehavior>().deflected = true;
                    gameObject.transform.rotation = new Quaternion(
                        gameObject.transform.rotation.x,
                        -gameObject.transform.rotation.y,
                        gameObject.transform.rotation.z,
                        -gameObject.transform.rotation.w
                        );
                    deflected = true;
                }
                gameObject.GetComponent<Rigidbody>().AddForce(dir * thrownObjectForce, ForceMode.Impulse);
                break;
            default:
                Debug.Log("Incorrect or no tag");
                break;
        }
    }
}