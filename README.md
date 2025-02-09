# Text-Based-Adventure-Game

A Console-Based Interactive Adventure Game using OOP & Design Patterns

📌 Project Overview

This is a fully interactive text-based adventure game developed in C# using object-oriented programming (OOP) principles and design patterns. The game allows players to navigate through different rooms, interact with NPCs, pick up and store items, unlock doors, and complete quests.

Players can use command-based input to control their actions, making it a flexible and immersive console-based game.

🎮 Features

🔹 Player Interactions
- Move between rooms using go <direction>.
- Use back to return to the previous room (supports full backtracking).
- Pick up and drop items with pickup <item> and drop <item>.
- Open locked doors with open <direction>.
- Unlock doors with unlock <direction> using keys.
  
🔹 NPC Interactions
- Use ask <npc> to talk to NPCs (Non-Playable Characters).
- Give items to NPCs using give <item> <npc>.
- Retrieve stored items from NPCs using retrieve <item> <npc>.
- Peek into an NPC’s storage using peek <npc>.
  
🔹 Game Mechanics
- Room Navigation System: Explore connected rooms, some requiring keys to unlock.
- Transporter Room: Randomly transports players to different locations.
- Item Management: Players have an inventory system with a weight limit.
- Quest System: NPCs request items, and fulfilling their requests can change the game state.
- Game Victory Condition: Completing NPC quests leads to a win state.

🛠️ Technologies & Concepts Used:
Language: C#
- Programming Concepts: Object-Oriented Programming (OOP)
- Data Structures: Stacks, Dictionaries, Lists
Design Patterns Implemented:
- Command Pattern – For executing user commands (e.g., go, open, give, peek).
- Singleton Pattern – For managing the GameWorld instance.
- Observer Pattern – For triggering game events like entering rooms.
- Factory Pattern – For dynamic object creation.

🚀 Installation & Setup

🔹 Prerequisites
Install .NET SDK.

A C# IDE (Visual Studio recommended) or a command-line compiler.
  
🔹 Run the Game
- Open the project in Visual Studio.
- Set the main file as the startup project and run it.

📝 How to Play
**Command	Description**
- go <direction>	Moves the player to a connected room (north, south, east, west).
- back	Returns to the previous room.
- open <direction>	Opens a door in the given direction.
- unlock <direction>	Unlocks a locked door using a key.
- pickup <item>	Picks up an item from the room.
- drop <item>	Drops an item into the room.
- ask <npc>	Asks an NPC about their requests.
- give <item> <npc>	Gives an item to an NPC.
- retrieve <item> <npc>	Retrieves an item previously given to an NPC.
- peek <npc>	Peeks into an NPC's storage.
- exit	Exits the game.
  
📜 Game Rules
- Weight Limit: The player can only carry a limited weight in their inventory.
- Quest Completion: Completing NPC requests progresses the game.
- Winning the Game: When all required items are given to the NPC, the game declares victory.
