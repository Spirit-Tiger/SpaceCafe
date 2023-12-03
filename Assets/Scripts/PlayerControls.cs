using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool _playerInKitchen = false;
    public Transform Player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _playerInKitchen)
        {
            _playerInKitchen = false;
            Player.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.D) && !_playerInKitchen)
        {
            _playerInKitchen = true;
            Player.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public void SwitchToStart()
    {
        _playerInKitchen = false;
        Player.rotation = Quaternion.Euler(0f, -90f, 0f);
    }
}
