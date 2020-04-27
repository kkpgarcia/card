using UnityEngine;

public class SpriteFactory : MonoBehaviour {
    public Sprite[] SpriteList;
    public static SpriteFactory Instance;

    public void Awake() {
        if(Instance == null)
            Instance = this;
    }

    public Sprite CardRequest(int number) {
        return SpriteList[number];
    }
}