const loginForm = document.querySelector('.loginForm');
const errorDisplay = document.querySelector('.error');

loginForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    // Clear previous errors
    errorDisplay.textContent = '';

    // Get values
    const email = document.getElementById('email').value.trim();
    const username = document.getElementById('username').value.trim();
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('password-confirm').value;

    let errors = [];

    // --- Validation Logic ---

    // 1. Check for empty fields
    if (!email || !username || !password || !confirmPassword) {
        errors.push("All fields are required.");
    }

    // 2. Robust Email Validation using Regex
    if (email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email)) {
            errors.push("Please enter a valid email address.");
        }
    }

    // 3. Username Length
    if (username && username.length < 3) {
        errors.push("Username must be at least 3 characters long.");
    }

    // 4. Password Strength (Minimum length)
    if (password && password.length < 6) {
        errors.push("Password must be at least 6 characters long.");
    }

    // 5. Password Match
    if (password !== confirmPassword) {
        errors.push("Passwords do not match.");
    }

    // --- Handling Results ---
    if (errors.length > 0) {
        // Display errors
        errorDisplay.innerHTML = errors.join('<br>');
    } else {
        // --- Form is valid! ---

        // Assemble data into an object
        const accountData = {
            userName: username,
            email: email,
            password: password
        };

        const options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(accountData),
        }

        const response = await fetch("http://localhost:5263/api/auth/register", options)
        console.log(response);
        const data = await response.json();
        console.log(data);
        localStorage.setItem(token, data.token)
        console.log("Account data assembled:", accountData.userName);
    }
});

//TODO
//no spaces in user, only alphanumeric
//capital,number,character in password
//error handling