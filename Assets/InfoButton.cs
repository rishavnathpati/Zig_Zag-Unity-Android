﻿using UnityEngine;

public class InfoButton : MonoBehaviour
{
    bool btnPressed;
    public GameObject infoPanel;
    // Start is called before the first frame update
    void Start()
    {
        btnPressed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGameButton()
    {
        JasperMovement.instance.StartGame();
    }

    public void infoButton()
    {
        //infoPanel.SetActive(true);

        if (btnPressed == false)
        {
            infoPanel.SetActive(true);
            btnPressed = true;
        }
        else if (btnPressed == true)
        {
            infoPanel.SetActive(false);
            btnPressed = false;
        }

    }

    public void fbButton()
    {
        Application.OpenURL("https://www.facebook.com/rishav.pati.1");

    }

    public void githubButton()
    {
        Application.OpenURL("https://github.com/rishavnathpati");

    }

    public void linkedinButton()
    {
        Application.OpenURL("https://www.linkedin.com/in/rishav-nath-p-67223bb9/");

    }

    public void gmailButton()
    {
        Application.OpenURL("https://mail.google.com/mail/?view=cm&fs=1&to=patirishavnath@gmail.com&su=Contact_for_apps&body=BODY&bcc=patirishavnath@gmail.com");

    }
}
