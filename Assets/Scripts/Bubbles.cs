using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bubbles : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject[] animals;
    public GameObject bubble;
    public float speed = 5f;
    public ParticleSystem pop;
    public int animalIndex;

    float maxHeight = 5f;

    GameObject animal;
    Rigidbody animalRB;
    Collider animalCollider;
    public string animalTag;

    public Material currentBubbleMaterial;
    public Material dullBubbleMaterial;


    private void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        //SpawnAnimals animal as soon as bubble is spawned. Animal index is passed from Game Manager
        Instantiate(animals[animalIndex], transform);

        //Get the transform of newly spawned animal
        animal = transform.GetChild(transform.childCount - 1).gameObject;
        animalRB = animal.GetComponent<Rigidbody>();
        animalCollider = animal.GetComponent<Collider>();
        animalTag = animal.tag;

        animalRB.useGravity = false;
        animalCollider.isTrigger = true;

        // originalBubbleMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        
    }
    void Update()
    {
        //CheckForCurrentAnimal();
        //Make bubble float up
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y >= maxHeight)
        {
            if(animal.GetComponent<Animal>().isCurrentAnimal)
            {
                gameManager.currentAnimalExists = false;
                gameManager.missed++;
            }
            Destroy(gameObject);
        }
    }
    public void CheckForCurrentAnimal()
    {
        if (animalIndex == gameManager.currentAnimalIndex && animal.GetComponent<Animal>().selectable == true)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material = currentBubbleMaterial;
        }
    }
    public void SetCurrentBubbleMaterial()
    {
        //if (animalIndex == gameManager.currentAnimalIndex)
        //{
            transform.GetChild(0).GetComponent<MeshRenderer>().material = currentBubbleMaterial;
        //}
    }
    public void SetDullBubbleMaterial()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = dullBubbleMaterial;
    }
    //Bubble is popped if correct animal is selected
    public IEnumerator PopBubble() 
    {
        if(animal.GetComponent<Animal>().selectable == true)
        {
            gameManager.currentAnimalExists = false;
            gameManager.AddScore();
            pop.Play();
            Destroy(bubble);
            //animal.GetComponent<Animal>().StopAnimalSound();
            animalRB.useGravity = true;
            animalCollider.isTrigger = false;
            animal.transform.parent = null;
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);
        }
    }
    //Game gameManager destroys all bubbles before starting next round
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
    
}
