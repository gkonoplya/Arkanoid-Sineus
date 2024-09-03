using UnityEngine;

namespace Gameplay.Buffs
{
    [RequireComponent(typeof(CollisionHandler))]
    public abstract class CollisionBuff : TimedBuff

    {
    private CollisionHandler _collisionHandler;

    protected override void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable() =>
        _collisionHandler.OnCollisionEnter += Execute;

    private void OnDisable()
    {
        _collisionHandler.OnCollisionEnter -= Execute;
    }

    private void Execute(Collision2D collision2D) =>
        Execute(collision2D.collider.gameObject, collision2D.otherCollider.gameObject);
    }
}