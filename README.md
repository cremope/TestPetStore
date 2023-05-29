# Introdução
Projeto que executa testes da api PetStore

# Processo de uso do TestPetStore
 1. Clonar o projeto através da [Url](https://github.com/cremope/TestPetStore.git)
 2. Executar os testes

# Tecnologias utilizadas
1. FrameWork Nunit - [Documentação](https://nunit.org/)
2. Biblioteca RestSharp - [Documentação](https://restsharp.dev/)
3. Referencia dll do projeto ExecuteService para executar os endpoints - [Url](https://github.com/cremope/ExecuteService.git)

## Estrutura do projeto
Projeto possui duas pastas principais, sendo:

- Models -> Representa as models das API's separadas por projeto.
- Test -> Representa os testes de cada controller identificada na API.

**Para incluir novos endpoints, seguir o seguinte fluxo:** 
	
 1. Criar a pasta do método - caso já exista - ignorar essa etapa.
 2. Criar a classe do endpoint - caso já exista - ignorar essa etapa.
 3. Inserir novo método de execução do endpoint, utilizando os métodos de parametrização da classe - caso seja uma nova classe - Utilizar a classe TestPetStore.Test.Controllers.Pet.Post.Pet.cs como exemplo;
 4. Inserir nome no método seguindo o padrão do projeto, sendo:
	 - "Retorno_StatusCode_funcionalidade" - Ex: "Retorno_200_AdicionaNovoPet";
