using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace VirtualHandshake.Utilities
{
    public static class XRDeviceUtility
    {
        public static bool IsPresent
        {
            get
            {
                var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
                SubsystemManager.GetInstances(xrDisplaySubsystems);
                foreach (var xrDisplay in xrDisplaySubsystems)
                {
                    if (xrDisplay.running)
                    {
                        return true;
                    }
                }
                
#if !UNITY_2020_1_OR_NEWER
                // fallback to older API
                Debug.LogWarning("IsPresent fallback to old api");
                return XRDevice.isPresent;
#else
                return false;
#endif
            }
        }

        public static void Recenter()
        {
            var subsystems = new List<XRInputSubsystem>();
            SubsystemManager.GetInstances(subsystems);
            foreach (var xrInput in subsystems)
            {
                xrInput.TrySetTrackingOriginMode(TrackingOriginModeFlags.Device);
                if (xrInput.TryRecenter()) return;
            }
            
#if !UNITY_2020_1_OR_NEWER
            // fallback to older API
            Debug.LogWarning("Recenter fallback to old api");
#pragma warning disable 618
            InputTracking.Recenter();
#pragma warning restore 618
#endif
        }
    }
}