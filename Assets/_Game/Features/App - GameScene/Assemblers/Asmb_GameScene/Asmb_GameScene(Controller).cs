// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debug;
using JovDK.Generic.SpatialUI;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace PackageName.MajorContext.MinorContext
{
    public partial class Asmb_GameScene : MonoBehaviour
    {
        void SetInitialState()
        {
            // PlaySphereRotationAnimation();
            RegisterSpatialUIItems();
        }

        // void PlaySphereRotationAnimation()
        // {
        //     Tween sphereTween =
        //         _sphereRotationPivot.DORotate(
        //             new Vector3(0f, 360, 0f),
        //             2f,
        //             RotateMode.FastBeyond360).SetEase(Ease.Linear);

        //     sphereTween.SetLoops(-1);
        // }

        void RegisterSpatialUIItems()
        {
            _spatialUIHandler.DoIfNotNull(() =>
            {
                Vector3 positionDelta = new Vector3(0f, 2f, 5f);
                _spatialUIHandler.RegisterSpatialUIItems(_mergeStationTransform, _mergeStationUIBubbleTransform, positionDelta);
            });
        }
    }
}
