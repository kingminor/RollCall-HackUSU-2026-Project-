import {API_URL} from "./main.js";
import { jwtDecode } from "jwt-decode"

let campaigns = [
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
        description: "Lorem Ipsum dolor"
    }
]

const username = jwtDecode(localStorage.getItem("token"))["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
console.log(username);

const campaignsHolder = document.querySelector("#campaigns");

function campaignTemplate(campaign) {
    return `
        <a class="campaign-wrapper folded" href="campaign.html?id=${campaign.id}">
            <div class="campaign-head">
                <div>
                    <h2>${campaign.name}</h2>
                    <p class="dm-name">DM: ${campaign.dm.userName}</p>
                </div>
                <p class="role ${campaign.dm.userName === username? "DM": "Player"}">${campaign.dm.userName === username? "DM" : "Player"}</p>
                
            </div>
            <div class="players">
                <span class="iconify" data-icon="heroicons:user"></span>
                <p>${campaign.campaignMemberships.length} Player${campaign.players !== 1 ? "s": ""}</p>
                <div class="pageFold"></div>
            </div>
            <p>${campaign.description}</p>
        </a>
    `
}


function renderCampaign(campaigns) {
    campaignsHolder.innerHTML = campaigns.map(campaignTemplate).join("");
}

const navButtons = document.querySelectorAll("nav button");
navButtons.forEach(button => {
    button.addEventListener("click", toggleCampaignView);
})

function toggleCampaignView(e) {
    const value = e.target.id;
    if(value === "current-campaigns") {
        document.querySelector("#current-campaigns").classList.add("selected");
        document.querySelector("#public-campaigns").classList.remove("selected");
        setCampaigns("user")
        //TODO - set the value of #campaigns to the player campaigns
    } else {
        document.querySelector("#current-campaigns").classList.remove("selected");
        document.querySelector("#public-campaigns").classList.add("selected");
        setCampaigns("public")
        //TODO - set the value of #campaigns to public campaigns
    }
}

async function setCampaigns(type) {
    const token = localStorage.getItem("token");
    const options = {
        method: "GET",
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        }
    }
    if (type === "user") {
        const response = await fetch(API_URL + "/api/campaign/getMyCampaigns", options)
        console.log(response)
        campaigns = await response.json();
        console.log(campaigns);
    } else {
        const response = await fetch(API_URL + "/api/campaign/getPublicCampaigns", options)
        console.log(response)
        if(response.ok) {
            campaigns = await response.json();
            console.log(campaigns);
        } else {
            campaigns = []
        }
    }
    renderCampaign(campaigns);
}

async function init() {
    setCampaigns("user");
}

document.getElementById("logout").addEventListener("click", () => {
    localStorage.removeItem("token");
    window.location.href="/index.html";
})

init();