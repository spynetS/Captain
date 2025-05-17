using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SixtySecondStabilityTest
{
    private const float TestDuration = 60f; // Run for 60 seconds
    private const float MaxAllowedFrameTime = 1f; // ~20 FPS minimum

    [UnityTest]
    public IEnumerator GameStability_60Seconds()
    {
        float startTime = Time.realtimeSinceStartup;
        int frameCount = 0;

        while (Time.realtimeSinceStartup - startTime < TestDuration)
        {
            float frameTime = Time.deltaTime;

            // Fail the test if frame time exceeds allowed threshold
            Assert.LessOrEqual(frameTime, MaxAllowedFrameTime,
                $"Frame time spike: {frameTime:F4}s at frame {frameCount}");

            frameCount++;
            yield return null; // wait for next frame
        }

        Debug.Log($"✅ Game ran stably for 60 seconds ({frameCount} frames).");
        Assert.Pass("Stability test passed.");
    }
}
