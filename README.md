# InventoryAPI

## Requisitos de instalación
Docker Desktop instalado y corriendo

## Instalación

```bash
# Clonar el repositorio
git clone https://github.com/camjesus/InventoryAPI
cd Inventory
 
# Levantar la aplicación completa
docker compose up --build
```
API Puerto: `8080`

Swagger UI: `http://localhost:8080/swagger`

## Endpoints

### Categories

[GET] `/api/category` **Listar categorías**

[GET] `/api/category/{id}` **Obtener categoría por ID**

[POST] `/api/category` **Crear categoría (idempotente)**

[PUT] `/api/category/{id}` **Actualizar categoría**

[DELETE] `/api/category/{id}` **Eliminar categoría (soft delete)**


### Products

[GET] `/api/product`  **Listar productos**

[GET] `/api/product/{id}`  **Obtener producto por ID**

[GET] `/api/product/sku/{sku}`  **Buscar por SKU**

[GET] `/api/product/category/{categoryId}`  **Productos por categoría**

[POST] `/api/product`  **Crear producto (idempotente)**

[PUT] `/api/product/{id}`  **Actualizar producto**

[DELETE] `/api/product/{id}`  **Eliminar producto (soft delete)**


### Stock Movements

[GET] `/api/stockmovement/product/{productId}` **Historial de movimientos**

[GET] `/api/stockmovement/type/{type}` **Movimientos por tipo**

[POST] `/api/stockmovement` **Registrar movimiento de stock**


## Pruebas por Swagger

### Crear una una categorías

```json
{
"name": "ElectroSmart",
"description": "Smart TV"
}

{
"name": "Perifericos",
"description": ""
}`
```

### Obtener categorías

Response
```json
[
{
"id": "7892caa0-c1b2-47ae-ad7c-7822a56cfa30",
"name": "ElectroSmart",
"description": "Smart TV",
"createdAt": "2026-05-10T18:52:38.8264739",
"updatedAt": "2026-05-10T21:49:18.5181553"
},
{
"id": "3d86932b-e910-4efb-bf89-7837700b3f50",
"name": "Perifericos",
"description": "",
"createdAt": "2026-05-10T22:20:39.6467565",
"updatedAt": null
}
]
```

### Crear productos con las categorias creadas

Json
```json
{
"name": "Teclado",
"description": "Teclado mecanico",
"sku": "0087",
"price": 200000.89,
"stock": 2,
"categoryId": "3d86932b-e910-4efb-bf89-7837700b3f50" //(Id de "Perifericos")
}

{
"name": "Samsung LLP",
"description": "32'",
"sku": "LLP-009",
"price": 30000.9,
"stock": 0,
"categoryId": "7892caa0-c1b2-47ae-ad7c-7822a56cfa30" //(Id de "ElectroSmart")
}
```

### Consultar los productos

Response 
```json
[
  {
    "id": "12a8c486-0611-4afe-b024-27b02076a0de",
    "name": "Teclado",
    "description": "Teclado mecanico",
    "sku": "0087",
    "price": 200000.89,
    "stock": 2,
    "categoryId": "3d86932b-e910-4efb-bf89-7837700b3f50",
    "categoryName": "Perifericos",
    "createdAt": "2026-05-10T22:25:16.1576678",
    "updatedAt": null
  },
  {
    "id": "6a3d3c05-d6ee-47b4-9d1b-38607c598301",
    "name": "Samsung LLP",
    "description": "32'",
    "sku": "LLP-009",
    "price": 30000.9,
    "stock": 0,
    "categoryId": "7892caa0-c1b2-47ae-ad7c-7822a56cfa30",
    "categoryName": "ElectroSmart",
    "createdAt": "2026-05-10T20:07:34.9833049",
    "updatedAt": "2026-05-10T20:36:40.7540933"
  }
]
```

### Actualizar Categoria de ElectroSmart

id: "7892caa0-c1b2-47ae-ad7c-7822a56cfa30"

Json
```json
{
"name": "Electro",
"description": "Smart"
}
```

### Consulto la modificación en la categoría por Id

id: "7892caa0-c1b2-47ae-ad7c-7822a56cfa30"

Response

```json
{
"id": "7892caa0-c1b2-47ae-ad7c-7822a56cfa30",
"name": "ElectroSmart",
"description": "Smart", //nuevo valor
"createdAt": "2026-05-10T18:52:38.8264739",
"updatedAt": "2026-05-11T16:28:53.2437094" //modifica el update
}
```

### Consulto el Producto para ver la modificación de la categoría

id: "6a3d3c05-d6ee-47b4-9d1b-38607c598301"

Response

```json
{
"id": "6a3d3c05-d6ee-47b4-9d1b-38607c598301",
"name": "Samsung LLP",
"description": "32'",
"sku": "LLP-009",
"price": 30000.9,
"stock": 20,
"categoryId": "7892caa0-c1b2-47ae-ad7c-7822a56cfa30",
"categoryName": "Electro",
"createdAt": "2026-05-10T20:07:34.9833049",
"updatedAt": "2026-05-10T20:36:40.7540933"
}
```

### Consulto un producto por sku 

con ambos valores para validar el caseSentitive
valor1 --> Sku: LLp-009 

valor2 --> Sku: LLP-009

Response
```json
{
"id": "6a3d3c05-d6ee-47b4-9d1b-38607c598301",
"name": "Samsung LLP",
"description": "32'",
"sku": "LLP-009",
"price": 30000.9,
"stock": 20,
"categoryId": "7892caa0-c1b2-47ae-ad7c-7822a56cfa30",
"categoryName": "Electro",
"createdAt": "2026-05-10T20:07:34.9833049",
"updatedAt": "2026-05-10T20:36:40.7540933"
}
```

### Cosulto producto por categoría

```json
id: 3d86932b-e910-4efb-bf89-7837700b3f50

Response

[
{
"id": "12a8c486-0611-4afe-b024-27b02076a0de",
"name": "Teclado",
"description": "Teclado mecanico",
"sku": "0087",
"price": 200000.89,
"stock": 2,
"categoryId": "3d86932b-e910-4efb-bf89-7837700b3f50",
"categoryName": "Perifericos",
"createdAt": "2026-05-10T22:25:16.1576678",
"updatedAt": null
}
]
```

### Creo un movimiento de stock

```json
Json

{
"productId": "6a3d3c05-d6ee-47b4-9d1b-38607c598301",
"quantity": 20,
"type": 1, //compra de productos
"reason": "initial stock",
}
```

### Consulto el producto para ver el movimiento
```json
id: "6a3d3c05-d6ee-47b4-9d1b-38607c598301"

Response 

{
"id": "6a3d3c05-d6ee-47b4-9d1b-38607c598301",
"name": "Samsung LLP",
"description": "32'",
"sku": "LLP-009",
"price": 30000.9,
"stock": 20, //se modifica el stock
"categoryId": "7892caa0-c1b2-47ae-ad7c-7822a56cfa30",
"categoryName": "Electro",
"createdAt": "2026-05-10T20:07:34.9833049",
"updatedAt": "2026-05-10T20:36:40.7540933"
}
```

### Consulto el stock de movimiento por producto

id: 6a3d3c05-d6ee-47b4-9d1b-38607c598301

Reponse
```json
[
{
"id": "1a0a1f3c-8346-419a-a1fe-c3f14e306eb7",
"productId": "6a3d3c05-d6ee-47b4-9d1b-38607c598301",
"productName": "Samsung LLP",
"productSku": "LLP-009",
"quantity": 20,
"type": 1,
"reason": "initial stock",
"movedAt": "2026-05-10T20:36:40.7744578"
}
]
```

### Consulto stock de movimiento por tipo

type: 1 //compra

Response

```json
[
{
"id": "1a0a1f3c-8346-419a-a1fe-c3f14e306eb7",
"productId": "6a3d3c05-d6ee-47b4-9d1b-38607c598301",
"productName": "Samsung LLP",
"productSku": "LLP-009",
"quantity": 20,
"type": 1,
"reason": "initial stock",
"movedAt": "2026-05-10T20:36:40.7744578"
}
]
```

### Hago el delete de una categoria

`id: 7892caa0-c1b2-47ae-ad7c-7822a56cfa30`

### Consulto otra vez las categorias

Solo queda uno 

Response 

```json
[
{
"id": "3d86932b-e910-4efb-bf89-7837700b3f50",
"name": "Perifericos",
"description": "",
"createdAt": "2026-05-10T22:20:39.6467565",
"updatedAt": null
}
]`
```

### Valido en la base de datos que siga existindo pero con el isDeleted false
row1 --> `7892caa0-c1b2-47ae-ad7c-7822a56cfa30,Electro,Smart,2026-05-10 18:52:38.8264739,2026-05-11 17:01:18.8192152,true

row2 --> 3d86932b-e910-4efb-bf89-7837700b3f50,Perifericos,"",2026-05-10 22:20:39.6467565,,false //cambió el valor
`

FIN
