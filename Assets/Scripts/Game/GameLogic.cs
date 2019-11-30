using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameLogic : MonoBehaviour
    {
        public GameObject hud;
        [HideInInspector]
        public GameObject playerLeft, playerRight;

        public List<GameObject> playerLeftCharacters;
        public List<GameObject> playerRightCharacters;

        public List<Transform> effectSpawnPoints;
        public List<GameObject> effectsPrefabs;
        private readonly List<GameObject> _effects = new List<GameObject>();
        private const float SpawnCheckFrequency = 5;
        private float _spawnCheckTimer = SpawnCheckFrequency;
        private float spawnProbability = 9;

        public Transform playerOneSpawnLocation, playerTwoSpawnLocation;

        private readonly List<Character.PlayerController.PlayerEnum> _rounds = new List<Character.PlayerController.PlayerEnum>();

        private HUDController _hudController;
        private Character.PlayerController _playerControllerLeft, _playerControllerRight;

        public static class GameState
        {
            public enum PlayerCharactersEnum
            {
                None = -1,
                Ben = 0,
                MechaNoot = 1,
                Richard = 2,
                UFO = 3,
            }

            public static bool playerWon; //0 is player1 , 1 is player2
            public static bool gameOver = false;
            public static bool running;

            public static PlayerCharactersEnum playerOnePrefab = PlayerCharactersEnum.None; //Holds the value to get the Player in the Characters Array for spawning
            public static PlayerCharactersEnum playerTwoPrefab = PlayerCharactersEnum.None;

        }

        private void Awake()
        {
            playerLeft = Instantiate(playerLeftCharacters[(int)GameState.playerOnePrefab], playerOneSpawnLocation);
            playerRight = Instantiate(playerRightCharacters[(int)GameState.playerTwoPrefab], playerTwoSpawnLocation);
            GameState.gameOver = false;
            GameState.running = false;
            for(int i = 0; i < 3; i++) _effects.Add(null);
        }

        private void Start()
        {
            _hudController = hud.GetComponent<HUDController>();
            _playerControllerLeft = playerLeft.GetComponent<Character.PlayerController>();
            _playerControllerRight = playerRight.GetComponent<Character.PlayerController>();
            _hudController.POName.sprite = _playerControllerLeft.nameSprite;
            _hudController.PTName.sprite = _playerControllerRight.nameSprite;
        }

        public void Update()
        {
            _spawnCheckTimer -= Time.deltaTime;
            if(_spawnCheckTimer <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (_effects[i] == null)
                    {
                        if (Random.Range(0, 100) <= spawnProbability)
                        {
                            _effects[i] = Instantiate(effectsPrefabs[Random.Range(0, 2)], effectSpawnPoints[i]);
                        }
                        else Debug.Log("No Spawn");
                    }
                }
                _spawnCheckTimer = SpawnCheckFrequency;
            }
        }

        public void LostRound(Character.PlayerController.PlayerEnum player)
        {
            bool over = false;
            Character.PlayerController.PlayerEnum playerWon = 0;

            if (player == Character.PlayerController.PlayerEnum.PlayerLeft)
            {
                if (_rounds.Contains(Character.PlayerController.PlayerEnum.PlayerRight))
                {
                    over = true;
                }
                playerWon = Character.PlayerController.PlayerEnum.PlayerRight;
            }
            else
            {
                if (_rounds.Contains(Character.PlayerController.PlayerEnum.PlayerLeft))
                {
                    over = true;
                }
                playerWon = Character.PlayerController.PlayerEnum.PlayerLeft;
            }

            _rounds.Add(playerWon);

            if (over)
            {
                GameState.gameOver = true;
                if (playerWon == Character.PlayerController.PlayerEnum.PlayerLeft) {
                    GameState.playerWon = false; //he doesnt let me put 0
                    _hudController.winnerName.sprite = _playerControllerLeft.nameSprite;
                }
                else {
                    GameState.playerWon = true;  //ditto for 1
                    _hudController.winnerName.sprite = _playerControllerRight.nameSprite;
                }            
            }

            _hudController.roundOver(playerWon);
            NextRound();
        }

        private void NextRound()
        {
            _playerControllerLeft.Reset();
            _playerControllerRight.Reset();
        }

    }
}
