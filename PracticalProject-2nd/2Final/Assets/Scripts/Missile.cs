using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 30f;  // how fast the missile should travel
    public int damage = 10;  // how much damage this missile will cause when it hits the player

    /*
1. When you instantiate a Missile, you start a coroutine called deathTimer(). 
    This is name of the method that the coroutine will call.
2. Move the missile forward at speed multiplied by the time between frames.
3. You'll notice that the method immediately returns a WaitForSeconds, set to 10. 
    Once those ten seconds have passed, the method will resume after the yield statement. 
    If the missile doesn¡¯t hit the player, it should auto-destruct.
     * */

    // Start is called before the first frame update
    // 1
    void Start()
    {
        StartCoroutine("deathTimer");
    }

    // Update is called once per frame
    // 2
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // 3
    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
