using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShakeControl
{
    public class Shake : MonoBehaviour
    {
        public static GameObject mainCamera;
        public static bool repeat = true;

        public static void ShakeCamera()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            while(repeat)
            {
                RandomTime();
            }
        }

        static IEnumerator RandomTime()
        {
            repeat = false;
            mainCamera.transform.Translate(new Vector2(Random.Range(0.1f, 0.2f), Random.Range(0.1f, 0.2f)));
            float randomTime;
            randomTime = Random.Range(0.05f, 0.1f);
            yield return new WaitForSecondsRealtime(randomTime);
            repeat = true;
        }
    }
}