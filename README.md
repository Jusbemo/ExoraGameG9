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

## TECHNOLOGY STACK
- **Engine**: Unity 2D 2022.3.62f3
- **Language**: C#
- **Version Control**: GitHub
- **Development Tools**: 
  - VS Code
  - Aseprite/Krita (sprite editing - optional)
- **Target Platform**: Windows PC
- **Resolution**: 1920x1080 (16:9)

## KNOWN LIMITATIONS & NOTES
- No Mega Bot assets downloaded (Warped Caves sufficient for Level 2)
- Boss Zhorak assets need custom creation (not in asset packs)
- LUMI-9 is low priority (design can be simple)
- Player idle animation is single frame (Space Runner Astronaut sprite)
- Warped Caves has more player animations (10 run frames, 6 jump frames)

## CONTACT & CREDITS
**Art Assets**: 
- MattWalkden (Space Runner) - https://mattwalkden.itch.io
- ansimuz (Warped Caves) - https://ansimuz.itch.io
- Licensed under CC0 (Public Domain)

**Development**: Grupo 9, Universidad Fidelitas
