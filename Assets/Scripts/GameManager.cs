using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeBetweenSpawns = 3f;
    public BubbleSpawner[] bubbleSpawners;
    public int activeSpawners = 0;
    public int spawner_i = 0;
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

    public Slider progressBarImage;
    public ParticleSystem progressBarParticleSystem;

    public UnityEvent onAddScore;

    public Canvas dataCanvas;

    AudioSource[] audioSources; 
    public Canvas HUD;

    public GameDataCollector collector;


    void Start()
    {
        onAddScore.AddListener(UpdateProgressBar);

        dataCanvas.enabled = false;
        
        ChangeCurrentAnimal();
        StartCoroutine(SpawnAnimals());
    }

    void UpdateProgressBar()
    {
        if (GameData.playerScore <= 100)
        {
            progressBarImage.value = GameData.playerScore * 10;
            progressBarParticleSystem.Play();
        }

    }

    //Updates the current animal to be selected
    public void ChangeCurrentAnimal()
    {
        currentAnimalIndex = Random.Range(0, animalTags.Length);
        currentAnimalTag = animalTags[currentAnimalIndex];

        currentAnimalText.text = currentAnimalTag;

        Debug.Log("Current Animal : " + currentAnimalTag);
        if(activeSpawners < 5)
        {
            activeSpawners++;
        }
        if(timeBetweenSpawns < 10)
        {
            timeBetweenSpawns++;
        }
    }

    //Assign the shuffled animal indexes to each spawner and call the spawner
    public IEnumerator SpawnAnimals()
    {
        GameData.StartTimer();
        while(true)
        {
            AnimalIndexShuffle();
            for (int i = 0; i < bubbleSpawners.Length; i++)
            {
                spawner_i++;
                if(spawner_i >= activeSpawners)
                {
                    spawner_i = 0;
                }
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
                bubbleSpawners[spawner_i].bubble.animalIndex = animalIndex[i];
                bubbleSpawners[spawner_i].SpawnBubble(); //Only spawn the bubble in BubbleSpawner. The script is written so that each animal is spawned as a child within each bubble. The game logic checks for child of selected bubble. 
                yield return new WaitForSeconds(timeBetweenSpawns);

                correctText.text = "Correct : " + GameData.playerScore.ToString();
                missedText.text = "Missed : " + GameData.playerMissed.ToString();
                wrongText.text = "Wrong : " + GameData.playerWrong.ToString();
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
        //score++;
        GameData.playerScore++;
        AudioManager.instance.playSFX(AudioManager.instance.correctSelection);
        FarmManager.instance.AddAnimal(currentAnimalTag);
        scoreText.text = "Score : " + GameData.playerScore.ToString();
        onAddScore?.Invoke();
        ChangeCurrentAnimal();
    }

    public void EnableDataCanvas()
    {
        dataCanvas.enabled = true;
        HUD.enabled = false;
        audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
        Time.timeScale = 0;    
    }

    public void DisableDataCanvas()
    {
        dataCanvas.enabled = false;
        HUD.enabled = true;
        audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.UnPause();
        }      
        Time.timeScale = 1;
    }
}
