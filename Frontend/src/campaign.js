import {API_URL} from "./main.js";
import {jwtDecode} from "jwt-decode";

const params = new URLSearchParams(document.location.search);
const campaignId = params.get("id");
const token = localStorage.getItem("token");

const username = jwtDecode(localStorage.getItem("token"))["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

const campaignInfo = await getCampaignInfo()

async function getCampaignInfo() {
    console.log(campaignId);
    const options = {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`,
        }
    }

    const response = await fetch(API_URL + `/api/character/memberships/${campaignId}`, options);
    const json = await response.json()
    console.log(json)
    console.log(response)
    return json;
}

function addUserTemplate(user) {
    return `
    <div id="${user.id}" class="addUser">
        <p>${user.playerName}</p>
        <button class="approve"
            onClick="approve('${user.id}')""
        >Approve</button>
        <button class="deny"
            onClick="deny('${user.id}')"
        >Deny</button>
    </div>
    `
}

function characterTemplate(character) {
    return `                <div class="character folded">
                    <div class="pageFold"></div>
                    <div class="character-head">
                        <div class="character-name">
                            <h4>Character Name</h4>
                            <p class="playerName">Player: ${character.playerName}</p>
                            <p class="characterName">Player: ${character.characterName}</p>

                        </div>
                        <p class="level">Level: ${character.level}</p>
                    </div>
                    <div class="character-info">
                        <div class="race">
                            <p>Race</p>
                            <p>${character.race}</p>
                        </div>
                        <div class="class">
                            <p>Class</p>
                            <p>${character.class}</p>
                        </div>
                        <div class="background">
                            <p>Background</p>
                            <p>${character.background}</p>
                        </div>
                        <div class="alignment">
                            <p>Alignment</p>
                            <p>${character.alignment}</p>
                        </div>
                    </div>
                    <div class="ability-scores">
                        <h4>Ability Scores</h4>
                        <div class="score">
                            <p><span class="iconify" data-icon="material-symbols:swords-outline"></span>Str</p>
                            <p>${character.strStat}</p>
                            <p>${Math.floor((character.str - 10) / 2)}</p>
                        </div>
                        <div class="score">
                            <p><span class="iconify" data-icon="material-symbols:electric-bolt-outline-rounded"></span>Dex</p>
                            <p>${character.dexStat}</p>
                            <p>${Math.floor((character.dexStat - 10) / 2)}</p>
                        </div>
                        <div class="score">
                            <p><span class="iconify" data-icon="material-symbols:shield-outline"></span>Con</p>
                            <p>${character.conStat}</p>
                            <p>${Math.floor((character.conStat - 10) / 2)}</p>
                        </div>
                        <div class="score">
                            <p><span class="iconify" data-icon="ic:round-cloud-queue"></span>Int</p>
                            <p>${character.intStat}</p>
                            <p>${Math.floor((character.intStat - 10) / 2)}</p>
                        </div>
                        <div class="score">
                            <p><span class="iconify" data-icon="heroicons:eye"></span>Wis</p>
                            <p>${character.wisStat}</p>
                            <p>${Math.floor((character.wisStat - 10) / 2)}</p>
                        </div>
                        <div class="score">
                            <p><span class="iconify" data-icon="heroicons:chat-bubble-oval-left"></span>Cha</p>
                            <p>${character.chaStat}</p>
                            <p>${Math.floor((character.chaStat - 10) / 2)}</p>
                        </div>
                    </div>
                    <div class="backstory">
                        <h4>Backstory</h4>
                        <p>${character.background}</p>
                    </div>
                </div>`
}

function charForm() {
    return `
            <form class="character-form folded">
            <div class="pageFold"></div>
            <div class="form-header">
                <h2>Character Creation</h2>
                <div id="file-input">
                    <label class="pdfUpload" for="pdf_upload">Import PDF Sheet</label>
                    <input type="file" id="pdf_upload" accept=".pdf"/>
                </div>

            </div>
            <div class="character-info-form">
                <h3>Basic Info</h3>
                <div>
                    <label for="char-name">Character Name <span>*</span></label>
                    <input type="text" id="char-name"/>
                </div>
                <div>
                    <label for="level">Level <span>*</span></label>
                    <input type="text" id="level"/>
                </div>
                <div>
                    <label for="race">Race<span>*</span></label>
                    <input type="text" id="race"/>
                </div>
                <div>
                    <label for="class">Class<span>*</span></label>
                    <input type="text" id="class"/>
                </div>
                <div>
                    <label for="background">Background<span>*</span></label>
                    <input type="text" id="background"/>
                </div>
                <div>
                    <label for="alignment">Alignment<span>*</span></label>
                    <input type="text" id="alignment"/>
                </div>
            </div>
            <div class="char-ablilty-score-form">
                <h3>Ablility Scores</h3>
                <div>
                    <label for="str"><span class="iconify" data-icon="material-symbols:swords-outline"></span>Strength</label>
                    <input type="number" id="str">
                </div>
                <div>
                    <label for="dex"><span class="iconify" data-icon="material-symbols:electric-bolt-outline-rounded"></span>Dexterity</label>
                    <input type="number" id="dex">
                </div>
                <div>
                    <label for="con"><span class="iconify" data-icon="material-symbols:shield-outline"></span>Constitution</label>
                    <input type="number" id="con">
                </div>
                <div>
                    <label for="int"><span class="iconify" data-icon="ic:round-cloud-queue"></span>Intelligence</label>
                    <input type="number" id="int">
                </div>
                <div>
                    <label for="wis"><span class="iconify" data-icon="heroicons:eye"></span>Wisdom</label>
                    <input type="number" id="wis">
                </div>
                <div>
                    <label for="cha"><span class="iconify" data-icon="heroicons:chat-bubble-oval-left"></span>Charisma</label>
                    <input type="number" id="cha">
                </div>
            </div>
            <div class="character-backstory-form">
                <label for="backstory">Backstory</label>
                <textarea rows="7" cols="50" id="backstory"></textarea>
            </div>
            <button type="submit">Submit Character</button>
            <p class="error"></p>
        </form>`
}

window.approve = async function(id) {
    console.log("Approving:", id);
    const options = {
        method: 'GET', // or PUT depending on your API
        headers: {
            'Authorization': `Bearer ${token}`,
        }
    }
    const response = await fetch(API_URL + `/api/campaign/approveJoinRequest/${id}`, options);
    console.log(response);
}

window.deny = async function(id) {
    console.log("Denying:", id);
    const options = {
        method: 'GET', // or PUT depending on your API
        headers: {
            'Authorization': `Bearer ${token}`,
        }
    }
    const response = await fetch(API_URL + `/api/campaign/denyJoinRequest/${id}`, options);
    console.log(response);
}
if(campaignInfo[0].dm === username) {
    const characters = campaignInfo.filter(user => user.activeCharacter !== null)
    const addUsers = campaignInfo.filter(user => user.isApproved === false)
    console.log(characters)
    console.log(addUsers)
    document.querySelector("#addChars").innerHTML = addUsers.map(user => addUserTemplate(user)).join("");
    //TODO - display character array
} else {
    const info = campaignInfo.filter(user => user.playerName === username);
    console.log(info)
    if(info[0].activeCharacter !== null) {
        document.querySelector(".party").classList.remove("hide");
    } else {
        console.log("need to submit")
        document.querySelector(".character-form").classList.remove("hide");
    }
}