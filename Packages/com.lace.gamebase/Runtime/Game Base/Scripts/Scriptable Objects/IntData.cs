using UnityEngine;

namespace GameBase
{

    // Creates a menu item at "Assets > Create > Data >"IntData" with default filename "IntData"
    [CreateAssetMenu(fileName = "IntData", menuName = "Data/IntData")]
    public class IntData : ScriptableObjectBase
    {
        // The main integer value stored in this ScriptableObject
        [SerializeField] int value;

        // Public property to get/set the value
        public int Value { get => value; set => this.value = value; }

        // Implicit conversion operator allows using this ScriptableObject directly as an int
        // Example usage: int x = myIntData; // instead of int x = myIntData.value;
        // The null-coalescing operator (??) returns 0 if variable is null (meaning an unassigned value defaults to 0)
        public static implicit operator int(IntData variable)
        {
            return variable?.value ?? 0;
        }
    }
}
