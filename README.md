# Tic Tac Toe Kata

## Description
The goal of this kata is to follow the TDD process, utilise Test Doubles (Mocks, Spies and Stubs) using custom-implementations at the start of development, then switch to the Moq framework. A Bottom-Up approach will be followed.

## Directions
This kata has been developed using .NET v6.0

### Run Main Program
    cd TicTacToe
    dotnet run

### Run Test Suite
    cd TicTacToe.Test
    dotnet test

## Language Design

### Test Driven Development
**Level 4**

This Tic Tac Toe kata was developed using a test-first, bottom-up approach. This meant the use of mocked implementations could be avoided and allow for real implementations to be used during development instead. Development started off by creating tests for smaller and lower-level components followed by their implementations. These low-level components were later made to work with each other through the creation of higher-level components. The implementation of these components were verified through long term tests to ensure the functionality of the solution was correct and performing as expected.

### Test Doubles
**Level 4**

In my previous Katas, custom-made Fakes were favoured over the use of Mocks and the Moq framework. This was due to (a) testing state over behaviour, (b) favouring real dependencies over mocks, (c) the small size of the program/kata. Testing for state over behaviour allows for higher certainty that the program works as expected; custom fakes allowed state in various areas to be inspected for change. If only behaviour was tested, the program might satisfy the behavioural requirements but could fail to guarantee the program works as it should. As always, favouring real dependencies over mocked implementations gives certainty components will work as they should and communicates with one another as expected; it gives an idea of how state is changing around the system.

However...In this Kata, the Moq framework was used to setup (true) Mocks (verified what methods were called, with what arguments and how often) and Spies (verified what methods were called). In areas where control flow was being tested such as the Game class, Moq was used to verify the flow of the program was correct; it made sure particular functions were being called with the correct arguments, and for the correct number of times. This methodology was further extended into classes containing I/O-related responsibilities such as the OutputMessenger and UserInput classes. In areas where State Behaviour had to be tested, traditional Assertions and real implementations of the SUT were used.

### YAGNI
**Level 4**

This principle was kept in mind throughout development of this kata. However, as this kata was used to learn a new concept (Exceptions), YAGNI was not strictly followed. Exceptions in Tic Tac Toe are _very_ unnecessary as all unintended inputs made by the user can be taken care of; there is no situation where a fault can't be handled. Utilising Exceptions to handle such faults is unfit for this kata. Exceptions are designed to be thrown in cases of rare/exceptional faults or truly unexpected behaviour, e.g. failure to connect to a database externally hosted on AWS.

In this Kata, two Exceptions were used to handle invalid user moves: (a) OccupiedSpaceException and (b) OutOfBoundsException; they are both thrown by the PlaceSymbol method in Models/Grid and caught in the control flow of the Start method in Models/Game. Both simply trigger the loop to run again which prompts the user for a new valid input/move and updates the grid. Given the simplicity of how the Exception is handled, similar logic could be implemented to handle the aforementioned scenarios without using Exceptions. A basic while-loop could be used to prompt the user for a valid move until it receives one, i.e., isn't an already occupied space or outside of the grid's boundaries.

### Object Composition
**Level 4**

Composition was strongly favoured in this kata to keep coupling to a minimum and allow for easier changes to be made. The design of this kata meant that it would make sense to use composition, i.e., HAS-A relationship, compared to inheritance, i.e., IS-A relationship. For example, the Game class HAS-A Player, TurnAllocator, Grid, etc.

### Keeping Things Small
**Level 4**

Classes and methods were consistently broken up into smaller components to ensure its readability and contained its own separate responsibilities. Areas such as the Game class containing logic for control flow could not be broken up into smaller parts.

### Revealing Intent
**Level 3**

Naming of classes, methods and variable names mostly reference domain-related concepts. However, a very small number of method and variable names may make references to technical-related concepts. No comments were used.

### Removing Duplication
**Level 3**

Repeatable patterns are identified based on code duplication. Duplicate code patterns are consistently pulled out into supporting methods or classes.

### Command Query Separation
**Level 4**

Consistently applies command query separation in code. Identifies exceptions to the general rule and can articulate trade-offs with others.
