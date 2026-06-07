const chatForm = document.getElementById("chatForm");
const questionInput = document.getElementById("questionInput");
const chatBox = document.getElementById("chatBox");

loadChatHistory();

chatForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    const question = questionInput.value.trim();

    if (!question) {
        addMessage("System", "Du skal skrive et spørgsmål.", "error-message");
        return;
    }

    if (question.length < 3) {
        addMessage("System", "Spørgsmålet skal være mindst 3 tegn langt.", "error-message");
        return;
    }

    if (question.length > 500) {
        addMessage("System", "Spørgsmålet må højst være 500 tegn langt.", "error-message");
        return;
    }

    addMessage("Dig", question, "user-message");
    questionInput.value = "";

    addTypingIndicator();

    try {
        const response = await fetch("/api/Chat", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                question: question
            })
        });

        removeLoadingMessage();

        const data = await response.json();

        if (!response.ok) {
            addMessage("System", data.message || "Der skete en fejl.", "error-message");
            return;
        }

        addMessage("Beck IT supportbot", data.answer, "bot-message");

        /*if (data.contextUsed && data.contextUsed.length > 0) {
            addContext([data.contextUsed[0]]); // kun top 1
        } else {
            addNoContextMessage();
        }*/
    } catch (error) {
        removeLoadingMessage();
        addMessage("System", "Kunne ikke kontakte serveren. Tjek at backend kører.", "error-message");
    }
});

function addMessage(sender, text, className, id = null, save = true) {
    const message = document.createElement("div");
    message.classList.add("message", className);

    if (id) {
        message.id = id;
    }

    const header = document.createElement("div");
    header.classList.add("message-header");
    header.textContent = sender;

    const body = document.createElement("div");
    body.innerHTML = text.replace(/\n/g, "<br>");

    const time = document.createElement("div");
    time.classList.add("message-time");
    time.textContent = getCurrentTime();

    message.appendChild(header);
    message.appendChild(body);
    message.appendChild(time);

    chatBox.appendChild(message);
    chatBox.scrollTop = chatBox.scrollHeight;

    if (save && id !== "loading") {
        saveMessage(sender, text, className);
    }
}

function addTypingIndicator() {
    const message = document.createElement("div");
    message.classList.add("message", "bot-message");
    message.id = "loading";

    const header = document.createElement("div");
    header.classList.add("message-header");
    header.textContent = "Beck IT supportbot";

    const dots = document.createElement("div");
    dots.classList.add("typing-dots");

    dots.innerHTML = `
        <span></span>
        <span></span>
        <span></span>
    `;

    message.appendChild(header);
    message.appendChild(dots);

    chatBox.appendChild(message);
    chatBox.scrollTop = chatBox.scrollHeight;
}

function removeLoadingMessage() {
    const loadingMessage = document.getElementById("loading");

    if (loadingMessage) {
        loadingMessage.remove();
    }
}

function getCurrentTime() {
    const now = new Date();

    return now.toLocaleTimeString("da-DK", {
        hour: "2-digit",
        minute: "2-digit"
    });
}

function addContext(contextItems) {
    const contextDiv = document.createElement("div");
    contextDiv.classList.add("message", "context-message");

    const header = document.createElement("div");
    header.classList.add("message-header");
    header.textContent = "Kilde brugt";

    const body = document.createElement("div");

    contextItems.forEach(item => {
        const itemDiv = document.createElement("div");
        itemDiv.classList.add("context-item");

        const readableTitle = cleanContextTitle(item.title);

        itemDiv.innerHTML = `
            ${readableTitle}<br>
            <strong>Kategori:</strong> ${item.category || "Ikke angivet"}<br>
            <strong>Relevansscore:</strong> ${item.score}
        `;

        body.appendChild(itemDiv);
    });

    const time = document.createElement("div");
    time.classList.add("message-time");
    time.textContent = getCurrentTime();

    contextDiv.appendChild(header);
    contextDiv.appendChild(body);
    contextDiv.appendChild(time);

    chatBox.appendChild(contextDiv);
    chatBox.scrollTop = chatBox.scrollHeight;
}

function saveMessage(sender, text, className) {
    const history = JSON.parse(localStorage.getItem("chatHistory")) || [];

    history.push({
        sender: sender,
        text: text,
        className: className,
        time: getCurrentTime()
    });

    localStorage.setItem("chatHistory", JSON.stringify(history));
}

function loadChatHistory() {
    const history = JSON.parse(localStorage.getItem("chatHistory")) || [];

    history.forEach(message => {
        addMessage(message.sender, message.text, message.className, null, false);
    });
}

function clearChatHistory() {
    localStorage.removeItem("chatHistory");

    chatBox.innerHTML = `
        <div class="message bot-message">
            <div class="message-header">Beck IT Supportbot</div>
            <div>Hej. Jeg er Beck IT supportbotten. Hvad kan jeg hjælpe med?</div>
        </div>
    `;
}

function addNoContextMessage() {
    const contextDiv = document.createElement("div");
    contextDiv.classList.add("message", "context-message");

    const header = document.createElement("div");
    header.classList.add("message-header");
    header.textContent = "Kontekst";

    const body = document.createElement("div");
    body.textContent = "Ingen relevant kontekst fundet";

    const time = document.createElement("div");
    time.classList.add("message-time");
    time.textContent = getCurrentTime();

    contextDiv.appendChild(header);
    contextDiv.appendChild(body);
    contextDiv.appendChild(time);

    chatBox.appendChild(contextDiv);
    chatBox.scrollTop = chatBox.scrollHeight;
}

function cleanContextTitle(title) {
    if (!title) {
        return "<strong>Kilde:</strong> Ukendt";
    }

    let cleaned = title
        .replace("bc3fdb25 – Supplement:", "")
        .replace("BC3 / bc3fdb25 – samlet masterfil til CustomGPT -", "")
        .replace("BC3 / bc3fdb25 – samlet masterfil til CustomGPT", "")
        .trim();

    const parts = cleaned.split(" - ");

    if (parts.length >= 2) {
        return `
            <strong>Kilde:</strong> ${parts[0].trim()}<br>
            <strong>Sektion:</strong> ${parts.slice(1).join(" - ").trim()}
        `;
    }

    return `<strong>Kilde:</strong> ${cleaned}`;
}