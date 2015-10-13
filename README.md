Testable-Code
=============
> Examples of how to write testable code

#### Why Unit Testing?
- Testable code forces you to write clean separated code => maintainability
- Tests can be automated to run after every change => safety net
- Bottom-up approach: first test parts of the program before testing the program itself

Demo 01: Test Driven Development
--------------------------------
#### Write small automated tests first
This requires the developer to fully understand requirements
Initially a new test fails, so add code & refactor until the test pass. 
Tests serves as documentation.

#### Example Requirements
Write a 'StringCalculator' class that accepts integers and return their sum:
- The Add method accepts a comma-delimited string of integers.
- For an empty string the Add method will return 0.
- The Add method will return their sum of numbers.
- The Add method supports user specified delimiters
