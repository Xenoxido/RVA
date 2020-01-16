using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameController : MonoBehaviour
{
    public GameObject capRed;
    public GameObject capBlue;
    // Start is called before the first frame update
    void Start()
    {
        capRed = (GameObject)Instantiate(capRed);
        capRed.AddComponent<MeshRenderer>();
        capRed.transform.parent = this.transform;
        capRed.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
