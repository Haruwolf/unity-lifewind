using UnityEngine;

namespace CoreMechanics.Wind
{
    [CreateAssetMenu(menuName = "Settings/Wind Pool", order = 1)]
    public class WindPoolSettings : ScriptableObject
    {
        public WindStart windStart;
        public bool collectionCheck;
        public int defaultPoolCapacity = 5;
        public int maxPoolSize = 10;
        public string windCloneName = "wind_name";
    }
    
    [CreateAssetMenu(menuName = "Settings/Wind Start", order = 2)]
    public class WindStartSettings : ScriptableObject
    {
        [Range(0.1f, 2)]
        public float windDecreateRate;
        [Range(2f, 8f)]
        public float windMaxSpeed;
        [Range(0.5f, 4)]
        public float windMinSpeed;
        [Range(0.5f, 1)] public float windMaxSize;
        [Range(0.05f, 0.5f)] public float windMinSize;
        [Range(0.01f, 1)] public float windSizeRate;



    }
    
    
}

