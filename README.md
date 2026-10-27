# Testable-Code

This repository contains examples of how to write testable code using C#

## Prerequisites

Install the .NET 10 SDK to build and run the demos:

```powershell
winget install --id Microsoft.DotNet.SDK.10 --exact
```

## Why Unit Testing?

- Testable code forces you to write clean separated code => maintainability
- Tests can be automated to run after every change => safety net
- Bottom-up approach: first test parts of the program before testing the program itself

## Demo 01 - Test Driven Development

### Write small automated tests first

This requires the developer to fully understand requirements.
Initially a new test fails, so add code & refactor until the test pass.
Tests serve as documentation.

### Demo 1 Example Requirements

Write a 'StringCalculator' class that accepts integers and return their sum:

- The Add method accepts a comma-delimited string of integers.
- For an empty string the Add method will return 0.
- The Add method will return their sum of numbers.
- The Add method supports user specified delimiters.
- The Add method must be able to skip a pre-defined number of fields from the start of the string.

### Running Demo 1

Demo 1 is a class library, so run its tests from the repository root:

```powershell
dotnet test .\Demo01.TestDrivenDevelopment.Test\Demo01.TestDrivenDevelopment.Test.csproj
```

## Demo 02 - Layered Architecture

### A traditional Layered Architecture

Creates unnecessary coupling

- The Presentation layer depends on ...
- The Business layer depends on ...
- The Data layer
... And everything depends on common utility and infrastructure classes.

To solve coupling, one could introduce Dependency Injection of these classes.

### The Onion Architecture

See [The Onion Architecture, by Jeffrey Palermo](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/)

Dependency Injection is integral part of this architecture. It encourages a better separation of concerns:

- Infrastructure concerns are implemented in classes that can be injected into the lower layers
- The Presentation layer depends on ...
- The Application Services layer depends on ...
- The Domain Services layer depends on ...
- The Domain Model
... And common utility and infrastructure classes are injected into the application services.

### Demo 2 Example Requirements

Users upload a character separated values (CSV) file containing expenses reports of our employees

- The applications accepts a CSV file.
  - The first column is the user id
  - The second column is the expenses category
  - The 12 next columns are the monthly expenses expressed in euro

  |EmployeeId|Category|January|February|March|April|May|June|July|August|September|October|November|December|
  |:---------|:-------|------:|-------:|----:|----:|--:|---:|---:|-----:|--------:|------:|-------:|-------:|
  |1|Travel|92|92|92|92|92|92|92|92|92|92|92|92|
  |1|Parking|20|20|20|20|35|00|20|35|20|20|20|20|
  |2|Travel|1500|0|0|1700|0|0|1400|0|0|2400|0|0|
  |2|Parking|200|0|0|150|0|0|235|0|0|175|0|0|
  |2|Hotel|700|0|0|750|0|0|335|0|0|1075|0|0|

### Running Demo 2

Demo 2 consists of class libraries. Run its application and domain tests from the repository root:

```powershell
dotnet test .\Demo02.LayeredArchitecture.Application.Test\Demo02.LayeredArchitecture.Application.Test.csproj
dotnet test .\Demo02.LayeredArchitecture.Domain.Test\Demo02.LayeredArchitecture.Domain.Test.csproj
```

## Demo 03 - Presentation

### Non Testable UI

Using the view as a datacontainer and putting code directly in the code-behind will create a tight coupling between the presentation logic and the actual view. This makes it impossible to write automated tests for the presentation logic.

### Model-View-ViewModel

Decouple the View from the presentation logic by applying the Model-View-ViewModel pattern. This demo is inspired by Jason Dolinger's Youtube video on Model-View-ViewModel that is no longer online

For more information, see Microsoft's archived article [WPF Apps With the Model-View-ViewModel Design Pattern](https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern).

- Move data out of the code-behind of the view and into the ViewModel and expose them as public properties
- Bind the controls on the view to the public properties of the ViewModel
- Inject external dependencies into the ViewModel
- Inject the ViewModel into the View from App.xaml.cs

### Running Demo 3

Run the WPF application from the repository root on Windows:

```powershell
dotnet run --project .\Demo03.Presentation.MVVM.UI\Demo03.Presentation.MVVM.UI.csproj
```

Run the Demo 3 test suite:

```powershell
dotnet test .\Demo03.Presentation.MVVM.UI.Test\Demo03.Presentation.MVVM.UI.Test.csproj
```
