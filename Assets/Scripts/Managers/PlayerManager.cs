using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { set; get; }
    //public static Action OnShoot;

    public Transform Player;

    [SerializeField] private Transform projectileOrigin;

    private Player playerInput;

    private void Awake()
    {
        Instance = this;
        playerInput = new Player();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        if (playerInput.PlayerMain.Shoot.triggered)
        {
            EventHolder.OnShoot?.Invoke(projectileOrigin);
        }
        //if (playerInput.PlayerMain.Shoot.triggered)
        //{
        //    OnShoot?.Invoke();
        //    Debug.Log("PUCA");
        //    EventHolder.OnShoot?.Invoke(projectileOrigin);
        //}
    }

}
