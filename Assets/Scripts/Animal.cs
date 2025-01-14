using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    GameManager manager;

    string animalTag;

    public bool selectable = false;
    public bool isCurrentAnimal = false;

    bool rotate = false;
    bool floating = false;
    float floatingSpeed = 5f;

   
    // Start is called before the first frame update
    void Start()
    {
        animalTag = gameObject.tag;
        manager = GameObject.FindAnyObjectByType<GameManager>();
        CheckForCurrentAnimal();
        StartCoroutine(AnimalSelectable());
    }

    // Update is called once per frame
    void Update()
    {
        //Make animal float away
        if (floating)
        {
            transform.Translate(Vector3.up * floatingSpeed * Time.deltaTime);
        }
        //Make animal rotate when it floats away
        if (rotate)
        {
            Debug.Log("roatating");
            transform.Rotate(1, 1, 1);
        }
        if (selectable && !isCurrentAnimal)
        {
            gameObject.transform.parent.GetComponent<Bubbles>().SetCurrentBubbleMaterial();
        }
    }
    //Check for animal colliding with the ground when it falls after bubble pop
    //Make animal spin and float away before destroying
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("Ground Collision");
            //gameObject.GetComponent<Rigidbody>().useGravity = false; //animal floats up after hitting ground
            //StopAnimalSound();
            //rotate = true; //animal spins
            //floating = true;
            Destroy(gameObject, 2f);
        }
    }
    public void CheckForCurrentAnimal()
    {
        if (animalTag == manager.currentAnimalTag)
        {
            isCurrentAnimal = true;
            gameObject.transform.parent.GetComponent<Bubbles>().SetFlickerMaterial();
        }
    }
    //Play sound of current animal
    public IEnumerator AnimalSelectable()
    {
        yield return new WaitForSeconds(manager.timeBetweenSpawns); //wait after spawning
        selectable = true; //make it interactable
        //gameObject.transform.parent.GetComponent<Bubbles>().SetCurrentBubbleMaterial(); //make bubble selectable/transparent color
        gameObject.GetComponent<AudioSource>().Play(); //play animal sound
        yield return new WaitForSeconds(manager.timeBetweenSpawns - 0.5f); //wait before making it un-selectable
        selectable = false;
        if (!isCurrentAnimal)
        {
            gameObject.transform.parent.GetComponent<Bubbles>().SetDullBubbleMaterial();

        }
        gameObject.GetComponent<AudioSource>().Stop();
    }
    public void StopAnimalSound()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
