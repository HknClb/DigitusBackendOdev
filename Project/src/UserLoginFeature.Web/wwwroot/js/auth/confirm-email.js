const inputElements = [...document.querySelectorAll('input.code-input')]

inputElements.forEach((ele, index) => {
    ele.addEventListener('keydown', (e) => {
        if (e.keyCode === 8 && e.target.value === '') inputElements[Math.max(0, index - 1)].focus()
    })
    ele.addEventListener('input', (e) => {
        const [first, ...rest] = e.target.value
        e.target.value = first ?? ''
        const lastInputBox = index === inputElements.length - 1
        const didInsertContent = first !== undefined
        if (didInsertContent && !lastInputBox) {
            inputElements[index + 1].focus()
            inputElements[index + 1].value = rest.join('')
            inputElements[index + 1].dispatchEvent(new Event('input'))
        }
    })
})

function resendConfirmation(event) {
    let resendCode = event.currentTarget;
    resendCode.setAttribute("disabled", "");
    resendCode.style = "cursor: no-drop;";
    let data = {
        Id: document.querySelector("#user-id").value
    };
    let url = new URL(window.location.origin + "/api/auth/sendemailconfirmation");
    fetch(url, {
        method: 'PUT',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'X-ANTI-FORGERY-TOKEN': document.getElementsByName('__RequestVerificationToken')[0]?.value
        },
        body: JSON.stringify(data)
    }).then(response => {
        if (response.status === 200)
            setTimeout(() => { resendCode.removeAttribute("disabled"); resendCode.style = "cursor: pointer;"; }, 2000);
    }).catch(reason => alert(reason));
}

function onSubmit(e) {
    const code = inputElements.map(({ value }) => value).join('')
    document.querySelector("#verification-code").value = code;
}