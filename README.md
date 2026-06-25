# Beck Support Bot

## Beskrivelse

Beck Support Bot er en ASP.NET Core Web API udviklet i C#, som fungerer som en AI-drevet supportassistent. Projektet kombinerer OpenAI's GPT-model med en lokal vidensbase for at levere præcise svar på spørgsmål om BridgeCentral, BridgeMate og BC3-databasen.

I stedet for kun at sende brugerens spørgsmål direkte til en sprogmodel søger applikationen først efter relevante dokumenter i en intern vidensbase. De mest relevante informationer sendes derefter sammen med spørgsmålet til OpenAI, hvilket giver mere præcise og kontekstbaserede svar.

---

## Funktioner

- AI-baseret support via OpenAI GPT
- REST API udviklet med ASP.NET Core
- Automatisk søgning i lokal vidensbase
- Relevansbaseret udvælgelse af dokumenter
- Swagger/OpenAPI til test af API
- Dependency Injection
- Struktureret Service Layer
- Fejlhåndtering og validering

---

## Teknologier

- C#
- .NET 8
- ASP.NET Core Web API
- OpenAI API
- Swagger / OpenAPI
- Dependency Injection
- JSON
- REST API

---

## Projektstruktur

```
BeckSupportBot
│
├── Controllers
│   └── ChatController
│
├── Interfaces
│
├── Services
│   ├── OpenAiService
│   └── KnowledgeService
│
├── Models
│
├── KnowledgeBase
│   ├── BridgeCentral
│   ├── BridgeMate
│   ├── Database
│   └── Skifteplaner
│
├── Program.cs
└── appsettings.json
```

---

## Hvordan fungerer projektet?

1. Brugeren sender et spørgsmål til API'et.
2. KnowledgeService søger i den lokale vidensbase efter relevante dokumenter.
3. De mest relevante dokumenter samles som kontekst.
4. Konteksten sendes sammen med spørgsmålet til OpenAI.
5. GPT genererer et svar baseret på både brugerens spørgsmål og den fundne dokumentation.
6. API'et returnerer svaret som JSON.

---

## API Endpoint

### POST

```
POST /api/chat
```

Eksempel på request:

```json
{
    "question": "Hvordan installerer jeg BridgeCentral?"
}
```

Eksempel på response:

```json
{
    "answer": "...",
    "contextUsed": [
        {
            "title": "...",
            "category": "...",
            "score": 4
        }
    ]
}
```

---

## Installation

Klon projektet

```bash
git clone https://github.com/Dasekan/BeckSupportBot.git
```

Gå til projektmappen

```bash
cd BeckSupportBot
```

Installer dependencies

```bash
dotnet restore
```

Tilføj din OpenAI API-nøgle via User Secrets eller appsettings.

Start projektet

```bash
dotnet run
```

Swagger kan herefter åbnes på

```
https://localhost:xxxx/swagger
```

---

## Mulige forbedringer

- Semantic Search med embeddings
- Vector Database
- Caching af AI-svar
- Unit Tests
- Docker support
- Authentication og API Keys
- Logging og monitorering

---

## Kompetencer demonstreret

Dette projekt demonstrerer erfaring med:

- ASP.NET Core Web API
- Objektorienteret programmering
- Service Layer Architecture
- Dependency Injection
- REST API design
- OpenAI API integration
- Dokumentbaseret informationssøgning
- JSON-kommunikation
- Exception Handling

---

## Udviklet af

**Dasekan**

GitHub:
https://github.com/Dasekan
