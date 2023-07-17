using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GnomeSpawner : MonoBehaviour
{
    public List<Gnome> gnomes;

    private List<Transform> gnomeSpawns = new List<Transform>();

    private List<Transform> occupiedGnomeSpawns = new List<Transform>();

    public int gnomesToSpawn;

    public bool onStart = false;

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

        if(onStart)
        {
            SpawnGnomes();
        }
    }

    public void SpawnGnomes()
    {
        for (int i = 0; i < gnomesToSpawn; i++)
        {
            Gnome gnomeToSpawn = FindGnome();
            int childrenRequired = CalculateChildrenRequired(gnomeToSpawn);

            Transform gnomeSpawn = GetAvailableGnomeSpawn(gnomeToSpawn, childrenRequired);

            if (gnomeSpawn != null)
            {
                occupiedGnomeSpawns.Add(gnomeSpawn); // Mark the gnomeSpawn as occupied
                int ran = Random.Range(0, gnomeToSpawn.gnomePrefabs.Count);
                GameObject newGnome = Instantiate(gnomeToSpawn.gnomePrefabs[ran], gnomeSpawn);
                newGnome.GetComponent<GnomeObject>().gnome = gnomeToSpawn;
            }
        }

        // Clear the occupied gnomeSpawns list for the next round of spawns
        occupiedGnomeSpawns.Clear();
    }

    private int CalculateChildrenRequired(Gnome gnomeToSpawn)
    {
        int currentChildren = 0;

        foreach (Transform spawn in gnomeSpawns)
        {
            foreach (Transform child in spawn)
            {
                if (child.GetComponent<GnomeObject>().gnome == gnomeToSpawn)
                {
                    currentChildren++;
                    break;
                }
            }
        }

        int childrenRequired = Mathf.Max(0, gnomesToSpawn - currentChildren);
        return childrenRequired;
    }

    private Transform GetAvailableGnomeSpawn(Gnome gnomeToSpawn, int childrenRequired)
    {
        foreach (Transform spawn in gnomeSpawns)
        {
            if (!occupiedGnomeSpawns.Contains(spawn) && spawn.childCount < childrenRequired)
            {
                bool alreadyHasChild = false;
                foreach (Transform child in spawn)
                {
                    if (child.GetComponent<GnomeObject>().gnome == gnomeToSpawn)
                    {
                        alreadyHasChild = true;
                        break;
                    }
                }

                if (!alreadyHasChild)
                {
                    return spawn;
                }
            }
        }

        return null;
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
