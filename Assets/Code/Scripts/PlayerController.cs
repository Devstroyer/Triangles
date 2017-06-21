using System.Collections.Generic;
using UnityEngine;



public class PlayerController : Abstract
{
    // FIELDS
    public KeyCode Red, Green, Blue;

    private Directions directionInput;
    public Field activeField;
    private bool isMoving;


    // PROPERTIES



    // OVERRIDES
    protected override void Start()
    {
        base.Start();
        GameManager.Players.Add(this);
        TryMoveTo(GameManager.Grids.Find(o => o != null).GetFieldClosestTo(this.transform.position));
    }

    protected override void Update()
    {
        base.Update();
        ReceiveInput();
        ConsumeInput();
    }



    // METHODS
    private void ReceiveInput()
    {
        if (Input.GetKeyDown(Red))
            directionInput = Directions.Red;
        if (Input.GetKeyDown(Green))
            directionInput = Directions.Green;
        if (Input.GetKeyDown(Blue))
            directionInput = Directions.Blue;
    }

    private void ConsumeInput()
    {
        if (directionInput != Directions.None && !isMoving)
        {
            TryMoveTo(activeField.GetNeighbors()[(int)directionInput]);
        }

        directionInput = Directions.None;
    }

    private void TryMoveTo(Field targetField)
    {
        if (targetField != null)
            StartCoroutine(SlowlyMoveTo(targetField));
    }

    private System.Collections.IEnumerator SlowlyMoveTo(Field targetField)
    {
        if (targetField != null)
        {
            isMoving = true;
            for (int i = 0; i < 15; i++)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, targetField.transform.position, 0.2f);
                yield return new WaitForSeconds(0.005f);
            }
            isMoving = false;

            activeField = targetField;
            this.transform.position = activeField.transform.position;
        }
    }

}
