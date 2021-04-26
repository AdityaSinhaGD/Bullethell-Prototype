using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();

        SeekingEnemy[] seeking = FindObjectsOfType<SeekingEnemy>(); //Adds seeking enemies to list
        for (int i = 0; i < seeking.Length; i++) 
            enemies.Add(seeking[i].gameObject);

        StationaryEnemy[] stationary = FindObjectsOfType<StationaryEnemy>(); //Adds stationary enemies to list
        for (int i = 0; i < stationary.Length; i++) 
            enemies.Add(stationary[i].gameObject);

        HybridEnemy[] hybrid = FindObjectsOfType<HybridEnemy>(); //Adds hybrid enemies to list
        for (int i = 0; i < hybrid.Length; i++) 
            enemies.Add(hybrid[i].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = enemies.Count-1; i >= 0; i--) //Clears defeated enemies
        {
            if (enemies[i] == null)
                enemies.RemoveAt(i);
        }

        if (door != null) //Hides door when all enemies are defeated
        {
            if (enemies.Count == 0 && door.activeInHierarchy)
                door.SetActive(false);
        }
    }
}
