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
    Vector3 mousePosition;

    #endregion Variables
    void Update()
    {
        CursorControl();
        CharacterMovementControl();
    }
    void CursorControl()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(gameControls.Player.MousePosition.ReadValue<Vector2>());
        #region Normal move
        if (!playerController.playerIsFocusing)
            transform.position = new Vector3(Mathf.Clamp(mousePosition.x, -15.75f, 15.75f), Mathf.Clamp(mousePosition.y, -8.5f + Camera.main.transform.position.y, 8.5f + Camera.main.transform.position.y), 0);
        #endregion Normal move

        #region Focusing
        if (playerController.playerIsFocusing)
        {
            float radius = 5; //radius of *black circle*
            Vector3 centerPosition = playerController.transform.localPosition; //center of *black circle*
            float distance = Vector3.Distance(mousePosition, centerPosition); //distance from ~green object~ to *black circle*

            if (distance > radius) //If the distance is less than the radius, it is already within the circle.
            {
                Vector3 playerToCursor = mousePosition - centerPosition; //~GreenPosition~ - *BlackCenter*
                playerToCursor *= radius / distance; //Multiply by radius //Divide by Distance
                transform.position = centerPosition + playerToCursor; //*BlackCenter* + all that Math

            }
        }
        #endregion Focusing

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
