# Image Video Maker

Aplicação desktop em .NET para gerar vídeos automaticamente a partir de imagens organizadas em pastas.

O objetivo do projeto é facilitar a criação de vídeos com fotos, especialmente para eventos, apresentações, telões e retrospectivas. A aplicação permite selecionar uma ou mais pastas de imagens, embaralhar a ordem das fotos e gerar um vídeo final de forma simples.

## Funcionalidades

* Seleção de uma ou mais pastas de imagens.
* Leitura automática das imagens contidas nas pastas selecionadas.
* Ordenação randômica das imagens dentro de cada pasta.
* Distribuição das imagens entre as pastas selecionadas.
* Geração de vídeo final a partir das imagens.
* Evita repetição de imagens antes que todas tenham sido utilizadas.
* Suporte para criação de vídeo em ambiente Windows.
* Possibilidade de personalização futura de duração, transições, música e resolução.

## Tecnologias utilizadas

* .NET
* C#
* Windows Desktop
* FFmpeg

## Requisitos

Antes de executar o projeto, é necessário ter instalado:

* [.NET SDK](https://dotnet.microsoft.com/download)
* FFmpeg instalado e configurado no Windows

Para verificar se o FFmpeg está instalado corretamente, execute no terminal:

```bash
ffmpeg -version
```

Caso o comando não seja reconhecido, adicione o caminho do FFmpeg nas variáveis de ambiente do Windows.

## Como executar o projeto

Clone o repositório:

```bash
git clone <url-do-repositorio>
```

Acesse a pasta do projeto:

```bash
cd <nome-do-projeto>
```

Restaure as dependências:

```bash
dotnet restore
```

Execute a aplicação:

```bash
dotnet run
```

Ou abra a solução diretamente pelo Visual Studio e execute o projeto principal.

## Como usar

1. Abra a aplicação.
2. Selecione as pastas que contêm as imagens.
3. Configure as opções desejadas para geração do vídeo.
4. Inicie o processamento.
5. Aguarde a criação do vídeo final.
6. O vídeo será salvo no diretório de saída configurado.

## Estrutura esperada das imagens

As imagens podem estar organizadas em múltiplas pastas, por exemplo:

```text
Fotos/
├── Casal/
│   ├── foto-01.jpg
│   ├── foto-02.jpg
│   └── foto-03.jpg
├── Familia/
│   ├── foto-01.jpg
│   ├── foto-02.jpg
│   └── foto-03.jpg
└── Festa/
    ├── foto-01.jpg
    ├── foto-02.jpg
    └── foto-03.jpg
```

A aplicação irá embaralhar as imagens de cada pasta e distribuir as fotos no vídeo final, buscando evitar que imagens de uma mesma pasta fiquem agrupadas demais.

## Formatos suportados

Formatos comuns de imagem suportados:

* `.jpg`
* `.jpeg`
* `.png`
* `.bmp`
* `.webp`

O suporte real pode depender da implementação atual e das configurações do FFmpeg.

## Geração do executável

Para gerar uma versão executável da aplicação:

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

O executável será gerado na pasta:

```text
bin/Release/<versao-dotnet>/win-x64/publish
```

## Ícone da aplicação

O projeto pode utilizar um arquivo `.ico` para personalizar:

* Ícone do executável.
* Ícone da janela principal da aplicação.

Certifique-se de que o arquivo `.ico` esteja incluído no projeto e configurado corretamente no arquivo `.csproj` ou nas propriedades do projeto no Visual Studio.

## Possíveis problemas

### FFmpeg não encontrado

Caso a aplicação não consiga gerar o vídeo, verifique se o FFmpeg está instalado e acessível pelo terminal:

```bash
ffmpeg -version
```

Se necessário, adicione a pasta `bin` do FFmpeg ao `PATH` do Windows.

### Imagens não aparecem no vídeo

Verifique se:

* As imagens estão em formatos suportados.
* As pastas selecionadas possuem arquivos de imagem.
* Os arquivos não estão corrompidos.
* O caminho das imagens não contém caracteres inválidos.

### Vídeo não é gerado

Verifique se:

* O diretório de saída existe.
* A aplicação tem permissão para gravar no local escolhido.
* O FFmpeg está instalado corretamente.
* Não há imagens bloqueadas ou abertas por outro programa.

## Roadmap

Possíveis melhorias futuras:

* Adicionar seleção de música de fundo.
* Configurar duração de cada imagem.
* Adicionar transições entre fotos.
* Permitir escolha de resolução do vídeo.
* Permitir escolha de FPS.
* Criar templates de animação.
* Exibir preview antes da geração final.
* Salvar presets de configuração.
* Gerar vídeos em diferentes formatos.

## Licença

Este projeto foi criado para uso pessoal e pode ser adaptado conforme necessidade.

## Autor

Desenvolvido por Rodrigo Machado.
