using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveDisplay : MonoBehaviour
{
    public Color positiveCashColor = Color.white;
    public Color negativeCashColor = Color.red;
    public Text cashText;
    public Text objectiveText;
    public Text timerText;

    public void SetObjectiveAmount(int amount)
    {
        objectiveText.text =  "Make " + CashToString(amount);
    }
    public void SetObjectiveDuration(int amount)
    {
        timerText.text = amount.ToString();
    }
    public void SetCashAmount(int amount)
    {
        cashText.color = amount >= 0 ? positiveCashColor : negativeCashColor;
        cashText.text = CashToString(amount);
    }

    public static string CashToString(int amount)
    {
        string intText = amount.ToString();

        int digitCount = 0;
        for (int i = intText.Length - 1; i > 0; i--)
        {
            digitCount++;
            if(digitCount == 3)
            {
                intText = intText.Insert(i, " ");
                digitCount = -1;
            }
        }

        return intText + " $";
    }
}
