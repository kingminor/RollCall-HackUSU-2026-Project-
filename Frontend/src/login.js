import Iconify from "@iconify/iconify";
import {API_URL} from "./main.js";

document.querySelector('form').addEventListener('submit', async (e) => {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            email,
            password,
        })
    }

    const response = await fetch(API_URL + "/api/auth/login", options);
    const data = await response.json();
    console.log(data);
    localStorage.setItem("token", data.token);

    //window.location.href="/home.html";
})

//TODO - error handling