const campaigns = [
    {
        id:1,
        name: "hello",
        dm: "me",
        role: "DM",
        players: 5,
        description: "Lorem Ipsum"
    },
    {
        id:2,
        name: "hello2",
        dm: "me2",
        role: "Player",
        players: 3,
        description: "Lorem Ipsum Dolor Si Amet"
    }
]

const campaignsHolder = document.querySelector("#campaigns");

function campaignTemplate(campaign) {
    return `
        <a class="campaign-wrapper folded" href="campaign.html?id=${campaign.id}">
            <div class="campaign-head">
                <div>
                    <h2>${campaign.name}</h2>
                    <p class="dm-name">DM: ${campaign.dm}</p>
                </div>
                <p class="role ${campaign.role === "Player"? "player": ""}">${campaign.role}</p>
            </div>
            <div class="players">
                <span class="iconify" data-icon="heroicons:user"></span>
                <p>${campaign.players} Player${campaign.players > 1 ? "s": ""}</p>
                <div class="pageFold"></div>
            </div>
            <p>${campaign.description}</p>
        </a>
    `
}

campaignsHolder.innerHTML = campaigns.map(campaignTemplate).join("");

const navButtons = document.querySelectorAll("nav button");
navButtons.forEach(button => {
    button.addEventListener("click", toggleCampaignView);
})

function toggleCampaignView(e) {
    const value = e.target.id;
    if(value === "current-campaigns") {
        document.querySelector("#current-campaigns").classList.add("selected");
        document.querySelector("#public-campaigns").classList.remove("selected");
        //TODO - set the value of #campaigns to the player campaigns
    } else {
        document.querySelector("#current-campaigns").classList.remove("selected");
        document.querySelector("#public-campaigns").classList.add("selected");
        //TODO - set the value of #campaigns to public campaigns
    }
}