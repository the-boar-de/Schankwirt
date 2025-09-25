# Dockerfile fuer den Pen_n_Paper Server
#  Multi-stage build fuer C# Discord Bot
# 
# Buidl Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

#Copy project file
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

#Runtime Stage
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

#Copy built application
COPY --from=build /app/out .

#Run Bot
CMD ["dotnet", "Discord_Bot.dll"]
