using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorBubbleSpawner : MonoBehaviour
{
    public GameObject distractorBubble;
    public float spawnInterval;
    public int numberOfDistractorSpawners;
    public Transform[] spawnDistractorLocations = new Transform[4];

    public Material flickerMaterial;

    int distractorAnimalIndex;
    private void Start()
    {
        StartCoroutine(spawnDistractorBubble());
    }

    public IEnumerator spawnDistractorBubble()
    {
        while (true)
        {
            Debug.Log("sntg");

            GameObject newDistacrtorBubble = Instantiate(distractorBubble, spawnDistractorLocations[Random.Range(0, numberOfDistractorSpawners)]);
            newDistacrtorBubble.GetComponent<Bubbles>().SetFlickerMaterial();
            newDistacrtorBubble.GetComponent<Bubbles>().isDistractor = true;

            Vector3 temp = newDistacrtorBubble.transform.position;
            temp.x += Random.Range(-0.3f, 0.3f);
            newDistacrtorBubble.transform.position = temp;
            temp = new Vector3(0, 0, 0);

            yield return new WaitForSeconds(spawnInterval);
        }

    }
}
