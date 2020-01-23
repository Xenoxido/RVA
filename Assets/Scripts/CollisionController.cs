using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    private Text marcador;
    private Coroutine coroutine;
    void Start()
    {
        marcador = GameObject.FindGameObjectWithTag("Marcador").GetComponent<Text>();
        coroutine = StartCoroutine(Die(9.0f));
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(this.transform.position.x, target.transform.position.y, this.transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        marcador.text = (Int32.Parse(marcador.text)+1).ToString();
        StopCoroutine(coroutine);
        coroutine = StartCoroutine(Die(0.01f));
    }

    private IEnumerator Die(float timeToDie)
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(this.gameObject);
    }
}
