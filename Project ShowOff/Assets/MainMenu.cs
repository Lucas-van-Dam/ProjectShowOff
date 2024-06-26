using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool MainMenuActive = false;

    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            MainMenuActive = !MainMenuActive;

            Time.timeScale = MainMenuActive ? 0 : 1;
            mainMenu.SetActive(MainMenuActive);
            // switch (MainMenuActive)
            // {
            //     case true:
            //         Time.timeScale = 0;
            //
            //         break;
            //     case false:
            //         Time.timeScale = 1;
            //         break;
            // }
        }

        if (!MainMenuActive)
            return;

        Vector2 dpadInput = new Vector2(Input.GetAxisRaw("DPADHorizontal"), Input.GetAxisRaw("DPADVertical"));
        if (Mathf.Abs(dpadInput.x) >= 0.2f)
        {
            if (dpadInput.x > 0)
            {
                MainMenuActive = !MainMenuActive;

                Time.timeScale = MainMenuActive ? 0 : 1;
                mainMenu.SetActive(MainMenuActive);
            }
            if (dpadInput.x < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            return;
        }
        if (Mathf.Abs(dpadInput.y) >= 0.2f)
        {
            if (dpadInput.y > 0)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (dpadInput.y < 0)
            {
                player.transform.position = UIManager.instance.respawnPoint;
            }
        }
    }
}
