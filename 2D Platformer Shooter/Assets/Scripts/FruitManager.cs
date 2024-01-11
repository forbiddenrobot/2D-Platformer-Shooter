using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitManager : MonoBehaviour
{
    private int totalFruits;
    private int fruitsCollected;
    [SerializeField] private TextMeshProUGUI fruitFoundText;

    private void Start()
    {
        totalFruits = GameObject.FindGameObjectsWithTag("Fruit").Length;
        UpdateFruitText();
    }

    public void FruitCollected()
    {
        fruitsCollected += 1;
        UpdateFruitText();
    }

    private void UpdateFruitText()
    {
        fruitFoundText.text = "Fruits Found: " + fruitsCollected + "/" + totalFruits; 
    }

    public int StarsGot()
    {
        float percentFruitsFound = (float)fruitsCollected / totalFruits;
        Debug.Log(percentFruitsFound);
        if (percentFruitsFound >= 1f)
        {
            return 3;
        }
        else if (percentFruitsFound >= .7f)
        {
            Debug.Log("Sending 2");
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
