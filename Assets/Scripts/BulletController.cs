using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public AudioClip plantDied;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
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
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
}
