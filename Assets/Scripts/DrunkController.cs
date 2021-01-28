using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkController : MonoBehaviour
{
    [Range(1, 10)]
    public int drunkLevel;
    public float drunkCurvePointer = 0;
    public float pointerSpeed = 0.001f;
    public AnimationCurve drunkCurve;

    // public int changeMoveDirPeriodms = 1000;
    // private Quaternion drunkQuaternion = Quaternion.identity;

    private void Start()
    {
        // UpdateDrunkQuaternion(tokenSource.Token).Forget();
    }

    // private async UniTaskVoid UpdateDrunkQuaternion(CancellationToken token)
    // {
    //     while (!token.IsCancellationRequested)
    //     {
    //         float newAngle = 90f / 10f * drunkLevel;
    //         drunkQuaternion = Quaternion.AngleAxis(Random.Range(-newAngle, newAngle), Vector3.up);
    //         await UniTask.Delay(changeMoveDirPeriodms);
    //     }
    // }

    public Quaternion GetDrunkQuaternion()
    {
        float newAngle = (drunkCurve.Evaluate(drunkCurvePointer) - 1) * 90f / 10f * drunkLevel;
        drunkCurvePointer += pointerSpeed;
        drunkCurvePointer = drunkCurvePointer > 1 ? 0 : drunkCurvePointer;
        return Quaternion.AngleAxis(newAngle, Vector3.up);
    }
}
