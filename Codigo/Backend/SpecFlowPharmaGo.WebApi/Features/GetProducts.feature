Feature: GetProducts

Narrativa:
Como usuario invitado
Quiero obtener los productos disponibles
Para poder ver su código, nombre, descripción y precio

@mytag
Scenario: Get the list of all products
	When perform a simple products request
	Then return a list of products