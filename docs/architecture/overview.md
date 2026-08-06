# Visão geral da arquitetura

O FlowCommerce adota uma separação de responsabilidades inspirada em Clean Architecture.

- **Domain** representa o núcleo da aplicação e concentra as regras e os conceitos de negócio. Não depende de nenhum outro projeto da solução.
- **Application** organiza os casos de uso e a coordenação da aplicação. Depende somente de Domain.
- **Infrastructure** abrigará integrações e implementações técnicas. Pode depender de Application e Domain, mas não de Api.
- **Api** é a porta de entrada HTTP e o ponto de composição da aplicação. Depende de Application e Infrastructure.

As dependências apontam para o núcleo: `Api → Infrastructure → Application → Domain`. Referências diretas adicionais são permitidas apenas quando declaradas na solução: Api também referencia Application, e Infrastructure também referencia Domain. Domain permanece independente.
