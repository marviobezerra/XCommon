
# XCommon

XCommon is a light library, which implement's some design patterns and some util functions in pure C#. It is divide in five projects;

- XCommon
- XCommon.Web
- XCommon.EF
- XCommon.Azure
- XCommon.CodeGenerator

Each project is available on Nuget with the same name.

### XCommon

This is the core base for all other projects. Each topic represent a name space on XCommon.

**Application**

> Classes and features for help the development process.

| Feature          | Description
 ----------------- | -----------------
| Cache | Data cache with Get, Put and Remove methods.
| CommandLine | Command line application. There are features to parse parameters.
| ConsoleX | Helper for console such loading, colors, etc.
|FileStorage| File access with Save, Exists, Load and Delete methods.
|Logger| Application logging, or tracing if you prefer.
|Login| Login control, doesn't controle permissions.
|Mail| Send email.
|Socket| Wrapper for WebSockets protocol. Implemented in XCommon.Web
|IApplicationSettings| Interface which has information for applications. *It isn't a name space.

**Extensions**

> All features are extensions methods.

| Feature          | Description
 ----------------- | -----------------
|Checks| Checkers for Int, Decimal and DateTime. It applies checkers like BigerThan, LessThan and InRange.
|Converters| It can converter a complex object in other. It is done by copying properties. There are more converters like Byte to Stream and others. 
|String| Utils functions for String, like IsEmpty and IsNotEmpty.
|Util| Utils functions for Enum, specially for bitwize enumerators.

**Patterns**

> Some design patterns implemented.

| Feature          | Description
 ----------------- | -----------------
|Ioc| Simple IOC. Not so fancy or complete (complex) like Ninject or Castle Windsor, but it's complete functional and fast.
|Repository| Yes, another repository pattern! The implementation is in XCommon.EF project.
|Specification| Two implementations of Specification Pattern. One for Validation and another for Query.

**UnitTest**

> The proposal is help to write Unit Test.

| Feature          | Description
 ----------------- | -----------------
|SceneryManager| It helps you for create and run a scenery when you are writing unit tests.

**Util**

> Many functions to make our life easier.

| Feature          | Description
 ----------------- | -----------------
|Pair| Like tuple of .Net.
|PairList| It's a Pair, but in list.
|RadomString| Generate a random string.
|RadomNumber| Generate a random number.
|Token| Generates a token (without cryptography).
|And others| Check the wiki page for a complete list.

> **Note:** In this project some features are just interfaces, some of then has an InMemory implementations for unit test. 

> For instance, the interface *IFileStorage* has two implementations. One in *XCommon*, just for unit tests, and another in *XCommon.Azure*, for real Azure blob storage access.
