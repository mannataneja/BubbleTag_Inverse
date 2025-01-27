using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bubbles : MonoBehaviour
{
    public bool isDistractor = false;
    public GameObject bubble;
    public Material currentBubbleMaterial;
    public Material dullBubbleMaterial;
    public Material flickerMaterial;

    public GameObject[] animals;
    public int animalIndex; //animalIndex is passed from GameManager
    public string animalTag;

    public float speed = 5f;
    public ParticleSystem pop;
    

    public float maxHeight = 7f;

    GameObject animal;
    Rigidbody animalRB;
    Collider animalCollider;


    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();

        //SpawnAnimals animal as soon as bubble is spawned. Animal index is passed from Game Manager
        if (!isDistractor)
        {
            Instantiate(animals[animalIndex], transform);
        }
        if (isDistractor)
        {
            animalIndex = Random.Range(0, gameManager.animalIndex.Length);
            if (animalIndex == gameManager.currentAnimalIndex)
            {
                if (animalIndex < gameManager.animalIndex.Length - 1)
                {
                    animalIndex++;
                }
                else
                {
                    animalIndex--;
                }
            }
            Instantiate(animals[animalIndex], transform);
        }

        //Get the transform of newly spawned animal
        animal = transform.GetChild(transform.childCount - 1).gameObject;
        animalRB = animal.GetComponent<Rigidbody>();
        animalCollider = animal.GetComponent<Collider>();
        animalTag = animal.tag;

        animalRB.useGravity = false;
        animalCollider.isTrigger = true;

        // originalBubbleMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().material;

        if (isDistractor)
        {
            Debug.Log("I am distractor" + gameObject.name);
            SetFlickerMaterial();
        }
    }
    void Update()
    {
        //Make bubble float up
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y >= maxHeight) //bubble and animal is despawned after reaching maximum height
        {
            if(animal.GetComponent<Animal>().isCurrentAnimal)
            {
                gameManager.currentAnimalExists = false;
                //gameManager.missed++;
                GameData.playerMissed++;
            }
            Destroy(gameObject);
        }
    }
    public void SetCurrentBubbleMaterial()
    {
        if (!isDistractor)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material = currentBubbleMaterial;
        }
    }
    public void SetDullBubbleMaterial()
    {
        if(!isDistractor)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material = dullBubbleMaterial;
        }
    }
    public void SetFlickerMaterial()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = flickerMaterial;
    }
    //Bubble is popped if correct animal is selected
    public IEnumerator PopBubble() 
    {
        if(animal.GetComponent<Animal>().selectable == true)
        {
            gameManager.currentAnimalExists = false;
            gameManager.AddScore();
            GameData.EndTimer();
            pop.Play();
            Destroy(bubble);
            //animal.GetComponent<Animal>().StopAnimalSound();
            //animalRB.useGravity = true;
            animalCollider.isTrigger = false;
            animal.transform.parent = null;
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);
            Destroy(animal);
        }
    }
}
