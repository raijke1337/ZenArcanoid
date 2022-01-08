using UnityEngine;
[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class BouncyItemComponent : MonoBehaviour
{
    private Rigidbody _rb;
    private ObjectType _type;
    private float _floatSpeed;
    // awake > enable > start


    private void OnEnable()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            // if an object is not setup (ie a wall) it is defaulted to frozen state
        }
        _rb.useGravity = false;
    }

    public void SetItemType(ObjectType type)
    {
        _type = type;
        if (type == ObjectType.Passthrough)
        {
            var coll = GetComponent<Collider>();
            coll.isTrigger = true;
        }
    }
    private void Start()
    {
        if (_type == ObjectType.Unset)
        {
            _type = ObjectType.Solid;
        }
        _floatSpeed = Random.Range(0, 2);

    }

    public ObjectType GetGameItemType() => _type;

    private void Update()
    {
        if (_type == ObjectType.Point)
        {
            transform.position += Vector3.up * Time.deltaTime * _floatSpeed;
        }
    }

}
