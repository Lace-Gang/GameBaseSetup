using UnityEngine;

namespace GameBase
{
    // Creates a menu item at "Assets > Create > Data >"BoolData" with default filename "BoolData"
    [CreateAssetMenu(fileName = "BoolData", menuName = "Data/BoolData")]
    public class BoolData : ScriptableObjectBase
    {
        // The main boolean value stored in this ScriptableObject
        [SerializeField] bool value;

        // Public property to get/set the value
        public bool Value { get => value; set => this.value = value; }

        // Implicit conversion operator allows using this ScriptableObject directly as a bool
        // Example usage: bool x = myBoolData; // instead of bool x = myBoolData.value;
        // The null-coalescing operator (??) returns false if variable is null (meaning an unassigned value defaults to false)
        public static implicit operator bool(BoolData variable)
        {
            return variable?.value ?? false;
        }
    }
}
