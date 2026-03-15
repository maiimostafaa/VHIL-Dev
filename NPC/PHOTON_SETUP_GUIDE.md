# Photon Unity Networking Setup & Testing Guide

## ✅ What's Already Configured

1. **Photon PUN Package**: Installed and ready
2. **Scripts**: 
   - `Launcher.cs` - Handles connection and room joining
   - `VRSync.cs` - Synchronizes VR head and hand positions
   - `VRPlayerSetup.cs` - Disables camera/audio for remote players
3. **NetworkedPlayer Prefab**: Located in `Assets/Resources/NetworkedPlayer.prefab` with:
   - PhotonView component
   - VRSync component
   - VRPlayerSetup component
   - OVR Camera Rig setup

## ⚠️ Critical: Missing Photon App ID

**You MUST configure your Photon App ID before testing!**

### Steps to Get and Configure App ID:

1. **Create/Login to Photon Account**:
   - Go to https://dashboard.photonengine.com/
   - Sign up or log in

2. **Create a New App**:
   - Click "Create a New App"
   - Choose "Photon PUN" as the type
   - Select your region (e.g., "US", "EU", "Asia")
   - Give it a name (e.g., "VR NPC Project")

3. **Get Your App ID**:
   - Copy the "App ID" (Realtime) from your dashboard
   - It looks like: `a1b2c3d4-e5f6-7890-abcd-ef1234567890`

4. **Configure in Unity**:
   - In Unity, go to: **Window → Photon Unity Networking → PUN Wizard**
   - Paste your App ID in the "AppId Realtime" field
   - Click "Setup Project"
   - OR manually edit: `Assets/Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset`
     - Set `AppIdRealtime` to your App ID

## 🔧 Setup Steps in Unity

1. **Add Launcher to Your Scene**:
   - Open your main scene (e.g., `NPScene.unity` or `SampleScene.unity`)
   - Create an empty GameObject (Right-click in Hierarchy → Create Empty)
   - Name it "PhotonLauncher"
   - Add the `Launcher` component to it:
     - Select the GameObject
     - In Inspector, click "Add Component"
     - Search for "Launcher" and add it

2. **Verify NetworkedPlayer Prefab**:
   - The prefab should be in `Assets/Resources/NetworkedPlayer.prefab`
   - Make sure it has:
     - PhotonView component (with Ownership Transfer set appropriately)
     - VRSync component (with head, leftHand, rightHand references)
     - VRPlayerSetup component (with OVRCameraRig reference)

## 🧪 How to Test Photon Networking

> **Note**: If you're experiencing Android build issues (Gradle failures), you can test Photon networking using Windows/Mac builds first. See `ANDROID_BUILD_TROUBLESHOOTING.md` for Android build help.

### Option 1: Test with Two Unity Editor Instances (Recommended for Development)

1. **Build Settings**:
   - File → Build Settings
   - Add your scene to the build
   - Make sure "Development Build" is checked

2. **First Instance**:
   - Open your scene in Unity
   - Press Play
   - Check Console for: "Connected to Photon Master Server"
   - You should see your player spawn

3. **Second Instance**:
   - File → Build and Run (or Build, then run the .exe)
   - OR use Unity's "Build and Run" to create a standalone build
   - Run the build while the first instance is still running
   - Both should connect to the same room "VRRoom"
   - You should see both players in the scene

### Option 1b: Test with Windows/Mac Standalone (If Android Build Fails)

1. **Build Settings**:
   - File → Build Settings
   - Switch platform to "PC, Mac & Linux Standalone"
   - Add your scene to build
   - Click "Build" (choose a folder)

2. **Run Two Instances**:
   - Run the built .exe twice (or .app on Mac)
   - Both should connect to Photon
   - This works even without VR - you can test networking logic

### Option 2: Test with VR Headset + Editor

1. **Build for Quest/VR**:
   - File → Build Settings
   - Switch platform to Android (for Quest) or your VR platform
   - **Note**: If you get Gradle build errors, see `ANDROID_BUILD_TROUBLESHOOTING.md`
   - Build and deploy to your headset

2. **Run Both**:
   - Start the build on your VR headset
   - Start Play mode in Unity Editor
   - Both should connect and see each other

### Option 3: Test with Two VR Headsets

1. Build and deploy to both headsets
2. Run both simultaneously
3. Both should connect to the same room

## 📊 What to Look For When Testing

### Success Indicators:
- ✅ Console shows: "Connected to Photon Master Server"
- ✅ Console shows: "Joined room: VRRoom"
- ✅ NetworkedPlayer prefab instantiates
- ✅ Other players appear in your scene
- ✅ Other players' head and hands move (if VRSync is working)

### Debugging Tips:

1. **Check Console Logs**:
   - Look for Photon connection messages
   - Check for any errors about missing prefabs or App IDs

2. **Photon Stats GUI** (Optional):
   - Add `PhotonStatsGui` component to a GameObject for real-time stats
   - Shows connection status, ping, room info

3. **Common Issues**:
   - **"Can't connect: Loading settings failed"**: App ID not configured
   - **"Failed to join room"**: Check room name matches ("VRRoom")
   - **No player spawns**: Check prefab name is "NetworkedPlayer" (not "VRPlayer")
   - **Players don't sync**: Check PhotonView is configured correctly on prefab

## 🔍 Verification Checklist

Before testing, verify:
- [ ] Photon App ID is configured in PhotonServerSettings
- [ ] Launcher script is attached to a GameObject in your scene
- [ ] NetworkedPlayer prefab exists in `Assets/Resources/`
- [ ] NetworkedPlayer prefab has PhotonView component
- [ ] NetworkedPlayer prefab has VRSync component with references set
- [ ] NetworkedPlayer prefab has VRPlayerSetup component

## 📝 Code Changes Made

1. **Fixed Launcher.cs**:
   - Changed prefab name from "VRPlayer" to "NetworkedPlayer"
   - Added error handling callbacks
   - Added debug logging

2. **Current Configuration**:
   - Room name: "VRRoom"
   - Max players: 4
   - Spawn position: Vector3.zero

## 🚀 Next Steps

Once basic networking works:
1. Test VR hand/head synchronization
2. Adjust spawn positions for multiple players
3. Add player names/identifiers
4. Implement room selection UI
5. Add disconnect handling

---

**Note**: The free tier of Photon allows up to 20 concurrent users (CCU). For more, you'll need a paid plan.
