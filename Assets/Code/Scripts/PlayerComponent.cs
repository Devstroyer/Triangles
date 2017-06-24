﻿using System.Collections.Generic;
using UnityEngine;



public class PlayerComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    public KeyCode Red, Green, Blue;

    private List<Action> actions;
    public List<Action> Actions
    {
        get { return actions; }
    }

    private Cards cardInput;
    private TileComponent activeTile;
    private bool isMoving;
    private int maxActions;



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES
    public bool IsReadyForResolvePhase
    {
        get { return actions.Count >= maxActions; }
    }



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Start()
    {
        base.Start();
        GameManager.Players.Add(this);
        actions = new List<Action>();
        maxActions = 3;
        TryMoveTo(GameManager.Grids.Find(o => o != null).GetTileClosestTo(this.transform.position));
    }

    protected override void Update()
    {
        base.Update();

        // Manage player input
        ReceiveInput();
        ConsumeInput();

        // Fix sprite sorting order
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-10 * transform.position.y);

        // Debug
        AddDebugLine(name);
        for(int i = 0; i < maxActions; i++)
            AddDebugLine("  " + i + ". " + (actions.Count > i ? actions[i].ToString() : "..."));
        AddDebugLine();
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    private void ReceiveInput()
    {
        if(Input.GetKeyDown(Red))
            cardInput = Cards.Red;
        if(Input.GetKeyDown(Green))
            cardInput = Cards.Green;
        if(Input.GetKeyDown(Blue))
            cardInput = Cards.Blue;
    }

    private void ConsumeInput()
    {
        if(cardInput != Cards.None)
            switch(GamePhase)
            {
                case Phases.Queue:
                    TryEnqueue(Cards.Green, cardInput);
                    break;

                case Phases.Realtime:
                    if(!isMoving)
                        TryMoveTo(activeTile.GetNeighbors()[(int)cardInput]);
                    break;
            }

        cardInput = Cards.None;
    }

    private void TryEnqueue(Cards a, Cards b)
    {
        if(actions.Count < maxActions)
            actions.Add(new Action(a, b));

    }

    public void TryMoveTo(TileComponent targetTile)
    {
        if(targetTile != null)
            StartCoroutine(SlowlyMoveTo(targetTile));
    }

    private System.Collections.IEnumerator SlowlyMoveTo(TileComponent targetTileComponent)
    {
        if(targetTileComponent != null)
        {
            isMoving = true;
            for(int i = 0; i < 15; i++)
            {
                LerpPositionTowards(targetTileComponent.transform.position, 0.2f);
                yield return new WaitForSeconds(0.005f);
            }
            isMoving = false;

            activeTile = targetTileComponent;
            this.transform.position = activeTile.transform.position;
        }
    }

    public void ResolveAction(Action action)
    {
        switch(action.A)
        {
            case Cards.Red:
                break;

            case Cards.Green:
                TryMoveTo(activeTile.GetNeighbors()[(int)action.B]);
                break;

            case Cards.Blue:
                break;
        }
    }

    public void ResetActionsList()
    {
        actions.Clear();
    }
}
