This documents what needs to be done in the project and when

**C#:**

PlayerRepository.cs
- [ ] Implement the functions that call stored procedures

Game.cs
- [x] Saving of the game state in a different thread with the GameSave class - Bailey 2/26

PlayerController.cs
- [ ] Implement LoginAsync to work correctly and use the db repository 
- [ ] There might need to be more functions created as well


**SQL:**
- [ ] Create stored procedure for reading / saving the player
- [ ] Create stored procedure for reading / saving the inventory

**Client Side:**
- [ ] Create a scroll view 
- [ ] Programmatically add buttons to it
- [ ] Make those buttons call the middle tier with put requests
- [ ] Pull data from the server every second on the current state of the user
