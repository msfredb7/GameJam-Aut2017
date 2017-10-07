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
        objectiveText.text =  "Make " + amount.ToString() + " $";
    }
    public void SetObjectiveDuration(int amount)
    {
        timerText.text = amount.ToString();
    }
    public void SetCashAmount(int amount)
    {
        cashText.color = amount >= 0 ? positiveCashColor : negativeCashColor;
        cashText.text = amount.ToString() + " $";
    }
}
