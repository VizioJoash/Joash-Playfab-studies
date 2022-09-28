using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    [Header("Panels")]
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject registrationPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void OnUserRegistered()
    {
        registrationPanel.SetActive(false);
        loginPanel.SetActive(true);
    }
}
