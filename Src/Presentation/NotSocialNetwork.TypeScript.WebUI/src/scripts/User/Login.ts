export async function Login(email: string) {
    const response = await fetch("https://localhost:5001/api/authentication",{
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            email: email
        })
    });

    if(response.ok === true){
        const loginResponse = await response.json();
        setDataInLocalStorage(loginResponse);

        console.log("Welcome " + email + "!");
        window.location.replace("index.html");
    }else{
        return console.log("Something went wrong...");
    }
}

function setDataInLocalStorage(loginResponse){
    localStorage.setItem("token", loginResponse.token);
    localStorage.setItem("userId", loginResponse.userId);
}