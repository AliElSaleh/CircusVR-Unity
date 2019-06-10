using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
	    public int Tier1 = 100;
	    public int Tier2 = 250;
	    public int Tier3 = 500;

        [UsedImplicitly]
        private void Start()
        {
            tag = "Target";
        }
    }
}