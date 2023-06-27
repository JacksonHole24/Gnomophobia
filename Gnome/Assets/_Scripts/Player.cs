using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int m_maxAmmoBoxes = 3;
    [SerializeField] int m_ammoBoxes; // temporarily serialized

    public int GetAmmoBoxes() { return m_ammoBoxes; }
    public void RemoveAmmo(int amount)
    {
        if (m_ammoBoxes - amount < 0)
        {
            m_ammoBoxes = 0;
        }
        else
        {
            m_ammoBoxes -= amount;
        }
    }

    public void AddAmmo(int amount)
    {
        if (m_ammoBoxes + amount > m_maxAmmoBoxes)
        {
            m_ammoBoxes = m_maxAmmoBoxes;
        }
        else
        {
            m_ammoBoxes += amount;
        }
    }
}
