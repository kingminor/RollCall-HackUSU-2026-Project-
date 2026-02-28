# Backend

## Models

### PlayerUser : IdentityUser

Vars

- string: aboutme

- Icollection(CampaignMembership): CampaignMemberships

- List(string): charactersIDs

- DateTime: createdAt

- DateTime: LastUpdated

### Campaign

Vars

- string: campaignID

- string: alphanumericJoinCode

- PlayerUser: DM

- string: Name

- string: Description

- ushort: Maxplayers

- bool: ispublic

- string: location (optional)

- string: setting

- DateTime: createdAt

- DateTime: LastUpdated

- ICollection(CampaignMembership): Members
  
  Enum Variables

- ExperienceLevel?

- CampaignLocationType?

- CampaignStatus?

- PreferredPlaystyle?

- CampaignTone?

- SessionFrequency?

- StatSystem?

- ContentMaturity?

### Campaign Membership

Vars

- Campaign: campaign

- PlayerUser: playeruser

- String: activeCharacterID (nullable for DM)

- Character: activeCharacter

- DateTIme: joinedAt

- DateTime: LastUpdated

- IsAproved

### Character

Vars

- string: characterId

- PlayerUser: player

- campaign: campaign

- DateTime: createdAt

- DateTime: LastUpdated

Info Items:

- String: Name

- String: Class

- String: Race

- String: Background

- Alignment (Enum)

- String: PersonalityTraits

- String: Ideals

- String: Bonds

- String: Flaws

---

## Enums

### Alignment

- LawfulGood

- NeutralGood

- ChaoticGood

- LawfulNeutral

- Neutral

- ChaoticNeutral

- LawfulEvil

- NeutralEvil

- ChaoticEvil

### ExperienceLevel

- Beginner

- Beginner-Intermediate

- Intermediate

- intermediate-Advanced

- Advanced

- MinMaxed

- NoPreference

### CampaignLocationType

- In-Person

- Online

- Hybrid

### CampaignStatus

- recruiting

- Ongoing

- Canceled

- Completed

### PreferredPlaystyle

- Roleplay Heavy

- Combat Heavy

- Dungeon Crawler

- Mixed

- Balanced

### CampaignTone

- Lighthearted

- Dark

- Horror 

- Comedy 

- Tragic

- Epic

- Mystery

- Whimsical

- Grimdark

### SessionFrequency

- Weekly

- BiWeekly

- Monthly

- Irregular

- Flexible

### StatSystem

- FourD6DropLowest

- FourD6DropLowestRerollOnes

- PointBuy

### ContentMaturity

- AllAges

- Teen

- Adult

---

## DTOs

### CampaignFilters

CampaignFilters holds all info relating to the campaign that is used as a filter when searching for a public campaign

###### Data (All ENUMS)

- ExperienceLevel?

- CampaignLocationType?

- CampaignStatus?

- PreferredPlaystyle?

- CampaignTone?

- SessionFrequency?

- StatSystem?

- ContentMaturity?

### CampaignInfo

CampaignInfo holds info relating to the campaign that do not count as filters.

###### Data

- string: Name

- string: Description

- ushort: Maxplayers

- bool: ispublic

- string: location (optional)

- string: setting

### UpdateAccountDTO

This is used to update the Info of an account.

###### Data

- int: ProfilePictureId

- string: aboutme

### StatDTO

Used to send stats

- STRStat

- DEXStat

- CONStat

- INTStat

- WISStat

- ChaStat

### ProfiecenyDTO

Used to send what they are profiecent in

- STRSave

- DEXSave

- CONSave

- INTSave

- WISSave

- CHASave

- Acrobatics

- AnimalHandling

- Arcana

- Athletics

- Deception

- History

- Insight

- Intimidation

- Investigation

- Medicine

- Nature

- Perception

- Performance

- Persuasion

- Religion

- SleightOfHand

- Stealth

- Survival

---

## Controllers/Services

### CampaignController

- `public async Task<IActionResult> CreateCampaign (Campaigninfo, campaign filters)`Returns ID of the created campaign

- `public async Task<IActionResult>  UpdateCampaign (Campagininfo, campaign filters)` They will use the same DTO, update will just ignore a few fields and update exisiting item in DB instead of creating a new one

- `public async Task<IActionResult> SearchCampaigns (CampaignFilters, string? name, string? description)` This will search for a campaign that fits the critera filted for, will return different json based on what is returned from the DB

- `public async Task<IActionResult> GetCampaign (campaignID)` This returns a campaign based on id, returns 404 if no campaign is found.

- `public async Task<IActionResult> RequestJoinCampaign (campaignID)` This is used to request to join a campaign, DM must aprove it.

- `public async Task<IActionResult> ReviewJoinCampaign (campaignID, playerID, bool allowed)` This approves or denies a players request to join a campaign, can only be used by DM.

- `public async Task<IActionResult> LeaveCampaign (campaignID)` Allows players to leave a campaign they joined.

- `public async Task<IActionResult> KickPlayer (campaignID,  playerID)` Removes player from campaign, can only be used by DM

### AccountController

- `public async Task<IActionResult> Login (Email, Password)` This will be used to login to an account, will return OK and JWT if sucsessful, BadRequest if not.

- `public async Task<IActionResult> Register (Email, Password)` This will be used to create a new account

- `public async Task<IActionResult> GetAccountInfo (playerID)` Gets non-private account info.

- `public async Task<IActionResult> UpdateUserProfile (UpdateAccountDTO)` Allows user to update their ProfilePictureId and about me.

### CharacterController

- Create Character
- UpdateCharacter
- Update
- DeleteCharacters
- GetCharacterFromPlayer
- GetCharacterFromCampaign

### Hubs







SPECIAL ENDS
