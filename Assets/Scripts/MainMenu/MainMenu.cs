using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Character;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public List<Button> buttons;
        public PlayerController.PlayerEnum player;
        private void Start()
        {
            buttons[0].onClick.AddListener(BenClicked);
            buttons[1].onClick.AddListener(RichardClicked);
            buttons[2].onClick.AddListener(DrillanClicked);
            buttons[3].onClick.AddListener(UfoClicked);
        }

        private void BenClicked()
        {

            Debug.Log("Ben");
            if (player == PlayerController.PlayerEnum.PlayerLeft)
            {
                GameLogic.GameState.playerOnePrefab = GameLogic.GameState.PlayerCharactersEnum.Ben;
            }
            else
            {
                GameLogic.GameState.playerTwoPrefab = GameLogic.GameState.PlayerCharactersEnum.Ben;
            }
            if (GameLogic.GameState.playerTwoPrefab != GameLogic.GameState.PlayerCharactersEnum.None && GameLogic.GameState.playerOnePrefab != GameLogic.GameState.PlayerCharactersEnum.None)
            {
                SceneManager.LoadScene("Default_Level");
            }
        }

        private void RichardClicked()
        {
            Debug.Log("Richard");
            if (player == PlayerController.PlayerEnum.PlayerLeft)
            {
                GameLogic.GameState.playerOnePrefab = GameLogic.GameState.PlayerCharactersEnum.Richard;
            }
            else
            {
                GameLogic.GameState.playerTwoPrefab = GameLogic.GameState.PlayerCharactersEnum.Richard;
            }
            if (GameLogic.GameState.playerTwoPrefab != GameLogic.GameState.PlayerCharactersEnum.None && GameLogic.GameState.playerOnePrefab != GameLogic.GameState.PlayerCharactersEnum.None)
            {
                SceneManager.LoadScene("Default_Level");
            }
        }

        private void DrillanClicked()
        {
            Debug.Log("Drillan");
            if (player == PlayerController.PlayerEnum.PlayerLeft)
            {
                GameLogic.GameState.playerOnePrefab = GameLogic.GameState.PlayerCharactersEnum.MechaNoot;
            }
            else
            {
                GameLogic.GameState.playerTwoPrefab = GameLogic.GameState.PlayerCharactersEnum.MechaNoot;
            }
            if (GameLogic.GameState.playerTwoPrefab != GameLogic.GameState.PlayerCharactersEnum.None && GameLogic.GameState.playerOnePrefab != GameLogic.GameState.PlayerCharactersEnum.None)
            {
                SceneManager.LoadScene("Default_Level");
            }
        }

        private void UfoClicked()
        {
            Debug.Log("UFO");
            if (player == PlayerController.PlayerEnum.PlayerLeft)
            {
                GameLogic.GameState.playerOnePrefab = GameLogic.GameState.PlayerCharactersEnum.UFO;
            }
            else
            {
                GameLogic.GameState.playerTwoPrefab = GameLogic.GameState.PlayerCharactersEnum.UFO;
            }
            if (GameLogic.GameState.playerTwoPrefab != GameLogic.GameState.PlayerCharactersEnum.None && GameLogic.GameState.playerOnePrefab != GameLogic.GameState.PlayerCharactersEnum.None)
            {
                SceneManager.LoadScene("Default_Level");
            }
        }
    }
}