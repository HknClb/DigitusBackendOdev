document.addEventListener("DOMContentLoaded", () => {
    const email = document.querySelector("#ResetPassword_Email");

    document.querySelector("#resend-resetpassword").addEventListener("click", () => {
        let data = {
            Email: email.value
        };
        let url = new URL(window.location.origin + "/api/Auth/SendResetPasswordEmail");
        fetch(url, {
            method: 'PUT',
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'X-ANTI-FORGERY-TOKEN': document.querySelector("input[name='__RequestVerificationToken']")?.value
            },
            body: JSON.stringify(data)
        });
    });
});