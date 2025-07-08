# 📰 Backend Challenge API

API RESTful desarrollada en **.NET 6** para la gestión de noticias. Implementa arquitectura en capas, paginación, filtros dinámicos, documentación Swagger y despliegue con Docker.

---

## 🚀 Tecnologías utilizadas

- ASP.NET Core 6
- Entity Framework Core
- PostgreSQL
- Docker + Docker Compose
- Swagger / Swashbuckle
- AutoMapper

---

## 📁 Estructura del proyecto

├── Controllers
│ ├── NewsController.cs
│ └── Base/
│ └── PaginateController.cs
├── Data
│ └── AppDbContext.cs
├── Migrations
├── Models
│ └── News.cs
├── Repositories
│ ├── Interfaces/
│ │ └── INewsRepository.cs
│ └── NewsRepository.cs
├── Services
│ ├── Interfaces/
│ │ ├── INewsService.cs
│ │ └── IPaginatedService.cs
│ └── NewsService.cs
├── ViewModels
│ ├── NewsPostViewModel.cs
│ ├── NewsPutViewModel.cs
│ ├── NewsViewModel.cs
│ ├── PaginationViewModel.cs
│ └── Filters/
│ ├── NewsFilterViewModel.cs
│ └── PaginationFilterViewModel.cs
├── appsettings.json
├── Dockerfile
├── docker-compose.yml
└── README.md

yaml
Copiar
Editar

---

## ⚙️ Configuración local

### 1. Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/backend-challenge-api.git
cd backend-challenge-api
2. Variables de entorno
Están configuradas en appsettings.json:

json
Copiar
Editar
"ConnectionStrings": {
  "DefaultConnection": "Host=db;Port=5432;Database=newsdb;Username=postgres;Password=postgres"
}

⚠️ Importante: No uses 127.0.0.1 como host de la DB si usás Docker, porque api debe conectarse al servicio db por nombre.

🐳 Ejecutar con Docker
bash
Copiar
Editar
docker-compose up --build
Esto levanta:

api: servicio backend en puerto 5000

db: contenedor PostgreSQL

Documentación interactiva:
http://localhost:5000/swagger

📬 Endpoints disponibles
Método	Ruta	Descripción
GET	/api/news	Obtener todas las noticias paginadas
GET	/api/news/{id}	Obtener noticia por ID
POST	/api/news	Crear una nueva noticia
PUT	/api/news/{id}	Actualizar una noticia existente
DELETE	/api/news/{id}	Eliminar una noticia
GET	/api/news/search?query=abc	Buscar noticias por texto

🔄 Paginación
Endpoint base
bash
Copiar
Editar
GET /api/news
Parámetros
page → número de página (default: 1)

pageSize → ítems por página (default: 10)

Ejemplo:
http
Copiar
Editar
GET /api/news?page=2&pageSize=5
Respuesta:

json
Copiar
Editar
{
  "currentPage": 2,
  "pageSize": 5,
  "totalPages": 4,
  "totalItems": 20,
  "data": [
    {
      "id": 6,
      "title": "Título de ejemplo",
      "author": "Lucas Tabaré",
      "date": "2025-07-07T00:00:00"
    }
  ]
}

🧪 Migraciones manuales (fuera de Docker)
bash
Copiar
Editar
dotnet ef migrations add InitialCreate
dotnet ef database update
🧹 Comandos útiles
Comando	Descripción
docker-compose up --build	Levanta la app y base de datos
docker-compose down	Detiene los servicios
dotnet ef migrations add ...	Crea una nueva migración
dotnet ef database update	Aplica migraciones a la base de datos

🧑 Autor
Lucas Tabaré