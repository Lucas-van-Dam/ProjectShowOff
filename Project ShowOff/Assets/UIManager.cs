using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class UIManager : MonoBehaviour
{
    
    public static UIManager instance { get; private set; }

    public static event Action<int> FragmentCollected;

    int fragments;
    public int keyTier;
    int currentPlayer;

    int Totems;

    [SerializeField]
    GameObject[] characterButtons;

    public Image promtImage;

    [SerializeField]
    public Image characterSwitcher;
    [SerializeField]
    public Image characterSwitchingSpriteSlot;
    [SerializeField]
    public Sprite[] CharacterSwitchingSprite;

    [SerializeField] private GameObject TotemsParent;
    [SerializeField] private Sprite totemCollectedSprite;

    [Header("Input")]
    [SerializeField] public KeyCode interactionKey = KeyCode.KeypadPeriod;

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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FragmentCollectedEvent();
        }
    }

    public void FragmentCollectedEvent()
    {
        fragments++;
        FragmentCollected?.Invoke(fragments);
    }

    public void TotemCollected()
    {
        Debug.Log("Totem Got");
        TotemsParent.transform.GetChild(Totems).GetComponent<Image>().sprite = totemCollectedSprite;
        Totems++;
    }

    public void changeCharacter(int character)
    {
        for(int i = 0; i < characterButtons.Length; i++) 
        {
            characterButtons[i].SetActive(false);
        }

        //Debug.Log(character);
        characterButtons[character].SetActive(true);
    }

    public void ToggleCharacterSwitching(int character, bool toggle)
    {
        characterSwitcher.gameObject.SetActive(toggle);

        characterSwitchingSpriteSlot.sprite = CharacterSwitchingSprite[character];
    }
}
