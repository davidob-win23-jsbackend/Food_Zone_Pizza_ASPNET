var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

// Get current user from hidden input
let currentUser = document.getElementById("currentUser")?.value || "Anonymous";

// Event handler for received messages
connection.on("ReceiveMessage", function (userName, message, datetime) {
    const chatBox = document.getElementById("chatBox");

    const messageDiv = document.createElement("div");
    messageDiv.classList.add("chat-message");

    const isOwnMessage = userName === "Anonymous";
    messageDiv.classList.add(isOwnMessage ? "chat-right" : "chat-left");

    // Format timestamp (optional polish)
    const time = new Date(datetime).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

    messageDiv.innerHTML = `
        <div class="chat-bubble">
            <strong>${userName}</strong> <span class="chat-time">${time}</span><br/>
            ${message}
        </div>
    `;

    chatBox.appendChild(messageDiv);
    chatBox.scrollTop = chatBox.scrollHeight;
});

// Send message
document.getElementById("sendButton").addEventListener("click", function () {
    var message = document.getElementById("messageInput").value;

    if (!message.trim()) return;

    connection.invoke("SendMessageToAll", message)
        .catch(function (err) {
            return console.error(err.toString());
        });

    document.getElementById("messageInput").value = '';
});

// Start connection
connection.start().catch(function (err) {
    return console.error(err.toString());
});
