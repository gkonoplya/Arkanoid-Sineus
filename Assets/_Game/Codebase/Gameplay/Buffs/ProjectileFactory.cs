using DG.Tweening;
using Infrastructure.AssetProvider;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Gameplay.Buffs
{
    public class ProjectileFactory
    {
        private const string Laser = "Laser";
        private readonly ObjectPool<Projectile> _projectilePool;

        private readonly IInstantiator _iinstantiator;
        private readonly AssetProvider _assets;

        public ProjectileFactory(IInstantiator iinstantiator, AssetProvider assets)
        {
            _iinstantiator = iinstantiator;
            _assets = assets;
            _projectilePool = new ObjectPool<Projectile>(
                CreateProjectile,
                OnGetProjectile,
                OnReleaseProjectile,
                x => Object.Destroy(x.gameObject));
        }

        public void Create(Vector3 position)
        {
            var projectile = _projectilePool.Get();
            projectile.transform.position = position;
            projectile.transform
                .DOMoveY(10, 10)
                .OnComplete(() => _projectilePool.Release(projectile));
        }

        private void OnReleaseProjectile(Projectile obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.position = Vector3.zero;
        }

        private void OnGetProjectile(Projectile obj) => 
            obj.gameObject.SetActive(true);

        private Projectile CreateProjectile() =>
            _iinstantiator.InstantiatePrefabForComponent<Projectile>(
                _assets.GetComponentOnPrefab<Projectile>(Laser));
    }
}