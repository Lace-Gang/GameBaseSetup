using UnityEngine;

namespace GameBase
{
    
    public interface IProjectile
    {
        public void SetTrajectory(Vector3 direction, float velocity);

    }
}
