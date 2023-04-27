using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenade;
    public float throwSpeed = 2000.0f;
    public float throwTime = 5.0f;

    private bool allowThrow = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G) && allowThrow)
        {
            GameObject obj = Instantiate(grenade);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(
                mousePos.x - transform.position.x,
                mousePos.y - transform.position.y
            ).normalized;

            obj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            obj.GetComponent<Rigidbody2D>().velocity = direction * throwSpeed * Time.deltaTime;
            obj.GetComponent<ExplosionGrenade>().endPos = mousePos;
            obj.GetComponent<ExplosionGrenade>().direction = direction;
            obj.GetComponent<ExplosionGrenade>().mousePos = mousePos;

            allowThrow = false;
            StartCoroutine(WaitForThrow());
        }

        IEnumerator WaitForThrow()
        {
            yield return new WaitForSecondsRealtime(throwTime);

            allowThrow = true;
        }
    }
}
