using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FarmAnimal : MonoBehaviour
{
    public string animalName;
    public int level = 1;
    public int maxLevel = 3;
    public int mergingRequirement = 2; //How many same animals can be merged into a higher level animal
    public float sizeScale = 0.5f; //How the size of the animal scale with level, e.g., 1.0 -> 1.5 -> 2.0
}
