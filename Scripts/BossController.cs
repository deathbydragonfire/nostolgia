using UnityEngine;

public class BossController : MonoBehaviour
{
    // The minimum and maximum roaming times
    public float minRoamingTime = 2.0f;
    public float maxRoamingTime = 5.0f;

    public Transform target;
    public Vector2[] corners;

    public float speed = 2f;

    // A timer to keep track of the elapsed time
    private float timer = 0.0f;
    public float attackPauseTime = 1f;
    public float attackSeekTime = 1f;

    // The current state of the boss
    private int state = 0; 

    // A reference to the boss's animator component
    public Animator animator;

    private Vector2 nextCorner;
    private float roamingtime;

    public bool move = true;

    private bool newAttack = true;
    public GameObject beamPrefab;
    public Transform beamAttackSource;

    void Start()
    {
        roamingtime = Random.Range(minRoamingTime, maxRoamingTime);
        GetNextCorner();
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check the current state of the boss
        if (state == 0)
        {
            roaming();
            // If the boss is roaming, check if the timer has exceeded the roaming time
            if (timer >= roamingtime)
            {
                // If the roaming time has been exceeded, transition to the attack state
                roamingtime = Random.Range(minRoamingTime, maxRoamingTime);
                timer = 0.0f;
                state = 1;
            }
        }
        else if (state == 1)
        {
            
            seek();
            if (timer > attackSeekTime)
            {
                timer = 0f;
                state = 2;
            }
        }
        else 
        {
            if (newAttack)
            {
                attack();
                newAttack = false;
            }

            
            if (timer > attackPauseTime)
            {
                timer = 0.0f;
                state = 0;
                newAttack = true;
            }
        }
    }

    void attack()
    {
        if (Random.Range(0, 1) == 0)
        {
            // Attack 1
            animator.SetTrigger("Attack1");
            GameObject newBeamAttack = GameObject.Instantiate(beamPrefab, beamAttackSource);
        }
        else
        {
            // Attack 2
            animator.SetTrigger("Attack2");
        }
    }

    void roaming()
    {
        if (move)
        {
            // If the boss is roaming, move towards the next corner
            transform.position = Vector2.MoveTowards(transform.position, nextCorner, speed * Time.deltaTime);
        }

        // Face towards the target
        if (target.position.x < transform.position.x)
        {
            
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else
        {
        
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        // Check if the boss has reached the next corner
        if (Vector2.Distance(transform.position, nextCorner) < 0.1f)
        {
            //Debug.Log("reached");
            GetNextCorner();
            //Debug.Log(nextCorner);
        }
    }

    void GetNextCorner()
    {
        nextCorner = corners[Random.Range(0, corners.Length)];
    }

    void seek()
    {
        if (move)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target.position.y), speed * Time.deltaTime);
        }
    }
}
