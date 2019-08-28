using System;
using Project.Scripts;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {
        _player = PlayerController.Player;
    }

    private void FixedUpdate()
    {
        const float speed = -0.1f;
        transform.position += _player.transform.forward * speed;
    }
}
