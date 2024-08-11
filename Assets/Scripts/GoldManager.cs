using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int gold;

    public void EarnGold()
    {
        gold += 10;
    }
}
