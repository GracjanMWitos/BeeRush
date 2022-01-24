using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    #region Assignment
    PlayerController playerController;
    GameControls gameControls;
    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameControls = new GameControls();
    }
    #endregion Assingnment

    #region Variables
    string hittedObjectName;
    Vector2 mousePosition;

    #endregion Variables
    void Update()
    {
        CursorControl();
        CharacterMovementControl();
    }
    void CursorControl()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(gameControls.Player.MousePosition.ReadValue<Vector2>());
        transform.position = new Vector3(Mathf.Clamp(mousePosition.x, -15.75f, 15.75f), Mathf.Clamp(mousePosition.y, -8.5f + Camera.main.transform.position.y, 8.5f + Camera.main.transform.position.y), 0);

        if (playerController.playerIsBusy)
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }
    void CharacterMovementControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
             hittedObjectName = hit.collider.gameObject.name;

        if (hittedObjectName == "Rock")
            playerController.playerCanMove = false;
        else if (hittedObjectName != "Rock" && hittedObjectName != "[Player] Bee")
            playerController.playerCanMove = true;
    }
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
