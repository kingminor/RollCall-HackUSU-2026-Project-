const form = document.querySelector('.character-form');
const errorDisplay = document.querySelector('.error');

form.addEventListener('submit', (e) => {
    e.preventDefault();

    errorDisplay.textContent = '';

    // Perform validation and get data
    const validationResult = validateAndGatherData();

    if (validationResult.errors.length > 0) {
        // Display errors
        errorDisplay.textContent = validationResult.errors.join(' ');
        errorDisplay.style.color = 'red';
    } else {
        // Form is valid, data is in validationResult.data
        console.log('Form data assembled:', validationResult.data);
        // Do something with validationResult.data here
    }
});

function validateAndGatherData() {
    let errorMessages = [];
    let formData = {};

    // Fields to check and collect
    const fieldsToProcess = [
        { id: 'char-name', label: 'Character Name' },
        { id: 'level', label: 'Level' },
        { id: 'race', label: 'Race' },
        { id: 'class', label: 'Class' },
        { id: 'background', label: 'Background' },
        { id: 'alignment', label: 'Alignment' },
        { id: 'backstory', label: 'Backstory' } // Added backstory
    ];

    // Process text/required fields
    fieldsToProcess.forEach(field => {
        const input = document.getElementById(field.id);
        const value = input.value.trim();

        if (!value) {
            errorMessages.push(`${field.label} is required.`);
        }

        formData[field.id] = value; // Add to data object
    });

    // Process ability scores
    const abilityScores = ['str', 'dex', 'con', 'int', 'wis', 'cha'];
    abilityScores.forEach(scoreId => {
        const input = document.getElementById(scoreId);
        const val = parseInt(input.value);

        // Ability scores are usually not required, but if filled, must be valid
        if (input.value && (isNaN(val) || val < 0)) {
            errorMessages.push(`${scoreId.toUpperCase()} must be a positive number.`);
        }

        formData[scoreId] = input.value; // Add to data object
    });

    return {
        errors: errorMessages,
        data: formData
    };
}