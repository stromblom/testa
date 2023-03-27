# TestA - A tool for the Obscure

This is a super nice tool. It can count for you! It might be very specific in how it counts, but it does this in a super nice way :)

## What does it do then?
It takes a file path as its only argument.
It then opens this file, if found, and counts the occurrences of the file name (without extension) in said file.

## How to build
##### Prerequisites
* dotnet 7

#### Build steps
* Build TaskA.ConsoleRunner.
* Run TaskA.ConsoleRunner.exe:
```
TestA.ConsoleRunner.exe <filePath>
```

or:

```
dotnet TestA.ConsoleRunner.dll <filePath>
```

Have fun, and don't stop counting :star: