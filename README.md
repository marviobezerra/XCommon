
# XCommon

XCommon is a light library, which implement's some design patterns and some util functions in pure C#. It is divide in five projects;

- XCommon
- XCommon.Web
- XCommon.EF
- XCommon.Azure
- XCommon.CodeGenerator

Each project is available on Nuget with the same name.

### XCommon

This is the core base for all other projects. 

> **Note:** In this project some features are just interfaces, some of then has an InMemory implementations for unit test. 

> For instance, the interface *IFileStorage* has two implementations. One in *XCommon*, just for unit tests, and another in *XCommon.Azure*, for real Azure blob storage access.

**Application**

| Feature          | Description                                   |
 ----------------- | ---------------------------------------------------------------------
| Cache | Data cache with Get, Put and Remove methods.
| CommandLine | Command line application. There are features to parse parameters.
| ConsoleX | Helper for console such loading, colors, etc.
|FileStorage| File access with Save, Exists, Load and Delete methods.
|Logger| Application logging, or tracing if you prefer.
|Login| Login control, doesn't controle permissions.
|Mail| Send email.
|Socket| Wrapper for WebSockets protocol. Implemented in XCommon.Web  
