using UnityEngine;

namespace GameBase
{

    // Base class that other ScriptableObjects can inherit from
    // "Data" classes will inherit from this
    // Provides common functionality such as a description field
    public class ScriptableObjectBase : ScriptableObject
    {
        // Description field that appears as a multi-line text area in the Inspector
        // Can be used to document the purpose of the ScriptableObject instance
        [TextArea] string description;
    }
}