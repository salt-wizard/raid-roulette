import { SetStateAction, useEffect, useState } from 'react';
import { Accordion, AccordionDetails, AccordionSummary, Box, Stack, TextField, Typography } from '@mui/material';
import Button from '@mui/material/Button';
import DoNotDisturbIcon from '@mui/icons-material/DoNotDisturb';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import LiveTvIcon from '@mui/icons-material/LiveTv';
import useWebSocket from 'react-use-websocket';

import './RaidScreen.css';

// URL for Streamer.Bot Web Socket
const RAID_WS_URL = 'ws://127.0.0.1:10002/'

const RaidScreen = () => {
    const [enableRedeem, setEnableRedeem] = useState(false);
    const [manualTextEntered, setManualTextEntered] = useState(true);
    const [raidInProgress, setRaidInProgress] = useState(false);
    const [manualTarget, setManualTarget] = useState("");
    const [raidStatus, setRaidStatus] = useState("Not Enabled");
    const [clientId, setClientId] = useState("");
    const [oAuthToken, setOAuthToken] = useState("");

    // The raid list will be provided through this state
    const [raidList, setRaidList] = useState([]);

    // Establish a web socket connection and look for latest messages.
    const {sendMessage, lastMessage} = useWebSocket(RAID_WS_URL, {
            onOpen: () => { 
                console.log('WebSocket connection established.'); 
            }, 
            share: true
        });


    /*
        Toggle the redeems on or off depending on which button is clicked.
        If the redeem is being toggled, it will request for a new redeem.
    */
    function toggleRedeemStatus(){
        setEnableRedeem(!enableRedeem);
        // eslint-disable-next-line @typescript-eslint/no-unused-expressions
        !enableRedeem ? setRaidStatus("Waiting For Input...") : setRaidStatus("Not Enabled");

        // If disabled and a raid is occurring, cancel raid!
        if(!enableRedeem && raidInProgress){
            setRaidInProgress(!raidInProgress);
            setRaidStatus("Waiting For Input...")
            sendMessage('{"action":"cancel"}');
        }
    }

    /*
        Enable the manual raid button if there is input in the text box
    */
    function handleManualTarget(event){
        if(event.target.value){
            setManualTextEntered(false);
        } else {
            setManualTextEntered(true);
        }

        setManualTarget(event.target.value);
    }

    /*
        Start the random raid
    */
    function startRandomRaid(){
        setRaidInProgress(!raidInProgress);
        setRaidStatus("Raiding Random User");
        // Notify streamer.bot that you want to raid a random person
        const json = {
            "action": "raid",
            "target": "RANDOM"
        }
        sendMessage(JSON.stringify(json));
    }

    /*
        START A TARGETED RAID (SUGGESTED)
        - Set the raid in progress to true
        - Set the raid status to the user targeted
        - Send a message over WS to indicate the raid target
    */
    function startTargetedRaid(_e){
        // The username is the target value
        const eventJson = JSON.parse(_e.target.value);
        const userName = eventJson.userName;
        const userId = eventJson.userId;
        setRaidInProgress(!raidInProgress);
        setRaidStatus("Manually Raiding " + userName)
        const json = {
            "action": "raid",
            "target": userId
        }
        sendMessage(JSON.stringify(json));

    }

    /*
        START A TARGETED RAID (INPUT)
        - Set the raid in progress to true
        - Set the raid status to the user targeted
        - Send a message over WS to indicate the raid target
    */
    function startManualRaid(){
        setRaidInProgress(!raidInProgress);
        setRaidStatus("Manually Raiding " + manualTarget)

        fetch(`https://api.twitch.tv/helix/users?login=${manualTarget}`, {
            method: 'GET',
            headers: {
                'Authorization': "Bearer " + oAuthToken,
                'Client-ID': clientId
            }
        })
        .then((res) => {
            return res.json();
        })
        .then((data) => {
            // Should be the first match
            const json = {
                "action": "raid",
                "target": data.data[0].id
            }
            sendMessage(JSON.stringify(json));
        });
    }

    /*
        CANCEL A RAID
        - Set the raid in progress to false
        - Reset the raid status
        - Send a message over WS to indicate raid canceled
    */
    function cancelRaid(){
        setRaidInProgress(!raidInProgress);
        setRaidStatus("Waiting For Input...")
        sendMessage('{"action":"cancel"}');
    }

    function blackListTarget(_e){
        // The username is the target value
        const eventJson = JSON.parse(_e.target.value);
        const userId = eventJson.userId;

        const json = {
            "action": "blacklist",
            "target": userId
        }

        sendMessage(JSON.stringify(json));
    }

    /*
        Handler for any messages that arrive from Streamer.bot
    */
    useEffect(()=>{
        if(lastMessage != null){
            //console.log(lastMessage.data);
            const jsonData = JSON.parse(lastMessage.data)
            if(jsonData.action == "update"){
                setRaidList(JSON.parse(jsonData.users));
            }
            if(jsonData.action == "token"){
                setClientId(jsonData.clientId);
                setOAuthToken(jsonData.token);
            }
        }
    },[lastMessage]);

    return(
        <div>
            <Box className="raid-box">
                <Button className="raid-button" color="success" variant="contained" disabled={enableRedeem} onClick={toggleRedeemStatus}>Enable</Button>
                <Button className="raid-button" color="error" variant="contained" disabled={!enableRedeem} onClick={toggleRedeemStatus}>Disable</Button>
            </Box>
            <Box className="raid-box">
                <Typography>Raid Status - {raidStatus}</Typography>
            </Box>
            {enableRedeem ? 
            <div>
                <Box className="raid-box">
                    <Button className="raid-button" color="success" variant="contained" disabled={raidInProgress} onClick={startRandomRaid}>Random Raid</Button>
                    <Button className="raid-button" color="error" variant="contained" disabled={!raidInProgress} onClick={cancelRaid}>Cancel Raid</Button>
                    <Button className="raid-button" color="info" variant="contained" disabled={!raidInProgress} ><LiveTvIcon/></Button>
                </Box>
                <Box className="raid-box">
                    <TextField 
                        className="raid-button " 
                        id="standard-basic" 
                        label="Manual Raid Target" 
                        variant="outlined"
                        onChange={handleManualTarget}
                    /> 
                    <Button 
                        className="raid-button manual-raid-button" 
                        variant="outlined"
                        // The button should be disabled if there is a raid in progress or the text input is empty
                        disabled={raidInProgress || manualTextEntered}
                        onClick={startManualRaid}
                    >
                        Raid
                    </Button>
                </Box>
                <Box className="buffer" />
                <Accordion>
                    <AccordionSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1-content"
                        id="panel1-header"
                    >
                        <Typography>Suggested Streamers</Typography>
                    </AccordionSummary>
                    <AccordionDetails>

                    
                
                {raidList !== null ? raidList.map((raidTarget) => {
                    return (
                        raidTarget["isBlacklisted"] === false && raidTarget["isOnline"] === true // Only show the target if they're not blacklisted.
                        ?
                        <Stack className="raid-box" key={raidTarget["userName"] + "-key"} direction="row">
                            <Box><Button disabled={raidInProgress} value={'{"userId": "' + raidTarget["userId"] + '","userName": "' + raidTarget["userName"] + '"}'} className="raid-button" color="success" variant="outlined" onClick={startTargetedRaid}>Raid</Button></Box>
                            <Box><Button value={'{"userId": "' + raidTarget["userId"] + '","userName": "' + raidTarget["userName"] + '"}'} className="raid-button" color="error" variant="outlined" onClick={blackListTarget}><DoNotDisturbIcon /></Button></Box>
                            <img 
                                style={{
                                    borderRadius: "50%",
                                    width: 35,
                                    height: 35,
                                    background: "red",
                                    display: "block",
                                    marginRight: "10px"
                                }} 
                                src={raidTarget["userPfp"]} 
                                alt="streamer"
                            />
                            <Box 
                            sx={{ width: "26ch", marginTop: "5px" }}
                            ><Typography>{raidTarget["userName"]}</Typography></Box>
                        </Stack>
                        : null
                    )
                })
            : null
            }
                </AccordionDetails>
            </Accordion>
            </div>
            : 
            null
            }
        </div>
    );
}

export default RaidScreen