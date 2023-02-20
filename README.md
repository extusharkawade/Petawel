# Petawel
Backend api's for the Pet Ecom

# 📑Instructions
- in appsetting.json file add server name as your mysql server name.
- Create database with name 'petawel'.
- Following table queries are needed to create tables.

"'create table users(userId int identity(1,1) primary key, "name" varchar(50), "contact" varchar(10), "email" varchar(50),"password" varchar(16));"

"'create table main_category(category_id int identity(1,1) primary key, category_name varchar(50), image_path varchar(225));"

"'create table category(category_id int identity(1,1) primary key, category_name varchar(50), image_path varchar(225), main_id int foreign key references       main_category(category_id) );"

"'create table products(prod_id int identity(1,1)primary key ,prod_name varchar(50),price float,prod_details varchar(128),available_quantity int,image_path varchar(50), category_id int foreign key references category(category_id));"

# ✅Features
- https://localhost:7028/api/Authorization/Authorize this api will generate jwt token if correct credentials are passed.
- https://localhost:7028/api/Product/Product pass the product id as parameter and this api will return detail of perticular product.
- https://localhost:7028/api/Product/getAllitems this will return all products of db.
- https://localhost:7028/api/Product/Product_Update by passing product id this api can update the product details.
- https://localhost:7028/api/Product/SaveProduct This will add new product in db.
- https://localhost:7028/api/Product/Registration for new users registration.


_please note Authorization is currently disabled, to enable just remove the comment of [Authorize] anotation in controller_
_if you enable Authorization please send authentication token in postman header_

# API ENDPOINTS        
### Petawel Users

#### Authentication Endpoints:
- 1: POST /api/Authorization/Authorize.

#### User Endpoints:
- 2: POST /api/Authorization/Registration.

#### Product Endpoints:
- 1: GET /api/Product/Product.
- 2: GET /api/Product/getAllitems.
- 3: GET /api/Product/ProductsByCategory.

#### Category endpoints:
- 1: GET /api/Category/AllCategories

### Petawel Admin
#### Authentication Endpoints:
- 1: POST /api/Authorization/Authorize

#### Category endpoints
- 1:GET /AllCategory
- 2:GET /Category
- 3:POST /AddCategory

#### Product endpoints:
- 1: GET /api/Product/Product
- 2: GET /api/Product/getAllitems
- 3: POST /api/Product/Product_Update
- 4: POST /api/Product/SaveProduct
