Feature: InsertProduct

Narrativa:
Como empleado de una farmacia y logeado al sistemo
Quiero agregar un producto a mi farmacia
Para que quede disponible para la venta

@mytag
Scenario: Insert new product correctly
	Given the name <name> of the product
	And the description <description> of the product
	And the code <code> of the product
	And the price <price> of the product
	When a user wants to add it to the system
	Then add the product to the user´s pharmacy and return the  product model

Examples: 
	| description        | name     | code  | price |
	| new shaker         | product1 | 59595 | 200   |
	| new excercise ball | product2 | 45612 | 300   |
