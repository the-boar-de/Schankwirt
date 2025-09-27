# Dockerfile fuer den Pen_n_Paper Server
#  Multi-stage build fuer C# Discord Bot
# 
# Buidl Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

#Copy project file
COPY Dirscord_Bot.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

#Runtime Stage
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

#Copy built application
COPY --from=build /src/out .

#Run Bot
ENTRYPOINT ["dotnet", "Dirscord_Bot.dll"]
