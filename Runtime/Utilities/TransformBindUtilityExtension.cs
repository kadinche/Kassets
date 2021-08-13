using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.Utilities
{
    [Flags]
    public enum BindingType
    {
        In = 1,
        Out = 1 << 1,
        InLocal = 1 << 2,
        OutLocal = 1 << 3,
    }

    public enum TransformBindingType
    {
        InPose,
        OutPose,
        AsChild,
        AsParent,
        AsTargetSibling,
        AsSourceSibling,
        AsGrandChild,
        AsGrandParent
    }
    
    public static class TransformBindUtilityExtension
    {
        public static void BindPosition(
            this Transform target,
            Vector3Variable positionVariable,
            BindingType bindingType = BindingType.In,
            CancellationToken cancellationToken = default)
        {
            var token = cancellationToken == CancellationToken.None
                ? target.GetCancellationTokenOnDestroy()
                : cancellationToken;

            if (bindingType.HasFlag(BindingType.In) || bindingType.HasFlag(BindingType.InLocal))
            {
                positionVariable
                    .Where(newPosition => newPosition != (bindingType.HasFlag(BindingType.In) ? target.position : target.localPosition))
                    .Subscribe(newPosition =>
                    {
                        if (bindingType.HasFlag(BindingType.In))
                            target.position = newPosition;
                        else
                            target.localPosition = newPosition;
                    }, token);
            }

            if (bindingType.HasFlag(BindingType.Out) || bindingType.HasFlag(BindingType.OutLocal))
            {
                UniTaskAsyncEnumerable.EveryValueChanged(target,
                    transform => bindingType.HasFlag(BindingType.Out) ? transform.position : transform.localPosition)
                    .Where(newPosition => newPosition != positionVariable.Value)
                    .Subscribe(newPosition => positionVariable.Value = newPosition, token);
            }
        }
        
        public static void BindRotation(
            this Transform target,
            QuaternionVariable rotationVariable,
            BindingType bindingType = BindingType.In,
            CancellationToken cancellationToken = default)
        {
            var token = cancellationToken == CancellationToken.None
                ? target.GetCancellationTokenOnDestroy()
                : cancellationToken;

            if (bindingType.HasFlag(BindingType.In) || bindingType.HasFlag(BindingType.InLocal))
            {
                rotationVariable
                    .Where(newRotation => newRotation != (bindingType.HasFlag(BindingType.In) ? target.rotation : target.localRotation))
                    .Subscribe(newRotation =>
                    {
                        if (bindingType.HasFlag(BindingType.In))
                            target.rotation = newRotation;
                        else
                            target.localRotation = newRotation;
                    }, token);
            }

            if (bindingType.HasFlag(BindingType.Out) || bindingType.HasFlag(BindingType.OutLocal))
            {
                UniTaskAsyncEnumerable.EveryValueChanged(target,
                        transform => bindingType.HasFlag(BindingType.Out) ? transform.rotation : transform.localRotation)
                    .Where(newRotation => newRotation != rotationVariable)
                    .Subscribe(newRotation => rotationVariable.Value = newRotation, token);
            }
        }
        
        public static void BindEulerAngle(
            this Transform target,
            Vector3Variable eulerAngleVariable,
            BindingType bindingType = BindingType.In,
            CancellationToken cancellationToken = default)
        {
            var token = cancellationToken == CancellationToken.None
                ? target.GetCancellationTokenOnDestroy()
                : cancellationToken;

            if (bindingType.HasFlag(BindingType.In) || bindingType.HasFlag(BindingType.InLocal))
            {
                eulerAngleVariable
                    .Where(newRotation => newRotation != (bindingType.HasFlag(BindingType.In) ? target.eulerAngles : target.localEulerAngles))
                    .Subscribe(newRotation =>
                    {
                        if (bindingType.HasFlag(BindingType.In))
                            target.rotation = Quaternion.Euler(newRotation);
                        else
                            target.localRotation = Quaternion.Euler(newRotation);
                    }, token);
            }

            if (bindingType.HasFlag(BindingType.Out) || bindingType.HasFlag(BindingType.OutLocal))
            {
                UniTaskAsyncEnumerable.EveryValueChanged(target,
                        transform => bindingType.HasFlag(BindingType.Out) ? transform.eulerAngles : transform.localEulerAngles)
                    .Where(newRotation => newRotation != eulerAngleVariable)
                    .Subscribe(newRotation => eulerAngleVariable.Value = newRotation, token);
            }
        }

        public static void BindPose(
            this Transform target,
            PoseVariable poseVariable,
            BindingType bindingTypePosition = BindingType.In,
            BindingType bindingTypeRotation = BindingType.In,
            CancellationToken cancellationToken = default)
        {
            var token = cancellationToken == CancellationToken.None
                ? target.GetCancellationTokenOnDestroy()
                : cancellationToken;

            if ((bindingTypePosition.HasFlag(BindingType.In) || bindingTypePosition.HasFlag(BindingType.InLocal))
                && (bindingTypeRotation.HasFlag(BindingType.In) || bindingTypeRotation.HasFlag(BindingType.InLocal)))
            {
                poseVariable
                    .Where(newPose =>
                        newPose.position == (bindingTypePosition.HasFlag(BindingType.In)
                            ? target.position
                            : target.localPosition)
                        || newPose.rotation == (bindingTypeRotation.HasFlag(BindingType.In)
                            ? target.rotation
                            : target.localRotation))

                    .Subscribe(newPose =>
                    {
                        if (bindingTypePosition.HasFlag(BindingType.In) && bindingTypeRotation.HasFlag(BindingType.In))
                        {
                            target.SetPositionAndRotation(newPose.position, newPose.rotation);
                            return;
                        }
                        
                        if (bindingTypePosition.HasFlag(BindingType.In))
                            target.position = newPose.position;
                        else
                            target.localPosition = newPose.position;
                        
                        if (bindingTypeRotation.HasFlag(BindingType.In))
                            target.rotation = newPose.rotation;
                        else
                            target.localRotation = newPose.rotation;
                    }, token);
            }

            if ((bindingTypePosition.HasFlag(BindingType.Out) || bindingTypePosition.HasFlag(BindingType.OutLocal))
                && (bindingTypePosition.HasFlag(BindingType.Out) || bindingTypePosition.HasFlag(BindingType.OutLocal)))
            {
                UniTaskAsyncEnumerable.EveryValueChanged(target,
                        transform =>
                        {
                            var pos = bindingTypePosition.HasFlag(BindingType.Out)
                                ? transform.position
                                : transform.localPosition;
                            var rot = bindingTypeRotation.HasFlag(BindingType.Out)
                                ? transform.rotation
                                : transform.localRotation;
                            return new Pose(pos, rot);
                        })
                    .Where(newPose => newPose != poseVariable.Value)
                    .Subscribe(newPose => poseVariable.Value = newPose, token);
            }
        }

        public static void BindTransform(
            this Transform source,
            Transform target,
            TransformBindingType bindingType,
            CancellationToken cancellationToken = default)
        {
            var token = cancellationToken == CancellationToken.None
                ? source.GetCancellationTokenOnDestroy()
                : cancellationToken;

            switch (bindingType)
            {
                case TransformBindingType.InPose:
                    UniTaskAsyncEnumerable.EveryValueChanged(target, transform =>
                    {
                        var pos = transform.position;
                        var rot = transform.rotation;
                        return new Pose(pos, rot);
                    }).Subscribe(newPose => source.SetPositionAndRotation(newPose.position, newPose.rotation), token);
                    break;
                case TransformBindingType.OutPose:
                    UniTaskAsyncEnumerable.EveryValueChanged(source, transform =>
                    {
                        var pos = transform.position;
                        var rot = transform.rotation;
                        return new Pose(pos, rot);
                    }).Subscribe(newPose => target.SetPositionAndRotation(newPose.position, newPose.rotation), token);
                    break;
                case TransformBindingType.AsChild:
                    source.SetParent(target, false);
                    break;
                case TransformBindingType.AsParent:
                    target.SetParent(source, false);
                    break;
                case TransformBindingType.AsTargetSibling:
                    source.SetParent(target, false);
                    source.SetParent(target.parent, true);
                    break;
                case TransformBindingType.AsSourceSibling:
                    target.SetParent(source, false);
                    target.SetParent(source.parent, true);
                    break;
                case TransformBindingType.AsGrandChild:
                    if (target.childCount > 0)
                    {
                        var targetChild = target.GetChild(0);
                        source.SetParent(targetChild, false);
                    }
                    else
                    {
                        UnityEngine.Debug.LogWarning("Target has no child. Try to bind as child instead.");
                        BindTransform(source, target, TransformBindingType.AsChild, token);
                    }
                    break;
                case TransformBindingType.AsGrandParent:
                    if (source.childCount > 0)
                    {
                        var sourceChild = source.GetChild(0);
                        target.SetParent(sourceChild, false);
                    }
                    else
                    {
                        UnityEngine.Debug.LogWarning("Source has no child. Try to bind as parent instead.");
                        BindTransform(source, target, TransformBindingType.AsParent, token);
                    }
                    break;
            }
        }

        public static void BindTransform(
            this Transform source,
            TransformVariable transformVariable,
            TransformBindingType bindingType,
            CancellationToken cancellationToken = default)
        {
            BindTransform(source, transformVariable.Value, bindingType, cancellationToken);
        }
    }
}