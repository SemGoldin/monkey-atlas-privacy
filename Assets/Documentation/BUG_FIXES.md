# Bug Fixes - Nut Colors and Bolt Positioning

## Issues Fixed

### Issue 1: All Nuts Appearing Red
**Problem:** All nuts were appearing with red color instead of having different colors (red, blue, green, yellow, cyan, magenta, orange, purple).

**Root Cause:** In the `Nut.cs` class, when `Initialize()` was called, the `SpriteRenderer` component might not exist yet. The code checked if `spriteRenderer` was null and tried to get it, but if it didn't exist on the GameObject, it would remain null and the color assignment would fail silently.

**Fix:** Enhanced the `Initialize()` method in `Nut.cs` to not only check for the `SpriteRenderer` component but also create it if it doesn't exist:
```csharp
if (spriteRenderer == null)
{
    spriteRenderer = GetComponent<SpriteRenderer>();
    if (spriteRenderer == null)
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }
}
spriteRenderer.color = color;
```

This ensures that the `SpriteRenderer` component exists before attempting to set the color, preventing the issue where all nuts would default to red (Unity's default color).

### Issue 2: First Bolt Spawning Off-Screen
**Problem:** The first bolt (with 4 nuts on it) was spawning outside the visible screen area at position (-3, 0, 0), making it impossible to interact with.

**Root Cause:** The `startPosition` in `LevelGenerator.cs` was hardcoded to `(-3f, 0f, 0f)`. With this position, the first bolt would always spawn at x = -3, which is too far left for most camera setups. Additionally, the layout didn't account for the total number of bolts, so the bolts weren't centered on screen.

**Fix:** Modified the `CreateBolts()` method in `LevelGenerator.cs` to dynamically calculate the start position based on the number of bolts and center them on screen:
```csharp
// Calculate centered start position based on number of bolts
float totalWidth = (count - 1) * boltSpacing;
Vector3 centeredStart = new Vector3(-totalWidth / 2f, startPosition.y, startPosition.z);
```

This calculation:
1. Determines the total width needed for all bolts: `(count - 1) * boltSpacing`
2. Centers them by starting at `-totalWidth / 2f`
3. Uses the Y and Z components from the original `startPosition` for vertical positioning

With this fix, all bolts (including the first one with 4 nuts) are now properly centered on screen and visible to the player.

## Testing

To verify these fixes:
1. **Nut Colors**: Start a new level and verify that nuts display with multiple different colors (red, blue, green, yellow, etc.) rather than all being red
2. **Bolt Positioning**: Verify that all bolts are visible on screen and centered, with the first bolt clearly visible on the left side

## Technical Details

**Files Modified:**
- `Assets/Scripts/Core/Nut.cs` - Enhanced SpriteRenderer initialization
- `Assets/Scripts/Core/LevelGenerator.cs` - Added dynamic bolt positioning calculation

**Impact:** These are minimal surgical changes that fix the core issues without affecting other game functionality.
