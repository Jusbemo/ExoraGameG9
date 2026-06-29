# 🚀 EXORA - Side-Scrolling 2D Platformer

## PROJECT OVERVIEW
**Type**: 2D Side-Scrolling Platformer | **Engine**: Unity 2022.3.62f3 | **Platform**: PC Windows
**Team**: Grupo 9, Universidad Fidelitas - Diseño de Videojuegos
**Members**: Lucía Soto Serrano, Alexander Campos Marín, Joustin Berger Monge, Tommy Alfaro Miranda, Luis Felipe Fernandez
**Delivery Date**: 04/08/2026

## STORY
Kaeron Vex, a space explorer, crashes on an alien planet. He must traverse 3 zones, collect 3 ship components, and defeat the final boss Zhorak to escape.

## CHARACTERS

### KAERON VEX (Protagonist/Hero)
- Space suit with dark helmet and glowing visor details
- Sprite: Astronaut (from Space Runner Assets)
- Animations: Idle, Run, Jump, Death

### LUMI-9 (Ally/Sage)
- Small robot with expressive glowing eyes
- (Low priority - simple sprites)

### ZHORAK (Antagonist/Final Boss)
- Purple/Black/Luminous color scheme
- 2-phase boss fight
- High priority asset

### ZHORAK'S ARMY (Enemies)
- Sentinel Patroller (ground enemies)
- Charge Hunter (charging enemies)
- Purple/Black/Luminous tones

## CORE MECHANICS
✅ Horizontal movement + Jump + Dash
✅ Ranged combat (energy projectiles, limited energy bar)
✅ 2 enemy types with unique AI
✅ Checkpoint system
✅ 3 ship component collection
✅ HUD: Health/Energy bars (top-left), Component counter (top-right)

## LEVEL STRUCTURE

### Level 1: Alien Jungle (Introductory)
- Source: Space Runner Assets + Mega Bot Forest
- Primary platforms: Nature-themed sci-fi
- Enemies: 1-2 Sentinels

### Level 2: Underground Caves (Intermediate)
- Source: Warped Caves
- Primary platforms: Cave environment with fungi/props
- Enemies: Sentinels + Charge Hunters

### Level 3: Alien Base/Fortress (Final)
- Source: Space Runner Assets + Mega Bot Base/Pipes
- Primary platforms: Industrial alien architecture
- Boss Arena: Zhorak (2 phases)

## DESIGN SPECIFICATIONS

### Resolution & Aspect Ratio
- **Resolution**: 1920x1080 (16:9)
- **Tile Size**: 32x32px (Space Runner), 16x16px (Warped Caves)
- **Character Scale**: ~16-32px sprites

### EXORA Color Palette
- **Dark Blue**: #032D42 (primary dark)
- **Cyan**: #63DF4E (accent/glow)
- **Purple**: #7D4E8C (secondary)
- **Dark Teal**: #044355 (shadow/depth)

## PROJECT STRUCTURE

```
Assets/
├── _Project/
│   ├── Scenes/
│   │   ├── 01_Prototype.unity (CURRENT)
│   │   ├── 01_Level1.unity (TODO)
│   │   ├── 02_Level2.unity (TODO)
│   │   ├── 03_Level3.unity (TODO)
│   │   └── 05_BossFight.unity (TODO)
│   ├── Scripts/
│   │   ├── Player/
│   │   │   ├── PlayerController.cs (CURRENT)
│   │   │   └── PlayerCombat.cs (TODO)
│   │   ├── Enemy/
│   │   │   ├── EnemyPatrol.cs (CURRENT)
│   │   │   ├── EnemyAI.cs (TODO)
│   │   │   └── BossZhorak.cs (TODO)
│   │   ├── UI/
│   │   │   ├── HUDManager.cs (TODO)
│   │   │   └── GameManager.cs (TODO)
│   │   └── Managers/
│   │       ├── LevelManager.cs (TODO)
│   │       └── AudioManager.cs (TODO)
│   ├── Art/
│   │   ├── Sprites/
│   │   │   ├── 01_SpaceRunner/
│   │   │   │   ├── Alien/ (4 sprites)
│   │   │   │   ├── Astronaut/ (4 sprites)
│   │   │   │   ├── Effects/
│   │   │   │   ├── Tiles/
│   │   │   │   ├── UI/
│   │   │   │   └── Other sprites/
│   │   │   └── 03_WarpedCaves/
│   │   │       ├── sprites/player/ (20+ animations)
│   │   │       ├── sprites/enemies/ (crab, jumper, octopus)
│   │   │       ├── sprites/Fx/ (effects)
│   │   │       └── environment/ (backgrounds, props)
│   │   ├── Materials/
│   │   └── Audio/ (TODO)
│   └── Prefabs/
│       ├── Player/ (TODO)
│       ├── Enemies/ (TODO)
│       ├── Items/ (TODO)
│       └── UI/ (TODO)
├── Materials/
├── Packages/
├── ProjectSettings/
└── Logs/
```

## ASSET PACKS USED

### ✅ Free Space Runner Pack (MattWalkden)
- **URL**: https://mattwalkden.itch.io/free-space-runner-pack
- **License**: CC0
- **Content**: Tileset, Astronaut, Aliens, FX, UI elements
- **Used in**: Level 1 & 3
- **Sprites Imported**: 30 (Alien, Astronaut, Effects, Tiles, UI, Digging Machines, Other sprites)

### ✅ Warped Caves (ansimuz)
- **URL**: https://ansimuz.itch.io/warped-caves
- **License**: CC0
- **Content**: Cave tileset, 3 enemy types, player animations, parallax, effects
- **Used in**: Level 2 (caves), player animations reference
- **Sprites Imported**: 107 (player animations, enemies, environment, effects)

### ❌ Mega Bot (ansimuz)
- Note: Not downloaded (Warped Caves covers Level 2 needs)

## SPRITE RECOLORING

### Palette Applied
All 148 sprites recolored to EXORA palette (v1):
- Dark Blue #032D42 → shadows/primary dark areas
- Cyan #63DF4E → glowing accents/visor details
- Purple #7D4E8C → secondary colors/energy effects
- Dark Teal #044355 → depth/backgrounds

### Organization
```
Assets/_Project/Art/Sprites/
├── 01_SpaceRunner/
│   ├── Alien/ (4 recolored sprites)
│   ├── Astronaut/ (4 recolored sprites)
│   ├── Digging Machines/
│   ├── Effects/
│   ├── Other sprites/
│   ├── Tiles/
│   └── UI/
└── 03_WarpedCaves/
    ├── environment/
    ├── sprites/
    │   ├── player/
    │   ├── enemies/
    │   └── Fx/
    └── spritesheets/
```

## CURRENT STATUS

### ✅ COMPLETED
- [x] Asset evaluation & selection
- [x] MCP Unity setup (v9.7.3)
- [x] Asset extraction & organization
- [x] Sprite recoloring (palette_v1 - EXORA colors)
- [x] 137 sprites imported to Assets/_Project/Art/Sprites/
- [x] Project cleanup (_ExternalPacks & _Output removed)
- [x] Project structure created
- [x] README.md created (this file)

### 🚧 IN PROGRESS
- [ ] Prototype scene: 01_Prototype.unity (about to create)
- [ ] PlayerController.cs (basic movement + jump)
- [ ] EnemyPatrol.cs (basic patrol AI)

### 📋 TODO - PRIORITY ORDER

**IMMEDIATE (Prototype Phase)**:
1. Create 01_Prototype scene with Player, Platforms, Enemy
2. PlayerController: WASD movement + SPACE jump + ground detection
3. EnemyPatrol: Left/right patrol with waypoints
4. Camera following Player with smoothing
5. Play test prototype (visual feedback)

**HIGH (Core Gameplay)**:
6. Player animations with Animator (Idle, Run, Jump, Death)
7. Combat system (projectile spawning + energy management)
8. Level 1 scene setup with platforms & enemies
9. Checkpoint/respawn system
10. Enemy types: Sentinel AI + Charge Hunter AI

**MEDIUM (Content)**:
11. Level 2 (Caves) scene setup
12. Level 3 (Base) scene setup
13. HUD/UI system (health bar, energy bar, component counter)
14. Boss Zhorak design & 2-phase AI
15. Audio system (background music + SFX)

**LOW (Polish)**:
16. Menu scene
17. LUMI-9 companion (optional/low priority)
18. Visual effects (particles, screen shake)
19. Game over/Win screens

## TECHNOLOGY STACK
- **Engine**: Unity 2D 2022.3.62f3
- **Language**: C#
- **Version Control**: GitHub
- **Development Tools**: 
  - VS Code (with Claude Code extension)
  - Unity MCP v9.7.3 (automated scene/asset management)
  - Aseprite/Krita (sprite editing - optional)
- **Target Platform**: Windows PC
- **Resolution**: 1920x1080 (16:9)

## DEVELOPMENT GUIDELINES FOR CLAUDE CODE

### Before Making Changes
1. **Read this README** - understand the 3-level structure and story
2. **Check Assets/_Project/Art/Sprites/** - know available sprites
3. **Respect the folder structure** - don't create assets outside _Project/
4. **Use EXORA colors** - #032D42, #63DF4E, #7D4E8C, #044355

### Code Standards
- **Naming**: PascalCase for classes (PlayerController), camelCase for variables/methods
- **Organization**: 
  - Player logic → Scripts/Player/
  - Enemy logic → Scripts/Enemy/
  - UI logic → Scripts/UI/
  - Managers → Scripts/Managers/
- **Comments**: Explain game logic (e.g., "Ground detection raycast for jump eligibility")
- **Modularity**: Keep concerns separated (movement ≠ animation ≠ combat)

### Script Templates

**PlayerController.cs** should have:
```csharp
- Movement (horizontal via WASD/Arrows)
- Jumping (SPACE, once per ground contact)
- Ground detection (raycast down)
- Sprite flipping based on direction
- Console logs for debugging
```

**EnemyPatrol.cs** should have:
```csharp
- Patrol between waypoints (left/right)
- Speed control (currently 2)
- Sprite flipping on direction change
- Configurable patrol distance
```

**Scene Setup** should include:
```
- Player at origin (0, 0, 0)
- Platforms with static Rigidbody2D
- Camera following Player
- Background/lighting appropriate to zone
```

## KNOWN LIMITATIONS & NOTES
- No Mega Bot assets downloaded (Warped Caves sufficient for Level 2)
- Boss Zhorak assets need custom creation (not in asset packs)
- LUMI-9 is low priority (design can be simple)
- Player idle animation is single frame (Space Runner Astronaut sprite)
- Warped Caves has more player animations (10 run frames, 6 jump frames)

## NEXT SESSION CHECKLIST
- [ ] Create 01_Prototype scene
- [ ] PlayerController implementation
- [ ] EnemyPatrol implementation
- [ ] Test Play mode (movement + jump + patrol)
- [ ] Add player animations with Animator
- [ ] Create Level 1 scene
- [ ] Expand enemy AI patterns (Sentinel vs Charge Hunter)

## USEFUL ASSET LOCATIONS

**Kaeron Vex (Player)**:
- `Assets/_Project/Art/Sprites/01_SpaceRunner/Astronaut/`
- Files: Astronaut_Idle.png, Astronaut_Run.png, Astronaut_Jump.png, Astronaut_Death.png

**Enemies**:
- Sentinel: `Assets/_Project/Art/Sprites/01_SpaceRunner/Alien/`
- Warped Caves: `Assets/_Project/Art/Sprites/03_WarpedCaves/sprites/enemies/`

**Platforms/Tilesets**:
- Space Runner: `Assets/_Project/Art/Sprites/01_SpaceRunner/Tiles/RunnerTileSet.png`
- Warped Caves: `Assets/_Project/Art/Sprites/03_WarpedCaves/environment/layers/`

**Effects**:
- Dust, sparkles: `Assets/_Project/Art/Sprites/01_SpaceRunner/Effects/`
- Power-ups, impacts: `Assets/_Project/Art/Sprites/03_WarpedCaves/sprites/Fx/`

## CONTACT & CREDITS
**Art Assets**: 
- MattWalkden (Space Runner) - https://mattwalkden.itch.io
- ansimuz (Warped Caves) - https://ansimuz.itch.io
- Licensed under CC0 (Public Domain)

**Development**: Grupo 9, Universidad Fidelitas
**Assistant**: Claude (AI development assistance via MCP)
