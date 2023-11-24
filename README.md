### Jogo-de-arremessos-de-pedras-na-agua

Serviço REST para contabilizar jogadas de um campeonato de arremessos de pedras na agua e retornar a classificação geral do campeonato.

### Tecnologias utilizadas
Desenvolvido em c#
Deploy na AWS
Testes Unitários
Arquitetura Limpa
.Net 6
Docker
RabbitMQ


### Executar projeto
Para montar , criar e executar o container da aplicação ouvindo na porta 7080 : 

1. entre na pasta raiz <code>cd ThrowingStonesGame\ </code>
2. execute o comando <code>docker-compose up --build</code>

### Exemplo de json de requisição

```json
{
    "jogadas": [{
            "jogada": "Egio x Jaco",
            "resultado": "4 x 2"
        },
        {
            "jogada": "Egio x Jaco",
            "resultado": "2 x 5"
        },
        {
            "jogada": "Egio x Jaco",
            "resultado": "0 x 3"
        },
        {
            "jogada": "Jaco x Egio",
            "resultado": "2 x 2"
        },
        {
            "jogada": "Jaco x Egio",
            "resultado": "2 x 6"
        },
        {
            "jogada": "Jaco x Egio",
            "resultado": "2 x 2"
        }
    ]
}
```

### Exemplo de json de resposta
