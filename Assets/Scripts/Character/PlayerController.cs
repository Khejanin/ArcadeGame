using UnityEngine;
using System.Collections.Generic;
using Game;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        public enum PlayerEnum
        {
            PlayerRight,
            PlayerLeft
        }

        public enum State
        {
            isShooting,
            Aiming,
            Moving,
            Idle
        }

        private CharacterController _characterController;

        //private GameObject[] _bulletInstances;

        private List<GameObject> _bulletInstances;

        private GameObject _futureBullet;

        private Transform _initialTransform;

        private State _state;
        private bool gameOver = false;

        private GameObject _item;

        //DEFINING ALL INPUT

        [HideInInspector] public bool doubleTap = false;

        // private bool isHit = false;

        private GameObject _gameManager;
        private GameLogic _gameLogic;

        public PlayerEnum player;

        public Ability ability;

        public Sprite nameSprite;

        public int health = 5;
        public int maxHealth = 5;

        public int moveSpeed = 5;
        public GameObject gun;

        public float gunAngle = 30f;
        public float gunDefaultAngle = 0f;

        public GameObject bullet;

        public GameObject bulletPosition;

        private bool _bulletCreating = false;

        public InputManager _inputManager;


        // Start is called before the first frame update
        void Start()
        {
            _inputManager = new InputManager(player);
            _gameManager = GameObject.Find("GameManager");
            _gameLogic = _gameManager.GetComponent<GameLogic>();
            _characterController = GetComponent<CharacterController>();
            _bulletInstances = new List<GameObject>();
            gun.transform.eulerAngles = player == PlayerEnum.PlayerLeft
                ? new Vector3(0, gunDefaultAngle + 0, 0)
                : new Vector3(0, gunDefaultAngle + 180, 0);
            bulletPosition.transform.rotation = gun.transform.rotation;
            _initialTransform = gameObject.transform;
        }

        public GameObject Shoot(Transform bulletTransform, float deltaRotation = 0.0f, bool isCopy = false)
        {
            if (!_bulletCreating)
            {
                _bulletCreating = true;
                //If we use bulletPosition, exrtapolate the defaultAngle in order to _inputManager.shoot correctly
                if (!bulletTransform) bulletTransform = bulletPosition.transform;
                //Create instance
                _bulletInstances.Add(Instantiate(bullet, bulletTransform.position, bulletTransform.rotation));
                //Add to list
                var bulletInstance = _bulletInstances[_bulletInstances.Count - 1];
                //Make it rotate for any special value, we abuse this for default angles
                bulletInstance.transform.Rotate(Vector3.up * deltaRotation);
                bulletInstance.GetComponent<Character.Projectile>().setIsCopy(isCopy);
                var bulletCollision = bulletInstance.GetComponent<BulletCollision>();
                bulletCollision.SetOwner(gameObject);
                _bulletCreating = false;
                //reset bulletPosition Transform
                bulletPosition.transform.rotation = gun.transform.rotation;
                return bulletInstance;
            }

            return null;
        }

        public void Hit()
        {
            foreach (var bulletInstance in _bulletInstances)
            {
                bulletInstance.GetComponent<BulletCollision>().hit = true;
            }

            _bulletInstances.Clear();
            health--;
            //isHit = true;
        }

        public void RemoveBullet(GameObject bulletToDestroy)
        {
            _bulletInstances.Remove(bulletToDestroy);
        }

        /*  public bool getIsHit()
          {
              return isHit;
          }

          public void setHitFalse()
          {
              isHit = false;
          }*/

        // Update is called once per frame
        void Update()
        {
            if (!gameOver && GameLogic.GameState.running)
            {
                if (health <= 0)
                {
                    /*GameLogic.GameState.gameOver = true;
                    GameLogic.GameState.playerWon = (player == PlayerEnum.PlayerLeft);
                    gameOver = true;
                    if (this.player == PlayerEnum.PlayerLeft) GameObject.Find("PlayerRight").GetComponent<PlayerController>().gameOver = true;
                    else GameObject.Find("PlayerLeft").GetComponent<PlayerController>().gameOver = true;*/
                    _gameLogic.LostRound(player);
                }

                if (ability.keyDown && Input.GetButtonDown(_inputManager.useAbility) ||
                    ability.keyUp && Input.GetButtonUp(_inputManager.useAbility) || ability.keyHold && Input.GetButton(_inputManager.useAbility))
                {
                    ability.use();
                }

                if (Input.GetButtonDown(_inputManager.useItem) && _futureBullet)
                {
                    _futureBullet.GetComponent<Projectile>().accelerate();
                    _bulletInstances.Add(_futureBullet);
                    _futureBullet = null;
                }

                if (doubleTap || (_bulletInstances.Count == 0 && !ability.isActive()))
                {
                    //Rotate the Gun around the Pivot point when holding, transformEulerAngles allows us to use traditional Degrees
                    if (Input.GetButton(_inputManager.shoot))
                    {
                        RotateBarrel();
                    }
                    //This comes before the Transform reset so the bullet gets the still modified Transform
                    else if (Input.GetButtonUp(_inputManager.shoot))
                    {
                        Shoot(null, -gunDefaultAngle);
                        doubleTap = false;
                    }
                    //Reset angle and move if needed
                    else
                    {
                        gun.transform.eulerAngles = player == PlayerEnum.PlayerLeft
                            ? new Vector3(0, gunDefaultAngle + 0, 0)
                            : new Vector3(0, gunDefaultAngle + 180, 0);
                        Vector3 movementVector3 = new Vector3(
                            Input.GetAxisRaw(_inputManager.inputHString), 0.0f, Input.GetAxisRaw(_inputManager.inputVString)
                        );
                        if (!doubleTap)
                            _characterController.Move(moveSpeed * Time.deltaTime * movementVector3);
                    }
                }
            }
        }

        private void RotateBarrel()
        {
            int axisValue = 0;
            if (Input.GetAxis(_inputManager.inputVString) > 0)
            {
                axisValue = 1;
            }
            else if (Input.GetAxis(_inputManager.inputVString) < 0)
            {
                axisValue = -1;
            }

            if (player == PlayerEnum.PlayerLeft)
                gun.transform.eulerAngles = new Vector3(0, gunDefaultAngle - gunAngle * axisValue, 0);
            else gun.transform.eulerAngles = new Vector3(0, gunDefaultAngle - 180 + gunAngle * axisValue, 0);
        }

        public bool IsShooting()
        {
            return _bulletInstances.Count > 0;
        }

        public void SetFutureBullet(GameObject futureBullet)
        {
            _bulletInstances.Remove(futureBullet);
            _futureBullet = futureBullet;
        }

        public bool HasFutureBullet()
        {
            return _futureBullet != null;
        }

        public void Reset()
        {
            health = maxHealth;
            ability.reset();
            gameObject.transform.position = _initialTransform.position;
            _bulletInstances.Clear();
        }
    }
}