using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cysharp.Threading.Tasks;
using System.Threading;

public class DrunkController : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private int drunkLevel;
    public float drunkCurvePointer = 0;
    public float pointerSpeed = 0.001f;
    public AnimationCurve drunkMoveCurve;
    public AnimationCurve drunkPPCurve;
    public bool distortX = true;
    public bool distortY = true;

    private Volume volume;
    private LensDistortion lensDistortion;

    private CancellationToken token;

    // public int changeMoveDirPeriodms = 1000;
    // private Quaternion drunkQuaternion = Quaternion.identity;

    private void Start()
    {
        // UpdateDrunkQuaternion(tokenSource.Token).Forget();
        volume = Camera.main.GetComponent<Volume>();
        volume.profile.TryGet(out lensDistortion);
        token = this.GetCancellationTokenOnDestroy();
        drunkMoveCurve.postWrapMode = WrapMode.Loop;
        drunkPPCurve.postWrapMode = WrapMode.Loop;
        //UpdateDrunkLevel(drunkLevel);
        StartDrunkVFX(token).Forget();
    }

    public void Drink()
    {
        UpdateDrunkLevel(drunkLevel + 1);
    }

    public void UpdateDrunkLevel(int drunkLevel)
    {
        if (this.drunkLevel >= 6) return;
        this.drunkLevel = drunkLevel;
        lensDistortion.intensity.value = -(float)drunkLevel / 10.0f;
    }

    public Quaternion GetDrunkQuaternion()
    {
        float newAngle = (drunkMoveCurve.Evaluate(drunkCurvePointer) - 1) * 90f / 10f * drunkLevel;
        drunkCurvePointer += pointerSpeed;
        return Quaternion.AngleAxis(newAngle, Vector3.up);
    }

    public async UniTaskVoid StartDrunkVFX(CancellationToken token)
    {
        float ppXPointer = 0;
        float ppYPointer;
        while (!token.IsCancellationRequested)
        {
            lensDistortion.intensity.value = -(float)drunkLevel / 10.0f;
            ppXPointer += pointerSpeed;
            ppYPointer = ppXPointer + .5f;
            lensDistortion.center.value = new Vector2(distortX ? drunkPPCurve.Evaluate(ppXPointer) : lensDistortion.center.value.x,
                distortY ? drunkPPCurve.Evaluate(ppYPointer) : lensDistortion.center.value.y);
            await UniTask.NextFrame();
        }
    }
}
