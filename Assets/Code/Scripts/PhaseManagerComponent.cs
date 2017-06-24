using System.Collections.Generic;
using UnityEngine;



public class PhaseManagerComponent : Abstract
{
    // ---------------------------------------------------------------- FIELDS
    // READ-ONLY
    private Phases gamePhase;
    public new Phases GamePhase
    { get { return gamePhase; } }

    // PRIVATE
    private bool isResolvingAction;
    private int absoluteActionsLimit;



    // ---------------------------------------------------------------- METHODS
    // MONOBEHAVIOUR
    override protected void Start()
    {
        base.Start();
        gamePhase = Phases.Queue;
        absoluteActionsLimit = 5;

    }

    override protected void Update()
    {
        base.Update();

        if(gamePhase == Phases.Queue)
            if(AllPlayersFinishedQueueing())
                gamePhase = Phases.Resolve;

        if(gamePhase == Phases.Resolve)
            if(!isResolvingAction)
                StartCoroutine(ResolveAllActions());

        AddDebugLine(gamePhase.ToString().ToUpper() + " PHASE");
        AddDebugLine();
    }

    // OTHER
    private bool AllPlayersFinishedQueueing()
    {
        foreach(PlayerComponent player in GameManager.Players)
            if(!player.IsReadyForResolvePhase)
                return false;
        return true;
    }

    private System.Collections.IEnumerator ResolveAllActions()
    {
        for(int i = 0; i < absoluteActionsLimit; i++)
            foreach(PlayerComponent player in GameManager.Players)
                if(player.Actions.Count > i)
                {
                    isResolvingAction = true;
                    player.ResolveAction(player.Actions[i]);
                    yield return new WaitForSeconds(0.5f);
                    isResolvingAction = false;
                }           

        foreach(PlayerComponent player in GameManager.Players)
            player.ResetActionsList();

        gamePhase = Phases.Queue;
    }

}
