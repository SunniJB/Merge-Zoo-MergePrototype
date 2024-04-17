using UnityEngine;

namespace tusj.Services {

public class ExampleVariableHolder : MonoBehaviour {
    
    public static Savable<int> numberOfBurgers = new("NumberOfBurgers", 0);
    public static Savable<string> burgerName = new("BurgerName", "McChicken");
    public static Savable<float> lengthOfBurger = new("LengthOfBurger", 10.5f);
    public static Savable<bool> isBurgerTasty = new("IsBurgerTasty", true);
    
    public static Savable<Vector3Int> burgerPosition = new("BurgerPosition", Vector3Int.zero);
    
}

}