using Assets.Scripts.Duck;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class MysteryTarget : Target
    {
        [Range(10.0f, 1000.0f)]
        public float LaunchForce = 500.0f;

        public DuckSpawner DuckSpawner;
        private Rigidbody Rigidbody;

        [UsedImplicitly]
        private void Start()
        {
            tag = "MysteryTarget";

            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.AddForce(Vector3.up * LaunchForce);

            transform.rotation = Quaternion.LookRotation(Vector3.zero - transform.position) * Quaternion.Inverse(new Quaternion(0.0f, 90.0f, 90.0f, 1.0f));
        }

        [UsedImplicitly]
        private void OnCollisionEnter(Collision Collision)
        {
            switch (Collision.transform.gameObject.tag)
            {
                case "Water":
                    Destroy(gameObject); // Destroy target
                break;

                default:
                    if (Collision.gameObject.GetComponent<Duck.Duck>())
                    {
                        int ID = Random.Range(0, 5);

                        // Spawn power-ups
                        for (int i = 0; i < 5; i++)
                        {
                            if (ID == 3) // Arbitrary number
                            {
                                DuckSpawner.SpawnDuck(DuckType.Super);
                            }
                            else
                            {
                                DuckSpawner.SpawnDuck(DuckType.DoublePoints);
                            }
                        }

                        Destroy(gameObject);
                    }
                break;
            }
        }
    }
}
