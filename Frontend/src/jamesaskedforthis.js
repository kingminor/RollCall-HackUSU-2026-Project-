const options = {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        },
        body: `${campaignId}`,
    }

await fetch(API_URL + "/api/character/getCampaignMembership", options);

// Object Looks like this
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