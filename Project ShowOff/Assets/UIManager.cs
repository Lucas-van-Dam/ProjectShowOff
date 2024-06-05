using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class UIManager : MonoBehaviour
{

    public static UIManager instance { get; private set; }

    public static event Action<int> FragmentCollected;

    int fragments;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }


    void Start()
    {

    }

    //When an enemy dies, spawn death effects and update currency
    public void FragmentCollectedEvent(int value)
    {
        if (!infiniMoney)
        {
            money += value;
            MoneyChanged?.Invoke(money);
        }
        else MoneyChanged?.Invoke(infiniteMoney);

        Instantiate(explosionEffect, position, Quaternion.identity);
        GameObject numPopup = Instantiate(cashPopup, position, Quaternion.identity);
        numPopup.GetComponent<CashPopupScript>().Initialize(value);

        FragmentCollected?.Invoke(fragments);

    }
}
