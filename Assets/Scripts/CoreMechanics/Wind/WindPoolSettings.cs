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
}

