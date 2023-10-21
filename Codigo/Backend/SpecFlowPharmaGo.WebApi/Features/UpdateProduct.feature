Feature: UpdateProducts

Narrativa:
Como usuario empleado
Quiero modificar el nombre, descripcion y/o precio
Para mantener los productos actualizados

@mytag
Scenario: Update product name
	Given The id "id" of the product
	And The name "name" of the product
	When try to update a product name
	Then return the updated product

Examples: 
	| id | name     |
	| 2  | product1 |
	| 2  | product2 |