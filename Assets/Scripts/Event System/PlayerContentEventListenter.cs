using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerContentEventListenter : UIEventListener
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    protected override void OnGameResumed()
    {
        _playerController.EnableControl();
    }

    protected override void OnShowUI()
    {
        _playerController.DisableControl();
    }
}
