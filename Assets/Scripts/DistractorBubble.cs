using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorBubble : MonoBehaviour
{
    public GameObject[] animals;
    public GameManager gameManager;
    [SerializeField] Transform animalPlaceholder;

    public float distractorSpeed = 0.5f;
    float distractorMaxHeight = 7f;
    private int animalIndex;

    

    private void Start()
    {
        
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        //animal = transform.GetChild(transform.childCount - 1).gameObject;
        //animal.GetComponent<Rigidbody>().useGravity = true;
    }

    private void Awake()
    {
        InstantiateAnimal();
    }

    void Update()
    {
        transform.Translate(Vector3.up * distractorSpeed * Time.deltaTime);

        if (transform.position.y >= distractorMaxHeight)
        {
            Destroy(animals[animalIndex]);
            Destroy(gameObject);
            GameObject.FindAnyObjectByType<DistractorBubbleSpawner>().spawnDistractorBubble();
        }
    }

    void InstantiateAnimal()
    {
        animalIndex = Random.Range(0, (animals.Length - 1));
        while (gameManager.currentAnimalIndex == animalIndex)
        {
            animalIndex = Random.Range(0, (animals.Length - 1));
        }
        Instantiate(animals[animalIndex], transform.position, Quaternion.identity, animalPlaceholder);
        //Instantiate(animals[animalIndex], animalPlaceholder);
        animals[animalIndex].GetComponent<Rigidbody>().useGravity = true;
    }
}
