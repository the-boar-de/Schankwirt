## Why this Bot ? 
This bot is a simple side project.
The Idea was to have a self-hosted bot and help my friends creating easily rolls,categories and channels.


## Docker Image
[DockerImage](https://hub.docker.com/r/theboarde/schankwirt)


## Libraries
1. [Dotnet](https://github.com/discord-net)
2. [MariaDB](https://github.com/MariaDB)


## Installation
```
#docker compose for discord bot
  #Maria DB
services:
  mariadb:
    image: mariadb:latest
    container_name: mariadb
    restart: always
    environment:
      MARIADB_ROOT_PASSWORD : example
      MARIADB_PASSWORD : testbot
      MARIADB_USER : testbot
      MARIADB_DATABASE : logs
    volumes: 
      - mariadb-volume:/var/lib/mysql

#Schankwirt
  schankwirt:
    image: theboarde/schankwirt:stable
    container_name: discord-bot
    restart: always
    depends_on:
      - mariadb

    environment:
        DISCORD_TOKEN : DISCORD_BOT_TOKEN #replace 'DISCORD_BOT_TOKEN' with the real token
        MARIADB_HOST : mariadb
        MARIADB_PORT : 3306
        MARIADB_USER : testbot
        MARIADB_PASSWORD : testbot
        MARIADB_DATABASE : logs


#volumes
volumes:
  mariadb-volume:
```
