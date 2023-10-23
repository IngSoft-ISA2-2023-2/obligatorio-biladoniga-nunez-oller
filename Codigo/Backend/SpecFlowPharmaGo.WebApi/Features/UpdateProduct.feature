Feature: UpdateProducts

Narrativa:
Como usuario empleado
Quiero modificar el nombre, descripcion y/o precio
Para mantener los productos actualizados

@mytag
Scenario: Update product attribute
	Given The id <id> of the product
	And The "<attribute>" with "<value>" of the product
	When try to update the product
	Then return the updated product

Examples: 
	| id | attribute   | value |
	| 1  | Name        | name1 |
	| 1  | Description | desc1 |
	| 1  | Price       | 2.2   |