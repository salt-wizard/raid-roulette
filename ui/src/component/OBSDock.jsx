import React from 'react';
import { SetStateAction, useEffect, useState } from 'react';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Box from '@mui/material/Box';
import RaidScreen from './RaidScreen.jsx';
import VetoScreen from './VetoScreen.jsx';
import BlacklistScreen from './BlacklistScreen.jsx';
import AcceptedScreen from './AcceptedScreen.jsx';
import useWebSocket from 'react-use-websocket';

// URL for Streamer.Bot Web Socket
const RAID_WS_URL = 'ws://127.0.0.1:10002/'
const RAID_WS_URL_UI = 'ws://127.0.0.1:10003/'

const OBSDock = () => {
    const [tabValue, setTabValue] = React.useState('Raid');
    const [raidList, setRaidList] = useState([]);
    const [clientId, setClientId] = useState("");
    const [oAuthToken, setOAuthToken] = useState("");

    const handleChange = (_e, newValue) => {
        //console.log(e);
        setTabValue(newValue);
    };

    // Establish two web socket connections; 1 from Streamer.Bot, 1 to Streamer.bot
    const {lastMessage} = useWebSocket(RAID_WS_URL, {
        onOpen: () => { 
            console.log('Streamer.Bot -> UI :: Connection established.'); 
        }, 
        share: true
    });

    const {sendMessage} = useWebSocket(RAID_WS_URL_UI, {
        onOpen: () => { 
            console.log('UI -> Streamer.Bot :: Connection established.'); 
            // Get raid targets on open
            requestRaiders()
            console.log('Requesting raid targets to render...');
        }, 
        share: true
    });

    function requestRaiders(){
        const json = {
            "action": "render"
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
            <Box sx={{ width: '100%' }}>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <Tabs
                        value={tabValue}
                        onChange={handleChange}
                        textColor="primary"
                        indicatorColor="primary"
                    >
                        <Tab value="Raid" label="Raid"/>
                        <Tab value="Allowed" label="Allowed"/>
                        <Tab value="Vetoed" label="Vetoed"/>
                        <Tab value="Blacklisted" label="Blacklisted"/>
                    </Tabs>
                </Box>
                { tabValue === "Raid" ? <RaidScreen sendMessage={sendMessage} raidList={raidList} clientId={clientId} oAuthToken={oAuthToken} /> : null }
                { tabValue === "Vetoed" ? <VetoScreen sendMessage={sendMessage} raidList={raidList} /> : null }
                { tabValue === "Blacklisted" ? <BlacklistScreen sendMessage={sendMessage} raidList={raidList} /> : null }
                { tabValue === "Allowed" ? <AcceptedScreen sendMessage={sendMessage} raidList={raidList} /> : null }                
            </Box>
            
        </div>
    );
}

export default OBSDock;