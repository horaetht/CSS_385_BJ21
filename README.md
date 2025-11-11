# CSS_385_BJ21
BlackJack21
#  BLACKJACK 21

##  Game Pitch
**BLACKJACK 21** is a digital recreation of the classic casino card game, built in Unity.  
Players start with a set amount of money and try to beat the dealer by getting a hand value closest to **21** without going over.  
The game includes full betting, hit/stay mechanics, saving/loading player progress, and a main menu with pause and game-over screens.

---

##  Game Mechanics Guide (How to Play)

###  Basic Rules
- The goal is to get **21 points** or as close as possible without exceeding it.  
- Each card has a point value:
  - Number cards = face value (e.g., 7 = 7 points)  
  - Face cards (J, Q, K) = 10 points  
  - Ace (A) = 1 or 11, whichever benefits the hand  
- The dealer draws cards until their hand value is **16 or higher**.  
- If the dealer busts (over 21), the player automatically wins.

###  Betting
- Players can click on the **chip** button to place bets before starting a round.
- Winning a round doubles your bet; losing deducts it from your balance.
- If your money reaches 0, the game displays **“YOU LOSE”** and returns to the main menu.

###  Gameplay Buttons
| Button | Description |
|---------|--------------|
| **Deal** | Start a new hand. |
| **Hit** | Draw one additional card. |
| **Stay / Call** | Stop drawing cards and let the dealer play. |
| **Bet Chip** | Place the shown amount as your bet before dealing. |

---

##  Controls
| Key / Action | Function |
|---------------|-----------|
| **Left Click** | Interact with buttons and chips |
| **ESC** | Opens pause menu (Resume, Save, Main Menu, Quit) 
  **will be added at the next update this is a prototype**

---

##  Features
- **Main Menu Scene** (`MainMenu.unity`)  
  - Start New Game  
  - Load Saved Game  **will be added at the next update this is a prototype**
  - Quit Game  **will be added at the next update this is a prototype**

- **Gameplay Scene** (`SampleScene.unity`)  
  - Full blackjack round system  
  - Working bet system  
  - Auto-check for busts and dealer logic  
  - Save/Load system with `PlayerPrefs`  
  **will be added at the next update this is a prototype**
  - Pause menu accessible via ESC  
  **will be added at the next update this is a prototype**

- **Save System**  **will be added at the next update this is a prototype**
  - Player money automatically saved after each round or when pressing “Save Game”.  
  - When loading, previous money is restored.  

---

##  Scripts Overview
| Script | Description |
|--------|-------------|
| **GameManager.cs** | Manages round flow, scoring, bets, and win/loss logic. |
| **PlayerScript.cs** | Tracks player’s hand, money, and save/load data. |
| **CardScript.cs** | Defines card behavior and value. |
| **DeckScript.cs** | Handles card dealing and shuffling. |
| **MainMenu.cs** | Handles scene transitions and main menu buttons. |

---

## 🧪 Objective 
- Start from the **Main Menu**, play through multiple rounds, test betting logic.  
- Confirm that “YOU LOSE” triggers correctly when money hits zero.  

---

## 🎥 Video Demonstration
**Link** : https://drive.google.com/file/d/1cQ-d8EfHoLJIcRIA4aojoEM2setC1wTQ/view?usp=sharing



