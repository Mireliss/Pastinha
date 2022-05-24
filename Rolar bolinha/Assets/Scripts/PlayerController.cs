using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSeed; 
        
    private GameInput _gameInput;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput; 

    private void OnEnable()
    {
        // inicialização da variavel 
        _gameInput = new GameInput();
        
        // Referencia dos componentes no mesmo objeto da Unity
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        
        // Referencia para a camera main guardada na classe camera
        _mainCamera = Camera.main;

        // delegate do action triggered no player input
        _playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        // Troquei Gameplay por Menu!!! <-------------
        // comparando o nome do action que esta chegando com o nome do action de moviment
        if (obj.action.name.CompareTo(_gameInput.Gameplay.Movement.name) == 0)
        {
           // atribuir ao moveInput o valor proveniente do Input do jogador como um vector
            _moveInput = obj.ReadValue<Vector2>();
        }
    }

    private void Move()
    {
        // linha 1(Y)- calcula o movimento no eixo da camera para o movimento frente/tras
        // linha 2(X)- calcula o movimento no eixo da camera para o movimento esquerda/direita
        _rigidbody.AddForce((_mainCamera.transform.forward * 
            _moveInput.y + _mainCamera.transform.right * _moveInput.x) * 
                            moveSeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        Move();
    }
}
