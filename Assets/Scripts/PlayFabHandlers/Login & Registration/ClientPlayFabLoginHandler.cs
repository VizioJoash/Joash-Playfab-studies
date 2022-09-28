using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayFab;
using PlayFab.ClientModels;


public class ClientPlayFabLoginHandler : MonoBehaviour
{
    public UnityEvent<LoginResult> onLoginSuccess = new UnityEvent<LoginResult>();
    public UnityEvent<PlayFabError> onLoginFail = new UnityEvent<PlayFabError>();

    private void Awake()
    {
        onLoginSuccess.AddListener((LoginResult result) => { Debug.Log("Successfully Logged in: " + result.SessionTicket); });
        onLoginFail.AddListener((PlayFabError error) => { Debug.Log("Failed to login user: " + error.ErrorMessage); });
    }

    public void LoginUser(string email, string password)
    {
        var loginReq = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,
                GetUserVirtualCurrency = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(loginReq, a => onLoginSuccess?.Invoke(a), a => onLoginFail?.Invoke(a));
    }
}
