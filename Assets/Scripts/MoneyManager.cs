using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int currentMoney = 100;
    bool isAddingMoney = false;
    public GameObject moneyText;
    public GameObject spendingText;
    public GameManager gameManager;
    private int earnings = 1;
    private int spending = 0;
    private bool isSpendingMoney;

    public void Spend(CellType type)
    {
        if (type == CellType.Grass)
            spending += 1;
        if (type == CellType.Road)
            spending += 2;
        if (type == CellType.Structure)
            spending += 5;
        if (type == CellType.SpecialStructure)
            spending += 10;
        isSpendingMoney = true;
        return;
    }

    public int GetEarning()
    {
        return earnings;
    }

    private int GetMoneyIncrement()
    {
        return earnings - spending;
    }

    void Update()
    {
        //if (isSpendingMoney)
        //{
            //StartCoroutine(SpendingMoney(spending));
        //}

        if (isAddingMoney == false)
        {
            StartCoroutine(AddMoney(GetMoneyIncrement()));
        }
    }

    IEnumerator SpendingMoney(int amount)
    {
        spendingText.SetActive(true);
        spendingText.GetComponent<Text>().text = "花掉了" + spending;
        yield return new WaitForSeconds(1);
        spendingText.SetActive(false);
        spending = 0;
        isSpendingMoney = false;
    }

    IEnumerator AddMoney(int amount)
    {
        isAddingMoney = true;
        currentMoney += amount;
        moneyText.GetComponent<Text>().text = currentMoney + "";  
        yield return new WaitForSeconds(1);
        isAddingMoney = false;
    }
}
