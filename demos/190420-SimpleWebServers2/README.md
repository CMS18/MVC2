# 190408-Http
Simple Web Server

## Kör server och anropa med en browser

Surfa till http://127.0.0.1:34567/ i en webbläsare och kolla att du ser "Hello World".

## Parsa  path

### GET /home HTTP/1.1
Skriv ut en hemsida med texten "Du är hemma"

Prova att surfa till http://127.0.0.1:34567/home och du ska se texten.

### GET /sayhello HTTP/1.1

Skriv ut en hemsida med texten "Hej!"

Prova att surfa till http://127.0.0.1:34567/sayhello och du ska se texten.

## Tolka en parameter

Gör ett anrop med http://127.0.0.1:34567/sayhello?name=Fredrik

Sidan ska skriva ut "Hej Fredrik!". Om du byter namn i URL ska det namnet visas så skriver du http://127.0.0.1:34567/sayhello?name=Ellen ska sidan säga "Hej Ellen!".

