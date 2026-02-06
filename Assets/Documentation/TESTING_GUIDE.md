# Testing Guide - Verifying Bug Fixes

This guide will help you verify that the nut color and bolt positioning issues have been fixed.

## Prerequisites

- Unity 2021.3 LTS or newer installed
- Project opened in Unity
- Game scene set up according to SETUP_GUIDE.md

## Test 1: Nut Colors (All Nuts Were Red)

### Expected Behavior BEFORE Fix:
- ❌ All nuts appeared red regardless of their intended color
- ❌ Unable to distinguish between different nut types
- ❌ Game was unplayable due to color-matching mechanics not working

### Expected Behavior AFTER Fix:
- ✅ Nuts display with 8 different colors:
  - Red (Color 0)
  - Blue (Color 1)
  - Green (Color 2)
  - Yellow (Color 3)
  - Cyan (Color 4)
  - Magenta (Color 5)
  - Orange (Color 6)
  - Purple (Color 7)
- ✅ Each nut is clearly distinguishable by color
- ✅ Color-matching mechanics work correctly

### How to Test:

1. **Open Unity Project**
   - Open the project in Unity
   - Navigate to Assets/Scenes/Game.unity

2. **Enter Play Mode**
   - Press the Play button (or Ctrl/Cmd + P)
   - Wait for the level to generate

3. **Visual Inspection**
   - Look at the bolts on screen
   - Verify that nuts have different colors
   - Count how many different colors you can see (should be multiple, not just red)

4. **Interact with Nuts**
   - Click on a bolt to pick up the top nut
   - Observe the nut's color
   - Click on different bolts and verify each nut type has a distinct color

### ✅ Test Passes If:
- You can see nuts in multiple different colors (not all red)
- At least 3-4 different colors are visible in the initial level
- Nuts of the same color are distinguishable from nuts of different colors

### ❌ Test Fails If:
- All nuts appear red
- Only one or two colors are visible
- Nuts don't display any color (appear white/transparent)

---

## Test 2: Bolt Positioning (First Bolt Off-Screen)

### Expected Behavior BEFORE Fix:
- ❌ First bolt spawned at x = -3.0, outside visible area
- ❌ Could not see or interact with the first bolt
- ❌ 4 nuts on the first bolt were inaccessible
- ❌ Game was unplayable from start

### Expected Behavior AFTER Fix:
- ✅ All bolts are visible on screen
- ✅ First bolt (leftmost) is fully visible and accessible
- ✅ Bolts are evenly distributed horizontally
- ✅ Layout is centered on screen

### How to Test:

1. **Open Unity Project**
   - Open the project in Unity
   - Navigate to Assets/Scenes/Game.unity

2. **Enter Play Mode**
   - Press the Play button
   - Wait for the level to generate

3. **Visual Inspection**
   - Look at the left edge of the screen
   - Verify that the leftmost bolt is fully visible
   - Count all bolts - they should all be on screen
   - Check that bolts appear centered

4. **Interact with First Bolt**
   - Click on the leftmost bolt
   - Verify that you can pick up nuts from it
   - Confirm it has nuts on it (should have 4 nuts)

5. **Check Different Levels**
   - Complete the first level or restart with different level numbers
   - Verify that positioning works correctly regardless of bolt count

### ✅ Test Passes If:
- All bolts are visible within the camera viewport
- The first bolt on the left is fully visible and clickable
- Bolts appear evenly distributed and centered
- You can interact with all bolts including the leftmost one

### ❌ Test Fails If:
- Any bolt is outside the visible area
- The leftmost bolt is cut off or partially visible
- Bolts are clustered to one side instead of centered
- Cannot click on the first bolt

---

## Test 3: Combined Gameplay (Integration Test)

### How to Test:

1. **Start a New Game**
   - Open Game scene and press Play
   - Or launch from Menu scene

2. **Play Through a Level**
   - Verify all nuts have different colors
   - Verify all bolts are visible and accessible
   - Pick up nuts from various bolts
   - Place nuts on different bolts
   - Complete a level by matching all nuts

3. **Try Multiple Levels**
   - Complete level 1 and proceed to level 2
   - Verify positioning works with increased bolt count
   - Verify colors remain correct across levels

### ✅ Test Passes If:
- Game is fully playable from start to finish
- All nuts are visible and have distinct colors
- All bolts are accessible throughout gameplay
- Can complete levels successfully

---

## Troubleshooting

### Issue: Still seeing all red nuts
**Possible causes:**
1. Changes not saved - ensure Nut.cs was modified and saved
2. Unity didn't recompile - try Assets → Refresh or restart Unity
3. Using old prefabs - ensure prefabs reference updated scripts

**Solution:** Check that the changes in Nut.cs are present (lines 30-33 should include the AddComponent check)

### Issue: Bolts still off-screen
**Possible causes:**
1. Changes not saved - ensure LevelGenerator.cs was modified
2. Camera settings incorrect - check camera orthographic size
3. Wrong scene - ensure using the Game scene not an old version

**Solution:** Check that LevelGenerator.cs has the centering calculation (lines 35-37)

### Issue: Unity compilation errors
**Possible causes:**
1. Syntax error in modified files
2. Missing Unity packages

**Solution:** Check Unity Console for specific error messages, verify code syntax

---

## Performance Verification

After fixes, also verify:
- [ ] No performance degradation
- [ ] No memory leaks
- [ ] Smooth gameplay
- [ ] No errors in Unity Console

---

## Reporting Issues

If tests fail or you encounter problems:

1. **Check Unity Console** for error messages
2. **Verify changes** were applied correctly using CODE_DIFF.md
3. **Review documentation**:
   - BUG_FIXES.md - Technical details
   - VISUAL_FIX_EXPLANATION.md - Visual explanations
   - FIX_SUMMARY.md - Complete overview

4. **Contact support** at vaschishin.s@gmail.com with:
   - Description of the problem
   - Screenshots
   - Unity version
   - Console error messages (if any)

---

## Success Criteria Summary

| Test | Criteria | Status |
|------|----------|--------|
| Nut Colors | Multiple colors visible | ⬜ |
| Bolt Positioning | All bolts on screen | ⬜ |
| First Bolt Accessible | Can click leftmost bolt | ⬜ |
| Gameplay Functional | Can play and complete level | ⬜ |
| No Errors | Unity console clean | ⬜ |

**All tests should pass** ✅

---

**Last Updated:** 2026-02-06  
**Fixes Applied:** Commits e8a50b1, c2607be, 5c1a0b9, 08b2eb3
