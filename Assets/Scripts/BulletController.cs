﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
}