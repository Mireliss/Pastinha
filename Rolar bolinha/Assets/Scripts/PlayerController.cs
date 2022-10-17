using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
  
    public int coins = 0;
    public int moneys = 0;
    public int maxHealth = 100;


    public float moveSeed;
    public float maxVelocity;

    public float rayDistance;
    public LayerMask groundLayer;

    public float jumpForce;
        
    private GameInput _gameInput;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput;

    private bool _isGrounded;
    private int _currentHealth;

    

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
        _currentHealth = maxHealth;
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

        if (obj.action.name.CompareTo(_gameInput.Gameplay.Jump.name) == 0)
        {
            if (obj.performed) Jump();
        }
    }

    private void Move()
    {
        Vector3 camForward = _mainCamera.transform.forward;
        camForward.y = 0;
        
        // linha 1(Y)- calcula o movimento no eixo da camera para o movimento frente/tras
       
        Vector3 moveVertical = camForward * _moveInput.y;
        Vector3 camRigth = _mainCamera.transform.right;
        
        // linha 2(X)- calcula o movimento no eixo da camera para o movimento esquerda/direita
        
        Vector3 moveHorizontal = camRigth * _moveInput.x;
        _rigidbody.AddForce((_mainCamera.transform.forward * 
            _moveInput.y + _mainCamera.transform.right * _moveInput.x) * 
                            moveSeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        Move();
        LimitVelocity();
    }

    private void LimitVelocity()
    {
        // pegar a velocidade doplayer
        Vector3 velocity = _rigidbody.velocity;

        //checar se a velocidade está dentro dos limites nos diferentes eixos
        // limitando o eixo x usando ifs, abs e sign.
        if (Math.Abs(velocity.x) > maxVelocity) velocity.x = Mathf.Sign(velocity.x) * maxVelocity;
        
        // maxVelocity < velocity.z < maxVelocity
        velocity.z = Mathf.Clamp(value: velocity.z, min: -maxVelocity, maxVelocity);
       
        //alterar a velocidade do player para ficar dentro dos limites
        _rigidbody.velocity = velocity;
    }
    // * como fazer o jogador pular
   
    // * 1 - Checar se o jogador está no chão
    // -- a - Checar colisão a partir da física (usando os eventos de colisão)
    // -- a - Vantagens: fácil de implementar (adicionar uma função que já existe na Unity - OnCollisionEnter)
    // -- a - Desvantagen: não sabemos a hora exata que a unity vai chamar essa função (pode ser que o jogador toque no
    //chão e demore alguns frames para o jogo saber que ele está no chão)
    // -- b - através do raycast: o-- bolinha vai atirar um raio, o raio vai bater em algum objeto e a  gente 
    //recebe o resultado dessa colisão.
    // -- b - Podemos usar Layers pra definir quais objetos que o raycast deve checar colisão 
    // -- b - vantagens: resposta da colisão é imediata
    // -- b - desvantagens: um pouco mais trabalhoso de configurar
    // -- uma variável bool que vai dizer para o resto do codigo se o jogador estará no chão (true) ou não (false) 
    // * 2 - Jogador precisa apertar o botão de pulo
    // -- precisamos configurar o botão a ser utilizado, para a ação de pular no nosso Input
    // -- na função OnActiontriggered

    private void Jump()
    {
        if(_isGrounded) _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    private void CheckGround()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);
    }

    private void Update()
    {

        CheckGround();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.cyan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            PlayerObserverManeger.PlayerCoinsChanged(coins);
            
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Coletaveis"))
        {
            moneys++;
            PlayerObserverManeger.PlayerMoneysChanged(moneys);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("FinishDoor"))
        {
            
            GameManager.Instance.PlayerReachedFinishDoor();
        }
    }

    private void TakeDamege(int damege)
    {
        _currentHealth -= damege;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            // alguma funçõo no game maneger pra indicar que o jogador morreu
        }
    }

    public void HealHealth(int heal)
    {
        _currentHealth += heal;
        if (_currentHealth >= maxHealth) _currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            TakeDamege(5);
        }
    }
}
