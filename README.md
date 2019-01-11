# b2e
Encurtador de URL´s - B2e

Instruções do projeto Encurtador de URL´s - B2e

1 - O código fonte pode ser encontrado no diretório on line: https://github.com/Puiatmp/b2e/

2 - Existe um script para criação do banco de dados e tabelas (MySQL) chamado B2e.sql, 
você deve ter o MySQL instalado e rodar esse script para o sistema funcionar. 
Não esqueça de alterar a connection string no arquivo Web.Config dentro do projeto 
com um usuário válido para que o sistema consiga acessar o banco de dados.

3 - O projeto é do tipo WebAPI utilizando o .Net Framework 4.6.1 e para acesso ao MySQL 
utiliza ADO.Net driver for MySQL for .Net Framework and .Net Core versão 8.0.13.0

4 - Foi incluido no projeto um arquivo dotipo colection.json do postman. Com todos os serviços disponíveis 
no projeto assim como os jsons de teste de post, delete e get.

5 - Tomei a liberdade de mudar um pouco as prerrogativas do projeto. Vocês poderão perceber
que o serviço GET /url/:id não faz o redirecionamento, somente traz os dados da URL passada.
Para o redirecionamento criei um serviço chamado "hit" (api/hit/{id}), aonde id = id da url.

6 - Na página principal do site não perdi muito tempo tentando fazer o melhor layout possível.
Existe um botão que ao ser clicado consome o serviço de estatísticas gerais do site e retorna 
o json/xml diretamente no navegador. Mostrando o total de urls, hits e as top10 urls dosistema.

7 - Em ajuda (link na página principal) é possível ter acesso a todos os serviços disponíveis
assim como os parametros de cada um para poder funcionar.

8 - Qualquer dúvida ou problema fico à disposição:

Felipe Appolonio
puiatmp@gmail.com
