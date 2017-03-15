#XCommon

XCommon is a light library, which implement's some design patterns and some util functions in pure C#. It is divide in five projects;


|Project | Build   | Version
|:-------| ------- | -------
| XCommon | [![Build status](https://ci.appveyor.com/api/projects/status/vu4oi81o5oqcltjh?svg=true)](https://ci.appveyor.com/project/marviobezerra/xcommon) |  [![NuGet version](https://badge.fury.io/nu/xcommon.svg)](https://badge.fury.io/nu/xcommon)
| XCommon.Azure | [![Build status](https://ci.appveyor.com/api/projects/status/vu4oi81o5oqcltjh?svg=true)](https://ci.appveyor.com/project/marviobezerra/xcommon) |  [![NuGet version](https://badge.fury.io/nu/xcommon.azure.svg)](https://badge.fury.io/nu/xcommon.azure)
| XCommon.Web | [![Build status](https://ci.appveyor.com/api/projects/status/vu4oi81o5oqcltjh?svg=true)](https://ci.appveyor.com/project/marviobezerra/xcommon) | [![NuGet version](https://badge.fury.io/nu/xcommon.web.svg)](https://badge.fury.io/nu/xcommon.web)
| XCommon.EF | [![Build status](https://ci.appveyor.com/api/projects/status/vu4oi81o5oqcltjh?svg=true)](https://ci.appveyor.com/project/marviobezerra/xcommon) | [![NuGet version](https://badge.fury.io/nu/xcommon.ef.svg)](https://badge.fury.io/nu/xcommon.ef)
| XCommon.CodeGenerator | [![Build status](https://ci.appveyor.com/api/projects/status/vu4oi81o5oqcltjh?svg=true)](https://ci.appveyor.com/project/marviobezerra/xcommon) | [![NuGet version](https://badge.fury.io/nu/xcommon.codegenerator.svg)](https://badge.fury.io/nu/xcommon.codegenerator)

##XCommon

This is the core base for all other projects. Each topic represent a name space on XCommon.

###**Application**

> Classes and features for help the development process.

| Feature          | Description
 ----------------- | -----------------
|[ApplicationSettings](https://github.com/marviobezerra/XCommon/wiki/XCommon---ApplicationSettings)| Interface which has information for applications. *It isn't a name space.
| [Cache](https://github.com/marviobezerra/XCommon/wiki/XCommon---Cache)| Data cache with Get, Put and Remove methods.
| [Execute](https://github.com/marviobezerra/XCommon/wiki/XComon---Execute) | Control and store messages from executions.
|[FileStorage](https://github.com/marviobezerra/XCommon/wiki/XCommon---FileStorage)| File access with Save, Exists, Load and Delete methods.
|[Logger](https://github.com/marviobezerra/XCommon/wiki/XCommon---Logger)| Application logging, or tracing if you prefer.
|[Login](https://github.com/marviobezerra/XCommon/wiki/XCommon---Login)| Login control, doesn't controle permissions.
|[Mail](https://github.com/marviobezerra/XCommon/wiki/XCommon---Mail)| Send email.
|[Socket](https://github.com/marviobezerra/XCommon/wiki/XCommon---Socket)| Wrapper for WebSockets protocol. Implemented in XCommon.Web

###**Extensions**

> All features are extensions methods.

| Feature          | Description
 ----------------- | -----------------
|[Checks](https://github.com/marviobezerra/XCommon/wiki/XCommon---Extensions#checks)| Checkers for Int, Decimal and DateTime. It applies checkers like BigerThan, LessThan and InRange.
|[Converters](https://github.com/marviobezerra/XCommon/wiki/XCommon---Extensions#converters)| It can converter a complex object in other. It is done by copying properties. There are more converters like Byte to Stream and others. 
|[String](https://github.com/marviobezerra/XCommon/wiki/XCommon---Extensions#string)| Utils functions for String, like IsEmpty and IsNotEmpty.
|[Util](https://github.com/marviobezerra/XCommon/wiki/XCommon---Extensions#util)| Utils functions for Enum, specially for bitwize enumerators.

###**Patterns**

> Some design patterns implemented.

| Feature          | Description
 ----------------- | -----------------
|[Ioc](https://github.com/marviobezerra/XCommon/wiki/XCommon---Ioc)| Simple IOC. Not so fancy or complete (complex) like Ninject or Castle Windsor, but it's complete functional and fast.
|[Repository](https://github.com/marviobezerra/XCommon/wiki/XCommon---Repository)| Yes, another repository pattern! The implementation is in XCommon.EF project.
|[Specification](https://github.com/marviobezerra/XCommon/wiki/XComonn---Specification)| Two implementations of Specification Pattern. One for [Validation](https://github.com/marviobezerra/XCommon/wiki/XComonn---Specification-Validation) and another for [Query](https://github.com/marviobezerra/XCommon/wiki/XComon---Specification-Query).

###**UnitTest**

> The proposal is help to write Unit Test.

| Feature          | Description
 ----------------- | -----------------
|SceneryManager| It helps you for create and run a scenery when you are writing unit tests.

###**Util**

> Many functions to make our life easier.

| Feature          | Description
 ----------------- | -----------------
|[Pair](https://github.com/marviobezerra/XCommon/wiki/XCommon---Utils)| Like tuple of .Net
|[PairList](https://github.com/marviobezerra/XCommon/wiki/XCommon---Utils)| It's a Pair, but in list.
|[RadomString](https://github.com/marviobezerra/XCommon/wiki/XCommon---Utils)| Generate a random string.
|[RadomNumber](https://github.com/marviobezerra/XCommon/wiki/XCommon---Utils)| Generate a random number.
|[Token](https://github.com/marviobezerra/XCommon/wiki/XCommon---Utils)| Generates a token (without cryptography).
|[And others](https://github.com/marviobezerra/XCommon/wiki/XCommon---Utils)| Check the wiki page for a complete list.

> **Note:** There are features in this project that are just interfaces, some of then has an InMemory implementations for unit test. 

> For instance, the interface *IFileStorage* has two implementations. One in *XCommon*, just for unit tests, and another in *XCommon.Azure*, for real Azure blob storage access.