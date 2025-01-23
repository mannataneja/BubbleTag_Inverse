using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorBubbleSpawner : MonoBehaviour
{
    public GameObject distractorBubble;
    public Transform[] spawnDistractorLocations = new Transform[4];

    public Material flickerMaterial;

    public GameManager gameManager;

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

            GameObject newDistacrtorBubble = Instantiate(distractorBubble, spawnDistractorLocations[Random.Range(0, (spawnDistractorLocations.Length - 1))]);
            newDistacrtorBubble.GetComponent<Bubbles>().SetFlickerMaterial();
            newDistacrtorBubble.GetComponent<Bubbles>().isDistractor = true;


/*            distractorAnimalIndex = Random.Range(0, gameManager.animalIndex.Length + 1);
            if(distractorAnimalIndex == gameManager.currentAnimalIndex)
            {
                if(distractorAnimalIndex < gameManager.animalIndex.Length - 1)
                {
                    distractorAnimalIndex++;
                }
                else
                {
                    distractorAnimalIndex--;
                }
            }
            newDistacrtorBubble.GetComponent<Bubbles>().animalIndex = distractorAnimalIndex;*/

            Vector3 temp = newDistacrtorBubble.transform.position;
            temp.x += Random.Range(-0.3f, 0.3f);
            newDistacrtorBubble.transform.position = temp;
            temp = new Vector3(0, 0, 0);

            yield return new WaitForSeconds(10f);
        }

    }
}
