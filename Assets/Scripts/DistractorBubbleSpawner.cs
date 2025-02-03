using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorBubbleSpawner : MonoBehaviour
{
    public GameObject distractorBubble;
    public float spawnInterval;
    public int numberOfDistractorSpawners;
    public Transform[] spawnDistractorLocations = new Transform[4];
    public int percentageThatFlicker;

    public Material flickerMaterial;
    //[SerializeField] float distractorBubblePercentage;

    int distractorAnimalIndex;
    private void Start()
    {
        //SpawnMultipledistractorBubbles();
        StartCoroutine(SpawnDistractorBubble());
    }

    public IEnumerator SpawnDistractorBubble()
    {
        while (true)
        {
           // GetPrecentageofDistractors(distractorBubblePercentage);
            GameObject newDistacrtorBubble = Instantiate(distractorBubble, spawnDistractorLocations[Random.Range(0, numberOfDistractorSpawners)]);

            int random = Random.Range(0, 100);
            if (random < percentageThatFlicker)
            {
                newDistacrtorBubble.GetComponent<Bubbles>().SetFlickerMaterial();
                Debug.Log("Flicker");
            }
            else
            {
                newDistacrtorBubble.GetComponent<Bubbles>().SetDullBubbleMaterial();
                Debug.Log("Not Flicker");
            }
            newDistacrtorBubble.GetComponent<Bubbles>().isDistractor = true;

            Vector3 temp = newDistacrtorBubble.transform.position;
            temp.x += Random.Range(-0.3f, 0.3f);
            newDistacrtorBubble.transform.position = temp;
            temp = new Vector3(0, 0, 0);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

/*    public int GetPrecentageofDistractors(float distractorBubblePercentage)
    {
        int noDistractorBubblePercentage = (int)((distractorBubblePercentage / 100) * numberOfDistractorSpawners);
        return noDistractorBubblePercentage;
    }*/

    public void SpawnMultipledistractorBubbles()
    {
/*        Debug.Log("Distractor spawners " + GetPrecentageofDistractors(distractorBubblePercentage));
        for (int distractorSpawners = 0; distractorSpawners < GetPrecentageofDistractors(distractorBubblePercentage); distractorSpawners++)
        {
            StartCoroutine(SpawnDistractorBubble());
        }*/
    }
}
