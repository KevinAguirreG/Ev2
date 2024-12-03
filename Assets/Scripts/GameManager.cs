using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Menu, Travel, Battle}

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Camera worldMainCamera;
    [SerializeField] private Camera menuCamera;

    private GameState _gameState;

    private void Awake()
    {
        _gameState = GameState.Menu;
    }

    void Start()
    {
        playerController.OnPokemonEncounter += StartPokemonBattle;
        battleManager.OnBattleFinish += FinishPokemonBattle;
        mainMenu.OnEnter += EnterTheGame;
    }

    void EnterTheGame()
    {
        _gameState = GameState.Travel;
        menuCamera.gameObject.SetActive(false);
        worldMainCamera.gameObject.SetActive(true);

    }

    void StartPokemonBattle()
    {
        _gameState = GameState.Battle;
        battleManager.gameObject.SetActive(true);
        worldMainCamera.gameObject.SetActive(false);

        battleManager.HandleStartBattle();

    }

    void FinishPokemonBattle(bool playerHasWon)
    {
        _gameState = GameState.Travel;
        battleManager.gameObject.SetActive(false);
        worldMainCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(_gameState == GameState.Menu)
        {
            mainMenu.HandleUpdate();
        }
        else if (_gameState == GameState.Travel)
        {
            playerController.HandleUpdate();
        } else if (_gameState == GameState.Battle)
        {
            battleManager.HandleUpdate();
        }
    }
}
