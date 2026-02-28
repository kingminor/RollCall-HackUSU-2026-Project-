// 1. Define the potential tags as an object
const campaignTags = {
    "fantasy": "Fantasy",
    "horror": "Horror",
    "high-magic": "High Magic",
    "low-magic": "Low Magic",
    "political": "Political Intrigue",
    "dungeon-crawl": "Dungeon Crawl"
};

// 2. Initialize the dropdown
const tagSelector = document.getElementById('tagSelector');
const tagsContainer = document.getElementById('selectedTagsContainer');

// Populate dropdown options
for (const [value, label] of Object.entries(campaignTags)) {
    const option = document.createElement('option');
    option.value = value;
    option.text = label;
    tagSelector.add(option);
}

// 3. Handle adding tags
tagSelector.addEventListener('change', (event) => {
    const selectedValue = event.target.value;
    const selectedLabel = campaignTags[selectedValue];

    if (selectedValue && !document.getElementById(`tag-${selectedValue}`)) {
        // Create tag element
        const tagSpan = document.createElement('p');
        tagSpan.id = `tag-${selectedValue}`;
        tagSpan.textContent = selectedLabel;

        // 4. Handle removing tags on click
        tagSpan.addEventListener('click', () => {
            tagSpan.remove();
        });

        tagsContainer.appendChild(tagSpan);
    }

    // Reset dropdown
    tagSelector.value = "";
});

const createForm = document.querySelector('#create form');
const errorDisplay = document.querySelector('#create .error'); // Select the error paragraph

createForm.addEventListener('submit', (event) => {
    // Prevent the page from reloading
    event.preventDefault();

    // Clear previous errors
    errorDisplay.textContent = '';

    // 1. Gather input values
    const nameInput = document.getElementById('campaignName');
    const name = nameInput.value.trim(); // Trim whitespace
    const description = document.getElementById('description').value;

    // 2. Error Handling: Check if name is empty
    if (!name) {
        errorDisplay.textContent = 'Error: A campaign name is required.';
        nameInput.focus(); // Put focus back on the input
        return; // Stop function execution
    }

    // 3. Gather selected tags from the container
    const selectedTags = [];
    const tagElements = tagsContainer.querySelectorAll('span');
    tagElements.forEach(tag => {
        // We get the ID, remove the 'tag-' prefix to get the original value
        selectedTags.push(tag.id.replace('tag-', ''));
    });

    // 4. Create the campaign object
    const campaignData = {
        name: name,
        description: description,
        tags: selectedTags
    };

    // For now, just log it to the console to verify
    console.log('Campaign Data:', campaignData);
});