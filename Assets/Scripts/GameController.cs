using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour
{
    public GameObject plant;
    public GameObject bala;

    private float dist;
    private Vector3 v3Offset;
    private Plane plane;
    public float thrust = 1.0f;
    public Button fireButton;
    public int lastTime;
    public int genTime = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        lastTime = System.DateTime.Now.Second;
    }

    // Update is called once per frame
    void Update()
    {
        if(DateTime.Now.Second == 0)
        {
            lastTime = 0;
        }
        if (DateTime.Now.Second - lastTime >= genTime)
        {
            float x = UnityEngine.Random.Range(-0.5f, 0.5f);
            float z = UnityEngine.Random.Range(-0.28f, 0.28f);
            GeneratePlant(x, z);
            lastTime = System.DateTime.Now.Second;
        }
    }

    public void GeneratePlant(float x, float z)
    {
        GameObject p = (GameObject)Instantiate(plant);
        //p.transform.position = this.transform.position + new Vector3(x, 0, z);
        p.transform.SetPositionAndRotation(this.transform.position /*+ new Vector3(x, 0, z)*/, this.transform.rotation);
        p.transform.Rotate(new Vector3(0, UnityEngine.Random.Range(0,360), 180));
        //p.transform.position = new Vector3(x, 0, z);
        p.transform.parent = this.transform;
        p.transform.position += new Vector3(x, 0, z);
        //Debug.Log(p.transform.position.y);
        p.SetActive(true);
    }


    public void OnFireButton()
    {
        fireButton.interactable = false;
        GameObject bullet = (GameObject)Instantiate(bala);
        bullet.transform.position = Camera.main.transform.position;
        bullet.transform.parent = this.transform;
        bullet.SetActive(true);
        plane.SetNormalAndPosition(Camera.main.transform.forward, bullet.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out dist);
        v3Offset = transform.position - ray.GetPoint(dist);
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * thrust, ForceMode.Impulse);
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        fireButton.interactable = true;
    }

}