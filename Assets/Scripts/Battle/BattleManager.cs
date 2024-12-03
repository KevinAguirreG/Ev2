using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    StartBattle,
    PlayerSelectAction,
    PlayerMove,
    EnemyMove,
    Busy
}

public class BattleManager : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHUD playerHUD;


    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD enemyHUD;


    [SerializeField] BattleDialogs battleDialogs;

    public BattleState state;

    public event Action<bool> OnBattleFinish;
    public void HandleStartBattle()
    {
        StartCoroutine(SetupBattle());  
    }

    public IEnumerator SetupBattle()
    {
        state = BattleState.StartBattle;

        playerUnit.SetupPokemon();
        playerHUD.setPokemonData(playerUnit.Pokemon);

        battleDialogs.SetPokemonMovements(playerUnit.Pokemon.Moves);

        enemyUnit.SetupPokemon();
        enemyHUD.setPokemonData(enemyUnit.Pokemon);

        yield return battleDialogs.SetDialog($"Un {enemyUnit.Pokemon.Base.Name} salvaje apareció.");

        if(enemyUnit.Pokemon.Speed > playerUnit.Pokemon.Speed)
        {
            StartCoroutine(battleDialogs.SetDialog("El enemigo ataca primero"));
            EnemyAction();
        }
        else
        {
            PlayerAction();

        }

    }

    void PlayerAction()
    {
        state = BattleState.PlayerSelectAction;
        StartCoroutine(battleDialogs.SetDialog("Selecciona una acción"));
        battleDialogs.ToggleDialogText(true);
        battleDialogs.ToggleActions(true);
        battleDialogs.ToggleMovements(false);
        currentSelectedAction = 0;
        battleDialogs.SelectAction(currentSelectedAction);
    }

    void PlayerMovement()
    {
        state = BattleState.PlayerMove;
        battleDialogs.ToggleDialogText(false);
        battleDialogs.ToggleActions(false);
        battleDialogs.ToggleMovements(true);
        currentSelectedMovement = 0;
        battleDialogs.SelectMovement(currentSelectedMovement, playerUnit.Pokemon.Moves[currentSelectedMovement]);
    }

    IEnumerator EnemyAction()
    {
        state = BattleState.EnemyMove;
        Move move = enemyUnit.Pokemon.RandomMove();
        yield return battleDialogs.SetDialog($"{enemyUnit.Pokemon.Base.Name} ha usado {move.Base.Name}");

        bool pokemonFainted = playerUnit.Pokemon.ReciveDamage(enemyUnit.Pokemon, move);
        playerHUD.UpdatePokemonData();

        if (pokemonFainted)
        {
            yield return battleDialogs.SetDialog($"{playerUnit.Pokemon.Base.Name} ha sido debilitado");
            yield return new WaitForSeconds(1.5f);
            OnBattleFinish(true);
        }
        else
        {
            PlayerAction();
        }
    }

    public void HandleUpdate()
    {

        timeSinceLastClick += Time.deltaTime;

        if(state == BattleState.PlayerSelectAction)
        {
            HandlePlayerActionSelection();
        }else if(state == BattleState.PlayerMove)
        {
            HandlePlayerMovementSelection();
        }
    }

    private int currentSelectedAction;
    private float timeSinceLastClick;
    public float timeBetweenClicks = 1.0f;

    void HandlePlayerActionSelection()
    {

        if(timeSinceLastClick < timeBetweenClicks)
        {
            return;
        }

        if (Input.GetAxisRaw("Vertical")!=0) 
        {
            timeSinceLastClick = 0;
            
            currentSelectedAction = (currentSelectedAction + 1) % 2;  

            battleDialogs.SelectAction(currentSelectedAction);

        }

        if (Input.GetAxisRaw("Submit") != 0)
        {
            timeSinceLastClick = 0;
            if (currentSelectedAction == 0)
            {
                PlayerMovement();   
            }else if (currentSelectedAction == 1)
            {

            }
        }
    }

    private int currentSelectedMovement;
    void HandlePlayerMovementSelection()
    {
        if(timeSinceLastClick < timeBetweenClicks)
        {
            return ;
        }

        if(Input.GetAxisRaw("Vertical") != 0)
        {
            timeSinceLastClick = 0;
            var oldSelectedMovement = currentSelectedMovement;
            currentSelectedMovement = (currentSelectedMovement + 2) % 4;
            if(currentSelectedMovement >= playerUnit.Pokemon.Moves.Count)
            {
                currentSelectedMovement = oldSelectedMovement;
            }
            battleDialogs.SelectMovement(currentSelectedMovement, playerUnit.Pokemon.Moves[currentSelectedMovement]);
        }else if (Input.GetAxisRaw("Horizontal") != 0)
        {
            timeSinceLastClick = 0;

            var oldSelectedMovement = currentSelectedMovement;
            if (currentSelectedMovement <= 1)
            {
                currentSelectedMovement = (currentSelectedMovement + 1) % 2;
            }
            else
            {
                currentSelectedMovement = (currentSelectedMovement + 1) % 2 + 2;
            }

            if (currentSelectedMovement >= playerUnit.Pokemon.Moves.Count)
            {
                currentSelectedMovement = oldSelectedMovement;
            }

            battleDialogs.SelectMovement(currentSelectedMovement, playerUnit.Pokemon.Moves[currentSelectedMovement]);
        }

        if(Input.GetAxisRaw("Submit") != 0)
        {
            timeSinceLastClick = 0;
            battleDialogs.ToggleMovements(false);
            battleDialogs.ToggleDialogText(true);
            StartCoroutine(PerformPlayerMovement());
        }
    }


    IEnumerator PerformPlayerMovement()
    {
        Move move = playerUnit.Pokemon.Moves[currentSelectedMovement];
        yield return battleDialogs.SetDialog($"{playerUnit.Pokemon.Base.Name} ha usado {move.Base.Name}");

        bool pokemonFainted = enemyUnit.Pokemon.ReciveDamage(playerUnit.Pokemon, move);
        enemyHUD.UpdatePokemonData();

        if (pokemonFainted)
        {
            yield return battleDialogs.SetDialog($"{enemyUnit.Pokemon.Base.Name} se ha debilitado");
            yield return new WaitForSeconds(1.5f);
            OnBattleFinish(true);
        }
        else
        {
            StartCoroutine(EnemyAction());
        }
    }
}
