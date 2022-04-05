using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public ARSessionManager ArSessionManager;
    public TMP_Text MessageText;
    public RectTransform MessagePanel,MainMenu,GenSelect,Controller;

    private void Start()
    {
        MessagePanel.gameObject.SetActive(false);
        GenSelect.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }
    public void StartAR()
    {
        if (ArSessionManager.CheckAR())
        {
            MainMenu.gameObject.SetActive(false);
            GenSelect.gameObject.SetActive(true);
        }
        else
        {
            ShowMessage("Device Not Supported");
        }
    }

    public void Spawn()
    {
        GenSelect.gameObject.SetActive(false);
        ArSessionManager.StartAR();
    }

    public void BacktoGen()
    {
        ArSessionManager.StopAR();
        GenSelect.gameObject.SetActive(true);
        Controller.gameObject.SetActive(false);
        GameObject clearUp = GameObject.FindGameObjectWithTag("ARMultiModel");
        Destroy(clearUp);
    }

    public void ExitAPP()
    {
        Application.Quit();
    }

    public void ShowMessage(string message)
    {
        MessagePanel.gameObject.SetActive(true);
        MessageText.text = message;
        Invoke(nameof(DisableMessage), 2f);
    }
    private void DisableMessage()
    {
        MessagePanel.gameObject.SetActive(false);
    }
}