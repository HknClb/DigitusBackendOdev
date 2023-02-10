$(document).ready(() => {
    const connectionBuilder = new signalR.HubConnectionBuilder();
    connectionBuilder.withUrl("https://localhost:7200/website");
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
});