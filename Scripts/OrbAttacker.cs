using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAttacker : MonoBehaviour
{
    public Animator anim;

    public void Attack()
    {
        anim.SetTrigger("attacking");
    }
}
