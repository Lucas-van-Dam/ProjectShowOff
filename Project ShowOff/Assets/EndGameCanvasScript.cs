using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCanvasScript : MonoBehaviour
{

    bool isMoving;
    bool endNow;

    float timer;

    [SerializeField]
    RectTransform transform;

    [SerializeField]
    float schpee;

    // Start is called before the first frame update
    void Start()
    {
        if (transform == null)
        {
            transform = GetComponentInChildren<RectTransform>();
        }
    }

    private void OnEnable()
    {
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;

            if(timer > 5) 
            {
                transform.anchoredPosition += new Vector2(0, Time.deltaTime * schpee);
            }

            //if have the time and doesnt work, make this work
            if (transform.anchoredPosition.y >= 4240)
            {
                isMoving = false;
                endNow = true;
                timer = 0;
            }
        }
        if(endNow) 
        {
            timer += Time.deltaTime;

            if (timer > 3)
            {
                UIManager.instance.EndGame();
            }
        }
    }
}
