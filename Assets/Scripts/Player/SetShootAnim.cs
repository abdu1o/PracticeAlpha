using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShootAnim : MonoBehaviour
{
    public Animator anim;
    public float delay = 0.25f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
            StartCoroutine(ReloadDelay());
        }
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(delay);
    }
}
