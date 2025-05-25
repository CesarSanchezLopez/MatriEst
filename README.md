
## 📦 Tecnologías

- ⚙️ **Backend**: ASP.NET Core (.NET 7) + Entity Framework Core
- 🌐 **Frontend**: Angular 15+
- 🗃️ **Base de Datos**: SQL Server


## ✅ Requisitos

### Backend (.NET Core API)

- SQL Server instalado o disponible online

### Frontend (Angular)

- Node.js v16+
- Angular CLI:
  npm install -g @angular/cli
  
### Pasos
 
 
### 1. Clona el repositorio

	git clone https://github.com/CesarSanchezLopez/MatriEst.git
	cd ManageVM

### 2.  Configura la base de datos

Configura la cadena de conexión al archivo:

\MatriEst\Backend\MatriEst.Api\appsettings.json

{
  "ConnectionStrings": {
   "DefaultConnection": "Server=.\\SQLEXPRESS;Database=MatriEstDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}

### 3. Ejecuta las migraciones

cd Backend/MatriEst.Api

dotnet ef migrations add InitialCreate
dotnet ef database update


### 4. Corre el Backend

dotnet run --project Backend/MatriEst.Api


### 5.  Configura  el Frontend Angular

Configura el punto de acceso a la api al archivo:
C:Frontend/matri-est-client/src/environments

export const environment = {
    production: false,
    apiUrl: 'https://localhost:7270/api'
  };


### 6. Corre el Frontend Angular

cd Frontend
npm install
ng serve


### 🔐 Estructura


MatriEst/
├── Api/               → Backend ASP.NET Core
│   ├── Controllers/
│   ├── Entities/
│   ├── Services/
│   └── Infrastructure/
└── matriest-frontend/ → Proyecto Angular
    ├── src/app/
    │   ├── components/
    │   ├── services/
    │   └── models/