### Cognito: Info on Building the Auth Library

**What was the hack I had to do to install the Cognito Lib? 
Very simple error to correct on my part with NuGet (User 13 error)**

**1.** I created my Class Library with the default class name Class which created Class.cs. I renamed the class in my code and similiar to Java the class name and file name have to match.

**2.** The NuGet library wouldn't allow for me to add the library to my .NET Core class because it didn't understand how to add it because it didn't know which class was the .NET Core library to add it to: Class or LexBotAuth since names was different.

**3.** I renamed Class.cs file to LexBotAuth. Saved file, Clean (on Solution), and then Build. Then I had a successful build for my project. 

**4.** Go back and add the Cognito Libraries from NuGet by searching in NuGet for AWSSDK Cognito.

**Voila!** AWSSDK libraries added and the code is up and running, ready to use.