# 🎲 Backend API Documentation

## Base URL

https://yourdomain.com/api

All protected endpoints require:

Authorization: Bearer {JWT_TOKEN}

---

# 🔐 Authentication

Base route:

/api/auth

---

## 🟢 Register

### `POST /api/auth/register`

Creates a new user account and returns a JWT token.

### Request Body

{  
  "userName": "string",  
  "email": "string",  
  "password": "string"  
}

### Success Response

{  
  "token": "jwt_token_here",  
  "email": "user@email.com"  
}

### Errors

- `400 Bad Request` – Validation or identity errors

---

## 🟢 Login

### `POST /api/auth/login`

Authenticates a user and returns a JWT token.

### Request Body

{  
  "email": "string",  
  "password": "string"  
}

### Success Response

{  
  "token": "jwt_token_here"  
}

### Errors

- `401 Unauthorized` – Invalid credentials

---

# 🎲 Campaign API

Base route:

/api/campaign

All endpoints require authentication.

---

## 🟢 Create Campaign

### `POST /api/campaign/create`

Creates a new campaign. The authenticated user becomes the DM.

### Request Body

{  
  "campaignInfo": {  
    "name": "string",  
    "description": "string",  
    "maxPlayers": 5,  
    "isPublic": true,  
    "location": "string",  
    "setting": "string"  
  },  
  "campaignFilters": {  
    "experienceLevel": 0,  
    "campaignLocationType": 0,  
    "status": 0,  
    "preferredPlaystyle": 0,  
    "campaignTone": 0,  
    "sessionFrequency": 0,  
    "statSystem": 0,  
    "contentMaturity": 0  
  }  
}

### Success Response

200 OK

---

## 🟢 Get Campaign By ID

### `GET /api/campaign/getById?id={campaignId}`

### Success Response

Returns full Campaign object.

### Errors

- `404 Not Found`

---

## 🟢 Get Campaign By Join Code

### `GET /api/campaign/getByAlphaCode?alphacode={CODE}`

### Success Response

Returns Campaign object.

### Errors

- `404 Not Found`

---

## 🟢 Get My Campaigns

### `GET /api/campaign/getMyCampaigns`

Returns campaigns where user is a member.

---

## 🟢 Request Join by Campaign ID

### `POST /api/campaign/requestJoinCampaignId?id={campaignId}`

Creates a pending join request.

### Success Response

200 OK

---

## 🟢 Request Join by Alpha Code

### `POST /api/campaign/requestJoinCampaignAlphaCode?code={JOINCODE}`

Creates a pending join request.  

---  

## 🟢 Approve Join Request

### `POST /api/campaign/approveJoinRequest?campaignMembershipId={id}`

Only DM can approve.  

### Success Response

200 OK

### Errors

- `401 Unauthorized`  
- `404 Not Found`  

---  

## 🟢 Deny Join Request

### `POST /api/campaign/denyJoinRequest?campaignMembershipId={id}`

Only DM can deny.  

---  

## 🔴 Leave Campaign

### `DELETE /api/campaign/leaveCampaign?campaignId={id}`

Removes user from campaign.  

---  

# 🧙 Character API

Base route:  

/api/character

All endpoints require authentication.  

---  

## 🟢 Get My Characters

### `GET /api/character/getMyCharacters`

Returns list of characters owned by user.  

---  

## 🟢 Create Character

### `POST /api/character/createCharacter`

### Request Body

```json
{  
  "name": "string",  
  "class": "string",  
  "race": "string",  
  "background": "string",  
  "alignment": 0,  
  "personalityTraits": "string",  
  "ideals": "string",  
  "bonds": "string",  
  "flaws": "string",  
  "backstory": "string"  
}

### Success Response

200 OK

---

## 🟢 Link Character To Campaign

### `POST /api/character/linkCharacterToCampaign`

### Request Body

{  
  "characterID": "string",  
  "campaignId": "string"  
}

Associates character as active character for that campaign membership.

---

## 🟢 Get Characters By Campaign

### `GET /api/character/getCharacterByCampaignId?campaignId={id}`

Returns characters in a campaign.

---

# 📦 Data Models

---

## Campaign

{  
  "id": "string",  
  "alphaNumericJoinCode": "string",  
  "dmId": "string",  
  "name": "string",  
  "description": "string",  
  "maxPlayers": 5,  
  "location": "string",  
  "setting": "string",  
  "createdAt": "datetime",  
  "lastUpdated": "datetime",  
  "experienceLevel": 0,  
  "campaignLocationType": 0,  
  "status": 0,  
  "preferredPlaystyle": 0,  
  "campaignTone": 0,  
  "sessionFrequency": 0,  
  "statSystem": 0,  
  "contentMaturity": 0  
}

---

## CampaignMembership

{  
  "id": "string",  
  "campaignId": "string",  
  "playerUserId": "string",  
  "activeCharacter": {},  
  "isApproved": true,  
  "dateCreated": "datetime",  
  "lastUpdated": "datetime"  
}

---

## Character

{  
  "id": "string",  
  "playerId": "string",  
  "campaignId": "string",  
  "name": "string",  
  "class": "string",  
  "race": "string",  
  "background": "string",  
  "alignment": 0,  
  "personalityTraits": "string",  
  "ideals": "string",  
  "bonds": "string",  
  "flaws": "string",  
  "backstory": "string"  
}

---

# 🔒 Authentication Usage Example

Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...

---

# ❌ Error Response Format

Currently returns:

- `400 BadRequest`

- `401 Unauthorized`

- `404 NotFound`

You may standardize in future:

{  
  "message": "Error description",  
  "statusCode": 400  
}
```
