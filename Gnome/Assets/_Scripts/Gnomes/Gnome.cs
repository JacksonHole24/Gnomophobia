using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gnome", menuName = "Create New Gnome")]

public class Gnome : ScriptableObject
{
    [Header("Gnome")]
    public string gnomeType;
    [Tooltip("All prefabs for this gnome type and a random one will be spawned")]
    public List<GameObject> gnomePrefabs;

    [Header("Gnome Info")]
    [Tooltip("Points gained/lost from shooting this gnome")]
    public int gnomeValue;
    [Tooltip("If true the gnomes value makes the player lose points and if true they gain points")]
    public bool losesPoints;
    [Tooltip("The higher the weight the higher the chance of the gnome spawning")]
    public float gnomeWeight;
}
