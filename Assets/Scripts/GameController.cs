using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour
{
    public GameObject capRed;
    public GameObject bala;

    private float dist;
    private Vector3 v3Offset;
    private Plane plane;
    public float thrust = 1.0f;
    public Button fireButton;
    public int lastTime;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = System.DateTime.Now.Second;
    }

    // Update is called once per frame
    void Update()
    {
        if (System.DateTime.Now.Second - lastTime > 1)
        {
            float x = UnityEngine.Random.Range(-0.5f, 0.5f);
            float z = UnityEngine.Random.Range(-0.28f, 0.28f);
            GenerarCapRed(x, z);
            lastTime = System.DateTime.Now.Second;
        }
    }

    public void GenerarCapRed(float x, float z)
    {
        GameObject cap = (GameObject)Instantiate(capRed);
        //cap.transform.position = this.transform.position + new Vector3(x, 0, z);
        cap.transform.SetPositionAndRotation(this.transform.position /*+ new Vector3(x, 0, z)*/, this.transform.rotation);
        //cap.transform.position = new Vector3(x, 0, z);
        cap.transform.parent = this.transform;
        cap.transform.position += new Vector3(x, 0, z);
        cap.SetActive(true);
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