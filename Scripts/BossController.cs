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
    private int nextCornerIndex = -1;
    private float roamingtime;

    public bool move = true;

    private bool newAttack = true;
    public GameObject beamPrefab;
    public Transform beamAttackSource;
    public GameObject chargePrefab;

    private int nextAttack;

    public float[] firetimes;
    private int shots = 0;

    void Start()
    {
        roamingtime = Random.Range(minRoamingTime, maxRoamingTime);
        GetNextCorner();
        GetNextAttack();
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
                GetNextAttack();
            }
        }
        else if (state == 1)
        {
            if (nextAttack != 0 || timer > attackSeekTime)
            {
                timer = 0f;
                state = 2;
            } else {
                //Debug.Log("seek");
                seek(); 
            }
            
        }
        else 
        {
            attack();

            
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
        if (nextAttack == 0)
        {
            if (newAttack)
            {
                // Attack 1
                attackPauseTime = 2f;
                animator.SetTrigger("Attack1");
                GameObject newBeamAttack = GameObject.Instantiate(beamPrefab, beamAttackSource);
                newAttack = false;

            }
        }
        else
        {
            if (newAttack)
            {
                shots = 0;
                newAttack = false;
            }
            // Attack 2
            //Debug.Log("firetime " + firetimes.Length);
            attackPauseTime = firetimes[firetimes.Length - 1] + 2f;
            //Debug.Log("attack pause " + attackPauseTime);
            
            
            
                if (shots < firetimes.Length && timer > firetimes[shots])
                {
                    animator.SetTrigger("Attack2");
                    GameObject charge = GameObject.Instantiate(chargePrefab, beamAttackSource.position, beamAttackSource.rotation);
                    shots++;
                }
            
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
        if (nextCornerIndex < 0 || corners.Length < 2)
        {
            nextCornerIndex = corners.Length - 1;
        } else
        {
            if (nextCornerIndex == 0)
            {
                nextCornerIndex++;
            } else if (nextCornerIndex == corners.Length-1)
            {
                nextCornerIndex--;
            } else
            {
                float i = Random.Range(-1, 1); 
                //Debug.Log(i);
                nextCornerIndex += (int)Mathf.Sign(i);
            }
        }
        Debug.Log("Corner: " + nextCornerIndex);
        nextCorner = corners[nextCornerIndex];
    }

    void seek()
    {
        if (move)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target.position.y), speed * Time.deltaTime);
        }
    }

    void GetNextAttack()
    {
        //nextAttack = Random.Range(0, 1);
        if (Mathf.Abs(transform.position.x) < 21)
        {
            nextAttack = 1;
        } else
        {
            nextAttack = 0;
        }

        //nextAttack = 1;
    }

    void OnDeath()
    {
        GetComponent<BossController>().enabled = false;
    }
}
