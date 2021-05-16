cd ../../src
dotnet build LogoFX.Client.Bootstrapping.Adapters.SimpleContainer.sln -c Release
dotnet test LogoFX.Client.Bootstrapping.Adapters.SimpleContainer.sln -c Release
cd ../devops/publish