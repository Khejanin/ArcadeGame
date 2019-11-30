using UnityEngine;

namespace Character
{
    public class Projectile : MonoBehaviour
    {
        public int bulletSpeed;
        private float xSpeed = 0;
        private float ySpeed;
        private bool _isCopy;
        public bool isFuture = false;
        private Rigidbody _rigidbody;
        private bool done = false;

        public void setIsCopy(bool copy)
        {
            _isCopy = copy;
        }

        public bool isCopy()
        {
            return _isCopy;
        }

        
        // Start is called before the first frame update
        void Start()
        {
            //Extract angle
            _rigidbody = GetComponent<Rigidbody>();
            accelerate();
        }

        /*public void bounce()
        {
            changeDirectionAndAddForce(90);
            Debug.Log("Bounce");
        }*/
        
        private void changeDirectionAndAddForce(float angle = 0)
        {
            //Get current angle
            float currentAngle;
            Vector3 axis = Vector3.zero;
            transform.rotation.ToAngleAxis(out currentAngle, out axis);
            currentAngle *= -axis.y;
            //Modifiy with given angle
            angle += currentAngle;
            angle = Mathf.Deg2Rad * angle;

            //Split Into x and y forces
            xSpeed = Mathf.Cos(angle) * bulletSpeed;
            ySpeed = Mathf.Sin(angle) * bulletSpeed;

            //Apply Forces to RigidBody
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(xSpeed, 0, ySpeed);
            done = true;
        }

        public void Deflect(float deflectAngle = 0)
        {
            //Clear all forces
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            if (deflectAngle == 0)
            {
                //Add Inverse Forces
                _rigidbody.AddForce(-xSpeed, 0, -ySpeed);
            }
            else
            {
                //Deflect in certain direction
                changeDirectionAndAddForce(deflectAngle);
            }
        }

        public void accelerate()
        {
            //We dont change the angle
            _rigidbody.drag = 0;
            changeDirectionAndAddForce();
        }

        public void setFuture()
        {
            isFuture = true;
            GetComponent<BulletCollision>().getOwner().GetComponent<Character.PlayerController>().SetFutureBullet(gameObject);
        }

        public void clearFuture()
        {
            isFuture = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
           /* if (done)
            {
                if (ySpeed != 0)
                {
                    if (_rigidbody.velocity.z == 0)
                    {
                        Debug.Log("Destroyed Myself");
                        Destroy(this.gameObject);
                    }
                }
            }*/
        }
    }
}
