using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeBetweenSpawns = 1f;
    public BubbleSpawner[] bubbleSpawners;
    public string[] animalTags;
    public int[] animalIndex = {0, 1, 2, 3, 4};

    public int currentAnimalIndex;
    public string currentAnimalTag;
    public bool currentAnimalExists = false;

    public TMP_Text currentAnimalText;
    public TMP_Text scoreText;

    public TMP_Text correctText;
    public TMP_Text missedText;
    public TMP_Text wrongText;

    public int score = 0;
    public int missed = 0;
    public int wrong = 0;

    public Slider progressBarImage;
    public ParticleSystem progressBarParticleSystem;

    public UnityEvent onAddScore;

    public Canvas dataCanvas;
    // Start is called before the first frame update
    void Start()
    {
        onAddScore.AddListener(UpdateProgressBar);

        dataCanvas.enabled = false;
        ChangeCurrentAnimal();
        StartCoroutine(SpawnAnimals());
    }

    void UpdateProgressBar()
    {
        if (score <= 100)
        {
            progressBarImage.value = score * 10;
            progressBarParticleSystem.Play();
            // Debug.Log(score);
        }

    }
    //Updates the current animal to be selected
    public void ChangeCurrentAnimal()
    {
        currentAnimalIndex = Random.Range(0, animalTags.Length);
        currentAnimalTag = animalTags[currentAnimalIndex];

        currentAnimalText.text = currentAnimalTag;

        Debug.Log("Current Animal : " + currentAnimalTag);   
    }

    //Assign the shuffled animal indexes to each spawner and call the spawner
    public IEnumerator SpawnAnimals()
    {
        while(true)
        {
            AnimalIndexShuffle();
            for (int i = 0; i < bubbleSpawners.Length; i++)
            {
                if (animalIndex[i] == currentAnimalIndex) //there should be only one current animal per game loop
                {
                    if (!currentAnimalExists)
                    {
                        currentAnimalExists = true;
                    }
                    else
                    {
                        continue;
                    }
                    
                }
                bubbleSpawners[i].bubble.animalIndex = animalIndex[i];
                bubbleSpawners[i].SpawnBubble(); //Only spawn the bubble in BubbleSpawner. The script is written so that each animal is spawned as a child within each bubble. The game logic checks for child of selected bubble. 
                yield return new WaitForSeconds(timeBetweenSpawns);

                correctText.text = "Correct : " + score.ToString();
                missedText.text = "Missed : " + missed.ToString();
                wrongText.text = "Wrong : " + wrong.ToString();
            }
        }
    }
    //Each spawner has animal index which decides which animal to spawn
    //This function randomizes the animal indexes
    public void AnimalIndexShuffle()
    {
        for (int i = 0; i < animalIndex.Length; i++)
        {
            int tmp = animalIndex[i];
            int r = Random.Range(i, animalIndex.Length);
            animalIndex[i] = animalIndex[r];
            animalIndex[r] = tmp;
        }
    }
    //Increase score if correct animal is selected
    public void AddScore()
    {   
        score++;
        scoreText.text = "Score : " + score.ToString();
        onAddScore?.Invoke();
        ChangeCurrentAnimal();
    }
    public void EnableDataCanvas()
    {
        dataCanvas.enabled = true;
        Time.timeScale = 0;
    }
    public void DisableDataCanvas()
    {
        dataCanvas.enabled = false;
        Time.timeScale = 1;
    }
}
