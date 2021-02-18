using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public GameObject moneyText;
    public GameObject spendingText;
    public GameManager gameManager;
    public int currentMoney;

    private int earnings = 1;
    private int spending = 0;
    private bool isSpendingMoney;
    private bool isAddingMoney = false;

    private const int GrassCost = 5;
    private const int RoadCost = 3;
    private const int StructureCost = 15;
    private const int SpecialStructureCost = 30;

    public void Spend(CellType type)
    {
        if (type == CellType.Grass)
            spending += GrassCost;
        if (type == CellType.Road)
            spending += RoadCost;
        if (type == CellType.Structure)
            spending += StructureCost;
        if (type == CellType.SpecialStructure)
            spending += SpecialStructureCost;
        //isSpendingMoney = true;
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
        spendingText.GetComponent<Text>().text = "花掉了: " + spending;
        yield return new WaitForSeconds(1);
        spendingText.SetActive(false);
        spending = 0;
        isSpendingMoney = false;
    }

    // 金币每秒增加 amount （amount可以是负数）
    IEnumerator AddMoney(int amount)
    {
        isAddingMoney = true;
        currentMoney += amount;
        moneyText.GetComponent<Text>().text = currentMoney + "";
        spending = 0;
        yield return new WaitForSeconds(1);
        isAddingMoney = false;
    }
}
