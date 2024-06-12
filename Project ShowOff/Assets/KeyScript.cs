using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{

    [SerializeField]
    Image m_Image;

    int fragments;

    [SerializeField]
    Sprite[] keyStages;
    [SerializeField]
    Image keyBar;

    [SerializeField]
    int[] fragNeededPerLevel;
    [SerializeField]
    float[] imageFillPerLevel;




    // Start is called before the first frame update
    void Start()
    { 
        UIManager.FragmentCollected += fragmentCollected; 
    
        if (m_Image == null)
        {
            m_Image = GetComponent<Image>();
        }

        keyBar.fillAmount = 0;
    }

    private void OnDestroy()
    { UIManager.FragmentCollected -= fragmentCollected; }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fragmentCollected(int fragments)
    {
        for(int i = 1; i < fragNeededPerLevel.Length; i++)
        {
            if (fragNeededPerLevel[i] >= fragments)
            {

                m_Image.sprite = keyStages[i - 1];

                float levelFill = imageFillPerLevel[i] - imageFillPerLevel[i - 1];
                float levelNeeded = (fragNeededPerLevel[i] - fragNeededPerLevel[i - 1]);
                float levelGot = (fragments - fragNeededPerLevel[i - 1]);
                float levelPercentage = levelGot / levelNeeded;

                keyBar.fillAmount = imageFillPerLevel[i - 1] + levelFill * levelPercentage;

                //Debug.Log(fragments);

                //Debug.Log(levelPercentage);

                if (fragments >= fragNeededPerLevel[i - 1])
                {
                    m_Image.sprite = keyStages[i];
                    UIManager.instance.keyTier = i;
                }

                if (levelGot == levelNeeded)
                {
                    m_Image.sprite = keyStages[i+1];
                    UIManager.instance.keyTier = i + 1;
                }

                break;
            }
        }
    }
}
