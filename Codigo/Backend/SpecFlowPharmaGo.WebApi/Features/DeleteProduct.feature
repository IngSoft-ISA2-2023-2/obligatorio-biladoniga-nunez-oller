Feature: DeleteProduct

Narrativa:
Como empleado de una farmacia
Quiero borrar un producto
Para que no esté disponible para la venta

@mytag
Scenario: Delete an existing product
	Given The id 1 of a product
	When try to delete the product
	Then the product has been successfully  deleted