using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorBubble : MonoBehaviour
{
    public GameObject[] animals;
    public GameManager gameManager;

    public float distractorSpeed = 5f;
    float distractorMaxHeight = 3f;
    private int animalIndex;
    private GameObject animal;

    

    private void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        animal = transform.GetChild(transform.childCount - 1).gameObject;
        animal.GetComponent<Rigidbody>().useGravity = true;
    }
    private void Awake()
    {
        //InstantiateAnimal();
    }

    void Update()
    {
        transform.Translate(Vector3.up * distractorSpeed * Time.deltaTime);

        if (transform.position.y >= distractorMaxHeight)
        {
            GameObject.FindAnyObjectByType<DistractorBubbleSpawner>().spawnDistractorBubble();
            Destroy(gameObject);
        }
    }

    void InstantiateAnimal()
    {
        animalIndex = Random.Range(0, (animals.Length - 1));
        while (gameManager.currentAnimalIndex == animalIndex)
        {
            animalIndex = Random.Range(0, (animals.Length - 1));
        }
        Instantiate(animals[animalIndex], transform);
    }
}
