using UnityEngine;

namespace _Scripts.AI.BodyParts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Head : BodyPart
    {
        public override void Punch(Vector3 forceDirection)
        {
            forceDirection.y = 0f;
            forceDirection.Normalize();
            transform.GetComponent<Rigidbody>().AddForce(forceDirection * 50, ForceMode.Impulse);
            
            _targetHealth.ApplyDamage(4);
        }
    }
}