import {getCampaignInfo} from "./jamesaskedforthis.js";

const params = new URLSearchParams();
const campaignId = params.get("id");
const token = localStorage.getItem("token");

getCampaignInfo(campaignId, token)