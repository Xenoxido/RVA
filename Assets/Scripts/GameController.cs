using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    private AudioSource audio;
    private bool firstTime = true;

    public GameObject globalPlant;
    public GameObject globalPipe;
    public GameObject globalBullet;

    public float thrust = 2.0f;
    public Button fireButton;
    public float timer = 0.0f;
    public float timeBetweenGeneration = 0.01f;
    public float spawnProb = 0.005f;

    public GameObject[] pipes = new GameObject[6];
    public GameObject[] plants = new GameObject[6];


    // Start is called before the first frame update
    void Start()
    {
        mTrackableBehaviour = gameObject.GetComponent<TrackableBehaviour>();
        audio = gameObject.GetComponent<AudioSource>();

        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        int i = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y += 2)
            {
                pipes[i] = GeneratePipe(0.5f * x, 0.28f * y);
                plants[i] = GeneratePlant(pipes[i], 0.5f * x, 0.28f * y);
                pipes[i].SetActive(false);
                i++;
            }

        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Enable pipes visualization
            if(firstTime)
            {
                for (int i = 0; i < 6; i++)
                {
                    pipes[i].SetActive(true);
                }
                audio.Play();
                firstTime = false;
            } else
            {
                // UnPause audio
                audio.UnPause();
            }
            // Enable fire button
            fireButton.interactable = true;
        }
        else
        {
            audio.Pause();
            fireButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenGeneration)
        {
            timer = 0;
            // TODO Esto antes generaba planticas. Ahora lo que tendrá que hacer es
            // respawnear (hacer crecer) las que están como hijas de las pipe (hay un método getChild o algo así).

            for (int i = 0; i < 6; i++)
            {

                if (Random.Range(0.0f, 1.0f) < spawnProb && !pipes[i].GetComponent<PipeController>().havePlant)
                {
                    plants[i].GetComponent<PlantController>().ActivePlant();
                    pipes[i].GetComponent<AudioSource>().Play();
                }
            }
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

    public GameObject GeneratePlant(GameObject pipe, float x, float y)
    {
        GameObject plant = (GameObject)Instantiate(globalPlant);
        //plant.transform.SetPositionAndRotation(pipe.transform.position, pipe.transform.rotation);
        plant.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        plant.transform.parent = pipe.transform;
        plant.transform.position = new Vector3(x, 0, -y);
        plant.GetComponent<PlantController>().pipe = pipe.GetComponent<PipeController>();
        plant.GetComponent<PlantController>().pipe.havePlant = true;
        plant.SetActive(true);
        return plant;
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