using UnityEngine;
[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class BouncyItemComponent : MonoBehaviour
{
    private Rigidbody _rb;

    public void SetObjectType(ObjectType t)
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        SetUpProperties(t);
    }
    public ObjectType GetGameItemType { get; private set; }
    private void SetUpProperties(ObjectType t)
    {
        GetGameItemType = t;
        switch (t)
        {
            case ObjectType.Cube:
                _rb.useGravity = false;
                _rb.AddForce(Vector3.up, ForceMode.Impulse);
                break;
            case ObjectType.Wall:
                _rb.constraints = RigidbodyConstraints.FreezeAll;
                break;
            case ObjectType.Bonus:
                break;
            case ObjectType.Trap:
                break;
            case ObjectType.Player:
                _rb.useGravity = false;
                break;
        }
    }

    // all logic is done in ball controller 
    public event CollisionEventsHandler CollisionHappenedEvent;
    private void OnCollisionEnter(Collision collision)
    {
        CollisionHappenedEvent?.Invoke(this);
    }


}
