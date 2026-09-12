# Kitchen

## Overview

Kitchen is a Unity-based kitchen interaction and cooking gameplay prototype.
It was created as a coursework and gameplay-systems exercise, focusing on
clear object ownership, counter interactions, recipe-driven state changes, and
visual feedback in a small 3D scene.

The project is a learning prototype rather than a finished commercial game. It
is intended to demonstrate the implementation of foundational Unity gameplay
systems in a kitchen setting.

## Gameplay Loop

The player moves through a kitchen and interacts with counters and food items.
The core loop is:

1. Select an interactive counter or kitchen object.
2. Pick up or place an ingredient.
3. Prepare the ingredient at the appropriate station, such as a cutting
   counter or stove.
4. Observe preparation progress and the resulting object state.
5. Clear or discard objects when necessary and continue the interaction loop.

This workflow supports common ingredient transitions, including cutting,
cooking, and burning.

## Gameplay Features

### Player Interaction

- Player movement and interaction handling are implemented through `Player`
  and `GameInput`.
- `IKitchenObjectParent` and `KitchenObject` establish the ownership and
  transfer rules used when objects are picked up, placed, or moved between
  counters.
- Player animation and counter-selection visuals provide feedback for movement
  and the active interaction target.

### Counter Systems

The project models several specialised counter types under a shared base:

- **Container Counter**: supplies configured kitchen objects or ingredients.
- **Clear Counter**: provides a neutral surface for placing and retrieving
  objects.
- **Cutting Counter**: transforms ingredients through defined cutting recipes.
- **Stove Counter**: progresses frying recipes and can transition food to a
  burned state.
- **Trash Counter**: removes unwanted objects from the current gameplay loop.

These systems are implemented in `Assets/NewScripts/Counter/` and use counter
prefabs in `Assets/PreFabs/Counters/`.

### Recipe and Object Data

Kitchen objects and state transitions are represented through ScriptableObject
data assets and recipe classes:

- `KitchenObjectSO` defines the available ingredients and prepared objects.
- `CuttingRecipeSO` maps an input ingredient to its cut result.
- `FryingRecipeSO` maps an uncooked object to its cooked result.
- `BurningRecipeSO` maps a cooked object to its burned result.

The associated assets are organised under `Assets/ScriptableObjects/`, making
the recipe data configurable without embedding object mappings directly in
gameplay scripts.

### Feedback and Presentation

- `ProgressBarUI` visualises time-based preparation progress.
- Counter-specific visual scripts control feedback for containers, cutting
  actions, stove states, and selected counters.
- The project includes prefabs, animation controllers, materials, meshes,
  sounds, and UI textures required by the sample kitchen scene.

## Project Structure

```text
Assets/
├── NewScripts/
│   ├── Counter/              # Counter behaviours and interaction rules
│   ├── SO/                   # Kitchen-object and recipe ScriptableObjects
│   ├── Visual/               # Counter and selection visual behaviours
│   ├── GameInput.cs          # Input abstraction
│   ├── KitchenObject.cs      # Pick-up and transfer behaviour
│   └── Player.cs             # Player movement and interactions
├── PreFabs/                  # Kitchen objects and counter prefabs
├── Scenes/SampleScene.unity  # Demonstration scene
├── ScriptableObjects/        # Configured ingredient and recipe data
└── _Assets/                  # Art, animation, audio, and presentation assets

ProjectSettings/              # Unity project configuration
Packages/                     # Unity package manifest and lock file
```

## Requirements and Setup

The project was created with **Unity 2022.3.62f2**. To open it:

1. Clone this repository.
2. In Unity Hub, add the cloned project folder.
3. Open the project with Unity 2022.3.62f2, or a compatible Unity 2022.3 LTS
   editor.
4. Open `Assets/Scenes/SampleScene.unity`.
5. Press **Play** in the Unity Editor to run the prototype.

Input bindings are defined in `Assets/PlayerInputActions.inputactions`. Refer
to that asset or the Unity Input System inspector when changing controls.

## Development Scope

This repository focuses on the gameplay architecture and prototype assets
needed to explore kitchen interactions. It does not claim to provide a
complete production game, platform build pipeline, save system, multiplayer
support, accessibility suite, or final user experience.

When contributing, keep generated Unity directories such as `Library/` and
`Logs/`, along with IDE build artefacts, out of version control unless there is
a specific reproducibility reason to include them.
