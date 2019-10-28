
Pré-requisitos
-Docker Desktop
-Dotnet Core 3.0

Executar o servidor de filas RabbitMQ
docker run -d --hostname rabbitmq --name rabbitmq -p 15672:15672 -p 5672:5672  rabbitmq:3-management