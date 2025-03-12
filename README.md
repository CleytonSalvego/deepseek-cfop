
# IA de suporte personalizado

Este projeto objetiva utilizar a inteligência artificial para auxiliar usuários em assuntos treinados préviamente que não existem nos modelos genéricos como: Contabilidade, Fiscal, Advocácia, etc...


## Configurações do Projeto

O projeto está utilizando as configurações descritas abaixo.

- .NET 9
- C#
- Ollama
- Deep Seek
- Chroma Database


## Processos

O processo de execução deste projeto se divide em duas partes Upload do Arquivo e Utilização.

### Upload do Arquivo
O upload do arquivo se refere ao processo de enviar dados personalizados para que sejam utilizados como contexto ao realizar a pergunta a IA.

#### Etapas
    1. Envio do arquivo podendo ser TXT, PDF, Word, Excel, etc...
    2. A aplicação irá realizar a leitura e extração dos dados deste arquivo.
    3. Uma vez obtido os dados realizará a quebra dos dados em dados menores chamado como Chunk.
    4. Em seguida realizará o Embeding de cada chunk e gravará essa informação na base de dados Chroma Database em forma de vetores.


### Utilização
É o processo de obter os dados que foram enviado no upload, obtendo os contextos em que a IA irá se basear para responder a pergunta.

#### Etapas 
    1. Receberá a pergunta do usuário.
    2. Irá buscar na base de dados contextos que sejam similares aos dados da pergunta.
    3. Irá criar o prompt combinando o contexto + pergunta e enviará a IA.
    4. A IA irá processar esses dados e gerar uma resposta baseada no contexto informado, personalizando assim as informações.



#### .NET 
O framework .NET utilizado é a versão .NET 9 com C#.

#### Ollama 
É um framework que disponibiliza os modelos LLM em forma de API, podendo assim fazermos requisições HTTP aos modelos.
- Download: https://ollama.com
- Modelo LLM utilizado deepseek-r1 https://ollama.com/library/deepseek-r1
- Instalação Ollama
    * Para a instalação baixe o instalador e realize a instalçao.
    * Após instalar será possível rodar comando ollama no CMD.
- Instalação Modelo LLM 
    * Abra o CMD e rode o comando "ollama run deepseek-r1" o Ollama irá fazer o downliad, instalação e execução do modelo.
    * Após finalizado será possível acessar uma API no endereço http://localhost:11434
  
#### Chrome Database
É uma base de dados vetorial open source para o armazanamento dos dados embeded com o modelo de LLM.
- Sua utilização é através do docker.
    * Para baixar a imagem, configuração e build do container é necessário ter instalado o docker-compose.
- Configurando o container docker com o Chrome Database
    * O arquivo para configuração do container está no seguinte diretório .\Files\ChromaDatabase\docker-compose.yml
    * Abra o CMD no seguinte diretório .\Files\ChromaDatabase e rode o comando docker-compose up -d --build
    * Isso irá baixar a imagem docker do Chroma Database e realizar o build do container.
    * Uma vez finalizado deve ser possível acesso o swagger da API do Chrome Database em http://localhost:8000/docs 
    * Você pode verificar o status do container no CMD com o comando "docker ps".

#### Treinamento
O arquivo de teste CFOP.txt para treinamento está no diretório .\Files\Train\CFOP.txt.
- Para que a aplicação consiga encontrar esse arquivo é necessário que ele seja copiado para o seguinte diretório .\bin\Debug\net9.0.
- Se o arquivo especificado na aplicação não existir nesse local será apresentado erro.
- Caso queira alterar o conteúdo do arquivo basta editá-lo e copiá-lo para o diretório .\bin\Debug\net9.0.
