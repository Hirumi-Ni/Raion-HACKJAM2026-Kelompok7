using UnityEngine;

[System.Serializable]
public class AnimalSpawns
{
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private int animalMaxCount;
}

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private AnimalSpawns[] animal; //0 domba, 1 wolf, dll
    [SerializeField] private float timerInterval;
    [SerializeField] private Transform spawnerArea;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void CheckAnimalSpawn()
    {

    }

    private void HandleAnimalSpawn()
    {

    }
}
