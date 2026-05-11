# InventoryAPI

## Requisitos de instalación
Docker Desktop instalado y corriendo

## Instalación

```bash
# Clonar el repositorio
git clone <url-del-repositorio>
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

[GET] `/api/product/low-stock?threshold=10`  ** Productos con stock bajo**

[POST] `/api/product`  **Crear producto (idempotente)**

[PUT] `/api/product/{id}`  **Actualizar producto**

[DELETE] `/api/product/{id}`  **Eliminar producto (soft delete)**


### Stock Movements

[GET] `/api/stockmovement/product/{productId}` **Historial de movimientos**

[GET] `/api/stockmovement/type/{type}` **Movimientos por tipo**

[POST] `/api/stockmovement` **Registrar movimiento de stock**


