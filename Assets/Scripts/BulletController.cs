using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public AudioClip plantDied;
    private Coroutine die;
    // Start is called before the first frame update
    void Start()
    {
        die = StartCoroutine(Die(10));
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Plant")
        {
            this.GetComponent<AudioSource>().PlayOneShot(plantDied);
            StopCoroutine(die);
            die = StartCoroutine(Die(1.2f));
        }
    }

    private IEnumerator Die(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
