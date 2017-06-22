using System.Collections.Generic;
using UnityEngine;



public class PlayerComponent : Abstract
{
    // FIELDS
    public KeyCode Red, Green, Blue;

    private Directions directionInput;
    public TileComponent activeTile;
    private bool isMoving;


    // PROPERTIES



    // OVERRIDES
    protected override void Start()
    {
        base.Start();
        GameManager.Players.Add(this);
        TryMoveTo(GameManager.Grids.Find(o => o != null).GetTileClosestTo(this.transform.position));
    }

    protected override void Update()
    {
        base.Update();
        ReceiveInput();
        ConsumeInput();

        AddDebugLine(transform.position.ToString());
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
            TryMoveTo(activeTile.GetNeighbors()[(int)directionInput]);
        }

        directionInput = Directions.None;
    }

    private void TryMoveTo(TileComponent targetTileComponent)
    {
        if (targetTileComponent != null)
            StartCoroutine(SlowlyMoveTo(targetTileComponent));
    }

    private System.Collections.IEnumerator SlowlyMoveTo(TileComponent targetTileComponent)
    {
        if (targetTileComponent != null)
        {
            isMoving = true;
            for (int i = 0; i < 15; i++)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, targetTileComponent.transform.position, 0.2f);
                yield return new WaitForSeconds(0.005f);
            }
            isMoving = false;

            activeTile = targetTileComponent;
            this.transform.position = activeTile.transform.position;
        }
    }

}
