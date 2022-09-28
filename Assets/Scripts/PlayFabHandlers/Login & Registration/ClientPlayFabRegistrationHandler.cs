using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using UnityEngine.Events;
using PlayFab.ClientModels;

public class ClientPlayFabRegistrationHandler : MonoBehaviour
{
    public UnityEvent<RegisterPlayFabUserResult> onRegisterSuccess = new UnityEvent<RegisterPlayFabUserResult>();
    public UnityEvent<PlayFabError> onRegisterFail = new UnityEvent<PlayFabError>();

    private void Awake()
    {
        onRegisterSuccess.AddListener((RegisterPlayFabUserResult result) => { Debug.Log("Successfully registered!"); });
        onRegisterFail.AddListener((PlayFabError error) => { Debug.Log("Failed to register user: " + error.ErrorMessage); });
    }

    public void RegisterUser(string email, string password, string displayName)
    {
        var registerReq = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            DisplayName = displayName,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerReq, a => onRegisterSuccess?.Invoke(a), a => onRegisterFail?.Invoke(a));
    }
}
