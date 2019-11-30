using UnityEngine;

namespace Abilities
{
    public class Dash : Ability
    {
        public bool dashing = false;
        private float _dashTime;
        private float startDashTime = 0.2f;
        private float dashSpeed = 200.0f;
        private Direction _dir;

        private enum Direction
        {
            None,Up,Down,Left,Right
        }

        protected override bool useAbility()
        {
            _dashTime = startDashTime;
            if (!dashing)
            {
                Debug.Log("Dash");
                if (Input.GetAxisRaw(_playerController._inputManager.inputHString) < 0)
                {
                    _dir = Direction.Left;
                    dashing = true;
                }
                else if (Input.GetAxisRaw(_playerController._inputManager.inputHString) > 0)
                {
                    _dir = Direction.Right;
                    dashing = true;
                }
                else if (Input.GetAxisRaw(_playerController._inputManager.inputVString) > 0)
                {
                    _dir = Direction.Up;
                    dashing = true;
                }
                else if (Input.GetAxisRaw(_playerController._inputManager.inputVString) < 0)
                {
                    _dir = Direction.Down;
                    dashing = true;
                }
            }
            return dashing;
        }

        // Start is called before the first frame update
        void Start()
        {
            base.Initialize();
            _dashTime = startDashTime;
            keyDown = true;
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
            if (dashing)
            {
                Debug.Log("Dashy");
                if (_dashTime <= 0)
                {
                    _dir = Direction.None;
                    dashing = false;
                }
                else
                {
                    _dashTime -= Time.deltaTime;
                    Vector3 dirVec = new Vector3(0,0,0);
                    if(_dir == Direction.Left)
                    {
                        dirVec = new Vector3(-1, 0, 0);
                    }
                    else if (_dir == Direction.Right)
                    {
                        dirVec = new Vector3(1, 0, 0);
                    }
                    else if (_dir == Direction.Up)
                    {
                        dirVec = new Vector3(0, 0, 1);
                    }
                    else if (_dir == Direction.Down)
                    {
                        dirVec = new Vector3(0, 0, -1);
                    }
                    if (_characterController)
                        _characterController.Move(Time.deltaTime * dashSpeed * _dashTime * dirVec);
                    else Debug.Log("_characterController is Null");
                }
            }
        }

        public override bool Cancel()
        {
            _dir = Direction.None;
            dashing = false;
            return true;
        }

        public override bool isActive()
        {
            return dashing;
        }
    }
}
