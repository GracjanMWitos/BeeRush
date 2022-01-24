using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Assignment
    Transform cursorTransform;
    GameControls gameControls;
    Rigidbody2D rigidbody;
    [SerializeField] SpriteRenderer returnControlSR;
    [SerializeField] SpriteRenderer dashLimitingSR;

    private void Awake()
    {
        cursorTransform = GameObject.Find("Cursor").transform;
        gameControls = new GameControls();
        rigidbody = GetComponent<Rigidbody2D>();
        returnControlSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        dashLimitingSR = transform.GetChild(1).GetComponent<SpriteRenderer>();
        #region Input Actions
        //Focusing
        gameControls.Player.Focusing.performed += ctx => playerIsFocusing = true;
        gameControls.Player.Focusing.canceled += ctx => playerIsFocusing = false;
        //MouseClick
        //gameControls.Player.MouseClick.performed += ctx => playerIsFocusing = true;
        //gameControls.Player.MouseClick.performed += ctx => playerIsFocusing = false;

        #endregion Input Actions
    }

    #endregion Assignment

    #region Variables
    [Header("Main")]
    public int playerLevel; //Level of player//
    [Space]
    public int playerHealth_Max; //Players Health//
    public float playerHealth_Actual;
    [Space]
    public int playerPower_Max; //Player Power//
    public float playerPower_Actual;
    [Space]

    [Header("Rotation Variables")]
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 lookDirection;

    [Header("Movement")]
    [SerializeField] float flyingSpeed = 2f;

    [Header("Movement Value")]
    public Vector3 moveValue;
    [SerializeField] Vector3 lastPosition; //Player position in last frame//

    [Header("Input Values")]
    [SerializeField] public Vector2 mousePosition;

    [Header("Time Count")]
    [SerializeField] float timeAboveWater; //Time above water//
    [SerializeField] float timeToDeathByWater;

    #region Player Cases
    [Header("Player Cases")]
    public bool playerIsMoving; //For checking if inputs, are inputed :P//
    public bool playerIsChangingPosition; //For cheking if player changing his position, mostly for animations//
    public bool playerIsDashing; //If dashing//
    public bool playerIsFocusing; // If player selects the direction in which will dash//
    public bool playerIsAboveTheWater; //If Crouching//
    public bool playerIsBusy; // If using UI hehe// 
    public bool playerIsDead; // If death comes for the player :)//

    #endregion Player Cases

    #region Player Possibilities
    [Header("Player Possibilities")]
    public bool playerCanRotate = true;
    public bool playerCanMove = true;
    public bool playerCanDash = true;

    #endregion Player Possibilities
    #endregion Variables
    void Update()
    {
        #region Input Update
        
        #endregion Input Update

        #region Movement Update
        Movement();
        PlayerMoveChecks();
        #endregion Movement Update
    }
    void FixedUpdate()
    {
        MovementExecution();
    }

    #region Movement
    void Movement()
    {
        #region Focusing
        if (playerIsFocusing)
        {
            dashLimitingSR.enabled = true;
        }
        #endregion Focusing

        #region Normal move
        else
        {
            moveValue = lastPosition;
            if (playerCanMove)
            {
                lastPosition = cursorTransform.position;
                returnControlSR.enabled = false;
            }
            else
                returnControlSR.enabled = true;

            dashLimitingSR.enabled = false;
        }
        #endregion Normal move
    }
    void Rotation()
    {

    }
    void MovementExecution()
    {
        rigidbody.position = Vector3.Lerp(transform.position, moveValue, flyingSpeed * Time.deltaTime);
    }

    #endregion Movement

    #region Environment Interaction 
    void PlayerAboveWater()
    {
        playerIsAboveTheWater = true;
        timeAboveWater += Time.deltaTime;

        if (timeAboveWater >= timeToDeathByWater)
        {
            Death();
        }
    }
    /* 
     void PlayerInFire()
      {
      
      }
    */
    #endregion Enviorment Interaction

    void Death()
    {
        playerIsDead = true;
        // Canvas appear
        // GameManager
        GameObject.Find("Platform").SetActive(false);
    }

    #region Trigger Collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            PlayerAboveWater();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            playerIsAboveTheWater = false;
            timeAboveWater = 0;
        }
    }

    #endregion Trigger Collider

    #region Statement Check

    void PlayerMoveChecks() //Checking if player Giving inputs, and if changing position//
    {
        #region Is Moving
        if (mousePosition != (Vector2)transform.position)//Player giving inputs//
        {
            playerIsMoving = true;
        }
        else //Or not//
        {
            playerIsMoving = false;
        }
        #endregion Is Moving

        #region Changing position

        //Working weird in 2D!!!

        if (transform.position != lastPosition) //If there are differences, then player changes his position//
        {
            playerIsChangingPosition = true;
        }
        else //When not, then not :P//
        {
            playerIsChangingPosition = false;
        }

        #endregion Changing position
    }

    #endregion Statement Check

    #region OnEnable OnDisable
    void OnEnable()
    {
        gameControls.Enable();
    }

    void OnDisable()
    {
        gameControls.Disable();
    }
    #endregion OnEnable OnDisable
}

