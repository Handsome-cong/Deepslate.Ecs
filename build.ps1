dotnet restore
$t4Command = "TextTransform"
if (!(Get-Command $t4Command -ErrorAction SilentlyContinue))
{
    $t4Command = "dotnet t4"
    dotnet new tool-manifest
    dotnet tool install --local dotnet-t4
}
Get-ChildItem -Path "." -Filter "*.tt" -File -Recurse | ForEach-Object { Invoke-Expression "$t4Command `"$( $_.FullName )`"" }
dotnet build --no-restore
dotnet test --no-build --verbosity normal