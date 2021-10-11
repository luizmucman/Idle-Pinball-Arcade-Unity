using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabSaveLoadManager 
{
    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;

    public void StartAuthentication()
    {
        if (_AuthService.AuthType == 0)
        {
            _AuthService.Authenticate(Authtypes.Silent);
        }
        else
        {
            _AuthService.Authenticate();
        }
    }

    private void SilentAuthentication()
    {
    }
}
