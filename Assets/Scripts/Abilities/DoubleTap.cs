namespace Abilities
{
    public class DoubleTap : Ability
    {
        public override bool Cancel()
        {
            return false;
        }

        public override bool isActive()
        {
            return false;
        }

        public override bool canUse()
        {
            if (charges == maxCharges && _playerController.IsShooting())
            {
                charges -= chargeDec;
                return true;
            }
            else return false;
        }

        protected override bool useAbility()
        {
            _playerController.doubleTap = true;
            return true;
        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            keyUp = true;
            keyHold = true;
        }

    }
}
