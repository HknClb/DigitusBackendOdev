$(document).ready(() => {
    const onlineUsersCounter = document.querySelector("#online-users-counter");    

    const connectionBuilder = new signalR.HubConnectionBuilder();
    connectionBuilder.withUrl("https://localhost:7200/websitestats");
    connectionBuilder.withAutomaticReconnect();

    const connection = connectionBuilder.build();
    startConnection();

    async function startConnection() {
        try {
            await connection.start();
        } catch {
            setTimeout(() => startConnection(), 2000);
        }
    }

    connection.on("updateOnlineUsersCounter", onlineUsersCount => {
        console.log(onlineUsersCount);
        onlineUsersCounter.textContent = onlineUsersCount;
    });
});