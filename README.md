docker run --name postgres -e POSTGRES_PASSWORD=PostgresPassword -e POSTGRES_USER=admin -e POSTGRES_DB=Company -v postgres:/var/lib/postgresql/data -p 5432:5432 -d postgres

docker exec -it postgres psql -U admin Company

dotnet build .\build.proj

F5
