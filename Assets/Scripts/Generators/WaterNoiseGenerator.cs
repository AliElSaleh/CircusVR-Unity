using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Generators
{
    public class WaterNoiseGenerator : MonoBehaviour
    {
        public float Power = 3;
        public float Scale = 3;
        public float TimeScale = 1;

        private float XOffset;
        private float YOffset;
        private MeshFilter MeshFilter;

        [UsedImplicitly]
        void Start()
        {
            MeshFilter = GetComponent<MeshFilter>();
            GenerateNoise();
        }
	
        [UsedImplicitly]
        void Update()
        {
		    GenerateNoise();
            XOffset += Time.deltaTime * TimeScale;
            YOffset += Time.deltaTime * TimeScale;
        }

        private void GenerateNoise()
        {
            Vector3[] Vertices = MeshFilter.mesh.vertices;

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].y = CalculateHeight(Vertices[i].x, Vertices[i].z) * Power;
            }

            MeshFilter.mesh.vertices = Vertices;
        }

        private float CalculateHeight(float x, float y)
        {
            float XCord = x * Scale + XOffset;
            float YCord = y * Scale + XOffset;

            return Mathf.PerlinNoise(XCord, YCord);
        }
    }
}
