

// Object Looks like this
import {API_URL} from "./main.js";

var object = {
    "Id": "string",
    "CampaignId": "string",
    "Campaign": {
        // Bunch of data relating to the campaign
    },
    "PlayerUserId": "string",
    "PlayerUser": {
        //Stuff for playeruser
    },
    "Character": {

    },
    "DateCreated": "DateTime",
    "LastUpdated": "DateTime",
    "IsApproved": true
}

export async function getCampaignInfo(campaignId, token) {
    const options = {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({campaignId}),

    }

    const response = await fetch(API_URL + "/api/character/getCampaignMembership", options);
    const json = await response.json()
    console.log(json)
    console.log(response)
}