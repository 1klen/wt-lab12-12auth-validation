dotnet run - запуск приложения
brew services start postgresql@18 - запуск PostgreSQL
psql -h localhost -U postgres -d wt_lab8 - проверка подключения к базе
dotnet add package Microsoft.EntityFrameworkCore.Design --version "10.*" - установка пакета

https://wt-lab8-8razor-postgresql-leshkevich.onrender.com/Cars- ссылка деплоя
localhost:10000/Cars - запуск в браузере
localhost:10000/Login - страничка аутентификации


INSERT INTO "Users" ("Login", "Password") VALUES ('student', '1234'); - создание юзера через БД