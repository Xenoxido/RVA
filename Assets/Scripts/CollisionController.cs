using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    private Text marcador;

    void Start()
    {

        marcador = GameObject.FindGameObjectWithTag("Marcador").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        marcador.text = (Int32.Parse(marcador.text)+1).ToString();
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
