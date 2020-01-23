using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour
{
    public GameObject globalPlant;
    public GameObject globalPipe;
    public GameObject globalBullet;

    public float thrust = 2.0f;
    public Button fireButton;
    public float timer = 0.0f;
    public float timeBetweenGeneration = 1.0f;

    public GameObject[] pipes = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y += 2)
            {
                pipes[i] = GeneratePipe(0.5f * x, 0.28f * y);
                GeneratePlant(pipes[i], 0.5f * x, 0.28f * y);
                i++;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenGeneration)
        {
            // TODO Esto antes generaba planticas. Ahora lo que tendrá que hacer es
            // respawnear (hacer crecer) las que están como hijas de las pipe (hay un método getChild o algo así).

            /*float x = Random.Range(-0.5f, 0.5f);
            float y = Random.Range(-0.28f, 0.28f);
            GeneratePipe(x, y);
            timer = 0.0f;*/
        }
    }

    public GameObject GeneratePipe(float x, float y)
    {
        GameObject pipe = (GameObject)Instantiate(globalPipe);
        pipe.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        pipe.transform.Rotate(new Vector3(-90, 0, 0));
        pipe.transform.parent = this.transform;
        pipe.transform.Translate(x, y, 0);
        pipe.SetActive(true);
        return pipe;
    }

    public void GeneratePlant(GameObject pipe, float x, float y)
    {
        GameObject plant = (GameObject)Instantiate(globalPlant);
        //plant.transform.SetPositionAndRotation(pipe.transform.position, pipe.transform.rotation);
        plant.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        plant.transform.parent = pipe.transform;
        plant.transform.position = new Vector3(x, 0, -y);
        plant.SetActive(true);
    }


    public void OnFireButton()
    {
        fireButton.interactable = false;
        GameObject bullet = (GameObject)Instantiate(globalBullet);
        bullet.transform.position = Camera.main.transform.position;
        bullet.transform.parent = this.transform;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * thrust, ForceMode.Impulse);
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        fireButton.interactable = true;
    }

}