# CP1 - HandsOn 04 | Agenda com Fuso Horário

## 📚 Disciplina

**C# Software Development**

## 👤 Aluno

**João Victor Soave**
**RM557595**

---

## 📝 Descrição do Projeto

Este projeto consiste em uma aplicação de console desenvolvida em C# para gerenciamento de compromissos com suporte a conversão de fuso horário.

O sistema permite cadastrar compromissos informando descrição, data, hora e timezone de origem, além de visualizar compromissos com conversão automática para o horário brasileiro (São Paulo).

---

## 🎯 Objetivo

Desenvolver uma agenda capaz de:

* Ler uma entrada de agenda com:

  * Descrição
  * Data/Hora
  * Timezone
* Exibir compromissos do dia atual com base no horário brasileiro
* Exibir compromissos de uma data específica com conversão para o horário brasileiro

---

## ⚙️ Funcionalidades

### ➕ Adicionar compromisso

O usuário pode registrar compromissos informando:

* Descrição
* Data e hora
* Fuso horário de origem:

  * Brasil (São Paulo)
  * UTC
  * Nova York

### 📅 Exibir compromissos de hoje

Mostra todos os compromissos do dia atual convertidos para o horário brasileiro.

### 📆 Exibir compromissos por data

Permite consultar compromissos de uma data específica também convertidos para o horário brasileiro.

---

## 🛠️ Tecnologias Utilizadas

* **C#**
* **.NET Console Application**
* **DateTime**
* **DateTimeOffset**
* **TimeZoneInfo**
* **LINQ**

---

## 📂 Estrutura do Projeto

```bash
AgendaFusoHorario/
│-- Program.cs
│-- README.md
```

---

## ▶️ Como Executar

### 1️⃣ Clone o repositório

```bash
git clone <url-do-repositorio>
```

### 2️⃣ Acesse a pasta

```bash
cd csharp-cp1-agenda-fuso-horario
```

### 3️⃣ Execute o projeto

```bash
dotnet run
```

---

## 💻 Exemplo de Uso

```bash
=== AGENDA COM CONVERSOR DE FUSO HORÁRIO ===
1 - Adicionar compromisso
2 - Ver compromissos de hoje
3 - Ver compromissos por data
4 - Sair
```

---

## 🌎 Fusos Horários Suportados

| Opção | Local              |
| ----- | ------------------ |
| 1     | Brasil (São Paulo) |
| 2     | UTC                |
| 3     | Nova York          |

---

## 📌 Conceitos Aplicados

* Estruturas de repetição (`while`)
* Estruturas condicionais (`switch`)
* Classes e Objetos
* Listas (`List<T>`)
* Manipulação de datas
* Conversão de timezone
* Filtragem com LINQ

---

## 🚀 Aprendizados

Durante o desenvolvimento deste projeto, foram praticados conceitos fundamentais de desenvolvimento em C#, incluindo:

* Organização de código
* Programação orientada a objetos
* Manipulação de datas e horários
* Conversão entre fusos horários
* Criação de aplicações interativas em console

---

## 📖 Observação

O sistema utiliza o timezone brasileiro como padrão para exibição:
**E. South America Standard Time (São Paulo)**

---

## 🏫 Instituição

**FIAP - Faculdade de Informática e Administração Paulista**

---

## 📄 Licença

Projeto acadêmico desenvolvido para fins educacionais.

```
```
