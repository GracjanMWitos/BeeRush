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

    Vector2 mousePosition;
    // Update is called once per frame
    void Update()
    {
        CursorControl();
        CharacterMovementControl();
    }
    void CursorControl()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(gameControls.Player.MousePosition.ReadValue<Vector2>());
        transform.position = new Vector3(Mathf.Clamp(mousePosition.x, -15.75f, 15.75f), Mathf.Clamp(mousePosition.y, -8.5f, 8.5f), 0);

        if (playerController.playerIsBusy)
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }
    void CharacterMovementControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        Debug.Log(hit.collider.gameObject.name);
        if (hit.collider.gameObject.name == "Rock")
            playerController.playerCanMove = false;
        else if (hit.collider.gameObject.name != "Rock")
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
