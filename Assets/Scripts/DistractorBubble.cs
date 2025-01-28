using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorBubble : MonoBehaviour
{
    public GameObject[] animals;

    public float distractorSpeed = 5f;
    float distractorMaxHeight = 3f;
    private GameManager gameManager;
    private int animalIndex;
    GameObject animal;

    private void Awake()
    {
       // InstantiateAnimal();
    }
    private void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        animalIndex = Random.Range(0, (animals.Length - 1));
        Instantiate(animals[animalIndex], transform);

        animal = transform.GetChild(transform.childCount - 1).gameObject;
        animal.GetComponent<Rigidbody>().useGravity = false;
    }


    void Update()
    {
        transform.Translate(Vector3.up * distractorSpeed * Time.deltaTime);

        if (transform.position.y >= distractorMaxHeight)
        {
            GameObject.FindAnyObjectByType<DistractorBubbleSpawner>().SpawnDistractorBubble();
            Destroy(gameObject);
        }
    }

    void InstantiateAnimal()
    {
        animalIndex = Random.Range(0, (animals.Length - 1));
/*        while (gameManager != null && gameManager.currentAnimalIndex == animalIndex)
        {
            animalIndex = Random.Range(0, (animals.Length - 1));
        }*/
        Instantiate(animals[animalIndex], transform);
    }
}
