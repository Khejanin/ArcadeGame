using UnityEngine;

namespace Abilities
{
    public class Teleport : Ability
    {
        private GameObject _teleporter;

        public Material activatedMaterialRef;

        public GameObject teleporterObject;

        private Shader _oldShader;

        public Shader teleportShader;


        public override bool Cancel()
        {
            return false;
        }

        public override bool isActive()
        {
            return false;
        }

        public override bool use()
        {
            if (_teleporter)
            {
                useAbility();
                return true;
            }
            else if (canUse())
            {
                useAbility();
                return true;
            }

            return false;
        }


        protected override bool useAbility()
        {
            if (!_teleporter)
                _teleporter = Instantiate(teleporterObject, transform.position, teleporterObject.transform.rotation);
            else
            {
                teleport();
                _teleporter.GetComponent<MeshRenderer>().material = activatedMaterialRef;
            }

            return true;
        }

        private void teleport()
        {
            Vector3 moveDelta = _teleporter.transform.position - _characterController.transform.position;
            _characterController.Move(moveDelta);
            _oldShader = this.GetComponent<MeshRenderer>().material.shader;
            this.GetComponent<MeshRenderer>().material.shader = teleportShader;
            _playerController.gun.GetComponent<MeshRenderer>().material.shader = teleportShader;
            Destroy(_teleporter);

            Invoke(nameof(ResetShader), 4);
        }

        private void ResetShader()
        {
            this.GetComponent<MeshRenderer>().material.shader = _oldShader;
            _playerController.gun.GetComponent<MeshRenderer>().material.shader = _oldShader;
        }

        // Start is called before the first frame update
        void Start()
        {
            base.Initialize();
            keyDown = true;
        }

        public override void reset()
        {
            charges = maxCharges;
            if (_teleporter)
            {
                Destroy(_teleporter);
            }
        }
    }
}