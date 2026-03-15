# Android Build Troubleshooting Guide

## Current Configuration
- **Unity Version**: 6000.2.9f1
- **Android Min SDK**: 32
- **Android Target SDK**: 32
- **Build System**: Gradle
- **Meta XR SDK**: 78.0.0

## Common Gradle Build Failures & Solutions

### 1. Check Unity Android SDK/NDK/JDK Setup

**In Unity:**
1. Go to **Edit → Preferences → External Tools** (Windows) or **Unity → Preferences → External Tools** (Mac)
2. Verify these paths are set correctly:
   - **Android SDK**: Should point to your Android SDK folder
   - **Android NDK**: Should point to your NDK folder (or leave empty to use Unity's bundled NDK)
   - **JDK**: Should point to Unity's bundled JDK or your own JDK installation

**If paths are missing:**
- **Download Android SDK**: Install Android Studio and let it download the SDK, or download command-line tools
- **Unity's Bundled Tools**: Unity includes OpenJDK, but you may need to set SDK paths manually

### 2. Check Gradle Version Compatibility

Unity 6000.2.9f1 typically uses Gradle 8.x. Check if there are version conflicts:

**To check Gradle version:**
1. Look in the build error output for Gradle version info
2. Check Unity's Gradle wrapper: `.utmp/RelWithDebInfo/[build-id]/gradle/wrapper/gradle-wrapper.properties`

**Common fixes:**
- Unity should handle Gradle automatically, but if you see version errors, you may need to update Unity or check for custom Gradle templates

### 3. Increase Gradle Memory Allocation

Gradle builds can fail due to insufficient memory. Add this to your Gradle properties:

**Create/Edit:** `Assets/Plugins/Android/gradle.properties` (if it doesn't exist, create it)

```properties
org.gradle.jvmargs=-Xmx4096m -XX:MaxMetaspaceSize=512m -XX:+HeapDumpOnOutOfMemoryError
org.gradle.parallel=true
org.gradle.daemon=true
org.gradle.configureondemand=true
android.useAndroidX=true
android.enableJetifier=true
```

### 4. Check for Path Issues

The error shows a path with spaces: `C:\Program Files\Unity\Hub\Editor\...`

**If you see path-related errors:**
- Ensure Unity Hub is installed correctly
- Try moving Unity installation to a path without spaces (not recommended, but can help diagnose)
- Check Windows environment variables don't have issues

### 5. Clean Build Folders

**Delete these folders and rebuild:**
- `.utmp/` folder (Unity temporary build files)
- `Library/` folder (Unity cache - **WARNING**: This will force Unity to reimport all assets)
- `Temp/` folder
- `Build/` folder (if exists)

**Steps:**
1. Close Unity completely
2. Delete `.utmp/` folder
3. Delete `Temp/` folder
4. Reopen Unity and let it reimport
5. Try building again

### 6. Check Android Manifest Issues

Your `AndroidManifest.xml` looks correct, but verify:
- No duplicate permissions
- No conflicting activity declarations
- Meta XR SDK requirements are met

### 7. Verify Meta XR SDK Compatibility

You're using Meta XR SDK 78.0.0. Ensure:
- SDK version is compatible with Unity 6000.2.9f1
- Oculus Integration is properly configured
- Check Oculus Project Config: `Assets/Oculus/OculusProjectConfig.asset`

### 8. Check for Dependency Conflicts

**Common issues:**
- Multiple versions of the same library
- Conflicting AndroidX dependencies
- Photon PUN dependencies conflicting with Meta SDK

**To diagnose:**
1. Check the full Gradle error log (scroll up in Console)
2. Look for "Conflict" or "Duplicate" errors
3. Check `Packages/packages-lock.json` for version conflicts

### 9. Enable Detailed Gradle Logging

**To see more detailed errors:**
1. In Unity, go to **Edit → Preferences → External Tools**
2. Check "Custom Gradle Properties Template" if you want to customize
3. Or check the full Console output - scroll up to see the actual Gradle error

**In Console, look for:**
- `FAILURE: Build failed with an exception`
- The specific error message below it
- Stack traces that show what failed

### 10. Try Building with Different Settings

**Temporary workarounds to test:**
1. **Switch Build System**: Try "Internal" instead of "Gradle" (if available)
   - **Edit → Project Settings → Player → Android → Other Settings**
   - Change "Build System" to "Internal" (if option exists)
   - Note: Gradle is recommended for modern projects

2. **Reduce Build Complexity**:
   - Uncheck "Development Build" temporarily
   - Disable "Script Debugging"
   - Try building a minimal scene first

3. **Check Build Target Architecture**:
   - Currently set to ARM64 (AndroidTargetArchitectures: 2)
   - Try building for ARMv7 only to test if it's architecture-specific

## Step-by-Step Diagnostic Process

### Step 1: Get the Full Error Message
1. Open Unity Console
2. Scroll up to find the **first** error (not just "Build failed")
3. Look for lines starting with `FAILURE:` or `ERROR:`
4. Copy the complete error message

### Step 2: Check Unity Android Setup
1. **Edit → Preferences → External Tools**
2. Verify Android SDK path exists and is valid
3. Click "Download" if SDK is missing
4. Verify JDK path (should be Unity's bundled JDK)

### Step 3: Check Project Settings
1. **Edit → Project Settings → Player → Android**
2. Verify:
   - Minimum API Level: 32 (matches your config)
   - Target API Level: 32
   - Build System: Gradle

### Step 4: Clean and Rebuild
1. Close Unity
2. Delete `.utmp/` folder
3. Delete `Temp/` folder
4. Reopen Unity
5. Wait for reimport
6. Try build again

### Step 5: Check Console for Specific Errors
Look for these common error patterns:

**"SDK location not found"**
→ Set Android SDK path in Preferences

**"Gradle sync failed"**
→ Check Gradle version compatibility

**"Duplicate class" or "Conflict"**
→ Dependency version conflict

**"OutOfMemoryError"**
→ Increase Gradle memory (see #3 above)

**"Could not resolve"**
→ Network/connectivity issue downloading dependencies

## Quick Fixes to Try First

1. **Restart Unity** - Sometimes build state gets corrupted
2. **Clean Build Folders** - Delete `.utmp/` and `Temp/`
3. **Check Internet Connection** - Gradle downloads dependencies
4. **Update Unity** - If on an older patch version
5. **Verify Android SDK** - Ensure it's properly installed

## Getting More Help

If none of these work, gather this information:

1. **Full error log** from Console (scroll up to find the root cause)
2. **Unity version** (6000.2.9f1)
3. **Android SDK version** (check in Preferences)
4. **Gradle version** (from error log or gradle-wrapper.properties)
5. **Specific error message** (the first error, not just "Build failed")

## Alternative: Test Photon Without Android Build

While fixing the Android build, you can still test Photon networking:

1. **Test in Unity Editor** (Windows/Mac build):
   - File → Build Settings → Switch Platform to "PC, Mac & Linux Standalone"
   - Build and run two instances
   - Both should connect via Photon

2. **Test with VR in Editor**:
   - Use Unity's Play mode with VR headset connected
   - Run a second instance as a build
   - Both connect to Photon

This lets you verify Photon works while fixing the Android build separately.

---

**Next Steps:**
1. Check the full error message in Console (scroll up)
2. Try the quick fixes above
3. If still failing, share the specific error message for targeted help
