using UnityEngine;

namespace tusj.Services {

public class ExampleVariableHolder : MonoBehaviour {
    
    public static Savable<int> numberOfBurgers = new("NumberOfBurgers", 0);
    public static Savable<string> burgerName = new("BurgerName", "McChicken");
    public static Savable<float> lengthOfBurger = new("LengthOfBurger", 10.5f);
    public static Savable<bool> isBurgerTasty = new("IsBurgerTasty", true);
    
    public static Savable<Vector3Int> burgerPosition = new("BurgerPosition", Vector3Int.zero);

    [ContextMenu("Increment")]
    private void Increment() {
        UnityEngine.Debug.Log(++numberOfBurgers.Value);
    }
    
    [ContextMenu("Decrement")]
    private void Decrement() {
        Debug.Log(--numberOfBurgers.Value);
    }

    [ContextMenu("Multiply")]
    private void Multiply() {
        Debug.Log(lengthOfBurger.Value *= 2);
    }

    [ContextMenu("Divide")]
    private void Divide() {
        Debug.Log(lengthOfBurger.Value /= 2);
    }


}

}