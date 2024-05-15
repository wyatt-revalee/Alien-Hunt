**INSTRUCTIONS**</br></br>
Play locally
- Download the folder labelled 'Alien Hunt - Demo' and run the executable within.

Play online
- Coming soon</br></br>

**DESIGN / GAMEPLAY**
</br></br>
Idea: </br>
A Galaga/Space Invaders style rogue-like. Players will control a character able to move side to side, and slightly up and down. Waves of enemies will come on screen shoot at the player. Player will shoot back while dodging incoming fire</br>
There will be bonuses to earn, power-ups to use, and items to buy.</br>
Players will fight through waves of enemies, with bosses every now and then, with the goal of beating a 'final' boss. Though, being a rogue-like, there should be tons of replay value.</br></br>
Genre : Point and Click Shooter</br></br>
Style : TBD (Pixel Art - Old Arcade Aesthetic?)</br></br>

**ABOUT**</br></br>
This project is sort of a sidestep from my bigger project, Prismatic Depths.</br></br>
I wanted to create a smaller, simpler game so I could see the full process from start to finish, as well as to just have a finished product under my belt.
</br></br></br>

**TODO**</br>



Gameplay
- Boss Fights
    - Should occur maybe every 5 waves, perhaps signifying the end of a stage or area.
    - Unique bosses, randomized pool (say, 3 different bosses for each level, randomly chosen. 4 regular boss fights per run plus final boss would mean 13 unique bosses)
    - Reward player with upgrade or better gear, maybe even reward based on performance (gungeon gives you extra health if you don't take damage from a boss, rewards skill but doesn't punish player if they get hurt)
- Power ups
  - Maybe things like exploding bullets, or bottomless magazine. These would be similar to pickups but they would only last for a certain time
~~- Weapons~~
 ~~b- Add more, unique weapons. Add limited ammo to them except for starter weapons. Add new ways to receive weapons besides shop.~~ Scope creep begone!


- Health pickups
  - ~~Add health pickups for players to recover health that they've lost~~ Done. A random enemy is assigned a health restoration that drops upon death. If enemy is missed, it is reassigned to a new enemy until player is healed.
  - Needs to be randomized. Player shouldn't just be guaranteed a health drop every time they are low.
- New enemy types
  - There are already a few but game would benefit from more. Think new enemies should maybe be introduced as the player progresses, rather than all at once
  - Boss waves - perhaps every 5th wave.
- ~~New bullet types~~ Tons of new items/upgrades to be added to game still.
~~- Add Buffs/Debuffs~~ Reducing scope
    ~~- Need to add in a buff/debuff system. Enemies and players should have some storage of these that run updates to them, doing whatever they do (give poison damage, movement speed, etc)~~
    ~~- This will probably require a rework of how stats are modified, due to multiple objects acting on a single stat.~~
    ~~- Potentially separate from player script, make modifiable~~
- Gameplay Loop Redesign
  - ~~Introduce more Rogue-Like elements? Perhaps a shop every wave/waves that allow players to upgrade weapons and skills, and purchase new items~~ Done. Shop added.
  - Add perma-unlocks after hitting milestones
  - Perhaps add an 'ending' (final boss?)

- ~~Add magazines and reloading~~
  - ~~Would add extra depth to gameplay and make different weapons feel more unique.~~ Done! Needs polishing.
- ~~Add a way to lose~~ With the rework, enemies will directly shoot at players. Players take damage and die if health reaches 0.
  - ~~Perhaps enemies can attack, or drop bombs that will explode and hurt the player unless destroyed, or even just make the player take damage for each enemy that gets off screen~~ Done
- ~~Weapon Pickups~~ Opted to do a shop system instead.
  - ~~Weapon pickups asset already exists and is function, need to work it into the gameplay somehow. Perhaps a random drop once a wave, or from an enemy?~~
 
- ~~Core Gameplay Rework~~ Done!
  - ~~Switch from point and click to a side-tos-side shooter (like galaga or space invaders)~~
  - ~~Allows for more appealing, simpler gameplay that is easy to expand on~~
  - ~~Allows for player to have a object to see themselves as, customizable.~~

 UI / Accessibility
 - Add start/pause/settings menus
  - Allow players to change color settings
    - Themes for UI / Background
    - ~~Color sliders for crosshair~~ Done!
    - Colorblind mode

Artwork & Animation</br>
- Would be great to have artwork for this game, but I'm not much of an artist. Something I would like to be better at in the future.
- Items, enemies, weapons, UI, menus, etc. would all be nice to have art and animations for - probably will come with polishing

Music & Sounds
- Main sound track
~~- Sounds for different guns being fired / reloaded~~ N/A anymore
- Sounds for enemies taking damange / dying
- Menu sounds
- End of wave / points awarded sounds
   
Data
  - Introduce high scores
      - Perhaps stored on my website, players can upload their scores and see leaderboards
 
