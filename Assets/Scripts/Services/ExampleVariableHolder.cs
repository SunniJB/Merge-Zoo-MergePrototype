using UnityEngine;

namespace tusj.Services {

public class ExampleVariableHolder : MonoBehaviour {
    
    public static Savable<int> numberOfBurgers = new("NumberOfBurgers", 0);
    public static Savable<string> burgerName = new("BurgerName", "McChicken");
    public static Savable<float> lengthOfBurger = new("LengthOfBurger", 10.5f);
    public static Savable<bool> isBurgerTasty = new("IsBurgerTasty", true);
    public static Savable<int> numberOfIngredients = new("NumberOfIngredients", 5);
    public static Savable<string> burgerPatty = new("BurgerPatty", "Chicken");
    public static Savable<string> burgerSauce = new("BurgerSauce", "Mayo", SaveFlag.Local);
    public static Savable<int> badBurgers = new("BadBurgers", 0, SaveFlag.Local);
    
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
    
    [ContextMenu("Increment2")]
    private void Increment2() {
        UnityEngine.Debug.Log(++badBurgers.Value);
    }
    
    [ContextMenu("Decrement2")]
    private void Decrement2() {
        Debug.Log(--badBurgers.Value);
    }

}

}