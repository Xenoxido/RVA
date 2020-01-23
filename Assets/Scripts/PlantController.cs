using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class PlantController : MonoBehaviour
{
    // Start is called before the first frame update
    private Text marcador;
    private Coroutine coroutine;
    void Start()
    {
        marcador = GameObject.FindGameObjectWithTag("Marcador").GetComponent<Text>();

        // TODO Las plantas no deberían de morir. Habrá que
        // hacer que en lugar de morir, se hagan pequeñas hasta casi desaparecer

        coroutine = StartCoroutine(Die(300.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        marcador.text = (int.Parse(marcador.text) + 1).ToString();
        StopCoroutine(coroutine);
        coroutine = StartCoroutine(Die(0.01f));
    }

    private IEnumerator Die(float timeToDie)
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(this.gameObject);
    }
}
