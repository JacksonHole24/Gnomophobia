using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] int m_scoreAmount; // temporarily serialized
    [SerializeField] TextMeshProUGUI m_scoreDisplay;
    [SerializeField] NumberSpinner hundredSpinner;
    [SerializeField] NumberSpinner tenSpinner;
    [SerializeField] NumberSpinner oneSpinner;

    [Header("Ammo")]
    [SerializeField] int m_maxAmmoBoxes = 3;
    [SerializeField] int m_ammoBoxes; // temporarily serialized
    [SerializeField] TextMeshProUGUI m_ammoBoxDisplay;

    #region Score
    public int GetScore() { return m_scoreAmount; }
    public void AddScore(int amount) 
    { 
        m_scoreAmount += amount; 
        DisplayScore();
    }
    public void SubtractScore(int amount) 
    { 
        m_scoreAmount -= amount; 
        if (m_scoreAmount < 0) m_scoreAmount = 0;
        DisplayScore();
    }
    public void SetScore(int amount) 
    { 
        m_scoreAmount = amount;
        DisplayScore();
    }

    public void DisplayScore()
    {
        if (m_scoreDisplay)
        {
            m_scoreDisplay.text = "Score: " + m_scoreAmount.ToString();
        }
        
        if(m_scoreAmount > 0)
        {
            int hundreds = m_scoreAmount / 100;
            int tens = (m_scoreAmount / 10) % 10;
            int units = m_scoreAmount % 10;

            hundredSpinner.Spin(hundreds);
            tenSpinner.Spin(tens);
            oneSpinner.Spin(units);
        }
        else
        {
            hundredSpinner.Spin(0);
            tenSpinner.Spin(0);
            oneSpinner.Spin(0);
        }
    }

    #endregion

    #region Ammo
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

        DisplayAmmoBoxes();
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

        DisplayAmmoBoxes();
    }

    public void DisplayAmmoBoxes()
    {
        m_ammoBoxDisplay.text = "Ammo Boxes: " + m_ammoBoxes.ToString() + "/" + m_maxAmmoBoxes.ToString();
    }
    #endregion


}
