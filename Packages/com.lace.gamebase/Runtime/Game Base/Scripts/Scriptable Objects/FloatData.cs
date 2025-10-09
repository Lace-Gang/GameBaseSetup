using UnityEngine;

namespace GameBase
{

    // Creates a menu item at "Assets > Create > Data >"FloatData" with default filename "FloatData"
    [CreateAssetMenu(fileName = "FloatData", menuName = "Data/FloatData")]
    public class FloatData : ScriptableObjectBase
    {
        // The main float value stored in this ScriptableObject
        [SerializeField] float value;

        // Public property to get/set the value
        public float Value { get => value; set => this.value = value; }

        // Implicit conversion operator allows using this ScriptableObject directly as a float
        // Example usage: float x = myFloatData; // instead of float x = myFloatData.value;
        // The null-coalescing operator (??) returns 0 if variable is null (meaning an unassigned value defaults to 0)
        public static implicit operator float(FloatData variable)
        {
            return variable?.value ?? 0.0f;
        }
    }

}