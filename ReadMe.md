# NeoGemino
Bootsrap project for generating output from data + template using .NET Core.

## How to use

Add data files in `/Data` and templates in `/Templates`

```shell
dotnet restore
dotnet run
```

## Notes

Use `Utilities.TrimLineStart(...)` on the final output to remove leading whitespace.