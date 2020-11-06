using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpen : MonoBehaviour
{
    public GameObject menuPanel;
    public bool menuIsOpen;
    private void Start()
    {
        CloseMenu();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuIsOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }
    public void OpenMenu()
    {
        menuIsOpen = true;
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        menuIsOpen = false;
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
