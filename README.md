# File Manager API 
File Manager API — это серверное приложение, разработанное на ASP.NET Core, предназначенное для управления файлами и папками пользователей. API предоставляет возможности загрузки, скачивания, удаления файлов, а также работы с директориями.

Проект предназначен для использования в системах хранения и управления файлами, аутентификации пользователей и организации удобного доступа к данным.

---

## Использованные технологии

- C#
- .NET Core 8.0
- Entity Framework Core
- JWT аутентификация
- Swagger
- Serilog
- AutoMapper

---

## Основные возможности API

**Аутентификация**
- POST /Auth/login — Вход пользователя
- POST /Auth/register — Регистрация пользователя
  
**Управление папками**
- GET /FileManager/getFolder — Получение списка папок
- POST /FileManager/addFolder — Добавление новой папки
- DELETE /FileManager/deleteFolder — Удаление папки

**Управление файлами**
- GET /FileManager/getFile — Получение списка файлов
- POST /FileManager/addFile — Добавление нового файла
- DELETE /FileManager/deleteFile — Удаление файла

В API реализована аутентификация и авторизация пользователей с использованием JWT-токена. Доступ к функциям возможен только при наличии корректного токена.

---

## Установка и запуск
1. Клонирование репозитория: git clone https://github.com/PetrOrmanji/FileManager.git
2. Указание параметров подключения к базе данных в appsettings.json
3. Указание JWT Token параметров в appsettings.json
4. Применение миграции к БД.
5. Запуск приложения
