# CSB310 C# Langauge Project (Java/Python Alternative)

### Visual Studio Project Organization:
> <pre>
>  
> AltLangProj  <--------------------------------->  (The Visual Studio Project)  
>  |   |   |  
>  |   |   |=== Program.cs  <-------------------->  (The Main Program Driver)  
>  |   |  
>  |   |=== Resources  <------------------------->  (Folder Containing Project Resources)  
>  |         |  |  |
>  |         |  |  |=== Git Screenshots  <------->  (Folder Containing Required Git Screenshots)  
>  |         |  |  
>  |         |  |=== Input  <-------------------->  (Folder Containing Input csv Files)  
>  |         |  
>  |         |=== Report Assets  <--------------->  (Folder Containing Screenshots for Report)  
>  |  
>  |=== Classes  <------------------------------->  (Folder Containing the Classes for the Project)
>          |  
>          |=== Cell.cs  <----------------------->  (Class representing one cell record)  
>          |  
>          |=== CellFields.cs  <----------------->  (Class representing a column oriented table)  
>          |  
>          |=== CellRecords.cs  <---------------->  (Class representing a row oriented table)  
>          |  
>          |=== CellTable.cs  <------------------>  (Class where I implemented my additional methods)
>          |  
>          |=== CleanCellData.cs  <-------------->  (Class to clean cell data)  
>          |  
>          |=== FilterParameter.cs  <------------>  (Class to filter results)  
>          |  
>          |=== ParseCsvFile.cs  <--------------->  (Class to parse csv file)  
>  
> </pre>

### Git Requirment Screenshots:
> <pre>
> 1. Staging
> </pre>
> ![Git_Staging_Screenshot](https://github.com/twopercentjazz/CSB310_AltLangProj/assets/49768882/7174de4b-62c7-45eb-971f-0cc1239611a6)
> <pre>
> 2. Commiting
> </pre>
> ![Git_Commiting_Screenshot](https://github.com/twopercentjazz/CSB310_AltLangProj/assets/49768882/cbf1d54b-78af-45f3-8e02-042f0bda1e4b)
> <pre>
> 3. Pushing
> </pre>
> ![Git_Pushing_Screenshot](https://github.com/twopercentjazz/CSB310_AltLangProj/assets/49768882/bb20bcdc-ebd2-4d12-992c-36c0c47ca2f6)

# Project Report

### Which Programming Language and version did you pick?
> <pre>
> For this project I chose to explore the C# programming language using the Visual Studio IDE. 
> </pre>
> <pre>
> C# Version 11
> </pre>
> <pre>
> .NET Version 7
> </pre>
> <pre>
> Visusal Studio Community 2022 Version 17.6.2
> </pre>

### Why did you pick this programming language?
> <pre>
> For this project I chose to explore the C# programming language because it is a widely used 
> general-purpose high-level language. C# is versatile in the industry and it is used today 
> to build a wide range of applications and web-based services. For example, C# is commonly used 
> for backend services, Windows applications, website development, and game development. 
>
> The primary reason I was interested in taking a deep dive into C# was for game development.
> As a hobbyist game developer that uses the Unity game development engine, which uses C# for
> scripting, I wanted to better understand the tools at my disposal and step up my game to 
> build better experiences (until now I only had peripheral experience with the language 
> because you can accomplish a lot with the GUI, but you can better customize an experience
> by writing more of your own code). 
>
> Additionally, I was interested in exploring C# for pratical reasons. To elaborate, I've heard 
> C# being called Java's sister language and I thought it would be useful to learn a language 
> that was similair to Java because it is the language I am most familair with, and I would
> like to get comfortable with as many languages as possible before schools over (to pad my
> resume with a larger skill set). 
> </pre>

### Explain how your programming language handles...
> <pre>
> Because this project has us exploring a languge that is an alternative to Java, and because
> Java is my primary frame of reference when it comes to programming, I will explore the 
> following concepts as they relate to Java (similarities/differences). 
> </pre>
> <pre>
> -  object-oriented programming
> </pre>
> <pre>
> -  file ingestion
> </pre>
> <pre>
> -  conditional statements
> </pre>
> <pre>
> -  assignment statements
> </pre>
> <pre>
> -  loops
> </pre>
> <pre>
> -  subprograms
> </pre>
> <pre>
> -  unit testing
> </pre>
> <pre>
> -  exception handling
> </pre>

### List 3 libraries you used (if applicable) and explain them. 
> <pre>
> For my project all of the libraries used were part of the System namespace.
> </pre>
> <pre>
> System.Diagnostics
>
> This provides classes that allow you to interact with system processes, event logs, and
> performance counters. It also provides classes that allow you to debug you application and
> trace the execution of your code. I used this library for the latter reason so I could write
> unit tests using the Debug.Assert method. I used them to test the functionality of my analytic
> methods that calulate the mean, median, and mode.
> </pre>
> <pre>
> System.Text.RegularExpressions
>
> This contains classes that represent ASCII and Unicode character encodings. It provides regular
> expression functionality that may be used from any platform or language that runs within the .NET
> framework. I used this library for regular expressions by using the Regex class and the Replace 
> and Match methods. I used these to aid in cleaning the data that was parsed from the csv file. 
> </pre>
> <pre>
> System.Collections
>
> This contains interfaces and classes that define various collections of objects, such as lists,
> queues, bit arrays, hash tables, and dictionaries. I used this library for the Hashtable class,
> which represents a collection of key/value pairs that are organized based on the hash code of 
> the key. I used this to create the column oriented cell table because the values are allowed to
> have different data types.
> </pre>

### Answer the following questions...
> <pre>
> 1. What company has the highest average weight of the phone body?
> 
>    Ulefone (with a average weight of 238.31)
> </pre>
> ![Report_Q1](https://github.com/twopercentjazz/CSB310_AltLangProj/assets/49768882/d8ebc3f1-bb4c-4939-b3a1-0b8267897da1)
> <pre>
> 2. Was there any phones that were announced in one year and released in another? 
>
>   Motorola (One Hyper & Razr 2019) and Xiaomi (Redmi K30 5G & Mi Mix Alpha)
>   - Each announced in 2019 and released in 2020
> </pre>
> ![Report_Q2](https://github.com/twopercentjazz/CSB310_AltLangProj/assets/49768882/c501d777-5c9a-4117-93ce-acb63fefd3e3)
> <pre>
> 3. How many phones have only one feature sensor?
>
>    432 (have only one feature sensor)
> </pre>
> ![Report_Q3](https://github.com/twopercentjazz/CSB310_AltLangProj/assets/49768882/7959a91f-68fe-45b1-b086-0cfb8c289371)
> <pre>
> 4. What year had the most phones launched in the 2000s?
>
>    2019 (with 299 launches)
> </pre>
> ![Report_Q4](https://github.com/twopercentjazz/CSB310_AltLangProj/assets/49768882/48a1f099-6aa5-4c9b-9d4f-defef13f7a09)



