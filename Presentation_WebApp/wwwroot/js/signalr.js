var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub") // Ensure this matches the backend SignalR Hub URL
    .build();

// Event handler when a new message is received
connection.on("ReceiveMessage", function (userName, message, datetime) {
    var msg = userName + " (" + datetime + "): " + message;
    var chatBox = document.getElementById("chatBox");
    chatBox.innerHTML += "<div>" + msg + "</div>";
});

// Send message to the server when the button is clicked
document.getElementById("sendButton").addEventListener("click", function () {
    var message = document.getElementById("messageInput").value;
    var userName = "User"; // Replace with real user data (like from session or authentication)

    // Send message to the server
    //connection.invoke("SendMessageToAll", userName, message, new Date())
    console.warn("message", message);
    connection.invoke("SendMessageToAll", message)
        .catch(function (err) {
            return console.error(err.toString());
        });

    document.getElementById("messageInput").value = ''; // Clear the input field
});

// Start the connection
connection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });
