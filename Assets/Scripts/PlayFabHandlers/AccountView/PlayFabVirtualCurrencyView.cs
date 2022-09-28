using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

public class PlayFabVirtualCurrencyView : MonoBehaviour
{
    int currentMetaCoins;

    public UnityEvent<ModifyUserVirtualCurrencyResult> OnCurrencyChanged;
    public UnityEvent<PlayFabError> OnCurrencyChangeFailed;

    private void Awake()
    {
        OnCurrencyChanged.AddListener((ModifyUserVirtualCurrencyResult a) => { Debug.Log("Currency Changed. Current MetaCoin Value: " + a.Balance); });
        OnCurrencyChangeFailed.AddListener(error => Debug.Log("Error Changing Currency: " + error.ErrorMessage));
    }

    public void AddCurrency(int add)
    {
        var AddCurrencyReq = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "MC",
            Amount = add
        };

        PlayFabClientAPI.AddUserVirtualCurrency(AddCurrencyReq, _ => OnCurrencyChanged?.Invoke(_), _ => OnCurrencyChangeFailed?.Invoke(_));
    }

    public void SubractCurrency(int sub)
    {
        var SubtractCurrencyReq = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "MC",
            Amount = sub
        };

        PlayFabClientAPI.SubtractUserVirtualCurrency(SubtractCurrencyReq, _ => OnCurrencyChanged?.Invoke(_), _ => OnCurrencyChangeFailed?.Invoke(_));
    }
}
