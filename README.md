# GrandeTravel
TAFE Diploma in Software Development Project

Projeto feito durante o Diploma em Software Dev no TAFE

O projeto se trata de um Travel Packages (Pacotes de viagem). Construido com arquitetura MVC, Dependency Injection e database com Entity Framework em C# (Code first), alem do front-end em HTML, CSS, Js, Bootstrap e jQuery.


Existem 3 roles: Admin, Travel Agent e Consumidor (Customer)

-ADMIN: Controla a maioria das funcoes no background, adciona ou descontinua Agentes ou Consumidores.

-AGENTE: Possui as funcoes CRUD (Create, Read, Update, Delete) para manejar os pacotes de viagem. Exemplo: criar o pacote, escolher titulo, descriçao, preço, localizaçao etc. Tem tambem a funcao de descontinuar um pacote (o pacote de viagem nao é deletado do database, apenas "esconde" o pacote para futuros clientes.

-CONSUMIDOR: Filtro e busca (search) por pacotes de viagem, ativa um booking na qual é gerado uma confirmação sobre a data e o preço do pacote levando em conta o numero de pessoas inclusas. 

