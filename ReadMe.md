# NeoGemino
Bootsrap project for generating output from data + template using .NET Core.

## How to use

1. Add data files in `/Data`.
1. Add templates in `/Templates`.
1. Associate data and templates in `Program.cs`.

```shell
dotnet restore
dotnet run
```

## Notes

Use `Utilities.TrimLineStart(...)` on the final output to remove leading whitespace.