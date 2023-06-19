using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GnomeSpawner : MonoBehaviour
{
    public List<Gnome> gnomes;

    private List<Transform> gnomeSpawns = new List<Transform>();

    public int gnomesToSpawn;

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            gnomeSpawns.Add(child);
        }
    }

    private void Start()
    {
        if(gnomeSpawns.Count < gnomesToSpawn)
        {
            Debug.LogError("Amount of gnomes to spawn is greater than the amount of spawns");
        }

        SpawnGnomes();
    }

    public void SpawnGnomes()
    {
        for (int i = 0; i < gnomesToSpawn; i++)
        {
            Gnome gnomeToSpawn = FindGnome();

            int ran = Random.Range(0, gnomeToSpawn.gnomePrefabs.Count);

            GameObject newGnome = Instantiate(gnomeToSpawn.gnomePrefabs[ran], gnomeSpawns[i]);

            newGnome.GetComponent<GnomeObject>().gnome = gnomeToSpawn;
        }
    }

    private Gnome FindGnome()
    {
        List<Gnome> allGnomes = new List<Gnome>();
        allGnomes.AddRange(gnomes);

        List<float> probabilities = new List<float>();

        float totalWeight = 0;
        foreach (Gnome gnome in allGnomes)
        {
            totalWeight += gnome.gnomeWeight;
        }

        float cumulativeProbability = 0;
        foreach (Gnome gnome in allGnomes)
        {
            float cardProbability = gnome.gnomeWeight / totalWeight;
            cumulativeProbability += cardProbability;
            probabilities.Add(cumulativeProbability);
        }

        float randomValue = Random.value;

        for (int i = 0; i < probabilities.Count; i++)
        {
            if (randomValue <= probabilities[i])
            {
                return allGnomes[i];
            }
        }

        // If no gnome is selected, return null or handle the case as appropriate
        Debug.LogError("No Gnome Was Selected");
        return null;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform gnome in transform)
        {
            Gizmos.DrawWireSphere(gnome.position, 0.3f);
        }
        
    }
}
