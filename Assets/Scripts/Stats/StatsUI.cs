using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;

public class StatsUI : MonoBehaviour
{
    public GameObject moneyUI, workerUI, productivityUI, RelationsUI, kingUI;

    TMP_Text moneyText, workerText, productivityText, kingText;
    
    private void Start()
    {
        moneyText = moneyUI.GetComponent<TMP_Text>();
        workerText = workerUI.GetComponent<TMP_Text>();
        productivityText = productivityUI.GetComponent<TMP_Text>();
        kingText = kingUI.GetComponent<TMP_Text>();

        StatsManager.OnStatChanged += UpdateStatUI;
        StatsManager.OnRelationChanged += UpdateRelationUI;
    }

   public void UpdateStatUI(StatType type, int amount)
    {
        switch(type)
        {
            case StatType.money:
                moneyText.text = $"Money: {amount}";
                break;

            case StatType.availableWorkers:
                workerText.text = $"Worker: {amount}";
                break;

            case StatType.productivity:
                productivityText.text = $"Productivity: {amount}%";
                break;

            default:
                Debug.LogError($"There are no UI elements of this type: {type}");
                break;
        }
    }

    public void UpdateRelationUI(RelationType type, int amount)
    {
        switch (type)
        {
            case RelationType.King:
                kingText.text = $"King: {amount}";
                break;
            default:
                Debug.LogError($"There are no UI elements of this relation: {type}");
                break;
        }
    }




}
