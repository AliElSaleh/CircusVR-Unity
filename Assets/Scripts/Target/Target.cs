using UnityEngine;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
        [Range(10, 500)]
        public int Reward = 10;

        public enum Tier
        {
            LVL_1, LVL_2, LVL_3, LVL_4
        }

        [UsedImplicitly]
        private void Start()
        {
            tag = "Target";
        }

        public void SetTier(Tier NewTier)
        {
            switch (NewTier)
            {
                case Tier.LVL_1:
                    Reward = 50;
                    break;

                case Tier.LVL_2:
                    Reward = 100;
                    break;

                case Tier.LVL_3:
                    Reward = 250;
                    break;

                case Tier.LVL_4:
                    Reward = 500;
                    break;
            }
        }
    }
}