using UnityEngine;

namespace Aezakmi.PrintSystem
{
    // Used for painting the entire texture of a shirt
    // rather than changing the materials color
    // as the materials color would affect any further painting applied to the texture.
    public static class SolidPainter
    {
        private const int SIZE = 256;

        public static void Paint(Material material, Color color)
        {
            Texture2D newTexture = new Texture2D(SIZE, SIZE);

            for (int y = 0; y < newTexture.height; y++)
            {
                for (int x = 0; x < newTexture.width; x++)
                {
                    newTexture.SetPixel(x, y, color);
                }
            }

            newTexture.Apply();
            material.SetTexture("_MainTex", newTexture);
        }
    }
}