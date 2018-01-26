using UnityEngine;

public class FatBunnyController : MonoBehaviour
{
    private readonly Vector2 LAUNCH_VELOCITY = new Vector2(20f, 80f);
    private readonly Vector2 INITIAL_POSITION = Vector2.zero;
    private readonly Vector2 GRAVITY = new Vector2(0f, -240f);
    private const float DELAY_UNTIL_LAUNCH = 1f;
    private int NUM_DOTS_TO_SHOW = 30;
    private float DOT_TIME_STEP = 0.05f;

    private bool launched = false;
    private float timeUntilLaunch = DELAY_UNTIL_LAUNCH;
    private Rigidbody2D rigidBody;

    public GameObject trajectoryDotPrefab;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        for (int i = 0; i < NUM_DOTS_TO_SHOW; i++)
        {
            GameObject trajectoryDot = Instantiate(trajectoryDotPrefab);
            trajectoryDot.transform.position = CalculatePosition(DOT_TIME_STEP * i);
        }
    }

    private void Update()
    {
        timeUntilLaunch -= Time.deltaTime;

        if (!launched && timeUntilLaunch <= 0)
        {
            Launch();
        }
    }

    private void Launch()
    {
        rigidBody.velocity = LAUNCH_VELOCITY;

        launched = true;
    }

    private Vector2 CalculatePosition(float elapsedTime)
    {
        return GRAVITY * elapsedTime * elapsedTime * 0.5f + LAUNCH_VELOCITY * elapsedTime + INITIAL_POSITION;
    }
}