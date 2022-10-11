using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPUIController : UnitySingleton<FPPUIController>
{
    [SerializeField] private GameObject _pauseUI;

    public void UpdateSensitivity(float value)
    {
        PlayerController.Instance.SetSensitivity(value);
    }

    public void TogglePauseMenu(bool shouldEnable)
    {
        _pauseUI.SetActive(shouldEnable);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

}
