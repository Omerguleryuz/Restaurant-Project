using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text goldText;

    public void PrintGold()
    {
        goldText.text = FindObjectOfType<GoldManager>().gold.ToString();
    }
}
