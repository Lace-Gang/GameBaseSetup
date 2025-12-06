using UnityEngine;

namespace GameBase
{
    public class StaticSpawnPoint : MonoBehaviour
    {
        [Header("StaticSpawnPoint Basic Info")]
        [Tooltip("A tag to indicate what the spawn point is used for")]
        [SerializeField] public string spawnTag;
    }
}
