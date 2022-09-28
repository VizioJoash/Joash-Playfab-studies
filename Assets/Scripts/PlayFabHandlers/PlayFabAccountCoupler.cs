using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabAccountCoupler : MonoBehaviour
{
    public static PlayFabAccountCoupler Instance;

    [Header("PlayFab Handlers")]
    [SerializeField] ClientPlayFabRegistrationHandler registrationHandler;
    [SerializeField] ClientPlayFabLoginHandler loginHandler;
    [SerializeField] PlayFabVirtualCurrencyView virtualCurrencyView;

    [Header("Login")]
    [SerializeField] TMP_InputField LoginEmailField;
    [SerializeField] TMP_InputField LoginPasswordField;
    [SerializeField] Button LoginButton;

    [Header("Registration")]
    [SerializeField] TMP_InputField RegisterDisplayNameField;
    [SerializeField] TMP_InputField RegisterEmailField;
    [SerializeField] TMP_InputField RegisterPasswordField;
    [SerializeField] Button RegisterButton;

    [Header("MetaCoins")]
    [SerializeField] Button RemoveCoinButton;
    [SerializeField] Button AddCoinButton;
    [SerializeField] TMP_Text MetaCoinText;

    [Header("User Info")]
    [SerializeField] TMP_Text PlayFabIDText;
    [SerializeField] TMP_Text DisplayNameText;
    [SerializeField] TMP_Text SessionIDText;

    private void Awake()
    {
        Instance = this;

        loginHandler.onLoginSuccess.AddListener( result => { UpdateVirtualCurrencyUI(result.InfoResultPayload.UserVirtualCurrency["MC"]); } );
        loginHandler.onLoginSuccess.AddListener(UpdateUserInfoUI);

        virtualCurrencyView.OnCurrencyChanged.AddListener( result => { UpdateVirtualCurrencyUI(result.Balance); } );

        RegisterButton.onClick.AddListener(() => registrationHandler.RegisterUser(RegisterEmailField.text, RegisterPasswordField.text, RegisterDisplayNameField.text));
        LoginButton.onClick.AddListener(() => loginHandler.LoginUser(LoginEmailField.text, LoginPasswordField.text));

        RemoveCoinButton.onClick.AddListener(() => virtualCurrencyView.SubractCurrency(10));
        AddCoinButton.onClick.AddListener(() => virtualCurrencyView.AddCurrency(10));
    }

    public void UpdateUserInfoUI(LoginResult result)
    {
        PlayFabIDText.text = "PlayFab ID: " + result.PlayFabId;
        DisplayNameText.text = "Display Name: " + result.InfoResultPayload.PlayerProfile.DisplayName;
        SessionIDText.text = "Session ID " + result.SessionTicket;
    }

    public void UpdateVirtualCurrencyUI(int amount) {
        MetaCoinText.text = $"User MetaCoins: {amount}";
    }
}
