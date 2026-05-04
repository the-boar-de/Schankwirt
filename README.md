## Why this Bot ? 
This bot is a simple side project.
The Idea was to have a self-hosted bot and help my friends creating easily rolls,categories and channels.

Main purpose of this bot is to make the creation of roles, channels and roles easier.


## Docker Image
[DockerImage](https://hub.docker.com/r/theboarde/schankwirt)

Docker Images comes in two versions 

1. stable  - stable version updates only a few times 

2. newest - updates more often but can have broken functionalities



## Libraries
1. [Dotnet](https://github.com/discord-net) Discord Dot-Net Library
2. [MariaDB](https://github.com/MariaDB)    Maria DB Library


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
      MARIADB_PASSWORD : testbot  #!!! Replace with a complex password
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
        MARIADB_PASSWORD : testbot  #!!! Replace with a complex password 
        MARIADB_DATABASE : logs


#volumes
volumes:
  mariadb-volume:
```
