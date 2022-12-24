using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Transform[] fruitPosition;
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private bool randomFruits;

    private int fruitIndex;
    void Start()
    {
        fruitPosition = GetComponentsInChildren<Transform>();

        for (int i = 1; i < fruitPosition.Length; i++)
        {
            GameObject newFruit = Instantiate(fruitPrefab, fruitPosition[i]);

            if (randomFruits)
            {
                fruitIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(FruitType)).Length);
                newFruit.GetComponent<Fruit_Item>().FruitSteup(fruitIndex);
            }
            else
            {
                newFruit.GetComponent<Fruit_Item>().FruitSteup(fruitIndex);
                fruitIndex++;

                if (fruitIndex > Enum.GetNames(typeof(FruitType)).Length)
                    fruitIndex = 0;
            }
            fruitPosition[i].GetComponent<SpriteRenderer>().sprite = null;

            int levelNumber = GameManager.instance.levelNumber;
            int totaAmountOfFruits = PlayerPrefs.GetInt("Level" + levelNumber + "TotalFruits");

            if (totaAmountOfFruits != fruitPosition.Length - 1)
                PlayerPrefs.SetInt("Level" + levelNumber + "TotaFruits", fruitPosition.Length - 1);

        }
    }
}

